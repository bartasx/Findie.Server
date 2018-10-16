using System.Linq;
using System.Threading.Tasks;
using Findie.Common.Models.IdentityModels;
using FindieServer.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Api.Admin
{
    [Produces("application/json")]
    [Route("api/UserManagementApi")]
    public class UserManagementApiController : Controller
    {
        #region Fields 

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DatabaseContext _databaseContext;

        #endregion
        
        #region Constructor

        public UserManagementApiController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DatabaseContext databaseContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._databaseContext = databaseContext;
        }
        
        #endregion
        [HttpGet("{username}")]
        [Route("GetUserInfoAsync")]
        public async Task<IActionResult> GetUserInfoAsync(string username)
        {
            var query = await (from p in this._databaseContext.Users
                where p.Email == username || p.UserName == username
                select p).ToListAsync();

            if (query != null)
            {
                return Ok(query);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("ChangeUserCredentials")]
        public async Task<IActionResult> ChangeUserCredentials(string username)
        {           
            var query = await (from p in this._databaseContext.Users
                where p.Email == username || p.UserName == username
                select p).ToListAsync();

            if (query != null)
            {
                
                await this._databaseContext.SaveChangesAsync();
            }              
            return Ok();
        }
    }
}