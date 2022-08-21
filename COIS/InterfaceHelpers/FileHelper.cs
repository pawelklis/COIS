using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


using System;
using System.IO;


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Pdf;
using VetCV.HtmlRendererCore.PdfSharpCore;
using PdfSharpCore;
using QRCoder;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using ZXing.QrCode.Internal;
using ZXing;
using ZXing.Rendering;

public class FileHelper
{
    /// <summary>
    /// import harmonogramu z CSIP do BazyDanych wg schematu kolumn w CrewFileModel
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static List<CrewModel> Import_CSIP_CrewFile(string filepath, ZoneType ZoneIfNull, LoggerType Logger, List<Tuple<string, int>> columnsConfiguration = null)
    {

        Logger.Log("Import pliku", 0, 0);

        if (columnsConfiguration == null)
            columnsConfiguration = CrewFileModel.ColumnsConfiguration();

        DataSet ds;
        ds = FileHelper.ExcelToDataSet(filepath, columnsConfiguration.Count,Logger); //@"C:\Users\klispawel\Downloads\k104.xlsx"


        Logger.Log("Przetwarzanie danych", 0, 0);
        List<CrewFileModel> l = CrewFileModel.GetList(ds.Tables[0], columnsConfiguration,Logger);

        Logger.Log("Import do bazy danych", 0, 0);

        List<CrewModel> cls = new List<CrewModel>();
        int i = 0;
        foreach (var cm in l)
        {            
            cls.Add(new CrewModel(cm,ZoneIfNull));
            
            double db = (double)(((double)i / (double)l.Count) * 100);

            Logger.Log("Import do bazy danych", db, 0);
            i++;
        }
        return cls;

    }

    public static DataSet ExcelToDataSet(string FileName, int columnsToImportCount,LoggerType Logger)
    {
        DataSet ds = new DataSet();
        string filePath = FileName;
        Logger.Log("Odczyt pliku", 0, 0);

        if (System.IO.Path.GetExtension(FileName) != ".xlsx")
        {

            Logger.Log("Plik w niepoprawnym formacie, można zaimportować tylko pliki .xlsx", 0, 0);
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {

            }
            ds.Tables.Add(new DataTable() { TableName = "empty" });
            return ds;
        }
        

        using (XLWorkbook workBook = new XLWorkbook(filePath))
        {           
            foreach (var workSheet in workBook.Worksheets)
            {
                int lastrow = workSheet.LastRowUsed().RowNumber();
                var rows = workSheet.Rows(1, lastrow);



                int rowIndex = 0;
                int max = rows.Count();
                DataTable dt = new DataTable();
                dt.TableName = workSheet.Name;              
                bool firstRow = true;

                foreach (IXLRow row in rows)
                {
                    IXLCell c = row.Cell(1);
                    if (firstRow)
                    {
                        int i = 0;
                        int rowcellcount = row.CellsUsed().Count();
                        if (rowcellcount >= columnsToImportCount)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                if (i <= columnsToImportCount)
                                {
                                    string b = cell.Value.ToString();
                                    b = b.Replace("\n", "").Replace("\n", "").Replace("\n", " ");
                                    cell.Value = b;
                                    dt.Columns.Add(b);
                                }
                                else
                                {
                                    break;
                                }
                                i++;
                            }
                            firstRow = false;
                        }
    

                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.CellsUsed())
                        {
                            if (i <= columnsToImportCount)
                            {
                                //string a = cell.WorksheetColumn().FirstCell().Value.ToString();
                                //a = a.Replace("\n", " ");
                                //if (!dt.Columns.Contains(a))
                                //    dt.Columns.Add(a);
                                //if (!string.IsNullOrEmpty(a))
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            }
                            else { break; }
                            i += 1;
                        }
                    }
                    rowIndex += 1;
                    double db = (double)(((double)rowIndex / (double)max) * 100);
                    Logger.Log("Odczytano wiersz " + rowIndex + "/" + max, db, rowIndex);
                }
                ds.Tables.Add(dt);
            }
        }

        try
        {
            System.IO.File.Delete(filePath);
        }
        catch (Exception)
        {

        }

        return ds;
    }




    public static async Task GeneretePDFAsync(string outputFile, string html)
    {
        //string testStylesheet =System.IO.File.ReadAllText( "wwwroot/css/bootstrap/bootstrap.min.css");
        //testStylesheet += " " + System.IO.File.ReadAllText("wwwroot/css/site.css");

        string testStylesheet = System.IO.File.ReadAllText("wwwroot/css/print.css");
        //testStylesheet += " " + System.IO.File.ReadAllText("");

        VetCV.HtmlRendererCore.Core.CssData cssData = VetCV.HtmlRendererCore.PdfSharpCore.PdfGenerator.ParseStyleSheet(testStylesheet, true); 
     
            string htmlString = html;            
            PdfDocument pdf = PdfGenerator.GeneratePdf(htmlString, PageSize.A4,20,cssData);
            pdf.Save(outputFile);
        
    }


    public static System.Drawing.Image GetQrCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            return new System.Drawing.Bitmap(30, 30);

        byte[] imageData;

        var qrWriter = new ZXing.BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions { Height = 100, Width = 100, Margin = 0 }
        };

        var pixelData = qrWriter.Write(code);
        Byte[] byteArray;
        Image img;
        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        {
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byteArray = ms.ToArray();
                img = Image.FromStream(ms);
            }
        }

        return img;
    }
    public static System.Drawing.Image GetBAR128Code(string code)
    {
        if (string.IsNullOrEmpty(code))
            return new System.Drawing.Bitmap(30,30);

        byte[] imageData;

        var qrWriter = new ZXing.BarcodeWriterPixelData
        {
            Format = BarcodeFormat.CODE_128
        };

        var pixelData = qrWriter.Write(code);
        Byte[] byteArray;
        Image img;
        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        {
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byteArray = ms.ToArray();
                img = Image.FromStream(ms);
            }
        }

        return img;
    }

    public static Image byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }
    public static string Base64(Image img)
    {
        return Convert.ToBase64String(imageToByteArray(img));
    }
    public static string Base64(byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
    public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();
    }

}


