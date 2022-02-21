using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models
{
    public class BuyTheGame
    {
        public int Id { get; set; }
        public string Messsage { get; set; }
        public DateTime Date { get; set; }

        public int GameId { get; set; }
        public string UserId { get; set; }

        public virtual Game game { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}