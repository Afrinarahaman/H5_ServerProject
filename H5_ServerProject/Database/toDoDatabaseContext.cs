using H5_ServerProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace H5_ServerProject.Database
{
    public class toDoDatabaseContext:DbContext
    {
        public toDoDatabaseContext(DbContextOptions<toDoDatabaseContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => _=modelBuilder.Entity<ToDoItem>().HasData(
               new ToDoItem()
               {
                   Id= 1,
                   Title = "Title1",
                   Description = "Description1",
                   
                   User_Id= Guid.Parse("35e39329-3a7a-4e35-9978-90bedc762867")


               });
    }
}
