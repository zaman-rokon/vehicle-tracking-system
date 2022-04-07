using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.DataAccess.DbContext
{
    public class SeedVTSDB
    {
        public static void InsertSeed(IApplicationBuilder builder)
        {
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();
            var userVehicleId = Guid.NewGuid().ToString();
            var adminVehicleId = Guid.NewGuid().ToString();
            var adminUserId = Guid.NewGuid().ToString();
            var userRoleUserId = Guid.NewGuid().ToString();

            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                /*if (!applicationDbContext.Roles.Any())
                {
                    
                }

                applicationDbContext.SaveChanges();*/
                if (!applicationDbContext.ApplicationUsers.Any())
                {
                    applicationDbContext.Roles.AddRange(
                        new IdentityRole
                        {
                            Id = adminRoleId,
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new IdentityRole
                        {
                            Id = userRoleId,
                            Name = "ApplicationUser",
                            NormalizedName = "APPLICATIONUSER"
                        }
                    );

                    applicationDbContext.ApplicationUsers.AddRange(
                        new ApplicationUser
                        {
                            Email = "user@example.com",
                            UserName = "user@example.com",
                            NormalizedEmail = "USER@EXAMPLE.COM",
                            NormalizedUserName = "USER@EXAMPLE.COM",
                            EmailConfirmed = false,
                            FirstName = "Mr",
                            LastName = "User",
                            Id = userRoleUserId,
                            SecurityStamp = "P4Y5XJKA7OGGR3A2HLPT6EMPTEFFEJ2G",
                            ConcurrencyStamp = "2bdd84c2-44b5-403b-b292-7e73571c2e9c",
                            PasswordHash = "AQAAAAEAACcQAAAAEH0xZA2IUHgeGdT4WNNtrqTW6ABEmM6cXvnHTsK4+PZybxMlhxpFdpHlW2cZwVQQ7g==",//User123@
                            LockoutEnabled = true,
                        }
                        , new ApplicationUser
                        {
                            Email = "admin@example.com",
                            UserName = "admin@example.com",
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN@EXAMPLE.COM",
                            EmailConfirmed = false,
                            FirstName = "Mr",
                            LastName = "Admin",
                            Id = adminUserId,
                            SecurityStamp = "JZVH3B2C5L64I7H7LQRDCN6PNJDDR2MT",
                            ConcurrencyStamp = "5f8070fc-9481-473a-9ce5-592fcb44a1a0",
                            PasswordHash = "AQAAAAEAACcQAAAAEIm2aK9iXxWr1rVA99VgKYRavvuWFy6ErMUpPYu1kB8emUw2TxHkwMw5gD+1Db8qTw==",//Admin123@
                            LockoutEnabled = true,
                        });

                    applicationDbContext.SaveChanges();

                    var admin = applicationDbContext.ApplicationUsers.FirstOrDefault(it => it.Id == adminUserId);
                    admin.Vehicles = new List<Vehicle>
                    {
                        new Vehicle
                        {
                            Id = adminVehicleId,
                            RegistrationNumber = "THL T 5555",
                            Name = "Axio",
                            Model = "G-2006",
                            OwnerName = "Mr Admin",
                            RegistrationDate = DateTime.Now.AddYears(-5),
                        }
                    };

                    var user = applicationDbContext.ApplicationUsers.FirstOrDefault(it => it.Id == userRoleUserId);
                    user.Vehicles = new List<Vehicle>
                    {
                        new Vehicle()
                        {
                            Id = userVehicleId,
                            RegistrationNumber = "DHA HA 64-9622",
                            Name = "Allion",
                            Model = "G-2006",
                            OwnerName = "Mr User",
                            RegistrationDate = DateTime.Now.AddYears(-5),
                        }
                    };


                    applicationDbContext.UserRoles.Add(new IdentityUserRole<string>
                    {
                        UserId = adminUserId,
                        RoleId = adminRoleId
                    });
                    applicationDbContext.UserRoles.Add(new IdentityUserRole<string>
                    {
                        UserId = userRoleUserId,
                        RoleId = userRoleId
                    });


                    applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
