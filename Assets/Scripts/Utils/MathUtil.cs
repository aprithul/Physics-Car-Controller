using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil {


    public static float get_y_component(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b) * Mathf.Deg2Rad;
        return (a.normalized.magnitude* b.magnitude * Mathf.Cos(angle));
    }

    public static float get_x_component(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b) * Mathf.Deg2Rad;
        return (a.normalized.magnitude * b.magnitude * Mathf.Sin(angle));
    }

    public static Vector3 get_right_vector(Vector3 forward)
    {
        return new Vector3(forward.z, forward.y, -forward.x);
    }
}
