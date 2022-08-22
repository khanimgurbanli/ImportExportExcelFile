using ClosedXML.Excel;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain;
using ECommerceAPI.Domain.EmailService;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.ResponseModels;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Presentation.Contexts;
using Microsoft.AspNetCore.Http;
using MimeKit;
using OfficeOpenXml;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using System.Net;

namespace ECommerceAPI.Presentation.Sales
{
    public class SalesReadRepository : ReadRepository<Sale>, ISaleReadRepository
    {
        private readonly IReadRepository<Sale> _readRepository;
        private readonly IEmailSender _emailSender;
        public SalesReadRepository(ECommerceAPIContext aPIContext,
                                   IReadRepository<Sale> readRepository,
                                   IEmailSender emailSender) : base(aPIContext)
        {
            _readRepository = readRepository;
            _emailSender = emailSender;
        }

        #region ExportExcel
        public void ExportExcel(dynamic returnReport, string file)
        {
            //Save rootfile
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(returnReport, true);
                package.Save();
            }
        }
        #endregion
        #region GetDataFromSqlGroupByCategory

        public async Task GetDataFromSql(Category ReportForThisCategory, DateTime startDate, DateTime endDate, string AcceptorEmail, string file)
        {
            #region QueriesForReport
            //define variable for return
            dynamic returnReportResult = default;


            //Check StartDate biger than EndDate
            var checkDateTime = DateTime.Compare(startDate, endDate);

            if (checkDateTime == -1)
            {
                if (ReportForThisCategory == Category.Segment)
                {
                    var segmentQueryResult = _readRepository.GetAll()
                        .Where(s => s.Date >= startDate && s.Date <= endDate)
                        .OrderBy(s => s.Segment)
                        .GroupBy(s => s.Segment)
                        .Select(s =>
                        new
                        {
                            Segment = s.Key,
                            ProductCount = s.Sum(x => x.UnitsSold),
                            TotalSale = s.Sum(x => x.Sales),
                            TotalDiscount = s.Sum(x => x.Discounts),
                            TotalProfit = s.Sum(s => s.Profit)
                        });

                    returnReportResult = ReportBySegment(segmentQueryResult, startDate.ToShortDateString(), endDate.ToShortDateString());
                }
                else if (ReportForThisCategory == Category.Country)
                {
                    var countryQueryResult = _readRepository.GetAll()
                           .Where(s => s.Date >= startDate && s.Date <= endDate)
                           .OrderBy(s => s.Date)
                           .GroupBy(s => s.Country)
                           .Select(s =>
                           new
                           {
                               Country = s.Key,
                               ProductCount = s.Sum(x => x.UnitsSold),
                               TotalSale = s.Sum(x => x.Sales),
                               TotalDiscount = s.Sum(x => x.Discounts),
                               TotalProfit = s.Sum(s => s.Profit)
                           });

                    returnReportResult = ReportByCountry(countryQueryResult, startDate.ToShortDateString(), endDate.ToShortDateString());
                }
                else if (ReportForThisCategory == Category.Product)
                {
                    var productQueryResult = _readRepository.GetAll()
                       .Where(s => s.Date >= startDate && s.Date <= endDate)
                       .OrderBy(s => s.Product)
                       .GroupBy(s => s.Product)
                       .Select(s =>
                       new
                       {
                           Product = s.Key,
                           ProductCount = s.Sum(x => x.UnitsSold),
                           TotalSale = s.Sum(x => x.Sales),
                           TotalDiscount = s.Sum(x => x.Discounts),
                           TotalProfit = s.Sum(s => s.Profit),
                           StartDate = startDate,
                           EndDate = endDate
                       });
                    returnReportResult = ReportByProduct(productQueryResult, startDate.ToShortDateString(), endDate.ToShortDateString());
                }
                else
                {
                    var discountQueryResult = _readRepository.GetAll()
                       .Where(s => s.Date >= startDate && s.Date <= endDate)
                       .OrderBy(s => s.Product)
                       .GroupBy(s => s.Product)
                       .Select(s =>
                       new
                       {
                           Product = s.Key,
                           Discount = s.Max(s => s.Discounts),
                           StartDate = startDate,
                           EndDate = endDate
                       });

                    returnReportResult = ReportByDiscount(discountQueryResult, startDate.ToShortDateString(), endDate.ToShortDateString());
                }
                #endregion


                //CreateExcel report file metod
                ExportExcel(returnReportResult, file);
                //send mail 
                await _emailSender.SendEmailAsync(AcceptorEmail, file);
            }
        }
        #endregion
        #region ReportByDiscount
        public dynamic ReportByDiscount(dynamic queryResult, string startDate, string endDate)
        {
            var discountProductList = new List<DiscountViewModel>();
            foreach (var data in queryResult)
            {
                discountProductList.Add(new DiscountViewModel()
                {
                    Product = data.Product,
                    Discount = (decimal)data.Discount,
                    StartDate = startDate,
                    EndDate = endDate
                });

            }
            return discountProductList;
        }
        #endregion
        #region ReportBySegment
        public dynamic ReportBySegment(dynamic queryResult, string startDate, string endDate)
        {
            // query data from database 
            var ExcelReportList = new List<SegmentViewModel>();
            foreach (var data in queryResult)
            {
                ExcelReportList.Add(new SegmentViewModel()
                {

                    Segment = data.Segment,
                    ProductCount = (decimal)data.ProductCount,
                    TotalSale = (decimal)data.TotalSale,
                    TotalDiscount = (decimal)data.TotalDiscount,
                    TotalProfit = (decimal)data.TotalProfit,
                    StartDate = startDate,
                    EndDate = endDate
                });

            }
            return ExcelReportList;
        }
        #endregion
        #region ReportByProduct
        public dynamic ReportByProduct(dynamic queryResult, string startDate, string endDate)
        {
            // query data from database 
            var ExcelReportList = new List<ProductViewModel>();

            foreach (var data in queryResult)
            {
                ExcelReportList.Add(new ProductViewModel()
                {
                    Product = data.Product,
                    ProductCount = (decimal)data.ProductCount,
                    TotalSale = (decimal)data.TotalSale,
                    TotalDiscount = (decimal)data.TotalDiscount,
                    TotalProfit = (decimal)data.TotalProfit,
                    StartDate = startDate,
                    EndDate = endDate
                });

            }
            return ExcelReportList;
        }

        #endregion
        #region ReportByCountry
        public dynamic ReportByCountry(dynamic queryResult, string startDate, string endDate)
        {
            // query data from database 
            var ExcelReportList = new List<CountryViewModel>();

            foreach (var data in queryResult)
            {
                ExcelReportList.Add(new CountryViewModel()
                {
                    Country = data.Country,
                    ProductCount = (decimal)data.ProductCount,
                    TotalSale = (decimal)data.TotalSale,
                    TotalDiscount = (decimal)data.TotalDiscount,
                    TotalProfit = (decimal)data.TotalProfit,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
            return ExcelReportList;
        }
        #endregion
    }
}








