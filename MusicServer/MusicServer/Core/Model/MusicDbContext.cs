using Microsoft.EntityFrameworkCore;

public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Student { get; set; }
    public DbSet<Grade> Grade { get; set; }
}