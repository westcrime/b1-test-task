namespace Task2.Data;

using Microsoft.EntityFrameworkCore;
using Task2.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<UploadedFile> UploadedFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}