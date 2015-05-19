#!/usr/bin/python3
# -*- coding: UTF-8 -*-

import sys
import time
import pylab as pl
import numpy as np
from math import sqrt
import queue
from vectorizer.contour import convert_mask_to_border_paths,border_points,border_path
from scipy import ndimage

def drawline(img, x1, x2, y1,y2,color):
    s = img.shape
    if y1 != y2:
        if y1 > y2:
            ty = y1
            y1 = y2
            y2 = ty
            tx = x1
            x1 = x2
            x2 = tx

        step = (x2-x1)/(y2-y1)
        for i in range(y1,y2):
            x = (i-y1)*step+x1
            xx = (i+1-y1)*step+x1
            xl = int(round(x))%s[1]
            xu = int(round(xx))%s[1]
            if xl < xu:
                for j in range(xl,xu+1):
                    img[i,j] = color
            else:
                for j in range(xu,xl+1):
                    img[i,j] = color
    else:
        if x1 < x2:
            for i in range(x1,x2):
                img[y1,i] = color
        else:
            for i in range(x2,x1):
                img[y1,i] = color


    return

def gray2rgb(img):
    s = img.shape
    cimg = np.zeros((s[0], s[1], 3), dtype=img.dtype)
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            for k in range(0, 3):
                cimg[i,j,k] = img[i,j]
    return(cimg)

def color_reduction(img):
    # na cepa 16 kolorów
    cMin = np.min(img)
    cMax = np.max(img)
    cStep = (cMax - cMin) / 8.0
    img2 = np.copy(img)

    s = img.shape
    for i in range(0, s[0]):
        print ("\rReducting line: {}".format(i), end=' ')
        for j in range(0, s[1]):
            c = img[i,j]
            pc = round((c - cMin) / cStep) * cStep
            img2[i,j] = pc
    return(img2)

def calculate_segments(img):
    s = img.shape
    segments = np.zeros(s,dtype=int)
    counter = 0
    stack = []
    for i in range(0, s[0]):
        print ("\rSegmenting line: {}".format(i), end=' ')
        for j in range(0, s[1]):
            if segments[i,j] == 0:
                counter = counter + 1
                stack.append((i,j))
                while len(stack) > 0:
                    (y,x) = stack.pop()
                    if segments[y,x] != 0 or img[y,x] != img[i,j]:
                        continue
                    segments[y,x] = counter
                    for k in range(-1,2):
                        for l in range(-1,2):
                            y2 = max(0, min(s[0]-1, y+k))
                            x2 = max(0, min(s[1]-1, x+l))
                            if segments[y2,x2] == 0 and img[y2,x2] == img[i,j]:
                                stack.append((y2,x2))

    return (segments)

def find_longest_contour_path(px, py, mask, segments, border):
    s = segments.shape
    seg = segments[py,px]
    visited = np.zeros(s, dtype=bool)

    # jak mamy obwód ala cykl wiemy, że ono ma dokładnie 2 punkty przecięcia
    # 1.no tylko powoduje przejście do linii (o to nam chodzi)

    cont = []

    for i in range(-1,2):
        for j in range(-1,2):
            y = (py+i)%s[0]
            x = (px+j)%s[1]
            if border[y,x] and seg == segments[py,px] and ((i != 0) or (j != 0)):
                visited[y,x] = True
                cont.append((y,x))

    if len(cont) == 0:
        return []

    start = cont.pop()
    q = queue.Queue()
    queued = np.zeros(s, dtype=bool)

    path = []
    q.put(start)

    while not(q.empty()):
        elem = q.get()
        (qy,qx) = elem
        visited[qy,qx] = True
        queued[qy,qx] = False

        if mask[qy,qx]:
            path.append((qy,qx))

        for i in range(-1,2):
            for j in range(-1,2):
                y = (qy+i)%s[0]
                x = (qx+j)%s[1]
                if segments[y,x] == seg and border[y,x] and not(visited[y,x]) and (not(queued[y,x])):
                    queued[y,x] = True
                    q.put((y,x))

        # teraz musimy zająć się pkt startowym i jego otoczeniem,
        # bo wykluczyliśmy go na początku z rozważań (blokada przejścia)

    for i in range(1,-2,-1):
        for j in range(1,-2,-1):
            y = (py+i)%s[0]
            x = (px+j)%s[1]
            path.append((y,x))

    return path

