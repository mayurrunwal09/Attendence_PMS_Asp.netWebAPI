using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.context
{
    public  class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Break> Breaks { get; set; }
        public DbSet<Attendence> Attenants { get; set; }
        public DbSet<ClockOut> ClockOuts { get; set; }
        public DbSet<FinishBreak> FinishBreaks { get; set; }

      


        public async Task<ICollection<Attendence>> GetAttendanceDetails(int userId)
        {
            var attendanceDetails = await Attenants
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return attendanceDetails;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(c => c.UserType)
                .WithMany(d => d.Users)
                .HasForeignKey(d => d.UserTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendence>()
                .HasOne(d=>d.User)
                .WithMany(d=>d.Attendances)
                .HasForeignKey(d=>d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

          



            modelBuilder.Entity<Break>()
                .HasOne(b => b.User)
                .WithMany(u => u.Breaks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClockOut>()
               .HasOne(d => d.User)
               .WithMany(d => d.ClockOuts)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<FinishBreak>()
              .HasOne(d => d.User)
              .WithMany(d => d.FinishBreaks)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Users)
                .WithMany(u => u.Leaves)
                .HasForeignKey(l => l.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(d=>d.Attendence)
                .WithMany(d=>d.ReportRequests)
                .HasForeignKey(d => d.AttendenceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
             .HasOne(d => d.Breaks)
             .WithMany(d => d.Reports)
             .HasForeignKey(d => d.BreakId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
           .HasOne(r => r.User)
           .WithMany(d=>d.Reports)
           .HasForeignKey(r => r.UserId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
           .HasOne(r => r.ClockOuts)
           .WithMany(d => d.Report)
           .HasForeignKey(r => r.ClockOutId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
           .HasOne(r => r.finishBreak)
           .WithMany(d => d.Reports)
           .HasForeignKey(r => r.FinishBreakId)
           .OnDelete(DeleteBehavior.Restrict);

           
        }





    }
}
