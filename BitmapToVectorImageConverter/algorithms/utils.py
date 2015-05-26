#!/usr/bin/python3
# -*- coding: UTF-8 -*-

import sys

import clr
clr.AddReference("System.Drawing")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *

from processing.segmentation.connected import *
from processing.segmentation.otsu import *
from datetime import datetime
from processing.shoes.shape import *
from processing.masks import *
from processing.contours.psweeping import *


def identify(bmp):
	return []

def bitmap2array(bmp):
	bmData = bmp.LockBits(Rectangle(0, 0, bmp.Width, bmp.Height),
			ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)
	width = bmp.Width
	height = bmp.Height
	stride = bmData.Stride

	total_bytes = (bmData.Stride) * bmData.Height
	rgbValues = Array.CreateInstance(Byte, total_bytes)
	Runtime.InteropServices.Marshal.Copy(bmData.Scan0, rgbValues, 0, total_bytes)

	bmp.UnlockBits(bmData)
	return (rgbValues,width,height,stride)

def array2bitmap(rgbValues, width, height,stride):
	bmp = Bitmap(width, height)
	bmData = bmp.LockBits(Rectangle(0, 0, width, height),
			ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)
	total_bytes = stride * height
	Runtime.InteropServices.Marshal.Copy(rgbValues, 0, bmData.Scan0, total_bytes)

	bmp.UnlockBits(bmData)
	return bmp

def array2matrix(rgbValues, width, height, stride):
	img = Array.CreateInstance(Byte, height, width, 3)

	for i in range(0, height):
		for j in range(0, width):
			for k in range(0, 3):
				img[i,j,k] = rgbValues[i*stride+j*3+k]

	return img

def matrix2array(matrix, width, height, stride):
	total_length = stride * height
	rgb_values = Array.CreateInstance(Byte, total_length)
	for i in range(0, height):
		for j in range(0, width):
			for k in range(0, 3):
				rgb_values[i*stride+j*3+k] = matrix[i,j,k]
	return rgb_values

def color2gray(data):
	height = data.GetLength(0)
	width = data.GetLength(1)
	result = Array.CreateInstance(Byte, height, width)
	for i in range(0, height):
		for j in range(0, width):
			s = 0
			for k in range(0, 3):
				s = max(s, int(data[i,j,k]))
			result[i,j] = Byte(s)
	return result

def gray2color(data):
	height = data.GetLength(0)
	width = data.GetLength(1)
	result = Array.CreateInstance(Byte, height, width, 3)

	for i in range(0, height):
		for j in range(0, width):
			for k in range(0, 3):
				result[i,j,k] = data[i,j]
	return result

def bool2gray(img):
	height = img.GetLength(0)
	width  = img.GetLength(1)

	gimg = Array.CreateInstance(Byte, height, width)

	for i in range(0, height):
		for j in range(0, width):
			gimg[i,j] = Byte(255) if img[i,j] else Byte(0)
	return gimg

def color2bmp(cmask, width, height, stride):
    amask = matrix2array(cmask, width, height, stride)
    return array2bitmap(amask, width, height, stride)

def image_color(cmask, width, height, stride):
    img = color2bmp(cmask, width, height,stride)
    return img

def image_gray(gmask, width, height, stride):
    cmask = gray2color(gmask)
    return image_color(cmask, width, height, stride)

def image_mask(mask, width, height, stride):
    gmask = bool2gray(mask)
    return image_gray(gmask, width, height, stride)

def save_color(path, cmask, width, height, stride):
    img = color2bmp(cmask, width, height,stride)
    img.Save(path)
    return

def save_gray(path, gmask, width, height, stride):
    cmask = gray2color(gmask)
    return save_color(path, cmask, width, height, stride)

def save_mask(path, mask, width, height, stride):
    gmask = bool2gray(mask)
    return save_gray(path, gmask, width, height, stride)

