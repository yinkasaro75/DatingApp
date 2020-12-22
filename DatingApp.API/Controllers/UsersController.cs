using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using DatingApp.API.Dto;

namespace DatingApp.API.Controllers
{

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IDatingRepository _repo;
    private readonly IMapper _mapper;
    public UsersController(IDatingRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;

    }

    [HttpGet]

    public async Task<IActionResult> GetUsers()
    {
      var users = await _repo.GetUsers();
      var userForReturn = _mapper.Map<IEnumerable<UserForList>>(users);
      return Ok(userForReturn);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
      var user = await _repo.GetUser(id);
      var userForReturn = _mapper.Map<UserForDetail>(user);
      return Ok(userForReturn);
     
    }





  }
}