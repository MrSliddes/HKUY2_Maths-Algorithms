using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public struct Vector3
    {
        // Vector2 alleen dan dus met een z erbij

        public float x;
        public float y;
        public float z;

        public float Magnitude
        {
            // + z*z
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        public Vector3 Normalized
        {
            get
            {
                int nX; if(x != 0) nX = x / Math.Abs(x);
                int nY; if(y != 0) nY = y / Math.Abs(y);
                int nZ; if(z != 0) nZ = z / Math.Abs(z);
                return new Vector3(nX, nY, nZ);
            }
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(Vector2 v)
        {
            // Van vector2 naar vector3
            return new Vector3(v.x, v.y, 0f);
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z);
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            // Wat vincent dus had gedaan op zn white board
            // y * z - z * y (<- vandaar de cross) z * x - x * z, x * y - y * x
            // lyrz lzry | lzrx lxrz | lxry lyrx, elke xyz komt dus 2x voor
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            // Zelfde als v2
            return a + (b - a) * t;
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            // Zelfde als v2 met z erbij
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static Vector3 operator *(Vector3 lhs, float scalar)
        {
            return new Vector3(lhs.x * scalar, lhs.y * scalar, lhs.z * scalar); // Dus dit zorgt ervoor dat je Vector.one * 3 kunt doen?
        }

        public static Vector3 operator /(Vector3 lhs, float scalar)
        {
            return new Vector3(lhs.x / scalar, lhs.y / scalar, lhs.z / scalar);
        }
    }
}
