using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.UserGames = new HashSet<UserGame>();
        }
        public int GameId { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
    }
}
