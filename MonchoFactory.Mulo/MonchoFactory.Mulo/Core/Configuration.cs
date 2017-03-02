using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MonchoFactory.Mulo.WebApi.Models;

namespace MonchoFactory.Mulo.WebApi.Core
{
    public class Configuration : DbMigrationsConfiguration<MuloContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        [Obsolete]
        protected override void Seed(MuloContext context)
        {
            context.Instruments.AddOrUpdate(new Instrument {Id = 1, Name = "Guitar"});
            context.Instruments.AddOrUpdate(new Instrument {Id = 2, Name = "Bass"});
            context.Instruments.AddOrUpdate(new Instrument {Id = 3, Name = "Drums"});

            context.Genres.AddOrUpdate(new Genre {Id = 1, Name = "Rock"});
            context.Genres.AddOrUpdate(new Genre {Id = 2, Name = "Blues"});
            context.Genres.AddOrUpdate(new Genre {Id = 3, Name = "Punk"});


            context.Musicians.AddOrUpdate(new Musician
            {
                Id = 1,
                FirstName = "Cosme",
                LastName = "Fulanito1",
                Email = "cosme@fulanito.com",
                Instruments = new List<Instrument>() {new Instrument {Id = 1, Name = "Guitar"}}
            });

            context.Musicians.AddOrUpdate(new Musician
            {
                Id = 2,
                FirstName = "Cosme",
                LastName = "Fulanito2",
                Email = "cosme@fulanito.com",
                Instruments = new List<Instrument>() {new Instrument {Id = 2, Name = "Guitar"}}
            });

            context.Musicians.AddOrUpdate(new Musician
            {
                Id = 3,
                FirstName = "Cosme",
                LastName = "Fulanito3",
                Email = "cosme@fulanito.com",
                Instruments = new List<Instrument>() {new Instrument {Id = 1, Name = "Guitar"}}
            });

            context.Musicians.AddOrUpdate(new Musician
            {
                Id = 4,
                FirstName = "Cosme",
                LastName = "Fulanito4",
                Email = "cosme@fulanito.com",
                Instruments = new List<Instrument>() {new Instrument {Id = 1, Name = "Guitar"}}
            });

            string adminRoleId;
            string userRoleId;
            if (!context.Roles.Any())
            {
                adminRoleId = context.Roles.Add(new IdentityRole("Administrator")).Id;
                userRoleId = context.Roles.Add(new IdentityRole("User")).Id;
            }
            else
            {
                adminRoleId = context.Roles.First(c => c.Name == "Administrator").Id;
                userRoleId = context.Roles.First(c => c.Name == "User").Id;
            }

            context.SaveChanges();

            if (!context.Users.Any())
            {
                var administrator =
                    context.Users.Add(new IdentityUser("administrator")
                    {
                        Email = "fc.verdaguer@outlook.com",
                        EmailConfirmed = true
                    });
                administrator.Roles.Add(new IdentityUserRole {RoleId = adminRoleId});

                var standardUser =
                    context.Users.Add(new IdentityUser("fverdaguer")
                    {
                        Email = "fc.verdaguer@outlook.com",
                        EmailConfirmed = true
                    });
                standardUser.Roles.Add(new IdentityUserRole {RoleId = userRoleId});

                context.SaveChanges();

                var store = new MuloUserStore();
                store.SetPasswordHashAsync(administrator,
                    new MuloUserManager().PasswordHasher.HashPassword("f.44144284"));
                store.SetPasswordHashAsync(standardUser, new MuloUserManager().PasswordHasher.HashPassword("f.44144284"));
            }

            context.SaveChanges();
        }
    }
}