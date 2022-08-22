using ECommerceAPI.Domain;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.ResponseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories.Products
{
    public interface ISaleReadRepository : IReadRepository<Sale>
    {
        Task GetDataFromSql(Category ReportForThisCategory, DateTime StartDate, DateTime EndDate, string AcceptorEmail, string file);
      //  void SendMail(string Email,string downloadUrl, IFormFileCollection files);
        void ExportExcel(dynamic returnReport, string file);
        dynamic ReportBySegment(dynamic queryResult, string startDate, string endDate);
        dynamic ReportByCountry(dynamic queryResult,string startDate, string endDate);
        dynamic ReportByProduct(dynamic queryResult,  string startDate, string endDate);
        dynamic ReportByDiscount(dynamic queryResult, string startDate, string endDate);
    }
}
