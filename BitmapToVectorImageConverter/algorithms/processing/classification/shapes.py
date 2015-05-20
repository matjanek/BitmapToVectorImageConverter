# -*- coding: UTF-8 -*-
import sys
from System import *
from collections import deque
from System.Math import *
from processing.classification.types import *

def classificate_shape(segments, cmask, sline):
    vertices = []
    for (py,px) in sline:
        vertices.append(Point(px,py))
    return Polygon(vertices)

def classificate_shapes(segments, cmask, slines):
    shapes = {}
    for (seg,sline) in slines.items():
        shapes[seg] = classificate_shape(segments, cmask, sline)
    return shapes
