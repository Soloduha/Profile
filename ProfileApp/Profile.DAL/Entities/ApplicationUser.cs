using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.DAL.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Surname { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }
    }
}
