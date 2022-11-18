using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FutsalReservation.Models;
using FutsalReservation.Data.Entities;

namespace FutsalReservation.Data
{
    public class FutsalReservationContext : DbContext
    {
        public FutsalReservationContext (DbContextOptions<FutsalReservationContext> options)
            : base(options)
        {
        }
        public DbSet<FutsalReservation.Data.Entities.User> User { get; set; }
        public DbSet<FutsalReservation.Data.Entities.Court> Court { get; set; }
        public DbSet<FutsalReservation.Data.Entities.Reservation> Reservation { get; set; }
        public DbSet<FutsalReservation.Data.Entities.Timing> Timing { get; set; }
    }
}
