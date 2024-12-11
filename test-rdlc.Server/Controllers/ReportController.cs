using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using test_rdlc.Server.Models;

namespace test_rdlc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpGet("generate/{type}")]
        public IActionResult GenerateReport(string type)
        {
            var items = GetDummyData();
            var parameters = new[] { new ReportParameter("Title", "Product Report") };

            var report = new LocalReport();
            var assembly = typeof(ReportController).Assembly;

            using (var rs = assembly.GetManifestResourceStream("test_rdlc.Server.Reports.Report1.rdlc"))
            {
                report.LoadReportDefinition(rs);
                report.DataSources.Add(new ReportDataSource("DataSet1", items));
                report.SetParameters(parameters);

                var result = report.Render(type.ToUpper());
                var contentType = type.ToUpper() == "PDF" ? "application/pdf" : "application/vnd.ms-excel";
                var fileName = $"Export.{(type.ToUpper() == "PDF" ? "pdf" : "xls")}";

                return File(result, contentType, fileName);
            }
        }

        private Product[] GetDummyData()
        {
            return new[] {
                new Product { ProductID = 1, ProductName = "Adjustable Race", ProductNumber = "AR-5381" },
                new Product { ProductID = 2, ProductName = "Bearing Ball", ProductNumber = "BA-8327" }
            };
        }
    }
}