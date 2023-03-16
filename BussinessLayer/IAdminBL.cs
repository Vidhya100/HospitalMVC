using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IAdminBL
    {
        public IEnumerable<Appoinment> GetAllApoointments();

        public CreateApModel Update(CreateApModel appoinment);
        public CreateApModel GetApoointment(int Aid);
        public bool Delete(int Aid);

    }
}
