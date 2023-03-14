using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IPatientsRL
    {
        public IEnumerable<Appoinment> GetDocList(string Role);
        public Appoinment GetApoointment(int DId, int PId);
    }
}
