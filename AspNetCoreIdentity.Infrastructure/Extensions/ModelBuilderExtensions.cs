using AspNetCoreIdentity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {        
            SeedStudents(modelBuilder);
            SeedProduts(modelBuilder);  
        }

        private static void SeedStudents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
               new Student(
                   name: "Pedro",
                   email: "pedro@gmail.com",
                   age: 23,
                   course: "Matemática"
              ),
                new Student(
                   name: "Amanda",
                   email: "amanda@gmail.com",
                   age: 17,
                   course: "Português"
              ),
                new Student(
                   name: "Lucas",
                   email: "lucas@gmail.com",
                   age: 10,
                   course: "Sem curso"
              ),
                new Student(
                   name: "Beatriz",
                   email: "beatriz@gmail.com",
                   age: 31,
                   course: "Artes"
              ));
        }

        private static void SeedProduts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(
                    name: "Pilha recarregável AA",
                    price: 25.00m
                ),
                new Product(
                    name: "Barbeador",
                    price: 94.75m
                ),
                new Product(
                    name: "Bola de vôlei - Penalt",
                    price: 175.90m
                ),
                new Product(
                    name: "Kit Travesseiro",
                    price: 250.99m
                ));
        }
    }
}
