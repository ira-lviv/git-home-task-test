using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {

            MyList<int> list = new MyList<int>();

            list.Add(1);
            list.Add(5);
            list.Add(17);
            list.Add(42);
            list.Add(-69);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            list.Delete(42);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        
    }

    internal class MyList<T> : IEnumerable<T>
    {
        private Item<T> _head = null;
        private Item<T> _tail = null;
        private int _count = 0;
        public int Count()
        {
            return _count;
        }

        public void Add(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            Item<T> item = new Item<T>(data);
            if (_head == null)
                _head = item;
            else _tail.Next = item;
            _tail = item;
            _count++;
        }
        public void Delete(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Item<T> current = _head;
            Item<T> previous = null;
            while (current != null)
            {
                if(current.Data.Equals(data))
                {
                    if(previous!=null)
                    {
                        previous.Next = current.Next;
                        if(current.Next==null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        _head = _head.Next;
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }
                    _count--;
                    break;
                }

                previous = current;
                current = current.Next;
            }
        }
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            Item<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
