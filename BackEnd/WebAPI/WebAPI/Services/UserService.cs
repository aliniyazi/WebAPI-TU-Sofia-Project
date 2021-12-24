using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Messages;
using WebAPI.Requests;

namespace WebAPI.Services
{
    public class UserService
    {
        private readonly UserDbContext userDbContext;

        public UserService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public async Task<User> LoginUser(LoginUserRequest userRequest)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email && u.Password == userRequest.Password);
            return user;
        }
        public async Task<bool> CreateUser(CreateUserRequest userRequest)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);
            if (user == null)
            {
                userDbContext.Users.Add(new User { UserName = userRequest.UserName, Password = userRequest.Password, Email = userRequest.Email });
                await this.userDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<User> GetUserById(int userInput)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userInput);
            return user;
        }
        public async Task DeleteUser(int userInput)
        {
            User user = new User() { UserId = userInput };
            userDbContext.Users.Attach(user);
            userDbContext.Users.Remove(user);
            await this.userDbContext.SaveChangesAsync();

        }
        public async Task<bool> AddUserScore(AddUserGameScoreRequest userInput)
        {
            //TO DO Make code more simple
            var user = await userDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userInput.userId);
            if (user == null)
            {
                return false;
            }
            var game = await userDbContext.Games.FirstOrDefaultAsync(u => u.GameId == userInput.GameId);
            if (game == null)
            {
                return false;
            }

            var UserGame = this.userDbContext.UserGames.FirstOrDefault(ug => ug.GameId == userInput.GameId && ug.UserId == userInput.userId);
            if (UserGame == null)
            {
                this.userDbContext.UserGames.Add(new UserGame { Game = game, User = user, Score = userInput.Score });
                await this.userDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                UserGame.Score = userInput.Score;
                await this.userDbContext.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<double>> GetUserScores(int userId, int gameId)
        {
            var userScores = await userDbContext.UserGames.Where(u => u.User.UserId == userId && u.Game.GameId == gameId).Select(u => u.Score).ToListAsync();
            return userScores;
        }
        public async Task<bool> ResetPasswordAsync(string email)
        {
            var existingUser = await userDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                Random rnd = new Random();
                var newPasswordNumberPart = rnd.Next(10000000, 99999999);
                var newPasswordLeterPart = (char)rnd.Next(65, 90);
                var newPassword = newPasswordNumberPart + newPasswordLeterPart.ToString();

                try
                {
                    var apiKey = GlobalConstants.SENDGRID_API_KEY;
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress("Your Valid Email Adress", "Name for Email");
                    var subject = "Subject Name";
                    var to = new EmailAddress(email, "");
                    var plainTextContent = $"You password has been changed to {newPassword}";
                    var htmlContent = $"<strong>You password has been changed to {newPassword}</strong>";
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                    if (response.IsSuccessStatusCode)
                    {
                        existingUser.Password = newPassword;
                        await this.userDbContext.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return false;
        }
    }
}
