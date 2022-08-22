using ECommerceAPI.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories.Products
{
    public interface ISaleWriteRepository :IWriteRepository<Sale>
    {
        Task UploadExcel(IFormFile excelFile);
    }
}
