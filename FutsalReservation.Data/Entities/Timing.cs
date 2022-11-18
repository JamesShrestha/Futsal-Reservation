using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutsalReservation.Data.Entities
{
    public class Timing
    {
        public Timing(string startTime, string endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public int TimingId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
