import sys

import clr
clr.AddReference("System.Drawing")

from System import *
from System.Drawing import *
from System.Drawing.Imaging import *

from utils import *
res = shoeShape(Bitmap("image2.jpg"))
res.Save("test.png")
