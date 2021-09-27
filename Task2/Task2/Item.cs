using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
   public class Item <T>
    {
        public T Data { get; set; }

        public Item<T> Next { get; set; }

        public Item(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            Data = data;
        }
    }
}
