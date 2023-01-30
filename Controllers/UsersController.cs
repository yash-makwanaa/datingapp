using API.DTOs;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseController 
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepository , IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){
        var users =await this.userRepository.GetUsersAsync();
        
        return Ok(users);

    }
    [HttpGet("{username}")]
    public async Task< ActionResult<MemberDto> >GetUser(string username){
        return await this.userRepository.GetMemberByNameAysnc(username);
       
        
    }

        
    }
   
}