
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain;
using ECommerceAPI.Domain.EmailService;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Presentation.Contexts;
using ECommerceAPI.Presentation.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Presentation
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection service)
        {
            // service.AddDbContext<ECommerceAPIContext>(option => option.UseSqlServer(Configuration.ConnectionString));
            service.AddScoped<IEmailSender, EmailSender>();
            service.AddDbContext<ECommerceAPIContext>(options =>
                              options.UseSqlite("Data Source=ExcelReportDb.db"));
            service.AddScoped<ISaleWriteRepository, SalesWriteRepository>();
            service.AddScoped<ISaleReadRepository, SalesReadRepository>();
            service.AddScoped<IWriteRepository<Sale>, WriteRepository<Sale>>();
            service.AddScoped<IReadRepository<Sale>, ReadRepository<Sale>>();

        }
    }
}
