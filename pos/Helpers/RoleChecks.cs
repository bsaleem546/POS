using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pos.Helpers
{
    class RoleChecks
    {
        public Boolean isAdmin(string Role) {
            return Role == "ADMIN" ? true : false;
        }
    }
}
