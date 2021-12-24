using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Messages;
using WebAPI.Requests;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly UserDbContext userDbContext;
        private readonly UserService userService;
        public UserControler(UserDbContext userDbContext, UserService userService)
        {
            this.userDbContext = userDbContext;
            this.userService = userService;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = userDbContext.Users.ToList();
            return Ok(users);
        }
        [HttpGet("user/id")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userService.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("user/score")]
        public async Task<IActionResult> GetUserScores(int userId , int gameId)
        {
            var result = await this.userService.GetUserScores(userId,gameId);
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return BadRequest(ErrorMessages.USER_OR_GAME_NOT_FOUND);
        }
        [HttpPost("user/register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest userInput)
        {
            var result = await userService.CreateUser(userInput);
            if(result == true)
            {
            return Ok();
            }
            return BadRequest(ErrorMessages.USER_ALREADY_EXIST);
        }
        [HttpPost("user/login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest userInput)
        {
            var result = await userService.LoginUser(userInput);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(ErrorMessages.USER_NOT_FOUND);
        }

        [HttpPut("user/score")]
        public async Task<IActionResult> AddUserScore([FromBody] AddUserGameScoreRequest userInput)
        {
            var result = await this.userService.AddUserScore(userInput);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest(ErrorMessages.USER_OR_GAME_NOT_FOUND);
        }

        [HttpPost("user/reset/password")]
        public async Task<IActionResult> ResetUserPassword([FromForm] string email)
        {
            var result =  await userService.ResetPasswordAsync(email);
            if(result == true)
            {
                return Ok();
            }
            return BadRequest("Email not Found ");
        }
        [HttpDelete("user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userService.DeleteUser(id);
            return Ok();
        }
    }
}
