using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IPatientsBL
    {
        public IEnumerable<Doctor> GetDocList(string Role);
        public CreateApModel CreateApoointment(int DId, int PId, CreateApModel appoinments);
        public IEnumerable<CreateApModel> ViewAppoinmentList(int PId, CreateApModel appoinment);

    }
}
