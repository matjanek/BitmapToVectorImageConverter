from System import *

def and_mask(m1, m2):
    if (m1.GetLength(0) != m2.GetLength(0)):
        raise "Heights of two mask does not equal"

    if (m1.GetLength(1) != m2.GetLength(1)):
        raise "Widths of two mask does not equal"

    height = m1.GetLength(0)
    width  = m1.GetLength(1)
    m = Array.CreateInstance(bool, height, width)
    for i in range(0, height):
        for j in range(0, width):
            m[i,j] = m1[i,j] and m2[i,j]

    return m

def or_mask(m1,m2):
    if (m1.GetLength(0) != m2.GetLength(0)):
        raise "Heights of two mask does not equal"

    if (m1.GetLength(1) != m2.GetLength(1)):
        raise "Widths of two mask does not equal"

    m = Array.CreateInstance(bool, height, width)
    height = m1.GetLength(0)
    width  = m1.GetLength(1)
    for i in range(0, height):
        for j in range(0, width):
            m[i,j] = m1[i,j] or m2[i,j]

    return m

def not_mask(m):
    height = m.GetLength(0)
    width  = m.GetLength(1)

    n = Array.CreateInstance(bool, height, width)
    for i in range(0, height):
        for j in range(0, width):
            n[i,j] = not(m[i,j])

    return m
