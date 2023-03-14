using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class RegiModel
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string Fullname { get; set; }
        public string Photo { get; set; }
        public string Degree { get; set; }
        public string Address { get; set; }
    }
}
