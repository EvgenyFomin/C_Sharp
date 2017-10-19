using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework2
{
    class Program
    {   
        static void Main(string[] args)
        {
            Set<int> set1 = new Set<int>(Comparer<int>.Default);
            Set<int> set2 = new Set<int>(Comparer<int>.Default);
            for (int i = 0; i < 20; i++)
            {
                set1.Add(i);
            }
            for (int i = 10; i < 30; i++)
            {
                set2.Add(i);
            }
            Set<int> set3 = set1 * set2;
            for (int i = 0; i < 100; i += 3)
            {
                set3.Remove(i);
            }
            set3.print();
            Console.ReadKey();

        }
    }
}
