import os
# for f in ['adidas', 'bakoma', 'bp', 'cube', 'dilbert', 'godlo', 'ksztalty', 'landscape16', 'nike', 'prostokaty', 'signs', 'teamliquid', 'text', 'xkcd']:
for f in ['landscape16', 'nike', 'prostokaty', 'signs', 'teamliquid', 'text', 'xkcd']:
    os.system("ipy .\\BitmapToVectorImageConverter\\algorithms\\segmentator.py .\\test_images\\{}\\{}.png".format(f,f))
    os.system("copy save.svg own-{}.svg".format(f))