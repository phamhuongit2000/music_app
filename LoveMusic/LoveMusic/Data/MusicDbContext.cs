using LoveMusic.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<History> Historys { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<SongCategory> SongCategorys { get; set; }
    public DbSet<SongAlbum> SongAlbums { get; set; }
    public DbSet<User> Users { get; set; }
}