using System;
using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public DateTime ConnectionDate { get; set; }
        public DateTime LastCheck { get; set; }
    }
}