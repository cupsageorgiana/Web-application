using AtelierAuto.Data;
using AtelierAuto.DataAccessLayer;
using AtelierAuto.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtelierAuto.Services
{
    public class AppointmentService
    {
        private UnitOfWork _unitOfWork;

        public AppointmentService(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Appointment> Index()
        {
            var appointments = _unitOfWork.AppointmentRepository.Get();
            return appointments.ToList();
        }

        public Appointment Details(int id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            return appointment;
        }

        public void Create([Bind("Id,Data,Ora,NumeClient,Telefon,Marca,Descriere,Status")] Appointment appointment)
        {
                _unitOfWork.AppointmentRepository.Insert(appointment);
                _unitOfWork.Save();         
        }

        public Appointment Edit(int id)
        {
            if (id == null)
            {
                return null;
            }

            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            if (appointment == null)
            {
                return null;
            }
            return appointment;
        }

        public void Edit(int id, [Bind("Id,Data,Ora,NumeClient,Telefon,Marca,Descriere,Status")] Appointment appointment)
        {           
                    _unitOfWork.AppointmentRepository.Update(appointment);
                    _unitOfWork.Save();           
        }
       
        public Appointment Delete(int id)
        {
            if (id == null)
            {
                return null;
            }

            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);

            if (appointment == null)
            {
                return null;
            }
            return appointment;
        }
        
        public void DeleteConfirmed(int id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            _unitOfWork.AppointmentRepository.Delete(id);
            _unitOfWork.Save();
        }

        private bool AppointmentExists(int id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            if (appointment == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public void UpdateStatus(int id)
        {
            Appointment appointment = _unitOfWork.AppointmentRepository.GetByID(id);
            appointment.Status = "rezolvata";
            _unitOfWork.Save();
        }

        public List<Appointment> SearchName(string name)
        {
            var appointments = _unitOfWork.AppointmentRepository.Get();

            if (!String.IsNullOrEmpty(name))
            {
                appointments = appointments.Where(s => s.NumeClient.Contains(name));
            }
            return appointments.ToList();
        }

        public List<Appointment> SearchDate(DateTime dateApp)
        {
            var appointments = _unitOfWork.AppointmentRepository.Get();

            if (!(dateApp == default(DateTime)))
            {
                appointments = appointments.Where(s => s.Data == dateApp);
            }
            return appointments.ToList();
        }

        public List<Appointment> SearchByDates(DateTime dateApp1, DateTime dateApp2)
        {
            var appointments = _unitOfWork.AppointmentRepository.Get();

            if (!(dateApp1 == default(DateTime) && !(dateApp2 == default(DateTime))))
            {
                appointments = appointments.Where(s => s.Data > dateApp1 && s.Data < dateApp2);
            }
            return appointments.ToList();
        }

        public StringBuilder ExportReports(DateTime dateApp1, DateTime dateApp2)
        {
            StringBuilder sbRtn = new StringBuilder();
            var appointments = _unitOfWork.AppointmentRepository.Get();
            List<Appointment> apps = appointments.ToList();

            var header = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                                       "Data",
                                       "Ora",
                                       "NumeClient",
                                       "Telefon",
                                       "Marca",
                                       "Descriere",
                                       "Status"
                                      );
            sbRtn.AppendLine(header);

            foreach (var item in apps)
            {
                if (item.Data > dateApp1 && item.Data < dateApp2)
                {
                    var listResults = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                                                      item.Data,
                                                      item.Ora,
                                                      item.NumeClient,
                                                      item.Telefon,
                                                      item.Marca,
                                                      item.Descriere,
                                                      item.Status
                                                     );
                    sbRtn.AppendLine(listResults);
                }
            }
            return sbRtn;
        }
    }
}
