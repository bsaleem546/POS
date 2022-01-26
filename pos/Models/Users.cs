using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pos.Models
{
    class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string contact { get; set; }
        public string role { get; set; }
        public bool status { get; set; }

        public static string NAME = "";
        public static string USERNAME = "";
        public static string ROLE = "";
    }
}
