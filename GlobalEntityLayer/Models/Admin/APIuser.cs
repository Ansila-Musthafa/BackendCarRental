using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEntityLayer.Models.Admin
{
    public class APIuser : IdentityUser
    {
       public string FirstName { get; set; }    
       public string LastName { get; set; } 
    }
}