def dump_segments(segments):
    height = segments.GetLength(0)
    width  = segments.GetLength(1)

    segments_set = set()

    for i in range(0, height):
        for j in range(0, width):
            segments_set.add(segments[i,j])

    segments_list = list(segments_set)
    # print ("Segments : {}".format(segments_list))
    ns = len(segments_list)
    segments_pairs = [(segments_list[i], i) for i in range(0, ns)]
    segments_map = dict(segments_pairs)

    g = Array.CreateInstance(bool, ns, ns) # macierz incydencji

    for i in range(0, height):
        for j in range(0, width):
            s1 = segments[i,j]
            for k in range(-1,2):
                for l in range(-1,2):
                    y = (i+k)%height
                    x = (j+l)%width

                    if k == 0 and l == 0:
                        continue

                    s2 = segments[y,x]
                    g[segments_map[s1],segments_map[s2]] = True

    stages = Array.CreateInstance(int, ns)
    max_stage = -1

    for i in range(0, ns):
        c = 0
        for j in range(0, ns):
            if g[i,j]:
                c += 1
        stages[i] = c
        if c > max_stage:
            max_stage = c

    # print ("We need {} colors".format(max_stage+1))

    max_color = 2**24-1
    colors = max_stage+1
    steps = max_color/(colors-1)

    seg_col_mapping = {}
    segidx_col_mapping = Array.CreateInstance(int, ns)
    for i in range(0, ns):
        segidx_col_mapping[i] = -1
    order = [el for el in range(0,ns)]

    sorder = sorted(order, key = lambda x : stages[x], reverse = True)

    currColor = 0

    # print ("sorder: {}".format(sorder))
    for i in range(0, ns):
        neigh_colors = set()
        for j in range(0, ns):
            c = segidx_col_mapping[j]
            if c >= 0:
                neigh_colors.add(c)

        choosenIdx = -1
        for j in range(0, currColor):
            if not(j in neigh_colors):
                choosenIdx = j
                break

        if choosenIdx < 0:
            currColor += 1
            choosenIdx = currColor
        segidx_col_mapping[i] = choosenIdx


    for s in segments_list:
        seg_col_mapping[s] = segidx_col_mapping[segments_map[s]]

    # print ("Colors count: {}".format(colors))

    bpallete = Array.CreateInstance(Byte, colors)
    gpallete = Array.CreateInstance(Byte, colors)
    rpallete = Array.CreateInstance(Byte, colors)
    # print ("Steps: {}".format(steps))

    for i in range(0, colors):
        c = int(steps*i)
        # print ("c: {}".format(c))
        b = c & 255
        g = (c >> 8) & 255
        r = (c >> 16) & 255
        bpallete[i] = b
        rpallete[i] = g
        gpallete[i] = r

        # print ("Color ({}) - b: {}, g: {}, r: {}".format(i, b, g, r))


    coloring = Array.CreateInstance(Byte, height, width, 3)

    for i in range(0, height):
        for j in range(0, width):
            s = segments[i,j]
            idx = seg_col_mapping[s]
            coloring[i,j,0] = Byte(bpallete[idx])
            coloring[i,j,1] = Byte(gpallete[idx])
            coloring[i,j,2] = Byte(rpallete[idx])

    return coloring

def setPixel(img, c, x, y):
#    print ("Pixel: ({},{})".format(x,y))
    if c is Byte:
        img[y,x] = c
    else:
        n = c.GetLength(0)
        for k in range(0, n):
            img[y,x,k] = c[k]
    return

def markPixel(img, c, x, y, window = 5):
    height = img.GetLength(0)
    width  = img.GetLength(1)
    for i in range(-window, window+1):
        y2 = (y+i)%height
        x2 = (x-i)%width
        setPixel(img, c, x2, y2)
        y2 = (y-i)%height
        x2 = (x-i)%width
        setPixel(img, c, x2, y2)


def drawLine(img, c, x1, y1, x2, y2):
    height = img.GetLength(0)
    width  = img.GetLength(1)

    dx = x2 - x1
    dy = y2 - y1

    s1 = 2*dy
    s2 = s1 - dx

    k = 0
    p = s2
    setPixel(img, c, x1, y1)

    yk = y1
    for xk in range(x1+1, x2+1):
        if p < 0:
            setPixel(img, c, xk, yk)
            p += s1
        else:
            yk += 1
            setPixel(img, c, xk, yk)
            p += s2
    return

def drawLine2(img, color, x1, y1, x2, y2):
    # print ("Drawline ({},{}) - ({},{})".format(x1,y1,x2,y2))
    height = img.GetLength(0)
    width  = img.GetLength(1)
    s = (height, width)
    if y1 != y2:
        if y1 > y2:
            ty = y1
            y1 = y2
            y2 = ty
            tx = x1
            x1 = x2
            x2 = tx

        step = float(x2-x1)/float(y2-y1)
        # print ("Step: {}".format(step))
        # print ("Drawline ({},{}) - ({},{})".format(x1,y1,x2,y2))
        for i in range(y1,y2):
            x = (i-y1)*step+x1
            xx = (i+1-y1)*step+x1
            xl = int(round(x))%s[1]
            xu = int(round(xx))%s[1]
            # print ("x: {}, xx: {}, xl: {}, xu: {}".format(x, xx, xl, xu))
            if xl < xu:
                for j in range(xl,xu+1):
                    # print ("aa ({},{})".format(j,i))
                    setPixel(img, color, j, i)
            else:
                for j in range(xu,xl+1):
                    # print ("aa ({},{})".format(j,i))
                    setPixel(img, color, j, i)

            setPixel(img, color, x2, y2)
    else:
        if x1 < x2:
            # print ("y1 == y2, od x1 do x2")
            for i in range(x1,x2+1):
                setPixel(img, color, i, y1)
        else:
            # print ("y1 == y2, od x2 do x1")
            for i in range(x2,x1+1):
                setPixel(img, color, i, y1)

    return


