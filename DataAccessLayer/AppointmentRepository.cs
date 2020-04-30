using AtelierAuto.Data;
using AtelierAuto.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtelierAuto.DataAccessLayer
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        private ApplicationDbContext context;
     
         public AppointmentRepository(ApplicationDbContext context) : base(context)
         {
              this.context = context;
         }
    }
}
