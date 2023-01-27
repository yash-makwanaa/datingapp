using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext context;

        public AccountController(DataContext context)
       {
            this.context = context;
        }
        [HttpPost ("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username) ) return BadRequest("username already taken ");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(), 
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            return user;

        }
        private async Task<bool> UserExists(string username){
            return await this.context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        [HttpPost ("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto){
            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if(user ==null) return Unauthorized("Invalid username");
            var hmac =new HMACSHA512(user.PasswordSalt);
            var computedhash= hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0;i<computedhash.Length;i++){
                if(computedhash[i]!=user.PasswordHash[i]) return Unauthorized("invalid password");
            }
            return user;
        }

    }
}