def flood_fill(sy, sx, segments, meansx, meansy, border, mask):
    threshold = 0.01
    s = segments.shape
    seg = segments[sy,sx]-1
    visited = np.zeros(s, dtype=bool)
    queued = np.zeros(s, dtype=bool)
    parents = np.zeros((s[0], s[1], 2), dtype=int)
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            parents[i,j] = np.array([-1,-1])

    mx = meansx[seg]
    my = meansy[seg]
    stack = queue.Queue()
    stack.put((sy,sx))


    while not(stack.empty()):
        (ey,ex) = stack.get()
#                    mask[ey,ex] = True
        visited[ey,ex] = True
        queued[ey,ex] = False

        p0x = ex
        p0y = ey
        p1x = parents[p0y,p0x][1]
        p1y = parents[p0y,p0x][0]
        p2x = -1 if p1x < 0 else parents[p1y,p1x][1]
        p2y = -1 if p1y < 0 else parents[p1y,p1x][0]


        if p0y >=0 and p1y >=0 and p2y >= 0:
            d0x = p0x-mx
            d0y = p0y-my
            d1x = p1x-mx
            d1y = p1y-my
            d2x = p2x-mx
            d2y = p2y-my
            r0 = sqrt(d0x*d0x+d0y*d0y)
            r1 = sqrt(d1x*d1x+d1y*d1y)
            r2 = sqrt(d2x*d2x+d2y*d2y)

            if (r1-r0)*(r2-r1) < -threshold:
                mask[p1y,p1x] = True

        for k in range(-1,2):
            for l in range(-1,2):
                y = (ey+k)%s[0]
                x = (ex+l)%s[1]
                if (segments[y,x] - 1 == seg) and border[y,x] and not(visited[y,x]) and not(queued[y,x]):
                    queued[y,x] = True
                    stack.put((y,x))
                    parents[y,x] = np.array([ey,ex])

    px = ex
    py = ey

    # dopóki nie dojdziemy do końca ścieżki (-1,-1) zbieramy w listę
    # wierzchołki

    vertices = find_longest_contour_path(px, py, mask, segments, border)

    return (mask,parents,vertices)


def seg_border_points(segments, meansx, meansy, border):
    threshold = 5
    s = segments.shape
    mask = np.zeros(s, dtype=bool)
    n = len(meansx)
    visitedSegs = np.zeros(n, dtype=int)
    svertices = []
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            seg = segments[i,j]-1
            if border[i,j] and visitedSegs[seg] < threshold:
                visitedSegs[seg] += 1
                (mask2,parents,vertices) = flood_fill(
                        i,j, segments, meansx, meansy, border,mask)
                svertices.append(vertices)

    return (mask,svertices)

def seg_parameters(segments):
    s = segments.shape
    c = int(np.max(segments))
    xsum = np.zeros(c, dtype=int)
    ysum = np.zeros(c, dtype=int)
    minx = np.zeros(c, dtype=int)
    maxx = np.zeros(c, dtype=int)
    miny = np.zeros(c, dtype=int)
    maxy = np.zeros(c, dtype=int)
    counts = np.zeros(c, dtype=int)
    meansx = np.zeros(c, dtype=int)
    meansy = np.zeros(c, dtype=int)

    for i in range(0, s[0]):
        for j in range(0, s[1]):
            segment = segments[i,j]
            xsum[segment-1] += j
            ysum[segment-1] += i
            minx[segment-1] = min(minx[segment-1], j)
            maxx[segment-1] = max(maxx[segment-1], j)
            miny[segment-1] = min(miny[segment-1], i)
            maxy[segment-1] = max(maxy[segment-1], i)
            counts[segment-1] += 1

    for i in range(0, c):
        meansx[i] = int(round(xsum[i] / counts[i]))
        meansy[i] = int(round(ysum[i] / counts[i]))

    return (meansx, meansy, counts, minx, maxx, miny, maxy)

def segment_borders(segment, m, threshold):
    s = segment.shape
    mask = np.zeros(s, dtype=bool)
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            if not(m[segment[i,j]-1]):
                continue
            c = 0
            for k in range(-1,2):
                for l in range(-1,2):
                    y = max(0, min(s[0]-1, i+k))
                    x = max(0, min(s[1]-1, j+l))
                    c = c + 1 if segment[y,x] != segment[i,j] else c
            mask[i,j] = True if c >= threshold else False
    return(mask)

