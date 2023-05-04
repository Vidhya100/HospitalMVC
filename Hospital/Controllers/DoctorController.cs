using BussinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorBL doctorBL;
        private readonly IHttpContextAccessor httpContext;
        public DoctorController(IDoctorBL doctorBL)
        {
            this.doctorBL = doctorBL;
        }
        //public IActionResult GetAppoinments()
        //{
        //    int DId = (int)HttpContext.Session.GetInt32("UserId");
        //    var role = HttpContext.Session.GetString("Role");
        //    if (role == "Doctor")
        //    {
        //        List<Appoinment> lstappoinment = new List<Appoinment>();
        //        lstappoinment = doctorBL.GetAllApoointments(DId).ToList();

        //        return View(lstappoinment);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [Route("DoctorController/Dashboard")]
        [HttpGet]
        public IActionResult ViewAppoinmentList(CreateApModel appoinment)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Doctor")
            {
                //1. get doctor Id 
                int DId = (int)HttpContext.Session.GetInt32("UserId");
                //2. fectch appointment id's for Doctor id from doctor table
                //3. then fetch and create list of appoinments object wih details
                List<CreateApModel> lstAppoinments = new List<CreateApModel>();
                lstAppoinments = doctorBL.ViewAppoinmentList(DId, appoinment).ToList();
                return View(lstAppoinments);
            }
            else
            {
                //if wrong role redirecting to login
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }

        }
    }
}


