using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Luis Felipe",
                    Email = "lutecirov@gmail.com",
                    Idade = 23,
                    Curso = "Matemática"
                }
            );
        }
    }
}
