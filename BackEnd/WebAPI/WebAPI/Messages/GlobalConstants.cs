using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Messages
{
    public class GlobalConstants
    {
        public const string SENDGRID_API_KEY = "Add Key Here ";


        public const string PASSWORD_ALLOWED_CHARACTERS = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,44}$";
        public const string EMAIL_ALLOWED_CHARACTERS = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
        public const string USER_NAME_ALLOWED_CHARACTERS = @"[a-z0-9_]+";
    }
}
