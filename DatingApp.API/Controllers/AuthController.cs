using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository Repo;
        private readonly IConfiguration Config;
        public AuthController (IAuthRepository repo, IConfiguration config) {
            this.Config = config;
            this.Repo = repo;

        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register (UserForRegisterDto registerDto) {
            //validate
            registerDto.Name = registerDto.Name.ToLower ();
            if (await Repo.UserExist (registerDto.Name))
                return BadRequest ("user already exist");
            var CreateUser = new User {
                Name = registerDto.Name
            };

            var CreatedUser = await Repo.Register (CreateUser, registerDto.Password);
            return StatusCode (201);

        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login (UserForLoginDto loginDto) {
            var CheckUser = await Repo.Login (loginDto.Name.ToLower (), loginDto.Password);
            if (CheckUser == null)
                return null;
            var Claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, CheckUser.Id.ToString ()),
                new Claim (ClaimTypes.Name, CheckUser.Name)
            };
            var Keys = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Config.GetSection ("AppSettings:Token").Value));
            var Creds = new SigningCredentials (Keys, SecurityAlgorithms.HmacSha512Signature);
            var TokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (Claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = Creds
            };
            var TokenHandler = new JwtSecurityTokenHandler ();
            var mytoken = TokenHandler.CreateToken (TokenDescriptor);
            return Ok (new {
                token = TokenHandler.WriteToken (mytoken)
            });

        }
    }
}