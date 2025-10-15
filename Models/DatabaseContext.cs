using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishWordsNoteBook.Models
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Section> Sections { get; set; }
        public DbSet<Word> Words { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Word>(options =>
            {
                options.Property<DateTime>(w => w.Date).HasDefaultValueSql("GETDATE()");
            });

            builder.Entity<Section>(options =>
            {
                options.Property<DateTime>(w => w.Date).HasDefaultValueSql("GETDATE()");
            });

            builder.Entity<ApplicationUser>(options =>
            {
                options.HasIndex(u => u.NormalizedEmail)
                       .IsUnique();
            });

            base.OnModelCreating(builder);
        }
    }
}
