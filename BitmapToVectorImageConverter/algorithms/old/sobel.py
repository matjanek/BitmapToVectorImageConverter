#!/usr/bin/python3

import pylab as pl
import scipy as sp
import numpy as np
from scipy import ndimage

img = pl.imread("converse3s.png")
s = img.shape
img2 = ndimage.sobel(img)
img3 = ndimage.median_filter(img2, 5)

wLeft = -3
wRight = 3
wBottom = -3
wTop = 3
thresholdMin = 49*0.2
thresholdMax = 49*0.4
img2 = np.copy(img)

print(s)

for i in range(0, s[0]):
    for j in range(0, s[1]):
        img[i,j] = round(img[i,j]*64.0) / 64.0

for i in range(0, s[0]):
    print("\rLinia {}/{}".format(i,s[0]), end=' ')
    for j in range(0, s[1]):
        c = 0
        for k in range(wBottom, wTop+1):
            for l in range(wLeft, wRight+1):
                y = (i+k)%s[0]
                x = (j+l)%s[1]
                if img[y,x] == img[i,j]:
                    c = c+1
        if thresholdMin < c and c < thresholdMax:
#            print ("{}-{}".format(i,j))
#            x1 = max(0, j+wLeft)
#            x2 = min(s[1]-1, j+wRight)
#            y1 = max(0, i+wBottom)
#            y2 = min(s[0]-1, i+wTop)
            img2[i,j] = 0
#            print ("x1: {}, x2: {}".format(x1,x2))
#            for t in range(x1,x2+1):
#                img2[y1,t] = 1.0
#                img2[y2,t] = 1.0
#            for k in range(y1,y2+1):
#                img2[k,x1] = 1.0
#                img2[k,x2] = 1.0


pl.imshow(img2,cmap=pl.gray())
pl.show()
