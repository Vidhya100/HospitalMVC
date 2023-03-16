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
        [HttpGet]
        public IActionResult GetDocList()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Patient")
            {
                List<Doctor> lstappoinment = new List<Doctor>();
                lstappoinment = patientsBL.GetDocList("Doctor").ToList();

                return View(lstappoinment);
            }
            else
            {
                return View();
            }
        }
        //return for storing Doctor Id and Name from view
        [HttpGet]
        public IActionResult GetAppointmentId(int DId,string Dname)
        {
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("DId", DId);
                HttpContext.Session.SetString("Dname", Dname);
                return RedirectToAction("CreateApoointment");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateApoointment()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateApoointment(CreateApModel appoinments)
        {
            
            if (ModelState.IsValid)
            {
                int PId = (int)HttpContext.Session.GetInt32("UserId");
                int DId = (int)HttpContext.Session.GetInt32("DId");
                patientsBL.CreateApoointment(DId, PId, appoinments);
                return RedirectToAction("ViewAppoinmentList", "User");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ViewAppoinmentList(CreateApModel appoinment)
        {
            int PId = (int)HttpContext.Session.GetInt32("UserId");
            List<CreateApModel> lstAppoinments = new List<CreateApModel>();
            lstAppoinments = patientsBL.ViewAppoinmentList(PId,appoinment).ToList();
            return View(lstAppoinments);
           
        }

    }
}
