using Microsoft.EntityFrameworkCore;
using Web_MZ.Entities;

namespace Web_MZ.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Langue> Langue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // --- Contact → Creator (1-N)
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Creator)
                .WithMany()
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Cascade); // conserve la cascade

            // --- Contact → Recruiter (1-N)
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Recruiter)
                .WithMany()
                .HasForeignKey(c => c.RecruiterId)
                .OnDelete(DeleteBehavior.Restrict); //  pour éviter multiple cascade paths
        }

    }
}
