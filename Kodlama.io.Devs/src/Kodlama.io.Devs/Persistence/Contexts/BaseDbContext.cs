using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserSocialMediaAddress> UserSocialMediaAddresses { get; set; }
        public DbSet<User> Users { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<Technology>(x =>
            {
                x.ToTable("Technologies").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.LanguageId).HasColumnName("LanguageId");
                x.Property(p => p.Name).HasColumnName("Name");
                x.HasOne(p => p.Language);
            });


            modelBuilder.Entity<UserSocialMediaAddress>(a =>
            {
                a.ToTable("UserSocialMediaAddresses").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.GithubUrl).HasColumnName("GithubUrl");
                a.HasOne(p => p.User);
            });
            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.FirstName).HasColumnName("FirstName");
                p.Property(u => u.LastName).HasColumnName("LastName");
                p.Property(u => u.Email).HasColumnName("Email");
                p.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                p.Property(u => u.Status).HasColumnName("Status");
                p.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                p.HasMany(c => c.UserOperationClaims);
                p.HasMany(c => c.RefreshTokens);
            });
            modelBuilder.Entity<OperationClaim>(p =>
            {
                p.ToTable("OperationClaims").HasKey(o => o.Id);
                p.Property(o => o.Id).HasColumnName("Id");
                p.Property(o => o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(p =>
            {
                p.ToTable("UserOperationClaims").HasKey(u => u.Id);
                p.Property(u => u.Id).HasColumnName("Id");
                p.Property(u => u.UserId).HasColumnName("UserId");
                p.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                p.HasOne(u => u.User);
                p.HasOne(u => u.OperationClaim);
            });


            Language[] languageEntitySeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
            modelBuilder.Entity<Language>().HasData(languageEntitySeeds);

            Technology[] technolgyEntitySeeds = { new(1, 1, "ASP.Net"), new(2, 1, "WPF"), new(3, 2, "Spring"), new(4, 2, "JSP") };
            modelBuilder.Entity<Technology>().HasData(technolgyEntitySeeds);


            OperationClaim[] operationClaimsEntitySeeds =
            {
                new(1, "Admin"),
                new(2, "User")
            };

            modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);

            //UserSocialMediaAddress[] userSocialMediaAddressEntitySeeds =
            //{
            //      new(1,"https://github.com/Ncivelek",1)
            //  };
            //modelBuilder.Entity<UserSocialMediaAddress>().HasData(userSocialMediaAddressEntitySeeds);
        }
    }
}
