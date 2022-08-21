using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

public class CrewItem : INoDatabase
{

    public string Guid { get; set; }
    public EmployeeType Employee { get; set; }
    public bool Present { get; set; }
    DateTime _st;
    DateTime _et;
    public DateTime StartTime { get { return _st;  } 
        set { _st = value; } }
    public DateTime EndTime { get { return _et; } set { _et = value; } }
    public List<ActivityType> Activities { get; set; }

    public int ScannerId { get; set; }
    public List<int> ForkLifts { get; set; }

    public bool Selected;
    public CrewItem()
    {
        Guid = System.Guid.NewGuid().ToString();
        Activities = new List<ActivityType>();
    }

    public CrewItem(DateTime starttime, DateTime endtime, EmployeeType employee)
    {

        Guid = System.Guid.NewGuid().ToString();
        StartTime = starttime;
        EndTime = endtime;
        Employee = employee;
        Activities = new List<ActivityType>();
        Present = true;
    }

    public List<Tuple<string,DateTime>> TimeLaps()
    {
        List<Tuple<string, DateTime>> l = new List<Tuple<string, DateTime>>();

        foreach(var act in Activities)
        {
            l.Add(new Tuple<string, DateTime>(act.Name, act.Time));
        }
        foreach (var del in DelegationType.DelegationHistory(Employee.Id, StartTime, EndTime))
        {
            l.Add(new Tuple<string, DateTime>(del.ToZone.Name, del.StartTime));
        }

        return l.OrderBy(x=>x.Item2).ToList();
    }
    public async Task<string> GetStatus()
    {
        string status = "";
        return await Task.Run(() => {
            if (this.Present == false) {
                status= "Nieobecny";                 
            }

            if (status == "")
            {
                DelegationType del = this.Employee.ActiveDelegation();
                if (del != null) 
                {
                    status = "W delegacji (" + del.ToZone.Name + ")"; 
                } 
                else
                { 
                    status = "Na stanowisku";
                };
            }
            return status;
        }).ConfigureAwait(false);
        
    }


    public bool IsActiveHour(DateTime hour, DateTime startTime, DateTime endTime)
    {
        if (endTime.ToString() == "0001-1-1 12:00:00 AM")
            endTime = startTime.AddHours(12);

        if (hour >= startTime)
        {
            if (hour < endTime)
            {
                return true;
            }
        }

        return false;
    }





}

