using System.Data;

    public class EmployeeService
    {
        public DataTable GetEmployees()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Imię");
            dt.Columns.Add("Nazwisko");
            dt.Columns.Add("Barkod", typeof(byte[]));
            dt.Columns.Add("KodQR", typeof(byte[]));
            dt.Columns.Add("Strefa");

            foreach(var em in EmployeeType.GetEmployees())
            {
                dt.Rows.Add(em.FirstName,
                    em.LastName,
                    FileHelper.imageToByteArray(FileHelper.GetBAR128Code(em.EmployeeCode)),
                    FileHelper.imageToByteArray(FileHelper.GetQrCode(em.EmployeeCode)),
                    em.Zone.Name
                );
            }

            return dt;
        }
    }

