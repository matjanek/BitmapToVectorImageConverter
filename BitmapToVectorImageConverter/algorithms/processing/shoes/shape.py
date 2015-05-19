
# -*- coding: UTF-8 -*-
from System import *

def select_shape(img):
	pass

def calculate_boundary(mask):
	height = mask.GetLength(0)
	width  = mask.GetLength(1)

	l = -1
	r = -1
	t = -1
	b = -1

	for i in range(0, height):
		for j in range(0, width):
			if mask[i,j]:
				if l < 0 or j < l:
					l = j

				if r < 0 or j > r:
					r = j

				if t < 0 or i < t:
					t = i

				if b < 0 or i > b:
					b = i

	return (l,r,t,b)

def calculate_parts(mask, threshold = 2, neighbourhood = 20):
	height = mask.GetLength(0)
	width  = mask.GetLength(1)
	aminimus = Array.CreateInstance(bool, width)
	amaximus = Array.CreateInstance(bool, width)
	minimums = []
	maximums = []
	print ("Width: {}, height: {}".format(width, height))

	h = Array.CreateInstance(int, width)
	print ("Threshold: {}, neighbourhood: {}".format(threshold, neighbourhood))

	for j in range(0, width): # idziemy kolumnami
		counter = 0
		for i in range(0, height): 
			if mask[i,j]:
				counter += 1
		h[j] = counter

	print ("Histogram: {}".format(h))

	# przetwarzamy histogram

	gMinimum = h[0]
	gMinIdx = -1
	gMaximum = h[0]
	gMaxIdx = -1
	threshold1 = 0
	threshold2 = 2

	for j in range(0, width):
		if h[j] < threshold:
			continue

		if gMinIdx < 0:
			gMinIdx = j
			gMinimum = h[j]

		if gMaxIdx < 0:
			gMaxIdx = j
			gMaximum = h[j]

		minimum = h[j]
		maximum = h[j]
		if h[j] > gMaximum:
			gMaximum = h[j]
			gMaxIdx = j

		if h[j] < gMinimum:
			gMinimum = h[j]
			gMinIdx = j

			
		lMin = h[(j-neighbourhood)%width]
		lMax = lMin

		rMin = h[(j+1)%width]
		rMax = rMin

		for k in range(-neighbourhood,0):
			x = (j+k)%width

			if h[x] > lMax:
				lMax = h[x]

			if h[x] < lMin:
				lMin = h[x]

		for k in range(1, neighbourhood+1):
			x = (j+k)%width

			if h[x] > rMax:
				rMax = h[x]

			if h[x] < rMin:
				rMin = h[x]

		if lMin >= h[j] + threshold1 and rMin >= h[j] + threshold1 and lMax >= h[j] + threshold2 and rMax >= h[j] + threshold2:
			minimums.append(j)
		elif lMax <= h[j] - threshold1 and rMax <= h[j] - threshold1 and lMin <= h[j] - threshold2 and rMin <= h[j] - threshold2:
			maximums.append(j)

	print ("h: {}".format(h))
	print ("Maximums: {}, minimums: {}".format(maximums, minimums))

	for j in range(0, width):
		amaximus[j] = False
		aminimus[j] = False

	for j in minimums:
		aminimus[j] = True

	for j in maximums:
		amaximus[j] = True

	l1 = -1 # -1 niezdefiniowano 2 minimum, 1 maksimum
	l2 = -1
	id1 = -1
	id2 = -1

	pairs = []

	for j in range(0, width):
		if amaximus[j]:
			print ("now:1 l1: {}, l2: {}, ({},{},{})".format(l1,l2,j,id1,id2))
			if l1 == 2 and l2 == 1:
				pairs.append((id2, id1, j))

			l2 = l1
			l1 = 1
			id2 = id1
			id1 = j
		elif aminimus[j]:

			print ("now:2 l1: {}, l2: {}, ({},{},{})".format(l1,l2,j,id1,id2))
			l2 = l1
			l1 = 2
			id2 = id1
			id1 = j

	print ("Pairs: {}".format(pairs))

	pairThreshold = -10
	result = None
	resultVal = -1

	for (m1,m2,m3) in pairs:
		c = h[m3]-h[m1]
		print ("Pair ({},{},{}) - {}".format(m1,m2,m3, c))
		if c >= pairThreshold:
			if result == None or c > resultVal:
				result = (m1,m2,m3)
				resultVal = c
		
	return (maximums, minimums, gMaxIdx, gMinIdx,result)

