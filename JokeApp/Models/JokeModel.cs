using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApp.Models
{
    public class JokeModel
    {
        [Key]
        public int JokeId { get; set; }
        public string JokeQuestion { get; set; }
        public string  JokeAnswer { get; set; }

        public JokeModel()
        {

        }
    }
}
