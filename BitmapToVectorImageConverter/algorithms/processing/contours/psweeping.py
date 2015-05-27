# -*- coding: UTF-8 -*-
from System import *
from collections import deque
from System.Math import *
from processing.segmentation.connected import *

def detect_contours(segments, mask):
	height = segments.GetLength(0)
	width  = segments.GetLength(1)

	rmask = Array.CreateInstance(bool, height, width)

	for i in range(0, height):
		for j in range(0, width):
			if mask != None and not(mask[i,j]):
				continue

			seg = segments[i,j]
			contour = False
			for k in range(-1,2):
				if contour:
					break

				for l in range(-1,2):
					y = (i+k)%height
					x = (j+l)%width

					if segments[y,x] != seg:
						contour = True
						break
			rmask[i,j] = contour
        for j in range(0, width):
            rmask[0,j] = True
            rmask[height-1,j] = True

        for i in range(0, height):
            rmask[i,0] = True
            rmask[i,width-1] = True

	return rmask

def calculate_chain(parents, x, y):
    l = []
    while x >= 0 and y >= 0:
        l.append((y,x))
        cy = parents[y,x,0]
        cx = parents[y,x,1]
#         print ("({},{}) -> ({},{})".format(x, y, cx, cy))
        y = cy
        x = cx

    return l

def calculate_contour_lines(segments, cmask = None):
    height  = segments.GetLength(0)
    width   = segments.GetLength(1)
    parents = Array.CreateInstance(int,height, width, 2)
    lengths = Array.CreateInstance(int, height, width)
    queued  = Array.CreateInstance(bool, height, width)
    segment_was = {}

    for i in range(0, height):
        for j in range(0, width):
            for k in range(0, 2):
                parents[i,j,k] = -1

    for i in range(0, height):
        for j in range(0, width):
            if cmask != None and not(cmask[i,j]) and not(queued[i,j]):
                continue
            s = segments[i,j]
            if segment_was.get(s) != None:
                continue
                
            q = deque()
            q.append([i,j])
            c = 1
            
            # print ("Segment: {}".format(s))
            queued[i,j] = True
            segment_was[s] = True

            while c > 0:
                print("")
                (qy,qx) = q.popleft()
                # print ("queue ({},{}),cmask: {} seg: {}".format(qy,qx, cmask[qy,qx], segments[qy,qx]))
                c -= 1
                for k in range(-1,2):
                    for l in range(-1,2):
                        y = (qy+k)
                        x = (qx+l)
                        if x < 0 or y < 0 or x >= width or y >= height:
                            continue

#                        if cmask != None:
#                            print ("cmask for ({},{}) ({},{}) - {}, s: {}, queued: {}".format(y,x, k, l,cmask[y,x], segments[y,x], queued[y,x]))
                        if cmask != None and not(cmask[y,x]):
                            continue
                        s2 = segments[y,x]
                        if s != s2 or queued[y,x]:
                            continue

                        q.append([y,x])
                        queued[y,x] = True
                        lengths[y,x] = lengths[qy,qx]+1
                        parents[y,x,0] = qy
                        parents[y,x,1] = qx
                        c += 1

    was_parent = Array.CreateInstance(bool, height, width)
    for el in was_parent:
        el = False

    for i in range(0, height):
        for j in range(0, width):
            if cmask != None and not(cmask[i,j]):
                continue

            py = parents[i,j,0]
            px = parents[i,j,1]
            if py < 0 or px < 0:
                continue

            was_parent[py,px] = True

    lchains = {}
    longest_threshold = 20

    for i in range(0, height):
        for j in range(0, width):
            if cmask != None and not(cmask[i,j]):
                continue

            if was_parent[i,j]:
                continue

            s = segments[i,j]
            l = lengths[i,j]

#            print ("End ({},{}) - seg: {}, length of the chain: {}"
#                    .format(j,i, s, l))

#            print ("({},{}) was not parent of any el, seg: {}, length: {}"
#                    .format(j,i, s, l))

            slchains = lchains.get(s)
            if slchains == None:
                slchains = [ (i,j) ]
            else:
                slchains.append((i,j))

            lchains[s] = slchains

    rchains = {}

#    print ("Chains joining")

    for seg in lchains.keys():
        sslchains = sorted(lchains[seg],
                            key = lambda (i,j) : lengths[i,j])
        sslchains.reverse()

#        print ("Lengths: {}"
#                .format([lengths[i,j] for (i,j) in sslchains]))

 #       print ("Sorted ssl chains: {}".format(sslchains))
        filtered_chains = sslchains[0:longest_threshold]

#        print ("Filtered chains: {}".format(filtered_chains))
        nchains = len(filtered_chains)
        chain_lists = [calculate_chain(parents, cx, cy) for (cy,cx) in filtered_chains]
        chain_sets  = [set(c) for c in chain_lists]
        mi = -1
        mj = -1
        mlength = -1

        for i in range(0, nchains):
            for j in range(0, i):
