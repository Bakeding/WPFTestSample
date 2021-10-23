using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChart
{
    public class ModelEvents
    {
        public List<object> EventList { get; private set; }
        public ModelEvents(List<object> objects)
        {
            this.EventList = objects;
        }
    }
}
