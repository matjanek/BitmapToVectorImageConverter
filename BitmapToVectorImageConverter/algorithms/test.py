import sys

import clr
clr.AddReference("System.Drawing")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *

from utils import *
from processing.contours.psweeping import *

dist = p2l_distance(0.0, 0.0, 10.0, 20.0, 5.0, 111.0)
print ("Distance: {}".format(dist))

