using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVM.Models
{
    public enum sexual_enum
    {
        BOY,
        GIRL
    }
    class People
    {
        
        public string name{ get; set; }
        public string age { get; set; }
        public sexual_enum sexual { get; set; }
    }
}
