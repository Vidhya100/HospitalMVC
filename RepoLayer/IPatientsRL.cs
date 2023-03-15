using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IPatientsRL
    {
        public IEnumerable<Doctor> GetDocList(string Role);
        public CreateApModel CreateApoointment(int DId, int PId, CreateApModel appoinments);
        public IEnumerable<CreateApModel> ViewAppoinmentList(int PId, CreateApModel appoinments);
    }
}
