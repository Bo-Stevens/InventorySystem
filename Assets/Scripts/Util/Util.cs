using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    public static Vector3 SnapToNearestPixel(Vector3 position, float pixelsPerUnit)
    {
        Vector3 positionInPixels = new Vector3(
            Mathf.RoundToInt(position.x * pixelsPerUnit),
            Mathf.RoundToInt(position.y * pixelsPerUnit),
            Mathf.RoundToInt(position.z * pixelsPerUnit)
            );



        return positionInPixels / pixelsPerUnit;
    }
}
