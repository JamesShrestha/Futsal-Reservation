using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutsalReservation.Data.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Required]
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CourtId { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("CourtId")]
        public virtual Court? Court { get; set; }
    }
}
