using BussinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminBL adminBL;
        private readonly IHttpContextAccessor httpContext;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        public IActionResult GetAppoinments()
        {
            var role = HttpContext.Session.GetString("Role");
            if(role == "Admin")
            {
                List<Appoinment> lstappoinment = new List<Appoinment>();
                lstappoinment = adminBL.GetAllApoointments().ToList();

                return View(lstappoinment);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int Aid)
        {
            //if (Aid == null)
            //{
            //    return NotFound();
            //}
            Appoinment appoinment = adminBL.GetApoointment(Aid);

            if (appoinment == null)
            {
                return NotFound();
            }
            return View(appoinment);
        }
        [HttpPost]
        public IActionResult Edit(int Aid, [Bind] Appoinment appoinment)
        {
           if (Aid != appoinment.AId)
            {
                return NotFound();
            }
           if(ModelState.IsValid)
            {
                adminBL.Update(appoinment);
                return RedirectToAction("GetAppoinments");
            }
            return View(appoinment);
        }

        [HttpGet]
        public IActionResult Delete(int Aid)
        {
            //if (Aid == null)
            //{
            //    return NotFound();
            //}
            Appoinment appoinment = adminBL.GetApoointment(Aid);

            if (appoinment == null)
            {
                return NotFound();
            }
            return View(appoinment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Aid)
        {
            adminBL.Delete(Aid);
            return RedirectToAction("GetAppoinments");
        }

    }
}
