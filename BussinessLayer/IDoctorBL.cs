using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IDoctorBL
    {
        public IEnumerable<CreateApModel> ViewAppoinmentList(int DId, CreateApModel appoinment);
    }
}
