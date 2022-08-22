using ECommerceAPI.Domain;
using ECommerceAPI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Presentation.Contexts
{

    public class ECommerceAPIContext : DbContext
    {
        public ECommerceAPIContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Sale> Sales { get; set; }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var datas = ChangeTracker
        //        .Entries<BaseEntity>();

        //    //foreach (var data in datas)
        //    //{
        //    //    _ =data.State switch
        //    //    {
        //    //        EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
        //    //        EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow
        //    //    };
        //    //}
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
