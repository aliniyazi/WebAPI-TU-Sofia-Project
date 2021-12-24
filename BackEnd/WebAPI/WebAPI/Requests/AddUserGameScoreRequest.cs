using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Requests
{
    public class AddUserGameScoreRequest
    {
        public int userId { get; set; }
        public int GameId { get; set; }
        public double Score { get; set; }
    }
}
