using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Matrix4x4
    {
        // Hier word een dubble array aangemaakt als matrix, dus gwn een 4x4
        public float[][] m = new float[4][] { new float[4], new float[4], new float[4], new float[4] };

        // Hiermee maak je dan een nieuwe matrix
        public Matrix4x4(float[][] matrix)
        {
            // Assign hier dan de local m (matrix)
            m = matrix;
        }

        // Krijg de identity van de matrix (zelfde als een Quaternioin.Identity)
        public static Matrix4x4 Identity
        {
            get {
                return new Matrix4x4(
                     new float[4][] 
                     {
                        new float[4] { 1, 0, 0, 0 },
                        new float[4] { 0, 1, 0, 0 },
                        new float[4] { 0, 0, 1, 0 },
                        new float[4] { 0, 0, 0, 1 }
                     }
                    );
            }
        }

        // Dit was te veel werk?
        public float Determinant
        {
            get { throw new NotImplementedException(); }
        }

        public Matrix4x4 Inverse
        {
            //https://www.youtube.com/watch?v=nNOz6Ez8Fn4 ????
            get { throw new NotImplementedException(); }
        }

        // Nieuwe matrix met vec3 translation, zelfde als identity maar laatste van array is vec3xyz en dan 1
        public static Matrix4x4 Translate(Vector3 translation)
        {
            return new Matrix4x4(new float[4][] { new float[4] { 1, 0, 0, translation.x }, new float[4] { 0, 1, 0, translation.y }, new float[4] { 0, 0, 1, translation.z }, new float[4] { 0, 0, 0, 1 } });
        }

        // Hiervoor kun gwn de Rotxyz gebruiken die je hieronder al uitrekend?
        public static Matrix4x4 Rotate(Vector3 rotation)
        {
            //Er zijn 2 manieren om deze te berekenen
            return RotateX(rotation.x) * RotateY(rotation.y) * RotateZ(rotation.z); // Volgorde maakt niet uit? (Qaternion bullshit) aangezien het allemaal bij elkaar wordt vermedigvuldigd
        }

        // !!!EERST cos dan sin dan sin dan cos!!! praise wiki https://en.wikipedia.org/wiki/Rotation_matrix
        public static Matrix4x4 RotateX(float rotation)
        {
            return new Matrix4x4(new float[4][]
                {
                    new float[4] { 1, 0, 0, 0 },
                    new float[4] { 0, (float)Math.Cos(rotation), (float)-Math.Sin(rotation), 0 },
                    new float[4] { 0, (float)Math.Sin(rotation), (float)Math.Cos(rotation), 0 },
                    new float[4] { 0, 0, 0, 1 }
                });
        }

        // Hier zelfde bs maar dan 1e ipv 2e (dus 1 omhoog, en naar links!!), vergeet dan niet om 2e float te veranderen -_-
        public static Matrix4x4 RotateY(float rotation)
        {
            return new Matrix4x4(new float[4][]
                {
                    new float[4] { (float)Math.Cos(rotation), 0, (float)-Math.Sin(rotation), 0 },
                    new float[4] { 0, 1, 0, 0 },
                    new float[4] { (float)Math.Sin(rotation), 0, (float)Math.Cos(rotation), 0 },
                    new float[4] { 0, 0, 0, 1 }
                });
        }

        public static Matrix4x4 RotateZ(float rotation)
        {
            return new Matrix4x4(new float[4][]
                {
                    new float[4] { (float)Math.Cos(rotation), (float)-Math.Sin(rotation), 0, 0 },
                    new float[4] { (float)Math.Sin(rotation), (float)Math.Cos(rotation), 0, 0 },
                    new float[4] { 0, 0, 1, 0 },
                    new float[4] { 0, 0, 0, 1 }
                });
        }

        // Replace de 1 met xyz?
        public static Matrix4x4 Scale(Vector3 scale)
        {
            return new Matrix4x4(new float[4][]
                {
                    new float[4] { scale.x, 0, 0, 0 },
                    new float[4] { 0, scale.y, 0, 0 },
                    new float[4] { 0, 0, scale.z, 0 },
                    new float[4] { 0, 0, 0, 1 }
                });
        }

        // lhs * rhs en dan voor elke rij (0-3)
        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return new Matrix4x4
                (
                    new float[4][]
                    {
                        multiplyMatrixRow(lhs, rhs, 0),
                        multiplyMatrixRow(lhs, rhs, 1),
                        multiplyMatrixRow(lhs, rhs, 2),
                        multiplyMatrixRow(lhs, rhs, 3)
                    }
                );
        }

        // Row * 0 + row * 1 + row * 2 + row * 3
        private static float[] multiplyMatrixRow(Matrix4x4 l, Matrix4x4 r, int row)
        {
            return new float[4]
            {
                l.m[row][0] * r.m[0][0] + l.m[row][1] * r.m[1][0] + l.m[row][2] * r.m[2][0] + l.m[row][3] * r.m[3][0],
                l.m[row][0] * r.m[0][1] + l.m[row][1] * r.m[1][1] + l.m[row][2] * r.m[2][1] + l.m[row][3] * r.m[3][1],
                l.m[row][0] * r.m[0][2] + l.m[row][1] * r.m[1][2] + l.m[row][2] * r.m[2][2] + l.m[row][3] * r.m[3][2],
                l.m[row][0] * r.m[0][3] + l.m[row][1] * r.m[1][3] + l.m[row][2] * r.m[2][3] + l.m[row][3] * r.m[3][3]
            };
        }

        //https://gamedev.stackexchange.com/questions/136573/multiply-matrix4x4-with-vec4, still confused
        public static Vector4 operator *(Matrix4x4 lhs, Vector4 rhs)
        {
            /*
            // moet van vec4 naar matrix naar vec4, dit werkt niet
            float[] m = new float[4]
                {
                    multiplyMatrixRow(lhs, rhs.x, 0),
                    multiplyMatrixRow(lhs, rhs.y, 1),
                    multiplyMatrixRow(lhs, rhs.z, 2),
                    multiplyMatrixRow(lhs, rhs.w, 3)
                };

            return new Vector4(m[0], m[1], m[2], m[3]);*/
            throw new NotImplementedException();
        }
    }
}

// https://www.youtube.com/watch?v=9LYVi-n-6Jw
// https://www.youtube.com/watch?v=dQw4w9WgXcQ deze maakte veel duidelijk