using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseController
    {
        private readonly DataContext context;

        public BugController(DataContext context)
        {
            this.context = context;
        }
         [Authorize]
         [HttpGet("auth")]
         public ActionResult<string> GetSecret(){
            return "secret text";
         }
         [HttpGet("not-found")]
         public ActionResult<AppUser> GetNotFound(){
            var thing=this.context.Users.Find(-1);
            if(thing== null) return NotFound();
            
            return thing;
         }
         [HttpGet("server-error")]
         public ActionResult<string> GetSeverError(){
            var thing=this.context.Users.Find(-1);
            var thingtoreturn= thing.ToString();
            return thingtoreturn;
         }
         [HttpGet("bad-request")]
         public ActionResult<string> GetBadRequest(){
            return BadRequest("this is a bad request");
         }
    }
}