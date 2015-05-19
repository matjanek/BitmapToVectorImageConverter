import cv

imcolor = cv.LoadImage('stairs.jpg')
image = cv.LoadImage('stairs.jpg',cv.CV_LOAD_IMAGE_GRAYSCALE)
cornerMap = cv.CreateMat(image.height, image.width, cv.CV_32FC1)
# OpenCV corner detection
cv.CornerHarris(image,cornerMap,3)

for y in range(0, image.height):
 for x in range(0, image.width):
  harris = cv.Get2D(cornerMap, y, x) # get the x,y value
  # check the corner detector response
  if harris[0] > 10e-06:
   # draw a small circle on the original image
   cv.Circle(imcolor,(x,y),2,cv.RGB(155, 0, 25))

cv.NamedWindow('Harris', cv.CV_WINDOW_AUTOSIZE)
cv.ShowImage('Harris', imcolor) # show the image
cv.SaveImage('harris.jpg', imcolor)
cv.WaitKey()
