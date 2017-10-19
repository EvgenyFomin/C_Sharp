using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework1
{
    public struct Matrix3x3
    {
        public double[,] matrix;

        public double M00
        {
            get
            {
                return matrix[0, 0];
            }
        }

        public double M01
        {
            get
            {
                return matrix[0, 1];
            }
        }

        public double M02
        {
            get
            {
                return matrix[0, 2];
            }
        }

        public double M10
        {
            get
            {
                return matrix[1, 0];
            }
        }

        public double M11
        {
            get
            {
                return matrix[1, 1];
            }
        }

        public double M12
        {
            get
            {
                return matrix[1, 2];
            }
        }

        public double M20
        {
            get
            {
                return matrix[2, 0];
            }
        }

        public double M21
        {
            get
            {
                return matrix[2, 1];
            }
        }

        public double M22
        {
            get
            {
                return matrix[2, 2];
            }
        }



        public Matrix3x3(double defaultValue)
        {
            matrix = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = defaultValue;
                }
            }
        }

        public Matrix3x3(double[,] matrix)
        {
            this.matrix = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right)
        {
            Matrix3x3 result = new Matrix3x3(left.matrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] += right.matrix[i, j];
                }
            }
            return result;
        }

        public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right)
        {
            Matrix3x3 result = new Matrix3x3(left.matrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] -= right.matrix[i, j];
                }
            }
            return result;
        }

        public static Matrix3x3 operator *(Matrix3x3 left, double right)
        {
            Matrix3x3 result = new Matrix3x3(left.matrix);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] *= right;
                }
            }
            return result;
        }

        public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
        {
            Matrix3x3 result = new Matrix3x3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        result.matrix[i, j] += left.matrix[i, k] * left.matrix[k, j];
                    }
                }
            }
            return result;
        }

        public static CoVector3 operator *(Matrix3x3 left, CoVector3 right)
        {
            CoVector3 result = new CoVector3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.vector[i] += left.matrix[i, j] * right.vector[j];
                }
            }
            return result;
        }

        public static Vector3 operator *(Vector3 left, Matrix3x3 right)
        {
            Vector3 result = new Vector3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.vector[i] += left.vector[j] * right.matrix[j, i];
                }
            }
            return result;
        }

        public double Trace()
        {
            double result = 0;
            for (int i = 0; i < 3; i++)
            {
                result += matrix[i, i];
            }
            return result;
        }

        public double Determinant()
        {
            double result = 0;
            for (int i = 0; i < 3; i++)
            {
                result += matrix[i, 0] * matrix[(i + 1) % 3, 1] * matrix[(i + 2) % 3, 2];
                result -= matrix[i, 0] * matrix[(i + 2) % 3, 1] * matrix[(i + 1) % 3, 2];
            }
            return result;
        }

        public Matrix3x3 Transpone()
        {
            Matrix3x3 result = new Matrix3x3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] = matrix[j, i];
                }
            }
            return result;
        }

        public Matrix3x3 Symmetric()
        {
            Matrix3x3 result = new Matrix3x3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] = (matrix[j, i] + matrix[i, j]) / 2;
                }
            }
            return result;
        }

        public Matrix3x3 Asymmetric()
        {
            Matrix3x3 result = new Matrix3x3(0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result.matrix[i, j] = (matrix[i, j] - matrix[j, i]) / 2;
                }
            }
            return result;
        }

  

        public void Print()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(" ");
                }

                Console.Write("\n");
            }
        }

    }


    public struct Vector3
    {
        public double[] vector;

        public double V0
        {
            get
            {
                return vector[0];
            }
        }

        public double V1
        {
            get
            {
                return vector[1];
            }
        }

        public double V2
        {
            get
            {
                return vector[2];
            }
        }

        public Vector3(double defaultValue)
        {
            vector = new double[3];
            for (int i = 0; i < 3; i++)
            {
                vector[i] = defaultValue;
            }
        }

        public Vector3(double[] array)
        {
            vector = new double[3];
            for (int i = 0; i < 3; i++)
            {
                vector[i] = array[i];
            }
        }

        public Vector3(CoVector3 coVector)
        {
            vector = coVector.vector;
        }


        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            Vector3 result = new Vector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] += right.vector[i];
            }
            return result;
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            Vector3 result = new Vector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] -= right.vector[i];
            }
            return result;
        }

        public static Vector3 CrossProduct(Vector3 left, Vector3 right)
        {
            Vector3 result = new Vector3(0);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] = left.vector[(i + 1) % 3] * right.vector[(i + 2) % 3] - left.vector[(i + 2) % 3] * right.vector[(i + 1) % 3];
            }
            return result;
        }

        public static Vector3 operator *(Vector3 left, double right)
        {
            Vector3 result = new Vector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] *= right;
            }
            return result;
        }

        public double Norm()
        {
            double result = 0;
            for (int i = 0; i < 3; i++)
            {
                result += vector[i] * vector[i];
            }
            return Math.Sqrt(result);
        }
    }
    

    public struct CoVector3
    {
        public double[] vector;

        public double V0
        {
            get
            {
                return vector[0];
            }
        }

        public double V1
        {
            get
            {
                return vector[1];
            }
        }

        public double V2
        {
            get
            {
                return vector[2];
            }
        }

        public CoVector3(double defaultValue)
        {
            vector = new double[3];
            for (int i = 0; i < 3; i++)
            {
                vector[i] = defaultValue;
            }
        }

        public CoVector3(double[] array)
        {
            vector = new double[3];
            for (int i = 0; i < 3; i++)
            {
                vector[i] = array[i];
            }
        }

        public CoVector3(CoVector3 coVector)
        {
            vector = coVector.vector;
        }


        public static CoVector3 operator +(CoVector3 left, CoVector3 right)
        {
            CoVector3 result = new CoVector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] += right.vector[i];
            }
            return result;
        }

        public static CoVector3 operator -(CoVector3 left, CoVector3 right)
        {
            CoVector3 result = new CoVector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] -= right.vector[i];
            }
            return result;
        }

        public static CoVector3 CrossProduct(CoVector3 left, CoVector3 right)
        {
            CoVector3 result = new CoVector3(0);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] = left.vector[(i + 1) % 3] * right.vector[(i + 2) % 3] - left.vector[(i + 2) % 3] * right.vector[(i + 1) % 3];
            }
            return result;
        }

        public static CoVector3 operator *(CoVector3 left, double right)
        {
            CoVector3 result = new CoVector3(left.vector);
            for (int i = 0; i < 3; i++)
            {
                result.vector[i] *= right;
            }
            return result;
        }

        public double Norm()
        {
            double result = 0;
            for (int i = 0; i < 3; i++)
            {
                result += vector[i] * vector[i];
            }
            return Math.Sqrt(result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            double[,] a = { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } };
            Matrix3x3 m = new Matrix3x3(a);
            (m * m).Print();
            Console.ReadKey();
        }
    }
}
