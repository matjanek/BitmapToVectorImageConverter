# -*- coding: UTF-8 -*-
import sys
from System import *
from collections import deque
from System.Math import *
from processing.classification.types import *
from processing.segmentation.connected import *
from processing.contours.psweeping import *

def check_for_circle(segments, cmask, cline, sline, info, window = 3, minSize = 30):
    (fy,fx) = cline[0]
    (counts,xmins, xmaxs, ymins, ymaxs, meansx, meansy) = info
    seg = segments[fy,fx]

    if counts[seg] < minSize:
        return False

    (my,mx) = (meansy[seg], meansx[seg])
    print ("Seg: {}".format(seg))

    distances = {}
    for (py,px) in cline:
        d = int(round(distance(mx, my, px, py)))
        c = distances.get(d)
        if c == None:
            c = 1
        else:
            c += 1
        distances[d] = c

    ldistances = distances.keys()
    minDist = min(ldistances)
    maxDist = max(ldistances)

    print ("seg: {}, minDist: {}, maxDist: {}".format(seg, minDist, maxDist))

    return (maxDist - minDist) <= window

def check_for_line(segments, cmask, cline, sline, info, window = 5, minSize = 20):
    (counts, xmins, xmaxs, ymins, ymaxs, meansx, meansy) = info
    (py,px) = cline[0]
    seg = segments[py,px]
    if counts[seg] < minSize:
        return False

    xmin = xmins[seg]
    ymin = ymins[seg]
    xmax = xmaxs[seg]
    ymax = ymaxs[seg]

    cyMax = 0
    for i in range(ymin, ymax+1):
        c = 0
        for j in range(xmin, xmax+1):
            if segments[i,j] == seg:
                c += 1

        if c > cyMax:
            cyMax = c

    cxMax = 0
    for j in range(xmin, xmax+1):
        c = 0
        for i in range(ymin, ymax+1):
            if segments[i,j] == seg:
                c += 1

        if c > cxMax:
            cxMax = c

    return cyMax <= window or cxMax <= window

def classificate_shape(segments, cmask, cline, sline, info):
    if check_for_circle(segments, cmask, cline, sline, info):
        print ("Circle detected")
    elif check_for_line(segments, cmask, cline, sline, info):
        print ("Line detected")

    vertices = []
    for (py,px) in sline:
        vertices.append(Point(px,py))
    return Polygon(vertices)

def classificate_shapes(segments, cmask, clines, slines):
    info =  calculate_segment_parameters(segments)

    shapes = {}
    for (seg,sline) in slines.items():
        shapes[seg] = classificate_shape(segments, cmask,
                                         clines[seg], sline, info)
    return shapes
