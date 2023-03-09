using BussinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;

namespace Hospital.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL iuserBL;
        //for sessons
        private readonly IHttpContextAccessor context;

        public UserController(IUserBL iuserBL, IHttpContextAccessor context)
        {
            this.iuserBL = iuserBL;
            this.context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind]RegiModel regiModel)
        {
            if (ModelState.IsValid)
            {
                iuserBL.Registration(regiModel);
                return RedirectToAction("Login");
            }
            return View();  
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind]LoginModel loginModel)
        {
            string message= string.Empty;
            if (ModelState.IsValid)
            {
                var result = iuserBL.Login(loginModel);
                if(result != null)
                {
                    //HttpContext.Session.SetString("Username", result.Username);
                    HttpContext.Session.SetString("Email", result.Email);
                    HttpContext.Session.SetString("Password", result.Password);
                    HttpContext.Session.SetString("Role", result.Role);
                    if (result.Role.Equals("Admin"))
                    {
                        return RedirectToAction("GetAllList");
                    }
                    else if (result.Role.Equals("Doctor"))
                    {
                        return RedirectToAction("DocAppointmen1ts");
                    }
                    else
                    {
                        return RedirectToAction("PatientView");
                    }
                    message = "Username and Password is correct.";
                    Console.WriteLine(message);
                }
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
