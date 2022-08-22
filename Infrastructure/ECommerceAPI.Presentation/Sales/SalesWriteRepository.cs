using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Presentation.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ECommerceAPI.Presentation.Sales
{
    public class SalesWriteRepository : WriteRepository<Sale>, ISaleWriteRepository
    {
        private readonly IWriteRepository<Sale> _writeRepository;
        private readonly IReadRepository<Sale> _readRepository;
        public SalesWriteRepository(ECommerceAPIContext context,
                                    IWriteRepository<Sale> writeRepository,
                                    IReadRepository<Sale> readRepository) : base(context)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task UploadExcel(IFormFile file)
        {
            var sale = new Sale();
            //Find of dote index, then get filename letters later by the index number with substring funcion 
            int findFileFormat = file.FileName.ToString().IndexOf(".");


            //If there are records in Sales table firstly we must remove records --> truncate table
            var checkSalesTable = await _readRepository.GetAll().ToListAsync();
            if (checkSalesTable.Count != 0)
            {
                _writeRepository.RemoveRange(_readRepository.GetAll().ToList());
                await _writeRepository.SaveAsync();
            }//try cachi harda yazmisdiq?shhhs

            // After checking whether the uploaded file is suitable
            //  for only one of the xlsx and xls formats and whether it is more
            //than 5 mb, the operations related to the database are carried out.

            decimal fileSize = (decimal)file.Length / 1048576;
            if (fileSize <= 5 && string.Compare("xlsx", file.FileName.Substring(findFileFormat + 1).ToLower()) == 0
                                   || String.Compare("xls", file.FileName.Substring(findFileFormat).ToLower()) == 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            await _writeRepository.AddAsync((new Sale
                            {
                                Segment = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Country = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Product = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                DiscountBand = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                UnitsSold = (double)worksheet.Cells[row, 5].Value,
                                ManufactoringSold = (double)worksheet.Cells[row, 6].Value,
                                SalePrice = (double)worksheet.Cells[row, 7].Value,
                                GrossSales = (double)worksheet.Cells[row, 8].Value,
                                Discounts = (double)worksheet.Cells[row, 9].Value,
                                Sales = (double)worksheet.Cells[row, 10].Value,
                                COGS = (double)worksheet.Cells[row, 11].Value,
                                Profit = (double)worksheet.Cells[row, 12].Value,
                                Date = (DateTime)worksheet.Cells[row, 13].Value
                            }));
                        }
                    }
                }
            }
        }
    }
}
