using Microsoft.EntityFrameworkCore;
using WhatsYourOpinion.Models;

namespace WhatsYourOpinion.Contexts
{
    public class OpinionContext : DbContext
    {
        public OpinionContext(DbContextOptions<OpinionContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasMany(o => o.Opinions)
                .WithOne(b => b.Topic);


            modelBuilder.Entity<Topic>().HasData(
                new Topic() { Id = 1, Title = "Donald Trump", Category = "Person" },
                new Topic() { Id = 2, Title = "Joe Biden", Category = "Person" }
            );
        }


        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}
