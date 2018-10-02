using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models;
using FindieServer.Models.DbModels;
using FindieServer.Models.IdentityModels;
using FindieServer.Services;
using FindieServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FindieServer.Managers
{
    public class AccountsManager : IAccountsManager
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _db;
        public AccountsManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IConfiguration configuration, DatabaseContext databaseContext)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
            this._db = databaseContext;
        }

        public async Task<string> LoginUser(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username,
                model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return this.JwtTokenBuilder(model.Username);
            }
            else
            {
                return null;
            }
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ChangeCredentials(UserInfo userInfo)
        {
            var query = await (from p in this._db.Users where userInfo.Username == p.UserName select p).FirstOrDefaultAsync();

            if (query != null)
            {
                query.AccountDescription = userInfo.AccountDescription;
                query.Name = userInfo.Name;
                query.Surname = userInfo.Surname;

                await this._db.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<string> RegisterAccount(RegisterViewModel model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Username,
                AccountDescription = model.AccountDescription
            };

            var result = await this._userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                MailService.SendRegistrationMail(model.Email, model.Username);
                await this._signInManager.SignInAsync(user, isPersistent: false);
                return JwtTokenBuilder(model.Username);
            }
            return null;
        }

        private string JwtTokenBuilder(string username)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: this._configuration["JWT:issuer"], audience: this._configuration["JWT:audience"],
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}