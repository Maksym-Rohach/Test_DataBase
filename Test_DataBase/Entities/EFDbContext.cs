﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_DataBase.Entities
{
    public class EFDbContext : IdentityDbContext<DbUser, DbRole, string, IdentityUserClaim<string>,
                              DbUserRole, IdentityUserLogin<string>,
                              IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        
        // public virtual DbSet<UserProfile> ClientProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
           

            base.OnModelCreating(builder);

            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new
                {
                    ur.UserId,
                    ur.RoleId
                });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                                .WithMany(r => r.UserRoles)
                                .HasForeignKey(ur => ur.UserId)
                                .IsRequired();
            });
        }
    }
}
