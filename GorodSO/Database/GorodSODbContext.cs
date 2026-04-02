using Microsoft.EntityFrameworkCore;
using GorodSO.Models;
using GorodSO.Services;

namespace GorodSO.Database;

public class GorodSODbContext : DbContext
{
    public GorodSODbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        var user = new AppUser() { VkId = 294458271, Role = AppUserRole.Admin };
        Users.Add(user);
        SaveChanges();
    }

    public GorodSODbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<QuestTask> Tasks { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Competition> Competitions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=postgres;Password=postgres");
}