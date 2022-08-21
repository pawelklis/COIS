using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


public class CrewFileModel:INoDatabase
{
    public CrewFileModel()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string ZoneName { get; set; }
    public string EmployeeNumber { get; set; }
    public string EmployeeName { get; set; }
    public DateTime Date { get; set; }
    public double StartTime { get; set; }
    public double WorkTime { get; set; }
    public string WorkTypeName { get; set; }





    public DateTime StartTimeDate()
    {
        return new DateTime(Date.Year, Date.Month, Date.Day, StartTimeSpan().Hours, StartTimeSpan().Minutes, 0);
    }
    public DateTime EndTimeDate()
    {
        return StartTimeDate().AddMinutes(WorkTimeSpan().TotalMinutes);
    }
    public TimeSpan WorkTimeSpan()
    {
        return TimeSpan.FromHours(WorkTime);
    }
    public TimeSpan StartTimeSpan()
    {       
        return TimeSpan.FromHours(StartTime);
    }
    public DateTime EndTime()
    {
        TimeSpan et = WorkTimeSpan() -TimeSpan.Parse(StartTime.ToString());
        return new DateTime(Date.Year, Date.Month, Date.Day, et.Hours, et.Minutes, 0);
    }
    public static List<Tuple<string,int>> ColumnsConfiguration()
    {
        List<Tuple<string, int>> config = new List<Tuple<string, int>>();

        config.Add(new Tuple<string, int>("ZoneName", 0));
        config.Add(new Tuple<string, int>("EmployeeNumber", 1));
        config.Add(new Tuple<string, int>("EmployeeName", 2));
        config.Add(new Tuple<string, int>("Date", 3));
        config.Add(new Tuple<string, int>("StartTime", 4));
        config.Add(new Tuple<string, int>("WorkTime", 5));
        config.Add(new Tuple<string, int>("WorkTypeName", 6));

        return config;
    }
    public static event EventHandler OnImportProgress;
    public static List<CrewFileModel> GetList(DataTable dt, List<Tuple<string, int>> columnsConfiguration, LoggerType Logger)
    {
        List<CrewFileModel> l = new List<CrewFileModel>();


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            CrewFileModel cm = new CrewFileModel();
            cm.ZoneName = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "ZoneName").Item2].ToString();
            cm.EmployeeNumber = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "EmployeeNumber").Item2].ToString();
            cm.EmployeeName = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "EmployeeName").Item2].ToString();
            try
            {
                string d = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "Date").Item2].ToString();
                cm.Date =DateTime.Parse(d);
            }
            catch (Exception)
            {
                //wiersz i nie zaimportowany
            }


            string st = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "StartTime").Item2].ToString();
            if(!string.IsNullOrEmpty(st))                
                cm.StartTime =double.Parse(st);

            try
            {
                string dd = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "WorkTime").Item2].ToString();
                cm.WorkTime = double.Parse(dd);
            }
            catch (Exception)
            {
                //wiersz i nie zaimportowany
            }


            cm.WorkTypeName = dt.Rows[i][columnsConfiguration.Find(x => x.Item1 == "WorkTypeName").Item2].ToString();



            
            if(!string.IsNullOrEmpty(cm.EmployeeName))
                l.Add(cm);

            EventHandler handler = OnImportProgress;
            double db = (double)(((double)i / (double)dt.Rows.Count) * 100);

            Logger.Log("Przetworzono obiekt " + i +"/" + dt.Rows.Count, db, i);
        }

        return l;
    }

}

