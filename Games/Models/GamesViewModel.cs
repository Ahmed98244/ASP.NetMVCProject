using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models
{
    public class GamesViewModel
    {
        public string GameTitle { get; set; }

        public IEnumerable<BuyTheGame> Items { get; set; }
    }
}