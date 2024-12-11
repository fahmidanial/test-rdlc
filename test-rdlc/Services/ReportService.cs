using Microsoft.Reporting.NETCore;
using System.Reflection;
using test_rdlc.Models;

namespace test_rdlc.Services
{
    public class ReportService
    {
        public byte[] GenerateReport(string reportType)
        {
            var items = GetDummyData();
            var parameters = new[] { new ReportParameter("Title", "Product Report") };

            var report = new LocalReport();
            var assembly = Assembly.GetExecutingAssembly();

            // Get all embedded resources to debug
            var resources = assembly.GetManifestResourceNames();
            var reportPath = resources.FirstOrDefault(x => x.EndsWith("Report1.rdlc"))
                ?? throw new FileNotFoundException("Report1.rdlc not found in embedded resources");

            using (var rs = assembly.GetManifestResourceStream(reportPath))
            {
                if (rs == null)
                    throw new FileNotFoundException($"Could not load report from resource: {reportPath}");

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