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

print("[{}] Calculating otsu mask...".format(datetime.now()))
omask = otsu_mask(mGray)

print("[{}] Calculating segments in otsu mask...".format(datetime.now()))
(segments,colors) = calculate_connected_parts(omask)

print("[{}] Calculating segments' parameters ...".format(datetime.now()))

(counts,xmins,xmaxs,ymins, ymaxs, meansx, meansy) = calculate_segment_parameters(segments)

values = counts.items()
fvalues = [(k,v) for (k,v) in values if colors[k]]
svalues = sorted(fvalues, key=lambda (a,b): b)
print(svalues)

svalues.reverse()
smask = select_segment(segments, svalues[0][0])
selmask = smask.Clone()

(maximus, minimus, gmax, gmin,parts) = calculate_parts(smask)
print("gmin: {}, gmax: {}".format(gmin, gmax))
print("Parts: {}".format(parts))

# for j in maximus:
# 	for i in range(0, height):
# 		smask[i,j] = True
	
# for j in minimus:
# 	for i in range(0, height):
# 		smask[i,j] = False

(m1,m2,m3) = parts
dist = m3-m1
m4 = m1 + 0.7*dist

(bl,br,bt,bb) = calculate_boundary(smask)


print("[{}] Calculating otsu mask for inner space ...".format(datetime.now()))

omask2 = otsu_mask(mGray, selmask)
omask3 = and_mask(selmask, omask2)


print("[{}] Calculating connected segments for inner ...".format(datetime.now()))

(segments2,colors2) = calculate_connected_parts(omask3)
(counts2,xmins2,xmaxs2,ymins2,ymaxs2,meansx2, meansy2) = calculate_segment_parameters(segments2, selmask)

print ("Colors: {}".format(colors2))
print ("N: {}".format(len(colors2)))

seg_threshold = 100

fcounts2 = dict([(k,v) for (k,v) in counts2.items() if v >= seg_threshold])
print ("Fcounts2: {}".format(fcounts2))

print("[{}] Calculating inner mask (by size) ...".format(datetime.now()))

inner_ex_mask = calculate_mask_segments(segments2, selmask, fcounts2.keys())


for j in [m1,m4]:
	for i in range(0, height):
		smask[i,j] = True

for j in [bl,br]:
	for i in range(0, height):
		smask[i,j] = True
                omask3[i,j] = True
for i in [bt,bb]:
	for j in range(0, width):
		smask[i,j] = True
                omask3[i,j] = True


# print("[{}] Converting otsu mask to gray...".format(datetime.now()))
# ogimg = bool2gray(omask)
# ogimg = bool2gray(smask)

csegments2 = dump_segments(segments2)

save_color("csegments2.png", csegments2, width, height, stride)

cmask = detect_contours(segments2, None)
cmaskImg = m.Clone()
clines = calculate_contour_lines(segments2, cmask)
slines = detect_border_points(segments2, cmask, clines)

print ("Clines: {}".format(clines))

print ("M: {}", m)

# for i in range(0, height):
#     for j in range(0, width):
#         if cmask[i,j] and selmask[i,j]:
#             cmaskImg[i,j,0] = Byte(0)
#             cmaskImg[i,j,1] = Byte(0)
#             cmaskImg[i,j,2] = Byte(255)

c = Array.CreateInstance(Byte, 3)
c[0] = Byte(0)
c[1] = Byte(0)
c[2] = Byte(255)
c2 = Array.CreateInstance(Byte, 3)
c2[0] = Byte(255)
c2[1] = Byte(0)
c2[2] = Byte(0)


# for seg in clines.keys():
#     sclines = clines[seg]
#     n = len(sclines)
#     for i in range(0, n):
#         i2 = (i+1)%n
#         (y1,x1) = sclines[i]
#         (y2,x2) = sclines[i2]
#         print ("From ({},{}) -> To ({},{})"
#                 .format(x1,y1,x2,y2))
#         drawLine(cmaskImg, c, x1, y1, x2, y2)

for seg in slines.keys():
    sline = slines[seg]
    n = len(sline)
    print ("sline: {}, n: {}".format(sline, n))
    for i in range(0, n):
        (cy,cx) = sline[i]
        (ny,nx) = sline[(i+1)%n]
        print ("c: ({},{}) -> n: ({},{})".format(cx, cy, nx, ny))
        drawLine2(cmaskImg, c, cx, cy, nx, ny)

        markPixel(cmaskImg, c2, cx, cy)
        markPixel(cmaskImg, c2, nx, ny)

print("[{}] Saving inner ex mask mask ...".format(datetime.now()))
save_mask("inner_ex_mask.png"  , inner_ex_mask, width, height, stride)


print("[{}] Saving sel mask ...".format(datetime.now()))
save_mask("result.png"  , selmask, width, height, stride)

print("[{}] Saving segment mask ...".format(datetime.now()))
save_mask("segments.png", smask, width, height, stride)

print("[{}] Saving inner otsu ...".format(datetime.now()))
save_mask("omask2.png", omask2, width, height, stride)


print("[{}] Saving inner otsu ...".format(datetime.now()))
save_mask("omask3.png", omask3, width, height, stride)

print("[{}] Saving img with contours ...".format(datetime.now()))
save_color("contours.png", cmaskImg, width, height, stride)

print("[{}] Saving cmask ...".format(datetime.now()))
save_mask("cmask.png", cmask, width, height, stride)
