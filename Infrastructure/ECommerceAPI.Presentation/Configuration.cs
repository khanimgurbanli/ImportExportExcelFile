using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Presentation
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configuration = new();
                configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\INSPIRON\\Downloads\\eCommerceApi-main\\ECommerceAPI\\ECommerceAPI\\ECommerceAPI"));
                configuration.AddJsonFile("appsettings.json");

                return configuration.GetConnectionString("ECommerceAPIConnection");
            }
        }
    }
}
