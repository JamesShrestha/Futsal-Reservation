using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutsalReservation.Data.Entities
{
    [Table("Timing")]
    public class Timing
    {
        public Timing(string startTime, string endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
        [Key]
        public int TimingId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        
        [ForeignKey("CourtId")]
        public virtual Court? Court { get; set; }
    }
}
