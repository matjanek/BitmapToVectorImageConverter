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

hist = ndimage.histogram(img2, 0, 1, 256)
colors = 16
dcolors = np.linspace(0.0, 100.0, colors)
for i in dcolors:
    print ("Percentile: {} equals: {}".format(i, np.percentile(img2, i)))


#    print("Kolor: {}".format(s))
