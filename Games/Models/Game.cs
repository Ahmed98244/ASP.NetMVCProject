using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Display(Name="Game Title")]
        public String GameTitle { get; set; }

        [Display(Name = "Game Content")]
        public String GameContent { get; set; }

        [Display(Name = "Game Appraisal")]
        public String GameAppraisal { get; set; }

        [Display(Name = "Game Image")]
        public String GameImage { get; set; }

        [Display(Name = "Game Category")]
        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Category Category { get; set; }
    }
}