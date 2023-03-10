using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IAdminBL
    {
        public IEnumerable<Appoinment> GetAllApoointments();

        public Appoinment Update(Appoinment appoinment);
        public Appoinment GetApoointment(int Aid);
        public bool Delete(int Aid);

    }
}
