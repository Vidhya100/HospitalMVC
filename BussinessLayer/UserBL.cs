using ModelLayer;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public class UserBL: IUserBL
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

         public RegiModel Registration(RegiModel regiModel)
        {
            try
            {
                return iuserRL.Registration(regiModel);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public RegiModel Login(LoginModel  loginModel)
        {
            try
            {
                return iuserRL.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
