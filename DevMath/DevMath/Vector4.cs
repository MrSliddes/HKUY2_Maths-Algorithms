using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public struct Vector4
    {
        //https://docs.unity3d.com/ScriptReference/Vector4.html
        // DLL https://purplelilgirl.com/2015/01/07/how-to-create-dlls-for-unity/

        public float x;
        public float y;
        public float z;
        public float w; // W is dus voor scaling "the W component is a factor which divides the other vector components"

        // Dus vector 3 met nu nog w erbij

        public float Magnitude
        {
            get { return (float)Math.Sqrt(x * x + y * y + z * z + w * w); }
        }

        public Vector4 Normalized
        {
            get { return this / Magnitude; }
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static implicit operator Vector4(Vector3 v)
        {
            // convert naar v4
            return new Vector4(v.x, v.y, v.z, 0f);
        }

        public static float Dot(Vector4 lhs, Vector4 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            return a + (b - a) * t;
        }
        
        // waarom is er trouwens geen == operator?

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.z);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(-v.x, -v.y, -v.z, -v.w);
        }

        public static Vector4 operator *(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.x * scalar, lhs.y * scalar, lhs.z * scalar, lhs.w * scalar);
        }

        public static Vector4 operator /(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.x / scalar, lhs.y / scalar, lhs.z / scalar, lhs.w / scalar);
        }
    }
}
