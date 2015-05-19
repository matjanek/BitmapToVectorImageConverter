import pylab as pl
import numpy as np
from scipy import ndimage
from scipy.stats import multivariate_normal

import sys

#img2 = pl.imread("converse2.jpg")
#img2 = pl.imread("obraz.png")
img2 = pl.imread(sys.argv[1])
s = img2.shape
print ("Min: {}, max: {}".format(np.min(img2),np.max(img2)))
for i in range(0, s[0]):
    print("\rRow: {}".format(i), end=' ')
    for j in range(0, s[1]):
        img2[i,j] = 32.0*round(256.0*img2[i,j]/32.0)/256.0

img = img2
pl.imsave("polprodukt.png", img, cmap=pl.gray())

pl.imsave("sobe.png", ndimage.sobel(img), cmap=pl.gray())

kX = np.array([[-1, 0, 1], [-1, 0, 1], [-1, 0, 1]])
kY = np.transpose(kX)

imgX = ndimage.convolve(img, kX)
imgY = ndimage.convolve(img, kY)

imgX2 = imgX * imgX
imgY2 = imgY * imgY
imgXY = imgX * imgY

var = multivariate_normal(mean=[0,0], cov=[[2,0],[0,2]])
g = np.zeros((9,9), dtype=float)
for i in range(-4, 5):
    for j in range(-4, 5):
        g[i+4,j+4] = var.pdf([i,j])

imgX2g = ndimage.convolve(imgX2,g)
imgY2g = ndimage.convolve(imgY2,g)
imgXYg = ndimage.convolve(imgXY,g)

k = 0.05
cim = (imgX2g * imgY2g - imgXYg**2) - k*(imgX2g + imgY2g)**2

pl.imsave(sys.argv[2],cim, cmap=pl.gray())

iMin = np.min(cim)
iMax = np.max(cim)

print("min: {}, max: {}".format(iMin,iMax))

for i in range(0, s[0]):
    for j in range(0, s[1]):
        cim[i,j] = (cim[i,j]-iMin)/(iMax - iMin)

iMin = np.min(cim)
iMax = np.max(cim)

print("min: {}, max: {}".format(iMin,iMax))

pl.imsave(sys.argv[3],cim, cmap=pl.gray())

result = cim > 0.4
pl.imshow(result, cmap=pl.gray())
pl.show()

#pl.imshow(imgR, cmap=pl.gray())
#pl.show()

#pl.imshow(cim, cmap=pl.gray())
#pl.show()

# pl.imshow(imgXY, cmap=pl.gray())
# pl.show()
