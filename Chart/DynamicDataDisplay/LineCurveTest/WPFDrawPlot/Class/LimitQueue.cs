using System;
using System.Collections.Generic;
using System.Text;

namespace WPFDrawPlot.Class
{
    class LimitQueue<T> : Queue<T> , IEnumerable<T>
    {
        private int limit;
        Queue<T> queue = new Queue<T>();
        public LimitQueue(int limit)
        {
            this.limit = limit;
        }

        public new void Enqueue(T t)
        {
            if (queue.Count > limit)
            {
                queue.Dequeue();
            }
            queue.Enqueue(t);
        }
    }
}