#                print ("Joining chain s1: {}, s2: {}"
#                    .format(filtered_chains[i], filtered_chains[j]))

                s1 = chain_sets[i]
                s2 = chain_sets[j]
#                print ("ns1: {}, ns2: {}".format(len(s1), len(s2)))
#                sand = s1.intersection(s2)
                sor  = s1.union(s2)
                nsor = len(sor)
                sand = s1.intersection(s2)
                nsand = len(sand)
#                print ("ns union: {}".format(nsor))

                if nsor > mlength and nsand < 10:
                    mlength = nsor
                    mi = i
                    mj = j
#                    print ("New longest with length: {}, join s1: {} with s2: {}"
#                        .format(mlength, filtered_chains[i], filtered_chains[j]))

        c1 = chain_lists[mi]
        c2 = chain_lists[mj]
#        print ("End: <{}, {}>".format(c1,c2))
        c2.reverse()

        rchains[seg] = c1 + c2

    return rchains

def distance(x1, y1, x2, y2):
    dx = x1-x2
    dy = y1-y2
    return Sqrt(dx*dx+dy*dy)

def detect_border_points(segments, cmask, slines, window = 50):
    height = segments.GetLength(0)
    width  = segments.GetLength(1)

    (counts,xmins,xmaxs,ymins, ymaxs, meansx, meansy) \
        = calculate_segment_parameters(segments, cmask)

    spoints = {}

    for seg in slines.keys():
        sline = slines[seg]

        n = len(sline)
#	(my,mx) = (0,0)
        (my,mx) = (meansy[seg], meansx[seg])
#	(my,mx) = (int(height/2), int(width/2))
	
        points = []

        for c in range(0, n):
            (py,px) = sline[c]
            r = distance(mx, my, px, py)

            rminleft  = None
            rmaxleft  = None
            rminright = None
            rmaxright = None

            # dla lewego otoczenia

            for j in range(-window, 0):
                (oy,ox) = sline[(c+j)%n]
                ro = distance(mx, my, ox, oy)
                rminleft = ro if rminleft == None else min(rminleft, ro)
                rmaxleft = ro if rmaxleft == None else min(rmaxleft, ro)

            # dla prawego otoczenia

            for j in range(1, window+1):
                (oy,ox) = sline[(c+j)%n]
                ro = distance(mx, my, ox, oy)
                rminright = ro if rminright == None else min(rminright, ro)
                rmaxright = ro if rmaxright == None else min(rmaxright, ro)

            # minimu jeśli

            if rminleft >= r and r <= rminright:
                # minimum
                points.append(sline[c])
            elif rmaxleft <= r and r >= rmaxright:
                # maximum
                points.append(sline[c])

            spoints[seg] = points

    return spoints

def p2p_distance(ax, ay, bx, by):
    dx = ax-bx
    dy = ay-by
    return Sqrt(dx*dx+dy*dy)

def p2l_distance(ax, ay, bx, by, cx, cy):
    # c to punkt
    # a, b linia
    r1   = p2p_distance(ax, ay, cx, cy)
    r2   = p2p_distance(bx, by, cx, cy)
    d    = p2p_distance(ax, ay, bx, by)
    s    = (r1*r1 - r2*r2 + d*d) / (2*d)
    dist = Sqrt(r1*r1-s*s)
    return dist

def contours_simplifications(slines, eps):
    slines2 = {}
    for (seg,sline) in slines.items():
        n = len(sline)
        # głupie x^2 , ale nie zostawia wrednych durnych punktów
        # FIXME wydajność
        mDist = -1
        mi = -1
        mj = -1
        for i in range(0, n):
            for j in range(0, i):
                (p1y, p1x) = sline[i]
                (p2y, p2x) = sline[j]
                d = p2p_distance(p1x, p1y, p2x, p2y)
                if d > mDist:
                    mDist = d
                    mi = i
                    mj = j
        l1 = sline[j:i+1]
        l2 = sline[i:n] + sline[0:j+1]
        c1 = curve_simplification(l1, eps)
        c2 = curve_simplification(l2, eps)
        nc1 = len(c1)
        c = c1[0:(nc1-1)]+c2
        slines2[seg] = c
    return slines2

def curve_simplification(sline, eps):
    n  = len(sline)
    if n < 2:
        return sline

    p1 = (p1y,p1x) = sline[0]
    p2 = (p2y,p2x) = sline[n-1]

    dMaxIdx = -1
    dMax = -1.0

    for i in range(1, n-1):
        (cy, cx) = sline[i]
        d = p2l_distance(p1x, p1y, p2x,p2y, cx,cy)
        if d > dMax:
            dMax = d
            dMaxIdx = i

    if dMax > eps:
        c1 = curve_simplification(sline[0:dMaxIdx+1],eps)
        c2 = curve_simplification(sline[dMaxIdx:n],eps)

        nc1 = len(c1)
        nc2 = len(c2)
        return c1[0:(nc1-1)] + c2
    else:
        return [p1,p2]



