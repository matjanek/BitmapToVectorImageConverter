import numpy as np
import queue
from math import sqrt
from vectorizer.utils.distances import  distance
from collections import deque


def flood_fill(px, py, segments, border):
    s = segments.shape
    seg = segments[py,px]
    visited = np.zeros(s, dtype=bool)
    queued  = np.zeros(s, dtype=bool)
    blockSize = 3

    # ale aktualnie brak pomysłu jak inaczej to zrobić

    # przerywamy cykl w jednym punkcie i przechodzimy od punktu blokady,
    # po powstałej z cyklu ścieżce

    cont = []

    for i in range(-blockSize,blockSize+1):
        for j in range(-blockSize,blockSize+1):
            y = (py+i)%s[0]
            x = (px+j)%s[1]
            if segments[y,x] == seg and border[y,x]:
                visited[y,x] = True
                if (y != py) or (x != px):
                    cont.append((y,x))

    if len(cont) <= 0:
        return []

    # wygląda to na niepawidłowe dane

    start = cont.pop()

    q = queue.Queue()
    q.put(start)
    path = []

    while not(q.empty()):
        (qy,qx) = q.get()
        path.append((qy,qx))
        visited[qy,qx] = True
        queued[qy,qx] = False

        for i in range(-1,2):
            for j in range(-1,2):
                y = (qy+i)%s[0]
                x = (qx+j)%s[1]
                if border[y,x] and segments[y,x] == seg and not(visited[y,x]) and not(queued[y,x]):
                    queued[y,x] = True
                    q.put((y,x))

    for i in range(-blockSize,blockSize+1):
        for j in range(-blockSize,blockSize+1):
            y = (py+i)%s[0]
            x = (px+j)%s[1]
            if segments[y,x] == seg and border[y,x]:
                visited[y,x] = False

    if len(path) > 0:
        q.put(path[len(path)-1])
    elif len(cont) > 0:
        q.put(cont[len(cont)-1])
    else:
        q.put(start)

    while not(q.empty()):
        (qy,qx) = q.get()
        path.append((qy,qx))
        visited[qy,qx] = True
        queued[qy,qx] = False

        for i in range(-1,2):
            for j in range(-1,2):
                y = (qy+i)%s[0]
                x = (qx+j)%s[1]
                if border[y,x] and segments[y,x] == seg and not(visited[y,x]) and not(queued[y,x]):
                    queued[y,x] = True
                    q.put((y,x))

    return path

def th(x):
    return x[2]

def reconstruct_path(x,y,parent):
    path = []
    while x >= 0 and y >= 0:
        path.append((y,x))
        (y,x) = parent[y,x]
    return path

def border_path(px, py, segments, border):
    sx = px
    sy = py
    s = segments.shape
    seg = segments[py,px]
    visited = np.zeros(s, dtype=bool)
    queued  = np.zeros(s, dtype=bool)
    lengths = np.zeros(s, dtype=int)
    parent  = np.zeros((s[0], s[1], 2), dtype=int)

    for i in range(0, s[0]):
        for j in range(0, s[1]):
            parent[i,j] = np.array([-1,-1])

    q = deque()
    q.append((py,px))

    c = 1

    while c > 0:
        (qy,qx) = q.popleft()
        visited[qy,qx] = True
        queued[qy,qx] = False
        c -= 1

        for i in range(-1,2):
            for j in range(-1,2):
                y = (qy+i)%s[0]
                x = (qx+j)%s[1]

                if segments[y,x] == seg and border[y,x] and not(visited[y,x]) and not(queued[y,x]):
#                    print ("Dodano: ({},{})".format(x,y))
                    q.append((y,x))
                    c += 1
#                    print("Liczba elementow: {}".format(c))
                    queued[y,x] = True
                    parent[y,x] = np.array([qy,qx])
                    lengths[y,x] = lengths[qy,qx] + 1

    was_parent = np.zeros(s, dtype=bool)

    for i in range(0, s[0]):
        for j in range(0, s[1]):
            if visited[i,j]:
                (py,px) = parent[i,j]
#                 print("Rodzic: ({},{}) pktu: ({},{})".format(px,py,j,i))
                was_parent[py,px] = True

    path_endings = []

    for i in range(0, s[0]):
        for j in range(0, s[1]):
            if visited[i,j] and not(was_parent[i,j]):
                path_endings.append((i,j,lengths[i,j]))

    print("Start: ({},{})".format(sx,sy))
    print("Path endings: {}".format(path_endings))

    # mamy zakończenia
    # bierzemy 2 najdłuższe ścieżki i jeśli możemy, a powinniśmy
    # tzn. są siadami to łączymy w jeden cykl

    size = -1
    mp1 = (-1,-1)
    mp2 = (-1,-1)

    pendings = []
    mapping = {}

    # ta część jest jedna z wolniejszych pewnie, albo zmniejsz liczbę segmentów
    # albo zmniejsz liczbę kandydatów (x największych ...)

    counter = 0
    cMax = 4

    for (ey,ex,el) in sorted(path_endings,key=th,reverse=True):
        if counter >= cMax:
            break

        l = reconstruct_path(ex,ey, parent)
        s = set(l)
        pendings.append(((ey,ex),s))
        mapping[(ey,ex)] = l
        counter += 1

    for (p1,s1) in pendings:
        for (p2,s2) in pendings:
            if len(s1) + len(s2) <= size: # nie ma sensu sprawdzać
                continue

            s = s1.union(s2)
            n = len(s)

            if n > size:
                mp1 = p1
                mp2 = p2
                size = n

    (p1y,p1x) = mp1
    (p2y,p2x) = mp2

    l1 = mapping[mp1]
    l2 = mapping[mp2]

    if distance(mp1,mp2) < distance(mp1,(sy,sx)):
        l1.reverse()

    print ("p1: {}, p2: {}".format(mp1,mp2))

    return (l1 + l2)

def convert_mask_to_border_paths(segments, border):
    n = np.max(segments)
    visited = np.zeros(n+1, dtype=bool)
    s = segments.shape
    paths = {}
    for i in range(0, s[0]):
        for j in range(0, s[1]):
            seg = segments[i,j]
            if border[i,j] and not(visited[seg]):
                visited[seg] = True
#                border_path = flood_fill(j,i, segments, border)
                b = border_path(j,i, segments, border)
                paths[seg] = b

    return paths


def border_points(bpath,my,mx):
    eps = 0.001
    n = len(bpath)
    points = []
    m = (my,mx)
    window = 5

    for i in range(0, n):
        p = bpath[i]
        r = distance(p,m)
        rmin = r
        rmax = r
        for j in range(-window,window+1):
            q = bpath[(i+j)%n]
            rq = distance(q,m)
            rmin = min(rmin, rq)
            rmax = max(rmax, rq)

        if abs(rmin-r) < eps:
            points.append(p)
        elif abs(rmax-r) < eps:
            points.append(p)


    return points




    return points
