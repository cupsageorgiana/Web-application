using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtelierAuto.Data;
using AtelierAuto.Models;
using System.Text;
using AtelierAuto.DataAccessLayer;
using AtelierAuto.Services;
using Microsoft.AspNetCore.Authorization;

namespace AtelierAuto.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private AppointmentService appService;

        public AppointmentsController(ApplicationDbContext context)
        {
            appService = new AppointmentService(context);
        }

        public ViewResult Index()
        {
            var appointments = appService.Index();
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ViewResult Details(int id)
        {
            Appointment appointment = appService.Details(id);
            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Data,Ora,NumeClient,Telefon,Marca,Descriere,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appService.Create(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = appService.Edit(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Data,Ora,NumeClient,Telefon,Marca,Descriere,Status")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                appService.Edit(id, appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = appService.Delete(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            appService.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/UpdateStatus/5
        public ActionResult UpdateStatus(int id)
        {
            appService.UpdateStatus(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult SearchName(string name)
        {
            var appointments = appService.SearchName(name);
            return View(appointments.ToList());
        }

        public ActionResult SearchDate(DateTime dateApp)
        {
            var appointments = appService.SearchDate(dateApp);
            return View(appointments.ToList());
        }

        public ActionResult SearchByDates(DateTime dateApp1, DateTime dateApp2)
        {
            var appointments = appService.SearchByDates(dateApp1, dateApp2);
            return View(appointments.ToList());
        }

        public FileContentResult ExportReports(DateTime dateApp1, DateTime dateApp2)
        {
            StringBuilder file = appService.ExportReports(dateApp1, dateApp2);
            return File(new UTF8Encoding().GetBytes(file.ToString()), "text/csv", "E:/Visual Studio .Net/AtelierAuto/Reports.csv");
        }
    }
}
