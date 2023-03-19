using Microsoft.OData.Edm;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class Appoinment
    {
        
        public int AId { get; set; }
        public int PId { get; set; }

        public int DId { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Photo { get; set; }
        
        [Required(ErrorMessage = "Please enter name")]
        public string Pname { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Dname { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Please enter name")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Visit_Time { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Visit_End { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Condition { get; set; }
        public int isHide { get; set; }
        
    }
}
