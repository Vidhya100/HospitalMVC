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

        public IEnumerable<Doctor> GetDocList(string Role)
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
        public CreateApModel CreateApoointment(int DId, int PId, CreateApModel appoinments)
        {
            try
            {
                return patientsRL.CreateApoointment(DId, PId, appoinments);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<CreateApModel> ViewAppoinmentList(int PId, CreateApModel appoinments)
        {
            try
            {
                return patientsRL.ViewAppoinmentList(PId,appoinments);
            }
            catch
            {
                throw;
            }
        }
    }
}
