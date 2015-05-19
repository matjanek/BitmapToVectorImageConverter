# -*- coding: UTF-8 -*-

from System import *

def calculate_mask_segments(segments, m, filtered):
    height = segments.GetLength(0)
    width  = segments.GetLength(1)

    mask = Array.CreateInstance(bool, height, width)
    for el in mask:
        el = False

    for i in range(0, height):
        for j in range(0, width):
            if m != None and not(m[i,j]):
                mask[i,j] = False
                continue

            mask[i,j] = segments[i,j] in filtered

    return mask

def calculate_seg_graph(seg, mask = None):
    height = seg.GetLength(0)
    width  = seg.GetLength(1)

    edges = {}
    m = 0

    for i in range(0, height):
        for j in range(0, width):
            if mask != None and not(mask[i,j]):
                continue

            s = seg[i,j]
            n = edges.get(s)

            if n == None:
                n = set()

            for k in range(-1,2):
                for l in range(-1,2):
                    y = (i+k)%height
                    x = (j+l)%width

                    if mask != None and not(mask[i,j]):
                        continue

                    s2 = seg[y,x]
                    if s != s2:
                        n.add(s2)
            edges[s] = n
            m = max(m, len(n))
    print ("Max: {}".format(m))
    return edges

def calculate_connected_parts(img, mask = None):
	height = img.GetLength(0)
	width  = img.GetLength(1)

	segments = Array.CreateInstance(int, height, width)
	colors = {}
	mapping = {}
	segIdx = 0

	# z powodu, ze przechodzimy od lewa do prawa
	# oznakowane rzeczy albo w górze albo po lewej stronie

	for i in range(0, height):
		for j in range(0, width):
			c = img[i,j]
			eqColorSegments = set()

                        if mask != None and not(mask[i,j]):
                            continue

			for k in range(-1,1):
				for l in range(-1,2):
					if k == 0 and l >= 0:
						break
#					print("({},{}) - ({},{})".format(j,i,x,y))
					y = (i+k)
					x = (j+l)

					if x < 0 or y < 0 or x >= width or y >= height:
						continue

                                        if mask != None and not(mask[y,x]):
                                            continue

					c2 = img[y,x]
					if c == c2:
						eqColorSegments.add(mapping[segments[y,x]])

			ns = len(eqColorSegments)
			leqColorSegments = list(eqColorSegments)
			if ns <= 0:
				segIdx += 1
				segments[i,j] = segIdx
				mapping[segIdx] = segIdx
				colors[segIdx] = c
			else:
				seg = leqColorSegments[0]
				while seg != mapping[seg]:
					print ("Reducting segment {} with {}"
							.format(seg, mapping[seg]))
					seg = mapping[seg]
				segments[i,j] = seg
				for t in range(0, ns):
					mapping[leqColorSegments[t]] = seg

	converted = set()
	for i in range(0, height):
		for j in range(0, width):
			seg = segments[i,j]
			m = mapping[seg]
			while m != mapping[m]:
				m = mapping[m]
			segments[i,j] = m
			converted.add(m)

	segs = set(mapping.values())

	print ("segments: {}, count: {}".format(segs, len(segs)))
	print (mapping)

	print("Converted: {}".format(converted))


	return (segments,colors)

def calculate_segment_parameters(segments, m = None):
    height = segments.GetLength(0)
    width  = segments.GetLength(1)

    counts = {}
    xmins = {}
    xmaxs = {}
    ymins = {}
    ymaxs = {}

    meansx = {}
    meansy = {}
    sumsx  = {}
    sumsy  = {}

    for i in range(0, height):
        for j in range(0, width):
            if m != None and not(m[i,j]):
                continue
            seg = segments[i,j]
            c = counts.get(seg)
            xmin = xmins.get(seg)
            xmax = xmaxs.get(seg)
            ymin = ymins.get(seg)
            ymax = ymaxs.get(seg)
            sumx = sumsx.get(seg)
            sumy = sumsy.get(seg)

            c = c + 1 if c != None else 1
            xmin = min(xmin,j) if xmin != None else j
            xmax = max(xmax,j) if xmax != None else j
            ymin = min(ymin,i) if ymin != None else i
            ymax = max(ymax,i) if ymax != None else i

            if sumx == None:
                sumx = 0.0
            if sumy == None:
                sumy = 0.0

            sumx += float(j)
            sumy += float(i)

            counts[seg] = c
            xmins[seg] = xmin
            xmaxs[seg] = xmax
            ymins[seg] = ymin
            ymaxs[seg] = ymax
            sumsx[seg] = sumx
            sumsy[seg] = sumy

    for seg in counts.keys():
        meansx[seg] = sumsx[seg] / float(counts[seg])
        meansy[seg] = sumsy[seg] / float(counts[seg])

    return (counts, xmins, xmaxs, ymins, ymaxs, meansx, meansy)

def select_segment(segments,seg):
	height = segments.GetLength(0)
	width  = segments.GetLength(1)

	mask = Array.CreateInstance(bool, height, width)

	for i in range(0, height):
		for j in range(0, width):
			mask[i,j] = segments[i,j] == seg
	return mask
