using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pos.Models
{
    class discount
    {
        public int id { get; set; }
        public string type { get; set; }
        public string type_name { get; set; }
        public string code { get; set; }
        public decimal amount { get; set; }
        public string cal_type { get; set; }
        public bool status { get; set; }
    }
}
