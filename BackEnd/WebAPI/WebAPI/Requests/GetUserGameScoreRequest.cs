using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Requests
{
    public class GetUserGameScoreRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
