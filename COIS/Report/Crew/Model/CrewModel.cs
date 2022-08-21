using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CrewModel:IDatabase
{
    public int Id { get; set; }
    public EmployeeType Employee { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ZoneType Zone { get; set; }
    public WorkHourType WorkHourTyp { get; set; }
    public bool IsWorking { get; set; }

    public CrewModel() { }
    public CrewModel(CrewFileModel model, ZoneType ZoneIfNull)
    {
        StartTime = model.StartTimeDate();
        EndTime = model.EndTimeDate();

        if (StartTime == EndTime)
            IsWorking = false;
        else
            IsWorking = true;

        Zone = ZoneType.GetZoneByAlternativeName(model.ZoneName);
        if (Zone == null)
            Zone = ZoneIfNull;

        Employee = EmployeeType.GetOrAddEmployee(model.EmployeeNumber, model.EmployeeName, Zone);

        

        WorkHourTyp = WorkHourType.GetOrAdd(model.WorkTypeName);



        Save();
    }

    public bool IsWork(DateTime starttime, DateTime endtime)
    {


        if (IsWorking == false)
            return false;

        bool datechange = false;
        if (starttime.Day != endtime.Day)
            datechange = true;


        if (datechange == true)
        {
            //odg >='" & cod & "' and dog <= '" & cdo &
            if (StartTime >= starttime && EndTime <= endtime)
                return true;
            // odg >'" & cod & "' and odg < '" & cdo & "' and dog > '" & cdo &
            if (StartTime > starttime && StartTime <endtime  && EndTime > endtime)
                return true;

            // odg <'" & cod & "' and dog < '" & cdo & "' and dog>'" & cod & 
            if (StartTime <  starttime  && EndTime <  endtime && EndTime> starttime)
                return true;
        }
        else
        {
            //odg >='" & cod & "' and dog <= '" & cdo
            if (StartTime >= starttime  && EndTime <=  endtime)
                return true;
            //odg>'" & cod & "' and odg < '" & cdo & "' and dog > '" & cdo
            if (StartTime >  starttime  && StartTime<  endtime  && EndTime >  endtime)
                return true;
            //odg <'" & cod & "' and dog > '" & cod
            if (StartTime <  starttime && EndTime >  starttime)
                return true;
        }




        return false;
    }
    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.RemovetObject(this);
    }

    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        if (StartTime.Hour != 0 && EndTime.Hour != 0)
            IsWorking = true;
        else
            IsWorking = false;
        if (StartTime.Hour != 0)
            IsWorking = true;
        if (EndTime.Hour != 0)
            IsWorking = true;

        AppDBContext db = new AppDBContext();
        CrewModel m = db.CrewModels.ToList().Where(x => x.Employee.Id == this.Employee.Id && x.StartTime == this.StartTime && x.EndTime == this.EndTime && x.Zone.Id==this.Zone.Id).FirstOrDefault();
        if (m != null)
        {
            db.CrewModels.Remove(m);
            db.SaveChangesAsync();
        }


        ap.UpdateObjectAsync(this);
    }

    public static List<CrewModel> GetCrewModels(int year,int month,int zoneid)
    {
        AppDBContext db = new AppDBContext();
        List<CrewModel> models = db.CrewModels.ToList().Where(x => x.StartTime.Year == year &&
                                                               x.StartTime.Month == month &&
                                                               x.Zone.Id == zoneid).ToList();
        return models;
    }
}

