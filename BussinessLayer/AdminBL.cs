using ModelLayer;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public IEnumerable<Appoinment> GetAllApoointments()
        {
            try
            {
                return adminRL.GetAllApoointments();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Appoinment Update(Appoinment appoinment)
        {
            try
            {
                    return adminRL.Update(appoinment);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public Appoinment GetApoointment(int Aid)
        {
            try
            {
                return adminRL.GetApoointment(Aid);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public bool Delete(int Aid)
        {
            try
            {
                return adminRL.Delete(Aid);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
