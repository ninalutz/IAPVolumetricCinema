﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


public static class Endian
{
    static int Reverse(int input)
    {
        uint x = unchecked((uint)input);
        uint y = 0;
        while (x != 0)
        {
            y <<= 1;    // Shift accumulated result left
            y |= x & 1; // Set the least significant bit if it is set in the input value
            x >>= 1;    // Shift input value right
        }
        return unchecked((int)y);
    }

    public static IEnumerable<bool> ToBinary(this byte n)
    {
        for (int i = 0; i < 8; i++)
        {
            yield return (n & (1 << i)) != 0;
        }
    }

    public static byte ToByte(this IEnumerable<bool> b)
    {
        var n = 0;
        var counter = 0;

        foreach (var i in b.Trim().Take(8))
        {
            n = n | (i ? 1 : 0) << counter;
            counter++;
        }

        return (byte)n;
    }

    private static IEnumerable<bool> Trim(this IEnumerable<bool> list)
    {
        bool trim = true;

        foreach (var i in list)
        {
            if (i)
            {
                trim = false;
            }

            if (!trim)
            {
                yield return i;
            }
        }
    }
}
