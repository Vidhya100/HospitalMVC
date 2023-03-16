using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IAdminRL
    {
        public IEnumerable<Appoinment> GetAllApoointments();
        public CreateApModel GetApoointment(int Aid);
        public CreateApModel Update(CreateApModel appoinment);
        public bool Delete(int Aid);
    }
}
