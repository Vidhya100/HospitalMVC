using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class CreateApModel
    {
        public int AId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int PId { get; set; }
        [HiddenInput(DisplayValue = false)]

        public int DId { get; set; }

 
        [Required(ErrorMessage = "Please enter name")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Pname { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Dname { get; set; }
        
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Visit_Time { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public DateTime Visit_End { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Condition { get; set; }
    }
}
