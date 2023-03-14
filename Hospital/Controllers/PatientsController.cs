using BussinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientsBL patientsBL;
        private readonly IHttpContextAccessor httpContext;
        public PatientsController(IPatientsBL patientsBL)
        {
            this.patientsBL = patientsBL;
        }
        public IActionResult GetDocList()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Patient")
            {
                List<Appoinment> lstappoinment = new List<Appoinment>();
                lstappoinment = patientsBL.GetDocList("Doctor").ToList();

                return View(lstappoinment);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult GetAppointment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAppointment(int DId)
        {
            
            if (ModelState.IsValid)
            {
                int PId = (int)HttpContext.Session.GetInt32("UserId");
                patientsBL.GetApoointment(DId, PId);
                return RedirectToAction("GetAppointmentList");
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetAppointmentList()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Patient")
            {
                List<Appoinment> lstappoinment = new List<Appoinment>();
                lstappoinment = patientsBL.GetDocList("Doctor").ToList();

                return View(lstappoinment);
            }
            else
            {
                return View();
            }
        }
    }
}
