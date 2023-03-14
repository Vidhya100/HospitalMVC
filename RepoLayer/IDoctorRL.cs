using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IDoctorRL
    {
        public IEnumerable<Appoinment> GetAllApoointments(int DId);
    }
}
