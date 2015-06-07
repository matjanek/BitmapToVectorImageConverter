import os
for f in ['adidas', 'bakoma', 'bp', 'cube', 'dilbert', 'godlo', 'ksztalty', 'landscape16', 'nike', 'prostokaty', 'signs', 'teamliquid', 'text', 'xkcd']:
    os.system("C:\python32\python .\\BitmapToVectorImageConverter\\algorithms\\result-stats.py own-{}.svg 2>>dane.txt".format(f,f))