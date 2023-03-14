using ModelLayer;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public class DoctorBL : IDoctorBL
    {
        private readonly IDoctorRL doctorRL;

        public DoctorBL(IDoctorRL doctorRL)
        {
            this.doctorRL = doctorRL;
        }

        public IEnumerable<Appoinment> GetAllApoointments(int DId)
        {
            try
            {
                return doctorRL.GetAllApoointments(DId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
