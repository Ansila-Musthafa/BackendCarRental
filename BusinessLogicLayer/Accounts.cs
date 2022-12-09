using BusinessLogicLayer.Interface;
using GlobalEntityLayer.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BusinessLogicLayer
{
    public class Accounts : IAccounts
    {
        private readonly UserManager<APIuser> userManager;
        private readonly SignInManager<APIuser> signInManager;
        private readonly IConfiguration configuration;

        public Accounts(UserManager<APIuser> userManager,
            SignInManager<APIuser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(RegisterModel registerModel)
        {
            var user = new APIuser()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                UserName = registerModel.Email

            };
            return await userManager.CreateAsync(user, registerModel.Password);
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Administrator")
            };
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authLoginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authLoginKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
