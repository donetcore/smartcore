﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartCore.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
         {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        [Route("GetRandomToken")]
        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }


    }
}
