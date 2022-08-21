using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DelegationType : IDatabase
{
    public static event EventHandler<DelegationType> OnDelegationStart;
    public static event EventHandler<DelegationType> OnDelegationEnd;

    private bool closed;
    private EmployeeType employee;
    private ZoneType fromZone;
    private ZoneType toZone;
    private DateTime startTime;
    private DateTime endTime;
    private string description;

    public DelegationType()
    {
        Closed = false;
    }
    public int Id { get; set; }
    public EmployeeType Employee { get => employee; set { employee = value; } }
    public ZoneType FromZone { get => fromZone; set { fromZone = value; ; } }
    public ZoneType ToZone { get => toZone; set { toZone = value; SetReportTo(new AppDBContext()); } }
    public DateTime StartTime { get => startTime; set { startTime = value; Save(); } }
    public DateTime EndTime { get => endTime; set { endTime = value; Save(); } }
    public string Description { get => description; set { description = value; Save(); } }
    public bool Closed { get => closed; set { closed = value; ; } }
    public int ReportFromId { get; set; }
    public int ReportToId { get; set; }
    public void Save()
    {
        if(description==null)
            description="";

        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);
    }

    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.RemovetObject(this);
    }
    public static DelegationType NewDelegation(int ReportID,ZoneType fromZone, ZoneType toZone, DateTime start, DateTime end, EmployeeType employee, string description = null)
    {
        AppDBContext db = new AppDBContext();

        DelegationType d = new DelegationType()
        {
            FromZone = fromZone,
            ToZone = toZone,
            StartTime = start,
            EndTime = end,
            Employee = employee,
            Description = description,
            ReportFromId = ReportID,            
            Closed=false
        };

        d.SetReportTo(db);

        d.Save();

        EventHandler<DelegationType> handler = OnDelegationStart;
        handler?.Invoke(d, d);
        
        return d;
    }

    public void EndDelegation()
    {
        this.Closed = true;
        Save();


        EventHandler<DelegationType> handler = OnDelegationEnd;
        handler?.Invoke(this, this);
    }

    private void SetReportTo(AppDBContext db)
    {
        try
        {
            ReportToId = db.Reports.ToList().Where(x => x.Closed = false).ToList()
                                    .Where(x => x.Zone.Id == ToZone.Id).ToList()
                                    .Where(x => x.GetModel().HasDelegations == true).FirstOrDefault().Id;
        }
        catch (Exception ex)
        {

            ReportToId = 0;
        }

    }

    public static List<DelegationType> DelegationHistory(int employeeid, DateTime startDate, DateTime endDate)
    {
        AppDBContext db = new AppDBContext();
        return db.Delegations.ToList().Where(x => x.Employee.Id == employeeid &&
                                    x.startTime >= startDate &&
                                    x.endTime <= endDate).ToList();
    }


}

