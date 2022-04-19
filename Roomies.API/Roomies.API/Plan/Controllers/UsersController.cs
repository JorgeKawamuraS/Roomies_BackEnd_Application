using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using Roomies.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthenticationResponse>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<AuthenticationResponse>> GetAll()
        {
            var users = await _userService.GetAll();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<AuthenticationResponse>>(users);

            return resources;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Usuario o  contraseña incorrecto" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            //try { 
            UserResponse response =  await _userService.Register(request);

            if (!response.Success)
                return BadRequest(response.Message);

            //var messageResource = _mapper.Map<User, UserResource>(response.Resource);
            var messageResource = $"El usuario {response.Resource.Username} ha sido creado";
            return Ok(messageResource);
        
            //return Ok(new { message = "Registration successful" });
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }
    }
}
