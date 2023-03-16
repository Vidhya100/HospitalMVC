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

        public IEnumerable<CreateApModel> ViewAppoinmentList(int DId, CreateApModel appoinment)
        {
            try
            {
                return doctorRL.ViewAppoinmentList(DId, appoinment);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
