using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    public class UserController
    {
        [ApiController]
        [Route("[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly UserDbService _userService;

            public UsersController(UserDbService userService)
            {
                this._userService = userService;
            }

            [HttpGet]
            [ActionName("Get all users")]
            public async Task<IActionResult> Get()
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }

            [HttpGet("{id:length(24)}")]
            public async Task<IActionResult> Get(string id)
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }

            [HttpPost]
            public async Task<IActionResult> Create(User user)
            {
                await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, User user)
            {
                var existingUser = await _userService.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                await _userService.UpdateUserAsync(id, user);
                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var existingUser = await _userService.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
        }
    }
}