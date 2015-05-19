# -*- coding: UTF-8 -*-

from System import *

def histogram(img, mask = None):
	# img jako macierz dwuwymiarowa
	width  = img.GetLength(1)
	height = img.GetLength(0)

	frequency = Array.CreateInstance(int, 256)
	for i in range(0, height):
		for j in range(0, width):
                    if mask == None or mask[i,j]:
			val = int(img[i,j])
			frequency[val] += 1
#	print("Histogram: {}".format(frequency))
	return frequency

def otsu(img, mask = None):
	hist = histogram(img,mask)
	n = sum(hist)
        print (hist)
	sa = 0
	for i in range(0, 256):
		sa += i*hist[i]

#	print ("n: {}".format(n))
#	print ("sa: {}".format(sa))

	firstIdx = 0;
	while hist[firstIdx] <= 0:
		firstIdx += 1

	a = sa / n
	nb = hist[firstIdx]
	no = n - hist[firstIdx]

	sb = hist[firstIdx]*firstIdx
	so = sa - hist[firstIdx]*firstIdx
	ab = sb / nb
	ao = so / no

	mBetween = nb * no * (ab - ao) * (ab - ao)
	mIdx = 0

	for i in range(firstIdx+1,255):
		nb += hist[i]
		no -= hist[i]

		if no <= 0:
			break

#		print("i: {}, no: {}, nb: {}".format(i, no, nb))
		
		sb += hist[i]*i
		so -= hist[i]*i

		ab = sb / nb
		ao = so / no

		between = nb * no * (ab - ao) * (ab - ao)

#		print("i: {}, odchylenie: {}"
#				.format(i, between))

		if between > mBetween:
#			print("between odchylenie {} wieksze od poprzedniego: {}, zamiana idx z {} na {}"
#					.format(between, mBetween, mIdx, i))
			mBetween = between
			mIdx = i
	
#	print("mIdx: {}".format(mIdx))
	return mIdx

def otsu_mask(img,mask = None):
	height = img.GetLength(0)
	width = img.GetLength(1)

	threshold = otsu(img,mask)
	mask = Array.CreateInstance(bool, height, width)

	for i in range(0, height):
		for j in range(0, width):
			mask[i,j] = img[i,j] <= threshold
	return mask
