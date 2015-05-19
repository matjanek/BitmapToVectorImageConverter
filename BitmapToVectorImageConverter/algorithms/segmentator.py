#!/usr/bin/python3
# -*- coding: UTF-8 -*-

import sys

import clr
clr.AddReference("System.Drawing")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *

from utils import *
from processing.segmentation.connected import *
from processing.segmentation.otsu import *
from datetime import datetime
from processing.shoes.shape import *
from processing.masks import *
from processing.contours.psweeping import *

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

print("[{}] Calculating segments' parameters ...".format(datetime.now()))

(counts,xmins,xmaxs,ymins, ymaxs, meansx, meansy) = calculate_segment_parameters(segments)


cmask = detect_contours(segments, None)
cmaskImg = m.Clone()
clines = calculate_contour_lines(segments, cmask)
# slines = detect_border_points(segments, cmask, clines)
slines = contours_simplifications(clines, 3)

c = Array.CreateInstance(Byte, 3)
c[0] = Byte(0)
c[1] = Byte(0)
c[2] = Byte(255)
c2 = Array.CreateInstance(Byte, 3)
c2[0] = Byte(255)
c2[1] = Byte(0)
c2[2] = Byte(0)


for seg in slines.keys():
    sline = slines[seg]
    n = len(sline)
    print ("sline: {}, n: {}".format(sline, n))
    for i in range(0, n):
        (cy,cx) = sline[i]
        (ny,nx) = sline[(i+1)%n]
        print ("c: ({},{}) -> n: ({},{})".format(cx, cy, nx, ny))
        drawLine2(cmaskImg, c, cx, cy, nx, ny)

#        markPixel(cmaskImg, c2, cx, cy)
#        markPixel(cmaskImg, c2, nx, ny)

for seg in meansx.keys():
	markPixel(cmaskImg, c2, meansx[seg], meansy[seg])


print("[{}] Saving img with contours ...".format(datetime.now()))
save_color("contours.png", cmaskImg, width, height, stride)

print("[{}] Saving img with contours ...".format(datetime.now()))
save_color("m.png", m, width, height, stride)



print("[{}] Saving cmask ...".format(datetime.now()))
save_mask("cmask.png", cmask, width, height, stride)