if len(sys.argv) < 2:
    print("Podales za malo parametrow")
    sys.exit(1)

path = sys.argv[1]

img = pl.imread(path)
if path.endswith(".png"):
    maxColor = 1.0
else:
    maxColor = 255

rimg2 = color_reduction(img)
rimg = ndimage.median_filter(rimg2, 10)
#rimg = ndimage.minimum_filter(rimg3, 10)
#rimg = img

#pl.imshow(rimg, cmap=pl.gray())
#rimg_sobel = ndimage.sobel(rimg)
#pl.imshow(rimg_sobel, cmap=pl.gray())


segments = calculate_segments(rimg)
segrimg = np.copy(rimg)
s = rimg.shape
for i in range(0, s[0]):
    for j in range(0, s[1]):
        segrimg[i,j] = float(segments[i,j])

print("Obliczanie parametrow segmentow")

(meansx, meansy, counts, minx, maxx,miny,maxy) = seg_parameters(segments)

print ("Filtrowanie duzych obiektow")

n = len(counts)

thresholdA = 30
thresholdB = 25000
filtered = 0

s = img.shape

rgb = gray2rgb(rimg)
dataThreshold = 100

visited = np.zeros(n, dtype=bool)

for i in range(0, s[0]):
   print ("\rColoring line: {}".format(i), end=' ')
   for j in range(0, s[1]):
       c = counts[segments[i,j]-1]
#       if thresholdA <= c and  c <= thresholdB:
       if thresholdA <= c and  c <= thresholdB and rimg[i,j] < dataThreshold:
#           rgb[i,j] = np.array([maxColor, 0, 0])
           if not(visited[segments[i,j]-1]):
               filtered += 1
               visited[segments[i,j]-1] = True

print("Segmentow: {}, filtrowanych: {}".format(n, filtered))
border = segment_borders(segments, visited, 1)
# (points,svertices) = seg_border_points(segments, meansx, meansy, border)
#
# for i in range(0, s[0]):
# #    print("Punkty: {}".format(i), end= '\r')
#     for j in range(0, s[1]):
#         if points[i,j]:
#             print ("Punkt: ({},{})".format(i,j))
#             rgb[i,j] = np.array([maxColor, 0, 0])
#
# print(svertices)
#
# for l in svertices:
#    n = len(l)
#    for (y,x) in l:
#        rgb[y,x] = np.array([maxColor, 0, maxColor])
#
#    for i in range(0, n):
#        (y1,x1) = l[i]
#        (y2,x2) = l[(i+1)%n]
#        print("edge:  ({},{}) - ({},{})".format(x1,y1,x2,y2))
# #       rgb[y1,x1] = np.array([maxColor, 0, 0])
# #       rgb[y2,x2] = np.array([maxColor, 0, 0])
#        drawline(rgb, x1,x2,y1,y2, np.array([0, maxColor, 0]))
#
# pl.imshow(rgb)
# pl.show()

border_paths = convert_mask_to_border_paths(segments, border)


def cross(rgb, x, y, color):
    s = rgb.shape
    for i in range(-5,6):
        rgb[y,(x+i)%s[1]] = color

    for i in range(-5,6):
        rgb[(y+i)%s[0],x] = color


for key in border_paths.keys():
    path_border_points = border_points(border_paths[key], meansy[key-1], meansx[key-1])
    # path_border_points = border_paths[key]

    cross(rgb, meansx[key-1], meansy[key-1], np.array([0, 0, maxColor]))

    n = len(path_border_points)

    for i in range(0, n):
        (py,px) = path_border_points[i]
        (qy,qx) = path_border_points[(i+1)%n]
        drawline(rgb, px,qx,py,qy, np.array([0, maxColor, 0]))

    for (py,px) in path_border_points:
        cross(rgb, px,py, np.array([maxColor, 0, 0]))
        # rgb[py,px] = np.array([maxColor, 0, 0])


#for key in border_paths.keys():
#    path = border_paths[key]
#    x = [i for i in range(0, len(path))]
#    y = [distance(path[i], (meansy[key-1], meansx[key-1])) for i in range(0, len(path))]
#    pl.plot(x,y)
#    pl.show()

pl.imsave("result.png", rgb)
# pl.imshow(rgb)
# pl.show()
