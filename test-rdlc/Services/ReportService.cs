using Microsoft.Reporting.NETCore;
using RDLC_Demo.Client.Models;
using test_rdlc.Models;

namespace RDLC_Demo.Client.Services
{
    public class ReportService
    {
        public byte[] GenerateReport(string reportType)
        {
            var items = GetDummyData();
            var parameters = new[] { new ReportParameter("Title", "Product Report") };

            var report = new LocalReport();
            var assembly = typeof(Product).Assembly;

            using (var rs = assembly.GetManifestResourceStream("RDLC_Demo.Client.Reports.Report1.rdlc"))
            {
                report.LoadReportDefinition(rs);
                report.DataSources.Add(new ReportDataSource("DataSet1", items));
                report.SetParameters(parameters);

                return report.Render(reportType);
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