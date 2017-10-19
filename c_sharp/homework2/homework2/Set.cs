using System;
using System.Collections;
using System.Collections.Generic;

namespace homework2
{
    internal class Set<T> : IEnumerable<T>
    {
        private List<T> set;
        private IComparer<T> comparer;

        public Set(IComparer<T> comp)
        {
            set = new List<T>();
            comparer = comp;
        }

        public Set(IEnumerable<T> enumerable, IComparer<T> comp)
        {
            set = new List<T>();
            comparer = comp;
            foreach (T i in enumerable)
            {
                Add(i);
            }
        }

        public int Count
        {
            get
            {
                return set.Count;
            }
        }

        public bool isEmpty
        {
            get
            {
                return set.Count == 0;
            }
        }

        public bool Contains(T item)
        {
            foreach (T e in set)
            {
                if (comparer.Compare(e, item) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }
            set.Add(item);
            return true;
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }
            set.Remove(item);
            return true;
        }



        public Set<T> Where(Predicate<T> filter)
        {
            return new Set<T>(set.FindAll(filter), Comparer<T>.Create(comparer.Compare));
        }

        public IEnumerator<T> GetEnumerator()
        {
            set.Sort(comparer);
            return set.GetEnumerator();
        }




        public static Set<T> operator +(Set<T> left, Set<T> right)
        {
            Set<T> newSet = new Set<T>(Comparer<T>.Create(left.comparer.Compare));
            foreach (T i in left)
            {
                newSet.Add(i);
            }
            foreach (T i in right)
            {
                newSet.Add(i);
            }
            return newSet;
        }

        public static Set<T> operator *(Set<T> left, Set<T> right)
        {
            Set<T> newSet = new Set<T>(Comparer<T>.Create(left.comparer.Compare));
            foreach (T i in left)
            {
                if (right.Contains(i))
                {
                    newSet.Add(i);
                }
            }
            return newSet;
        }

        public static Set<T> operator -(Set<T> left, Set<T> right)
        {
            Set<T> newSet = new Set<T>(Comparer<T>.Create(left.comparer.Compare));
            foreach (T i in left)
            {
                if (!right.Contains(i))
                {
                    newSet.Add(i);
                }
            }
            return newSet;
        }

        public void print()
        {
            foreach (T item in set)
            {
                Console.WriteLine(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}