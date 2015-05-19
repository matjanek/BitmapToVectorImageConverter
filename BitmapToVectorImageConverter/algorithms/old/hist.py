# coding: utf-8
import pylab as pl
img = pl.imread("converse2.jpg")
img
pl.imshow(img)
pl.show()
pl.imshow(img, cmap=pl.gray())
pl.show()
import scipy
from scipy import ndimage
img2 = ndimage.sobel(img)
pl.imshow(img2, cmap=pl.gray())
pl.show()
mask = img2
img2 = ndimage.gaussian_filter(img,3)
img3 = mask*img+(1-mask)*img2
img3
pl.imshow(img2, cmap=pl.gray())
pl.imshow(img3, cmap=pl.gray())
pl.show()
img2
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img, cmap=pl.gray())
pl.show()
mask
img3 = mask*img+(255-mask)*img2
mask2 = mask / 256.0
mask2 = mask / 255.0
mask
mask2
min(mask2)
(mask2

)
pl.imshow(mask2)
pl.show()
import numpy as np
np.max(mask2)
np.max(mask)
img3 = mask*img+(1.0-mask)*img2
pl.imshow(mask3, cmap=pl.gray())
pl.imshow(img3, cmap=pl.gray())
pl.show()
img3 = mask2*img+(1.0-mask2)*img2
pl.imshow(img3, cmap=pl.gray())
pl.show()
img3 = (1.0 - mask2)*img+mask2*img2
pl.imshow(mask3, cmap=pl.gray())
pl.imshow(img3, cmap=pl.gray())
pl.show()
from skimage import date
hist = ndimage.histogram(img, 0, 255, 256)
hist
a = sum(hist)
a
for i in range(0,256):

    pass
s = 0
for i in range(0,256):
    s += hist[i]
    if s >= (a/2.0):
        print (i)
        break
    
s
a
s
a
s
a
s
a
threshold = a/2.0
a
threshold
s = 0
for i in range(0,256):
    s += hist[i]
    if s >= threshold:
        print (i)
        break
    
s
hist
sum[0,:]
sum[0,10]
sum[0:10]
hist[0:2]
hist[0:3]
hist[0:128]
hist[0:128] > threshold
sum(hist[0:128]) > threshold
sum(hist[0:128]) >= threshold
sum(hist[0:200]) >= threshold
sum(hist[0:220]) >= threshold
sum(hist[0:240]) >= threshold
sum(hist[0:250]) >= threshold
sum(hist[0:254]) >= threshold
sum(hist[0:255]) >= threshold
sum(hist[0:256]) >= threshold
hist = ndimage.histogram(img, 0, 255, 256)
count(hist)
len(hist)
a
a = sum(hist)
threshold = a/2
threshold
a = sum(hist[0:100])
a = sum(hist[0:100])
a = sum(hist)
sum(hist[0:100])
sum(hist[0:200])
sum(hist[0:250])
sum(hist[0:254])
sum(hist[0:255])
sum(hist[0:256])
img2 = round(img / 16.0)*16.0
s = img.shape
img2 = np.copy(img)
for in range(0, s[0]):
  
for i in range(0, s[0]):
    for j in range(0, s[1]):
        img2[i,j] = round(img[i,j]/16.0)*16.0
        
pl.imshow(img2, cmap=pl.gray())
pl.show()
for i in range(0, s[0]):
    for j in range(0, s[1]):
        img2[i,j] = round(img[i,j]/64.0)*64.0
        
img2
pl.imshow(img2, cmap=pl.gray())
pl.show()
for i in range(0, s[0]):
    for j in range(0, s[1]):
        img2[i,j] = round(img[i,j]/32.0)*32.0
        
pl.imshow(img2, cmap=pl.gray())
pl.show()
hist = ndimage.histogram(0, 255, 256)
hist = ndimage.histogram(img,0, 255, 256)
pl.plot([0:255], hist)
bins = 0:255
bins = [0:255]
bins = range(0, 256)
bins
bins = [i in range(0,256)]
bins
bins = [i for i in range(0,256)]
i
bins
pl.plot(bins, hist)
pl.show()
pl.plot(bins[0:254], hist[0:254])
pl.show()
pl.show()
pl.plot(bins[0:254], hist[0:254])
pl.show()
get_ipython().magic('edit')
img
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img, cmap=pl.gray())
pl.show()
pl.imshow(img2, cmap=pl.gray())
pl.show()
img = pl.imread("converse2.jpg")
get_ipython().magic('edit Out[137]')
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img, cmap=pl.gray())
pl.show()
get_ipython().magic('edit Out[146]')
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img, cmap=pl.gray())
get_ipython().magic('edit Out[146]')
pl.show()
get_ipython().magic('edit Out[146]')
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img, cmap=pl.gray())
pl.show()
pl.imshow(img2, cmap=pl.gray())
pl.show()
get_ipython().magic('edit Out[146]')
pl.imshow(img2, cmap=pl.gray())
pl.show()
pl.imshow(img2, cmap=pl.gray())
pl.show()
img3 = ndimage.sobel(img2)
pl.imshow(img3, cmap=pl.gray())
pl.show()
img4 = ndimage.maximum_filter(img3, 3)
pl.imshow(img4, cmap=pl.gray())
pl.show()
img4 = ndimage.maximum_filter(img3, 2)
pl.imshow(img4, cmap=pl.gray())
pl.show()
pl.imshow(img3, cmap=pl.gray())
pl.show()
mask = np.copy(img4)
mask = np.copy(img3)
get_ipython().magic('save "hist.py" 1-182')
