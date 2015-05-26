#!/usr/bin/python3
# -*- coding: UTF-8 -*-

import sys

import clr
clr.AddReference("System.Drawing")
clr.AddReferenceToFileAndPath("Svg.dll")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *
from Svg import *

from utils import *
from processing.segmentation.connected import *
from processing.segmentation.otsu import *
from datetime import datetime
from processing.shoes.shape import *
from processing.masks import *
from processing.contours.psweeping import *
from processing.classification.shapes import *

if len(sys.argv) <= 1:
	print("Too small arguments...")
	sys.exit(1)

path = sys.argv[1]
print ("Path: {}".format(path))
img = Bitmap(path)
print("[{}] Converting to array...".format(datetime.now()))
(data,width,height,stride) = bitmap2array(img)

print("[{}] Converting to matrix...".format(datetime.now()))
m = array2matrix(data, width, height, stride)

print("[{}] Converting to gray...".format(datetime.now()))
mGray = color2gray(m)


print("[{}] Calculating segments in otsu mask...".format(datetime.now()))
(segments,colors) = calculate_connected_parts(mGray)
# selmask = select_segment(segments, 200) # koło

print("[{}] Calculating segments' parameters ...".format(datetime.now()))

(counts,xmins,xmaxs,ymins, ymaxs, meansx, meansy) = calculate_segment_parameters(segments)


cmask = detect_contours(segments, None)
cmaskImg = m.Clone()
clines = calculate_contour_lines(segments, cmask)
# slines = detect_border_points(segments, cmask, clines)
slines = contours_simplifications(clines, 4)
classification = classificate_shapes(segments, None, clines, slines)

filtered_segments = [k for k in slines.keys() if counts[k] > 20]

seg_inside = {}

for seg1 in filtered_segments:
    for seg2 in filtered_segments:
        if seg1 == seg2:
            seg_inside[(seg1,seg1)] = False
            continue
        print ("Checking if {} in {}".format(seg1, seg2))
        poly1 = slines[seg1]
        poly2 = slines[seg2]
        inside = polygon_inside(poly1,poly2,width)
        seg_inside[(seg1,seg2)] = inside
        if inside:
            print ("Segment {} in {}".format(seg1,seg2))
            print ("p1: {}".format(poly1))
            print ("p2: {}".format(poly2))

def calculate_z(segments,inside,m,seg):
    if m.get(seg) != None:
        return (m, m[seg])

    zmax = -1

    for nseg in segments:
        if inside[(nseg,seg)]:
            res = calculate_z(segments,inside,m,nseg)
            print ("res: {}".format(res))
            (m,z) = res
            print ("m: {}, z: {}".format(m,z))
            zmax = max(zmax, z)
    m[seg] = zmax+1
    print ("m: {}, znew: {}".format(m, zmax+10))
    return (m,zmax+1)

def calculate_z_all(segments,inside,m):
    print ("Segments: {}".format(segments))
    for seg in segments:
        (m,z) = calculate_z(segments,inside,m,seg)
        print ("x'm: {}, z: {}".format(m,z))

    return m

msegz = calculate_z_all(filtered_segments, seg_inside, {})

print("msegz: {}".format(msegz))

c = Array.CreateInstance(Byte, 3)
c[0] = Byte(0)
c[1] = Byte(0)
c[2] = Byte(255)
c2 = Array.CreateInstance(Byte, 3)
c2[0] = Byte(255)
c2[1] = Byte(0)
c2[2] = Byte(0)

doc = SvgDocument()
doc.Width = SvgUnit(width)
doc.Height = SvgUnit(height)
g = SvgGroup()

xlines = [k for k in filtered_segments]
xlines2 = sorted(xlines, key=lambda k: msegz[k], reverse=True)

for seg in xlines2:
    cx = colors[seg]
#    if seg == segments[0,0]:
#        continue

    if counts[seg] < 20:
        continue
    print ("Segment: {}, color: {}".format(seg, cx))
    c3 = Color.FromArgb(cx, cx, cx)
    poly = SvgPolygon()
    poly.Stroke = SvgColourServer(Color.Red)
    poly.Fill = SvgColourServer(c3)
    poly.StrokeWidth = SvgUnit(1)
    poly.Points = SvgPointCollection()

    sline = slines[seg]
    n = len(sline)
    markPixel(cmaskImg, c2, meansx[seg], meansy[seg])
#    print ("sline: {}, n: {}".format(sline, n))
    for i in range(0, n):
        (cy,cx) = sline[i]
        poly.Points.Add(SvgUnit(cx))
        poly.Points.Add(SvgUnit(cy))

        (ny,nx) = sline[(i+1)%n]
#        print ("c: ({},{}) -> n: ({},{})".format(cx, cy, nx, ny))
        drawLine2(cmaskImg, c, cx, cy, nx, ny)

#        markPixel(cmaskImg, c2, cx, cy)
#        markPixel(cmaskImg, c2, nx, ny)

#for seg in meansx.keys():
#	markPixel(cmaskImg, c2, meansx[seg], meansy[seg])
    g.Children.Add(poly)

doc.Children.Add(g)
doc.Write("save.svg")

print("[{}] Saving img with contours ...".format(datetime.now()))
save_color("contours.png", cmaskImg, width, height, stride)

print("[{}] Saving img with contours ...".format(datetime.now()))
save_color("m.png", m, width, height, stride)



print("[{}] Saving cmask ...".format(datetime.now()))
save_mask("cmask.png", cmask, width, height, stride)
