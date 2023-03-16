using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IDoctorRL
    {
        public IEnumerable<CreateApModel> ViewAppoinmentList(int DId, CreateApModel appoinment);
    }
}
