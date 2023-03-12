using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserDbService _userService;
        private readonly IUserUtility _userUtility;

        public UsersController(UserDbService userService, IUserUtility userUtility)
        {
            this._userService = userService;
            this._userUtility = userUtility;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <returns>Gets the user by ID.</returns>
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

        /// <summary>
        /// Login method.
        /// </summary>
        /// <returns>Login.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserCredentials userCreds)
        {
            var user = await _userService.GetUserByUsernameAsync(userCreds.Username);
            if (user == null)
            {
                return NotFound();
            }
            if (!_userUtility.VerifyPassword(userCreds.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <returns>Gets the user by username.</returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <returns>Creates a new user.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _userService.CreateUserAsync(user);
            return Ok(user);
        }
        /// <summary>
        /// Update user.
        /// </summary>
        /// <returns>Updates a user.</returns>
        [Authorize]
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

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <returns>Deletes a user.</returns>
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