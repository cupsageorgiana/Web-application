using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AtelierAuto.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}",
               ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Ora { get; set; }
        public string NumeClient { get; set; }
        public string Telefon { get; set; }
        public string Marca { get; set; }
        public string Descriere { get; set; }
        public string Status { get; set; }
    }
}