def shoeShape(img): # bitmap
    (data,width,height,stride) = bitmap2array(img)

    print("[{}] Converting to matrix...".format(datetime.now()))
    m = array2matrix(data, width, height, stride)

    print("[{}] Converting to gray...".format(datetime.now()))
    mGray = color2gray(m)

    print("[{}] Calculating otsu mask...".format(datetime.now()))
    omask = otsu_mask(mGray)

    print("[{}] Calculating segments in otsu mask...".format(datetime.now()))
    (segments,colors) = calculate_connected_parts(omask)

    print("[{}] Calculating segments' parameters ...".format(datetime.now()))

    (counts,xmins,xmaxs,ymins, ymaxs) = calculate_segment_parameters(segments)

    values = counts.items()
    fvalues = [(k,v) for (k,v) in values if colors[k]]
    svalues = sorted(fvalues, key=lambda (a,b): b)
    print(svalues)

    svalues.reverse()
    smask = select_segment(segments, svalues[0][0])
    selmask = smask.Clone()
    save_mask("test2.png", selmask, width, height, stride)
    img_color = image_mask(selmask, width, height, stride)
    return img_color

def line_intersection(x1, y1, x2, y2, x3, y3, x4, y4, eps = 1e-6):
#    print("\n\n")
#    print ("({},{}) - ({},{}) -> ({},{}) - ({},{})"
#            .format(x1,y1,x2,y2,x3,y3,x4,y4))
    a1 = y2-y1
    b1 = x2-x1
    c1 = a1*x1+b1*y1
#    print("a1: {}, b1: {}, c1: {}".format(a1,b1, c1))

    a2 = y4-y3
    b2 = x4-x3
    c2 = a2*x3+b2*y3

#    print("a2: {}, b2: {}, c2: {}".format(a2,b2,c2))

    det = a1*b2 - a2*b1
#    print ("Det: {}".format(det))
    if abs(det) < eps:
        return None # linie równoległe
    else:
        x = float(b2*c1 - b1*c2)/float(det)
        y = float(a1*c2 - a2*c1)/float(det)
#        print ("x:{}, y:{}".format(x,y))
        if x < min(x1,x2) or x > max(x1,x2):
#            print ("c1")
            return None

        if x < min(x3,x4) or x > max(x3,x4):
#            print ("c2")
            return None

        if y < min(y1,y2) or y > max(y1,y2):
#            print ("c3")
            return None

        if y < min(y3,y4) or y > max(y3,y4):
#            print ("c4")
            return None


        return (x,y)

def polygon_inside(poly1, poly2,width): # czy p1 jest w p2
    n = len(poly1)
    m = len(poly2)

    for i in range(0, n):
        i2 = (i+1)%n
        for j in range(i, m):
            j2 = (j+1)%m

            (y1,x1) = poly1[i]
            (y2,x2) = poly1[i2]
            (y3,x3) = poly2[j]
            (y4,x4) = poly2[j2]

            if line_intersection(x1,y1,x2,y2,x3,y3,x4,y4) != None:
#                print ("Linia {}-ta przecina się z {}-tą".format(i,j))
                return False

    (qy,qx) = poly1[0]
    xmin = xmax = poly2[0][1]

    for (py,px) in poly2:
        xmin = min(xmin, px)
        xmax = max(xmax, px)

#    print ("xmax: {}".format(xmax))

    # wyznaczmy poziomą linię?

    (y2,x2) = (qy,width-1)
    (y1,x1) = (qy,qx)

    c = 0


#    print ("Półprosta ({},{}) - ({},{})".format(x1,y1,x2,y2))

    for i in range(0, m):
        i2 = (i+1)%m

        (y3,x3) = poly2[i]
        (y4,x4) = poly2[i2]

        r2 = line_intersection(x1,y1,x2,y2,x3,y3,x4,y4)
#        print ("Linia ({},{}) - ({},{})".format(x3,y3,x4,y4))
        if r2 != None:
#            print ("Przecina się z {}-tą".format(i))
#            print ("R2: {}".format(r2))
            c += 1

#    print ("c: {}".format(c))
    return (c % 2 == 1)


