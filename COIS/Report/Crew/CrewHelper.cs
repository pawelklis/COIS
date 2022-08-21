using System;
using System.Collections.Generic;
using System.Text;


public class CrewHelper
{

    public static List<CrewStatDataDiagram> EmployeeWorkInHours(DateTime startInterval, DateTime endInterval, CrewItem crew)
    {
        List<CrewStatDataDiagram> l = new List<CrewStatDataDiagram>();

        for (int i = iFrom(startInterval); i < iTo(startInterval, endInterval); i++)
        {
            DateTime cH = new DateTime(crew.StartTime.Year, crew.StartTime.Month, crew.StartTime.Day, i, 0, 0);
            if (crew.IsActiveHour(cH, crew.StartTime, crew.EndTime))
            {
                l.Add(new CrewStatDataDiagram() { Hour = new TimeSpan(i, 0, 0), Count = 1, Category = "" });
            }
            else
            {
                l.Add(new CrewStatDataDiagram() { Hour = new TimeSpan(i, 0, 0), Count = 0, Category = "" });
            }
        }

        return l;
    }


    static int iFrom(DateTime startDate)
    {
        return startDate.Hour;
    }
    static int iTo(DateTime startDate, DateTime endDate)
    {
        return (int)startDate.Hour + (int)(endDate - startDate).TotalHours;
    }

}



public class CrewStatDataDiagram
{
    public TimeSpan Hour { get; set; }
    public string Category { get; set; }
    public int Count { get; set; }

}


