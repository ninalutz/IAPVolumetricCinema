using System.Collections;
using UnityEngine;

public static class ColorExtension {
    public static int toInt(this Color col)
    {
        Color32 A = col;
        return (A.r << 16) | (A.g << 8) | A.b;
    }

    public static int toInt(this Color32 col)
    {
        return (col.r << 16) | (col.g << 8) | col.b;
    }
}
