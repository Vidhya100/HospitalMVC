using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer
{
    public interface IUserRL
    {
        public RegiModel Registration(RegiModel regiModel);
        public RegiModel Login(LoginModel loginModel);
    }
}
