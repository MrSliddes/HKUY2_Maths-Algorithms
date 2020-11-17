using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public struct Vector2
    {
        public float x;
        public float y;

        public float Magnitude
        {
            // The length of the vector is square root of (x*x+y*y). <- unity
            get { return (float)Math.Sqrt(x * x + y * y); }
        }

        public Vector2 Normalized
        {
            // Dus x = 9 word dan 1 en x = -9 is -1. Dus gwn delen door zichzelf. Let op dat je niet door 0 kan delen!
            get
            {
                int nX; if(x != 0) nX = x / Math.Abs(x);
                int nY; if(y != 0) nY = y / Math.Abs(y);
                return new Vector2(nX, nY);
            }
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            // Return de dot product
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            // Lerp functie die ik dus verkeerd heb gebruikt, en vergeet niet de haakjes
            return a + (b - a) * t;
        }

        public static float Angle(Vector2 lhs, Vector2 rhs)
        {
            // Vergeet niet dat de volgorde anders is -_-
            return Math.Atan2(rhs.y - lhs.y, rhs.x - lhs.x);
        }

        public static Vector2 DirectionFromAngle(float angle)
        {
            // Cos en sin. Hier dan dus devmath gebruiken ipv math? sinds ik de DegToRad toch al heb geschreven
            int dX = (float)Math.Cos(DevMath.DegToRad(angle));
            int dY = (float)Math.Sin(DevMath.DegToRad(angle));
            return new Vector2(dX, dY);
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            // gwn x + x en y + y. Operator = "An operator is a symbol that tells the compiler to perform specific mathematical or logical manipulations."
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y); 
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            // Zelfde alleen min
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            // return de vector negatief oftewel ~inverse -> -1 wordt dan 1
            return new Vector2(-v.x, -v.y); // ipv v.x - 1 kun je dus ook gwn er een - voor pleuren, wauw
        }

        public static Vector2 operator *(Vector2 lhs, float scalar)
        {
            // X en y * de scale doen
            return new Vector2(lhs.x * scalar, lhs.y * scalar);
        }

        public static Vector2 operator /(Vector2 lhs, float scalar)
        {
            // En hier dan delen
            return new Vector2(lhs.x / scalar, lhs.y / scalar);
        }
    }
}
