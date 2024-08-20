namespace Task1.Data;

using Microsoft.EntityFrameworkCore;
using Task1.Models;
public class LineDbContext: DbContext
{
    public DbSet<Line> Lines => Set<Line>();
    public LineDbContext() => Database.EnsureCreated();
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Lines.db");
    }
}
