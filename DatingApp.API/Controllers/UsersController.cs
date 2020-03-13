using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository Repo;
        private readonly IMapper Mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(Mapper.Map<IEnumerable<UserForListDto>>(await this.Repo.GetUsers()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            return Ok(Mapper.Map<UserForDetailedDto>(await this.Repo.GetUser(id)));
        }
    }
}