import sys

import clr
clr.AddReference("System.Drawing")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *

from utils import *
from processing.contours.psweeping import *

dist = p2l_distance(0.0, 0.0, 10.0, 20.0, 5.0, 111.0)
print ("Distance: {}".format(dist))

p2 = [
        (468,373),
        (468,374),
        (263,374),
        (262,184),
        (262,176),
        (467,176),
        (468,373)
     ]

p1 = [
        (252,345),
        (252,346),
        (128,345),
        (128,179),
        (252,180),
        (252,345)
     ]

tp1 = [(r2,r1) for (r1,r2) in p1]
tp2 = [(r2,r1) for (r1,r2) in p2]

print ("tp1: {}".format(tp1))
print ("tp2: {}".format(tp2))

print ("A inside B: {}".format(polygon_inside(tp1,tp2,1000)))


x1 = 83
y1 = 65
x2 = 599
y2 = 65

x3 = 110
y3 = 69
x4 = 117
y4 = 62

res = line_intersection(x1,y1,x2,y2,x3,y3,x4,y4)
print ("Res: {}".format(res))
