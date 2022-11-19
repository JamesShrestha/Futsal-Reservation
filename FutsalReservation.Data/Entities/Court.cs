using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutsalReservation.Data.Entities
{
    [Table("Court")]
    public class Court
    {
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Timing>? Timings { get; set; }

        //public Court()
        //{
        //    _timings = new List<String>()
        //        {
        //            "5am - 6am", "6am - 7am", "7am - 8am", "8am - 9am", "9am - 10am", "10am - 11am", "11am - 12pm", "12pm - 1pm", "1pm - 2pm", "2pm - 3pm", "3pm - 4pm", "4pm - 5pm", "5pm - 6pm", "6pm - 7pm", "7pm - 8pm", "8pm - 9pm", "9pm - 10pm"
        //        };
        //}
    }

    
   
}
