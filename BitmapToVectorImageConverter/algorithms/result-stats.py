#!/usr/bin/python3

from lxml import etree
import sys
import os
from pdb import set_trace as bp

argv = sys.argv
argc = len(argv)

if argc < 2:
    print ("Podano za mało argumentów: {}".format(argc))
    sys.exit(1)

fpath = argv[1]
# jeżeli dla autotrace robisz to musisz koniecznie dać 2. argument 0
ns_give = True if argc < 3 or argv[2] != "0" else False

ns = "{http://www.w3.org/2000/svg}" if ns_give else ""
root = etree.parse(fpath)
exp = "//{}polygon".format(ns,ns)
polygons = root.findall(exp)
exp2 = "//{}path".format(ns,ns)
paths = root.findall(exp2)


idx = 0
print ("\"id of poly\",\"count of points\",\"style\"")
for poly in polygons:
    idx += 1
    points = poly.attrib['points']
    print("{},{},{}".format(idx, len(points) // 2, poly.attrib['style']))

idx = 0
for path in paths:
    idx += 1
    points = path.attrib['d'].strip()
    points2 = points.replace("M", " ") \
                    .replace("L", " ") \
                    .replace("H", " ") \
                    .replace("V", " ") \
                    .replace("C", " ") \
                    .replace("S", " ") \
                    .replace("Q", " ") \
                    .replace("T", " ") \
                    .replace("A", " ") \
                    .replace("Z", " ") \
                    .replace(",", " ")

    splits = points2.split()
    c = len(splits)
    print("{},{},{}".format(idx, len(splits) // 2, path.attrib['style']))

stats = os.stat(fpath)
fsize = stats.st_size

print ("file size of {} is {} ({:.2f} KB, {:.2f} MB)"
        .format(fpath, fsize,fsize/1024, fsize/1024/1024), file=sys.stderr)
