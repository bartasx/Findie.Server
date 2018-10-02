using FindieServer.Models.AdminsModel;
using FindieServer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Models.DbModels
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}