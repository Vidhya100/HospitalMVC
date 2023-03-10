using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class Patients
    {
       
        public int PatientID { get; set; }
        public string Pname { get; set;}
        public string ProfileImg { get; set; }
        public string Email { get; set; }
        public int Number { get; set; }
    }
}
