using FramaFlix.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;

namespace FramaFlix.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Configuração de Muitos para Muitos do MoviGenre
        builder.Entity<MovieGenre>()
           .HasKey(mg => new { mg.MovieId, mg.GenreId });

        builder.Entity<MovieGenre>()
           .HasOne(mg => mg.Movie)
           .WithMany(m => m.Genres)
           .HasForeignKey(mg => mg.MovieId);

        builder.Entity<MovieGenre>()
           .HasOne(mg => mg.Genre)
           .WithMany(g => g.Movies)
           .HasForeignKey(mg => mg.GenreId);
        #endregion
    }
}