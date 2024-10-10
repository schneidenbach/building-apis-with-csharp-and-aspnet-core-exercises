using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheEmployeeAPI.Security
{
    [ApiController]
    [Route("security")]
    public class SecurityController : ControllerBase
    {
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        //I can't emphasize this enough - this is made for DEMO PURPOSES ONLY and is the only part of this workshop that you SHOULD NOT EVER REUSE!
        [HttpPost("generateAVeryInsecureToken_pleasedontusethisever")]
        public IActionResult GetToken([FromBody] GetTokenRequestBody request)
        {
            return Ok(HttpContext.GenerateJwt(request.Role, request.Username));
        }
    }
}