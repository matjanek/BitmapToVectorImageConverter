#/usr/bin/python3

import pylab as pl
from scipy import ndimage
import numpy as np

eps=1e-3

windowLeft = -3
windowRight = 3
windowBottom = -3
windowTop = 3

def reducedColorSearch(bins, color):
    nColor = int(color * 256.0)
    oldColor = 0
    for i in range(0, len(bins)):
        if nColor < bins[i]:
            return (oldColor + bins[i])/2.0
        oldColor = bins[i]
    return 255.0

def reduceColor(img, colors):
    s = img.shape
    hist = ndimage.histogram(img, 0.0, 1.0, 256)
    pixels = s[0]*s[1]
    print ("Pixels: {}".format(pixels))
    pixelPerBin = pixels / colors
    print ("Pixels per bin: {}".format(pixelPerBin))
    print(hist)

    sum = 0
    binStarts = []
    for i in range(0, 256):
        sum += hist[i]
        if sum >= pixelPerBin:
            sum = 0
            binStarts.append(i)

    print(binStarts)

    img2 = np.copy(img)
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            img2[i,j] = (reducedColorSearch(binStarts, img[i,j]))/256.0

    return img2

u = 2
v = 2
k = 0.05

def harrisDetection(img):
    s = img.shape
    imgMask = np.array(s, pl.int32)

    for i in range(0, s[0]):
        for j in range(0, s[1]):
            x = (j+u)
            y = (i+v)

            ix = img



img = pl.imread("X2.png")
rImg = reduceColor(img, 4)
s = img.shape

rangeX = 3
rangeY = 3
threshold = (rangeX+1)*(rangeY + 1) * 0.75
print(threshold)
mask = np.zeros(s, dtype=pl.bool8)

for i in range(0, s[0]):
    for j in range(0, s[1]):
        c = 0
        for k in range(-rangeY+1,rangeY):
            for l in range(-rangeX+1, rangeX):
                y = (i+k)%s[0]
                x = (j+l)%s[1]
                if abs(rImg[y,x]-rImg[i,j]) < eps:
                    c += 1
        mask[i,j] = True if c >= threshold else False


pl.imshow(mask, pl.gray())
pl.show()
