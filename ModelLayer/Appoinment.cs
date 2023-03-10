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
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Visit_Time { get; set; }
        //public DateTime Visit_End { get; set; }
        public int Number { get; set; }

        public string Doctor { get; set; }
        public string Condition { get; set; }
        
    }
}
