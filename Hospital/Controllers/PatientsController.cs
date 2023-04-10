using BussinessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientsBL patientsBL;
        private readonly IHttpContextAccessor httpContext;
        private readonly IWebHostEnvironment webEnv;
        public PatientsController(IPatientsBL patientsBL, IWebHostEnvironment webHostEnvironment)
        {
            this.patientsBL = patientsBL;
            webEnv = webHostEnvironment;
        }
        [Route("PatientsController/Dahboard")]
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

                string imgPath = uploadImage(appoinments.ProfileImg);
                appoinments.Photo = imgPath;
                patientsBL.CreateApoointment(DId, PId, appoinments);
                return RedirectToAction("ViewAppoinmentList");
            }
            Console.WriteLine("Invalid patient data, returning to same view");
            return View();
        }

        private string uploadImage(IFormFile profileImg)
        {
            string imgName = "";
            try
            {
                //...\\webrootpath\\assets\\pathient-img
                string folderpath = Path.Combine(webEnv.WebRootPath, "assets", "patient-img");
                // Random random = new Random();
                // int num = random.Next();

                //...\\webrootpath\\assets\\pathient-img\\guid_FileNamewithExt;
                 imgName = Guid.NewGuid().ToString() + "_" + profileImg.FileName;

                string imgPath = Path.Combine(folderpath, imgName);
                using (var fstream = new FileStream(imgPath, FileMode.CreateNew))
                {
                    profileImg.CopyTo(fstream);
                }

            }
            catch(Exception ex)
            {
                throw;
            }
         //   imgPath = "~\\" + imgPath.Substring(webEnv.WebRootPath.Length - 1);

            return imgName;
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
