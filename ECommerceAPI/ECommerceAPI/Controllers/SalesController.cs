using ClosedXML.Excel;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain;
using ECommerceAPI.Domain.EmailService;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ISaleWriteRepository _salesWriteRepository;
        private readonly ISaleReadRepository _salesReadRepository;
        private readonly IWebHostEnvironment _webhostEnvironment;


        public SalesController(
            ISaleWriteRepository productWriteRepository,
            ISaleReadRepository productReadRepository,
            IWebHostEnvironment wehostEnvironment)
        {
            _salesWriteRepository = productWriteRepository;
            _salesReadRepository = productReadRepository;
            _webhostEnvironment = wehostEnvironment;
        }

        #region CreateExcelRootFile
        private string CreateExcelFileRoot()
        {
            string folder = _webhostEnvironment.WebRootPath;
            string excelName = $"SalesReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
            FileInfo file = new FileInfo(Path.Combine(folder, excelName));

            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(folder, excelName));
            }
            return file.ToString();
        }
        #endregion

        [HttpPost("UploadData")]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            try
            {
                await _salesWriteRepository.UploadExcel(file);
                await _salesWriteRepository.SaveAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("SendReport")]
        public async Task<IActionResult> SendReport(Category ReportForThisCategory, DateTime StartDate, DateTime EndDate, string AcceptorEmail)
        {
            #region ExportFileWwwroot
           
            #endregion
            try
            {
                var file=CreateExcelFileRoot();
                await _salesReadRepository.GetDataFromSql(ReportForThisCategory, StartDate, EndDate, AcceptorEmail, file);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            // return DemoResponse<string>.GetResult(0, "OK", downloadUrl);
        }
    }
}


