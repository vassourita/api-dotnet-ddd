using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _Service;
        public LoginController(ILoginService service)
        {
            _Service = service;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDTO loginInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (loginInfo == null)
                return BadRequest();

            try
            {
                var result = await _Service.Login(loginInfo);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }
    }
}