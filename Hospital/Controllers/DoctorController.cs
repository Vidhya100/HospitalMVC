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
        public IActionResult GetAppoinments()
        {
            int DId = (int)HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetString("Role");
            if (role == "Doctor")
            {
                List<Appoinment> lstappoinment = new List<Appoinment>();
                lstappoinment = doctorBL.GetAllApoointments(DId).ToList();

                return View(lstappoinment);
            }
            else
            {
                return View();
            }
        }

     }
}
