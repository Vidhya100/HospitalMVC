using ModelLayer;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public class PatientsBL: IPatientsBL
    {
        private readonly IPatientsRL patientsRL;

        public PatientsBL(IPatientsRL patientsRL)
        {
            this.patientsRL = patientsRL;
        }

        public IEnumerable<Appoinment> GetDocList(string Role)
        {
            try
            {
                return patientsRL.GetDocList(Role);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Appoinment GetApoointment(int DId, int PId)
        {
            try
            {
                return patientsRL.GetApoointment(DId,PId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
