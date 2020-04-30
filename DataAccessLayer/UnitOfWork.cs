using AtelierAuto.Data;
using AtelierAuto.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtelierAuto.DataAccessLayer
{
    public class UnitOfWork
    {
        private ApplicationDbContext _context; 
        private AppointmentRepository appointmentRepository;
       
        public UnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AtelierAutoContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ApplicationDbContext(optionsBuilder.Options);

            appointmentRepository = new AppointmentRepository(_context);
        }

        public AppointmentRepository AppointmentRepository
        {
            get
            {
                if (this.appointmentRepository == null)
                {
                    this.appointmentRepository = new AppointmentRepository(_context);
                }
                return appointmentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
