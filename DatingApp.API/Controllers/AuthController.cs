using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dto;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthController(IAuthRepository repo, IConfiguration config)
    {
      _config = config;
      _repo = repo;

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterForDto userRegisterForDto)
    {
      //validate request

      userRegisterForDto.Username = userRegisterForDto.Username.ToLower();
      if (await _repo.UserExist(userRegisterForDto.Username))
        return BadRequest("Username already exist");

      var userCreated = new User
      {
        UserName = userRegisterForDto.Username
      };
      var createdUser = await _repo.Register(userCreated, userRegisterForDto.Password);

      return StatusCode(201);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginForDto userLoginForDto)
    {
      // throw new Exception("Computer say no");
      var userFromLoginRepo = await _repo.Login(userLoginForDto.Username.ToLower(), userLoginForDto.Password);
      if (userFromLoginRepo == null)
        return Unauthorized();

      var claims = new[]
      {
    new Claim(ClaimTypes.NameIdentifier, userFromLoginRepo.Id.ToString()),
    new Claim(ClaimTypes.Name, userFromLoginRepo.UserName)
    };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

      var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor= new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(claims),
           Expires = DateTime.Now.AddDays(1),
           SigningCredentials = cred

      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new {
          token = tokenHandler.WriteToken(token)
      });

    }


  }
}