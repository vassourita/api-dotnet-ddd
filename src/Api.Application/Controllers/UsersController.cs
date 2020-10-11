using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _Service;
        public UsersController(IUserService service)
        {
            _Service = service;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public async Task<ActionResult> Index()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _Service.GetAll());
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("{id:guid}", Name = "GetWithId")]
        public async Task<ActionResult> Show(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _Service.GetById(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Store([FromBody] UserCreateDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _Service.Create(user);
                if (result == null)
                    return BadRequest();

                return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public async Task<ActionResult> Update([FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _Service.Update(user);
                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpDelete]
        [Authorize("Bearer")]
        [Route("{id:guid}")]
        public async Task<ActionResult> Destroy(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var deleted = await _Service.Delete(id);
                return Ok(new { deleted });
            }
            catch (ArgumentException err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }
    }
}