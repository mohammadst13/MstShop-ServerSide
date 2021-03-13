using Microsoft.EntityFrameworkCore;
using MstShop_ServerSide.DataLayer.Entities.Access;
using MstShop_ServerSide.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MstShop_ServerSide.DataLayer.Context
{
    public class MstShopDbContext : DbContext
    {
        #region constructor

        public MstShopDbContext(DbContextOptions<MstShopDbContext> options) : base(options)
        {

        }

        #endregion

        #region Db Sets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}