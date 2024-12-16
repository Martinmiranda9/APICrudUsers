using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Context;
using UserAPI.Models;
using UserAPI.Models.DTOs;
using UserAPI.Repositories;
using UserAPI.Services;


namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await UserService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"El usuario con ID {id} no se existe en la base de datos.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] AddUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            var newUser = await UserService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"El usuario con ID {id} no se existe en la base de datos.");
            } 
            else {
                user.Name = dto.Name;
                user.Email = dto.Email;
                user.Password = dto.Password;

                await UserService.UpdateUserAsync(user);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Usuario con ID {id} no se existe en la base de datos.");
            }

            await UserService.DeleteUserAsync(id);
            return Ok($"Usuario con ID {id} ha sido eliminado con éxito.");
        }
    }
}
