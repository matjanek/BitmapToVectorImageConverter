# -*- coding: UTF-8 -*-

from System import *

class Shape:
	pass

class Point:
	def __init__(self,x,y):
		self.x = x
		self.y = y

class Polygon(Shape):
	def __init__(self, vertices):
		self.vertices = vertices

class Circle(Shape):
	def __init__(self, middle, radius):
		self.middle = middle
		self.radius = radius

class Line(Shape):
	def __init__(self, a, b):
		self.a = a
		self.b = b

class Bezier(Shape):
	def __init__(self, vertices):
		self.vertices = vertices

