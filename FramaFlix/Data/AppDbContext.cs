using FramaFlix.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FramaFlix.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<AppUser> AppUsers { get; set; }
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

        #region Popular os dados de Usúario
         // Perfil - IdentityRole
         List<IdentityRole> roles = new()
         {
            new IdentityRole()
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Moderador",
               NormalizedName = "MODERADOR"
            },
            new IdentityRole()
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Usuário",
               NormalizedName = "USUÁRIO"
            }
         };
         builder.Entity<IdentityRole>().HasData(roles);

         //Conta de Usuário - identityUser
         List<IdentityUser> users = new()
         {
            new IdentityUser()
            {
               Id = Guid.NewGuid().ToString(),
               Email = "admin@framaflix.com",
               NormalizedEmail = "ADMIN@FRAMAFLIX.COM",
               UserName = "Admin",
               NormalizedUserName = "ADMIN",
               EmailConfirmed = true,
               LockoutEnabled = false
            },
             new IdentityUser()
            {
               Id = Guid.NewGuid().ToString(),
               Email = "User@gmail.com",
               NormalizedEmail = "USER@GMAIL.COM",
               UserName = "User",
               NormalizedUserName = "USER",
               EmailConfirmed = true,
               LockoutEnabled = true
            }
         };
         foreach (var user in users)
         {
            PasswordHasher<IdentityUser> pass = new();
            pass.HashPassword(user, "@Etec123");
         }
         builder.Entity<IdentityUser>().HasData(users);

         //Dados pessoais do Usuário - AppUser
         List<AppUser> appUsers = new()
         {
            new AppUser()
            {
               AppUserId = users[0].Id,
               Name = "Amanda Furtado",
               Birthday = DateTime.Parse("21/08/2007"),
               Photo = "/img/users/avatar.png"
            },
            new AppUser()
            {
               AppUserId = users[1].Id,
               Name = "Fulaninho",
               Birthday = DateTime.Parse("25/12/2000")
            }
         };
          builder.Entity<AppUser>().HasData(appUsers);
          //Perfis dos Usuários - IdentityUserRole
          List<IdentityUserRole<string>> userRoles = new()
          {
            new IdentityUserRole<String>()
            {
               UserId = users[0].Id,
               RoleId = roles[0].Id
            },
             new IdentityUserRole<String>()
            {
               UserId = users[0].Id,
               RoleId = roles[1].Id
            },
             new IdentityUserRole<String>()
            {
               UserId = users[0].Id,
               RoleId = roles[2].Id
            },
             new IdentityUserRole<String>()
            {
               UserId = users[1].Id,
               RoleId = roles[2].Id
            }
          };
        #endregion
    }
}