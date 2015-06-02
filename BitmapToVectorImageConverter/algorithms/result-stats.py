#!/usr/bin/python3

from lxml import etree
import sys
import os

argv = sys.argv
argc = len(argv)

if argc != 2:
    print ("Podano za mało argumentów: {}".format(argc))
    sys.exit(1)

path = argv[1]
ns = "{http://www.w3.org/2000/svg}"
root = etree.parse(path)
exp = "{}g/{}polygon".format(ns,ns)
elements = root.findall(exp)

idx = 0
print ("\"id of poly\",\"count of points\",\"style\"")
for poly in elements:
    idx += 1
    points = poly.attrib['points']
    print("{},{},{}".format(idx, len(points) // 2, poly.attrib['style']))

stats = os.stat(path)
fsize = stats.st_size

print ("file size of {} is {} ({:.2f} KB, {:.2f} MB)"
        .format(path, fsize,fsize/1024, fsize/1024/1024), file=sys.stderr)
