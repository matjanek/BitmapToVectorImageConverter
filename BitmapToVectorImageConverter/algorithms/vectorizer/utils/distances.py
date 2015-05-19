__author__ = 'tpolgrabia'

from math import sqrt

def distance (v1,v2):
    (y1,x1) = v1
    (y2,x2) = v2
    dx = x1-x2
    dy = y1-y2
    return sqrt(dx*dx+dy*dy
                )
