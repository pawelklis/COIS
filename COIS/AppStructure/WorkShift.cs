using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

public class WorkShift:IDatabase
{
    public int Id { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Godzina rozpoczęcia", FrontendType.text, true)]
    public TimeSpan StartTime { get; set; }
    [FrontEnd("Godzina zakończenia", FrontendType.text, true)]
    public TimeSpan EndTime { get; set; }
    public ZoneType Zone { get; set; }

    public string HoursWorkString()
    {
        return StartTime.Hours.ToString("00")
            + ":" + StartTime.Minutes.ToString("00")
            + "-" + EndTime.Hours.ToString("00")
            + ":" + EndTime.Minutes.ToString("00");
    }

    public int Timeminutes()
    {
        int tm = 0;

        tm = (int)(EndTime - StartTime).TotalMinutes;
        return tm;
    }

    public static WorkShift GetWorkShift(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.WorkShifts.Where(x => x.Id == id).FirstOrDefault();
    }

    public static List<WorkShift> GetWorkShifts(int zoneID)
    {
        AppDBContext db = new AppDBContext();
        return db.WorkShifts.ToList().Where(x => x.Zone.Id == zoneID).ToList();
    }

    public static List<WorkShift> GetWorkShifts(int[] zoneID)
    {
        List<WorkShift> ws = new List<WorkShift>();
        AppDBContext db = new AppDBContext();
        foreach(var z in zoneID)
        {
            ws.AddRange(db.WorkShifts.Where(x => x.Zone.Id == z).ToArray());
        }        

        return ws;
    }
    public static List<WorkShift> GetWorkShifts(ZoneType[] zoneID)
    {
        List<WorkShift> ws = new List<WorkShift>();
        AppDBContext db = new AppDBContext();
        foreach (var z in zoneID)
        {
            ws.AddRange(db.WorkShifts.ToList().Where(x => x.Zone.Id == z.Id).ToArray());
        }

        return ws;
    }
    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.RemovetObject(this);
    }

    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.UpdateObject(this);
    }
}

