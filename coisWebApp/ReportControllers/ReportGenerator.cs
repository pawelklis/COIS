
using AspNetCore.Reporting;
using System.Data;
using System.Text;

public class ReportGenerator
{
    private IWebHostEnvironment webHostEnvironment;

    public ReportGenerator(IWebHostEnvironment _webHostEnvironment)
    {
        webHostEnvironment = _webHostEnvironment;
        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }


    public string GetReport(int reportType, ReportName reportName)
    {
        DataTable dt = getData(reportName);

        string mimeType = "";
        int extension = 1;
        string path = $"{this.webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";


        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("param", "test");

        LocalReport localReport = new LocalReport(path);
        localReport.AddDataSource("DataSet1", dt);

        string filePath = $"{this.webHostEnvironment.WebRootPath}\\Download\\{Guid.NewGuid().ToString()}";

        //PDF
        if (reportType == 1)
        {
            filePath += ".pdf";
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            System.IO.File.WriteAllBytes(filePath, result.MainStream);
            //return File(result.MainStream, "application/pdf");
        }

        //XLS
        if (reportType == 2)
        {

            filePath += ".xls";
            var result = localReport.Execute(RenderType.Excel, extension, parameters, mimeType);
            System.IO.File.WriteAllBytes(filePath, result.MainStream);
            //return File(result.MainStream, "application/mxexcel");
        }
        return filePath;
    }

    private DataTable getData(ReportName reportName)
    {
        switch (reportName)
        {
            case ReportName.EmployeesStandard:
                return new EmployeeService().GetEmployees();
            default:
                return new DataTable();
        }        
    }

}



public enum ReportName
{
    EmployeesStandard
}

