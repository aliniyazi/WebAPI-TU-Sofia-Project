using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public DateTime? RegisterDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public ICollection<UserGame> UserGames { get; set; } = new HashSet<UserGame>();
    }
}
