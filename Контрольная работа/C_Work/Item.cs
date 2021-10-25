using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Work
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string code { get; set; }
        public string info { get; set; }
    }

    public class Disk : Item
    {
        public int id { get; set; }
        public string type { get; set; }
        public string inc { get; set; }

    }

    public class Book : Item
    {
        public int id { get; set; }
        public string autor { get; set; }
        public int pages { get; set; }

    }
}
