using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IPatientsBL
    {
        public IEnumerable<Appoinment> GetDocList(string Role);
        public Appoinment GetApoointment(int DId, int PId);
    }
}
