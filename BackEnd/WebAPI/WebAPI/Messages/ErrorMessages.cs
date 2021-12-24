using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Messages
{
    public class ErrorMessages
    {
        public static string USER_NOT_FOUND = "User with this name is not found";
        public static string GAME_NOT_FOUND = "Game with this name is not found";
        public static string USEREMAIL_OR_USERPASSWORD_IS_INVALID = "User email or user password is not found";
        public static string USER_OR_GAME_NOT_FOUND = "User or Game is not found";
        public static string USER_ALREADY_EXIST = "User already exist";
    }
}
