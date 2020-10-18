using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    public static Vector2 ToUnity(this DevMath.Vector2 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static DevMath.Vector2 ToDevMath(this Vector2 v)
    {
        return new DevMath.Vector2(v.x, v.y);
    }

    public static Matrix4x4 ToUnity(this DevMath.Matrix4x4 m)
    {
        //throw new NotImplementedException(); // return new Matrix4x4
        return new Matrix4x4
            (
            new Vector4(m.m[0][0], m.m[0][1], m.m[0][2], m.m[0][3]),
            new Vector4(m.m[1][0], m.m[1][1], m.m[1][2], m.m[1][3]),
            new Vector4(m.m[2][0], m.m[2][1], m.m[2][2], m.m[2][3]),
            new Vector4(m.m[3][0], m.m[3][1], m.m[3][2], m.m[3][3])
            );
    }

    public static DevMath.Matrix4x4 ToDevMath(this Matrix4x4 m)
    {
        return new DevMath.Matrix4x4
            (
                new float[][]
                {
                    new float[] {m.m00, m.m01, m.m02, m.m03},
                    new float[] {m.m10, m.m11, m.m12, m.m13},
                    new float[] {m.m20, m.m21, m.m22, m.m23},
                    new float[] {m.m30, m.m31, m.m32, m.m33}
                }
            );
    }
}
