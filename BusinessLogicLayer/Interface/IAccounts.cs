using GlobalEntityLayer.Models.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interface
{
    public interface IAccounts
    {
        Task<IdentityResult> SignUpAsync(RegisterModel registerModel);
        Task<string> LoginAsync(LoginModel loginModel); 
    }
}
