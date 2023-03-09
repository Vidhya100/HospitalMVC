using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer
{
    public interface IUserBL
    {
        public RegiModel Registration(RegiModel regiModel);
        public RegiModel Login(LoginModel loginModel);
    }
}
