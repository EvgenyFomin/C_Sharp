using System;
using System.Reflection.Emit;

namespace EmitCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator g = new Generator("(a + b) * c + d");
            Func<int,int,int,int,int>
                f0 = g.GenerateFunc<int>();
            Func<double,double,double,double,double>
                f1 = g.GenerateFunc<double>();
            Console.WriteLine(f0(2, 3, 4, 5));
            Console.WriteLine(f1(2.1, 3.2, 4.3, 5.4));
        }
    }

    class Generator
    {
        private string s;
        private int pos;
        private char c;
        private bool end;
        private ILGenerator il;
        private DynamicMethod mth;
        private char[] lvSign = new char[] { '*', '+' };
        private OpCode[] lvCode = new OpCode[] { OpCodes.Mul, OpCodes.Add };

        public Generator(string expression)
        {
            s = expression;
        }

        public Func<T,T,T,T,T> GenerateFunc<T>()
        {
            GenerateMethod(typeof(T));
            Delegate res = mth.CreateDelegate(typeof(Func<T,T,T,T,T>));
            return (Func<T,T,T,T,T>)res;
        }

        private void GenerateMethod(Type t)
        {
            Type[] methodArgs = new Type[4];
            for (int i = 0; i < 4; i++)
                methodArgs[i] = t;
            mth = new DynamicMethod("f", t, methodArgs,
                typeof(Generator).Module );

            il = mth.GetILGenerator();
            pos = 0;
            end = false;
            Next();
            GenerateExpression();
            il.Emit(OpCodes.Ret);
        }

        private void Next()
        {
            while (pos < s.Length)
            {
                c = s[pos++];
                if (c != ' ')
                    break;
            }
            if (pos == s.Length)
                end = true;
        }

        private void GenerateExpression(int lv = 1)
        {
            if (lv < 0)
            {
                if (c == '(')
                {
                    Next();
                    GenerateExpression();
                }
                else /* if (c >= 'a' && c <= 'd') */
                    il.Emit(OpCodes.Ldarg, c - 'a');
                Next();
                return;
            }
            GenerateExpression(lv - 1);
            while (!end && c == lvSign[lv])
            {
                Next();
                GenerateExpression(lv - 1);
                il.Emit(lvCode[lv]);
            }
        }
    }
}
