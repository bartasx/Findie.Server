using Findie.Common.Models.AdminsModel;
using Findie.Common.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.DbModels
{
    public partial class DatabaseContext : IdentityDbContext<AppUser, AppRole, int>
    { 
        public DatabaseContext()
        {           
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Friends> Friends { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Notepads> NotepadTables { get; set; }
        public DbSet<EventComments> EventComments { get; set; }
        public DbSet<EventParticipants> EventParticipants { get; set; }
        public DbSet<Events> Events { get; set; }

        public virtual DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}