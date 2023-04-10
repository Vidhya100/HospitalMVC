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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllAbout()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterAp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAP([Bind]RegiModel regiModel)
        {
            if (ModelState.IsValid)
            {
                iuserBL.Registration(regiModel);
                return RedirectToAction("Login");
            }
            return View();  
        }
        [HttpGet]
        public IActionResult RegisterDoc()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterDoc([Bind] RegiModel regiModel)
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
       
        [HttpPost ]
        //custom route
       // [Route("User/UserLogin")]
        public IActionResult Login([Bind]LoginModel loginModel)
        {
            string message= string.Empty;
            if (ModelState.IsValid)
            {
                var result = iuserBL.Login(loginModel);
                if(result != null)
                {
                    HttpContext.Session.SetInt32("UserId", result.UserId);
                    HttpContext.Session.SetString("Username", result.Username);
                    HttpContext.Session.SetString("Email", result.Email);
                    HttpContext.Session.SetString("Password", result.Password);
                    HttpContext.Session.SetString("Role", result.Role);
                    if (result.Role.Equals("Admin"))
                    {
                        return RedirectToAction("GetAppoinments", "Admin");
                    }
                    else if (result.Role.Equals("Doctor"))
                    {
                        return RedirectToAction("ViewAppoinmentList", "Doctor");
                    }
                    else
                    {
                        return RedirectToAction("GetDocList", "Patients");
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
