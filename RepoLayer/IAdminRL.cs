using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IAdminRL
    {
        public IEnumerable<Appoinment> GetAllApoointments();
        public Appoinment GetApoointment(int Aid);
        public Appoinment Update(Appoinment appoinment);
        public bool Delete(int Aid);
    }
}
