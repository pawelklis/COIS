using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


public class ReportType:IDatabase
{
    public event EventHandler<DelegationType> OnNewDelegationStart;
    public event EventHandler<DelegationType> OnNewDelegationEnd;
    public event EventHandler OnSaved;


    public int Id { get; set; }
    public int ModelId { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
    public CrewType Crew { get; set; }
    public UserType User { get; set; }
    public ZoneType Zone { get; set; }
    public WorkShift WorkShift { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Number { get; set; }
    public List<ReportItem> Items { get; set; }
    public List<DelegationType> DelegationsFrom(bool closed = false)
    {
        AppDBContext db = new AppDBContext();
        return db.Delegations.ToList().Where(x => x.FromZone.Id == this.Zone.Id).ToList().Where(x => x.Closed == closed).ToList(); 
    }
    public List<DelegationType> DelegationsTo(bool closed = false)
    {
        AppDBContext db = new AppDBContext();
        return db.Delegations.ToList().Where(x => x.ToZone.Id == this.Zone.Id).ToList().Where(x => x.Closed == closed).ToList();
    }
    public List<DelegationType> Delegations()
    {
        AppDBContext db = new AppDBContext();

        List<DelegationType> l = db.Delegations.ToList().Where(x => 
            (x.ToZone.Id == this.Zone.Id || x.FromZone.Id == this.Zone.Id) &&
            (x.StartTime >= this.DateStart && x.EndTime<=this.DateEnd)||
            (x.StartTime>=this.DateStart && x.StartTime<=this.DateEnd && x.EndTime >=this.DateEnd) ||
            (x.StartTime<=this.DateStart && x.EndTime>=this.DateStart && x.EndTime<=this.DateEnd)
        ).ToList().OrderBy(x=>x.StartTime).ToList();

        return l;
    }
    public List<DelegationType> DelegationsEnded()
    {
        List<DelegationType> l = new List<DelegationType>();
        l.AddRange(DelegationsFrom(true).ToList().Where(x=>x.StartTime>=this.DateStart && x.EndTime<= this.DateEnd).ToArray());
        l.AddRange(DelegationsTo(true).ToArray().Where(x => x.StartTime >= this.DateStart && x.EndTime <= this.DateEnd).ToArray());
        return l;
    }
    public List<EmployeeType> EnableToDelegateEmployees()
    {
        AppDBContext db = new AppDBContext();
        List<DelegationType> l = DelegationsFrom();
        l.AddRange(DelegationsTo());

        List<EmployeeType> dele = new List<EmployeeType>();
        l.ForEach(x => { dele.Add(x.Employee); });

        return Crew.Employees().ToList().Except(dele.ToList()).ToList();
    }
    public bool Closed { get; set; }

    public ReportType()
    {
        DelegationType.OnDelegationStart += DelegationType_OnDelegationStart;
        DelegationType.OnDelegationEnd += DelegationType_OnDelegationEnd;
    }
    public ReportType(ReportModel model,ZoneType zone,WorkShift workShift, DateTime date, UserType user, ProgramInstance cois)
    {

        ModelId = model.Id;
        User = user;
        Items = new List<ReportItem>();
        Name = "";
        Description = "";
        Zone = zone;
        WorkShift = workShift;
        DateStart =new DateTime(date.Year,date.Month,date.Day,workShift.StartTime.Hours,workShift.StartTime.Minutes,0);
        DateEnd = DateStart.AddMinutes(workShift.Timeminutes());

        Crew = new CrewType();
        if (model.HasCrew == true)
            Crew.Import(Zone, DateStart, DateEnd);

        model.Items.ForEach(x => {
            if (x.EnabledAdWorkshifts.Find(w=>w.Id== workShift.Id) !=null)
                Items.Add(x.NewItem(workShift, cois));
        });

        int i = 0;
        this.Items.ForEach(x => { x.OrderNumber = i;i++; });

        SetNumber();

        Closed = false;
    }


    public static List<ReportType> GetReports(DateTime date, int zoneid)
    {
        AppDBContext db = new AppDBContext();
        return db.Reports.ToList().Where(x=>x.DateStart.ToShortDateString()==date.ToShortDateString() && x.Zone.Id==zoneid).ToList();
    }
    public static List<ReportType> GetReports(int orgunitid,string phrase)
    {
        AppDBContext db = new AppDBContext();
        //List<ReportType> l =  db.Reports.ToList().Where(x=>x.DateStart.ToShortDateString()==date.ToShortDateString() && x.Zone.Id==zoneid).ToList();
        return db.Reports.ToList().Where(r =>r.Zone.OrganizationUnitID==orgunitid && ( r.User.Login.ToLower().Contains(phrase) ||
                            r.User.Employee.FullName().ToLower().Contains(phrase) ||
                            r.Crew.Items.Any(c => c.Employee.FullName().ToLower().Contains(phrase)) ||
                            r.Crew.Items.Any(c => c.Activities.Any(a => a.Name.ToLower().Contains(phrase))) ||
                            r.Items.Any(i=> i.Name.ToLower().Contains(phrase)) ||
                            r.Items.Any(i => i.AllFields().Any(f => f.ValueToString().ToLower().Contains(phrase))))).ToList();


    }
    private void DelegationType_OnDelegationEnd(object? sender, DelegationType e)
    {
        if(e.FromZone.Id==this.Zone.Id || e.ToZone.Id == this.Zone.Id)
        {
            EventHandler<DelegationType> handler = OnNewDelegationEnd;
            handler?.Invoke(this, e);
        }
    }

    private void DelegationType_OnDelegationStart(object? sender, DelegationType e)
    {
        if (e.FromZone.Id == this.Zone.Id || e.ToZone.Id == this.Zone.Id)
        {
            EventHandler<DelegationType> handler = OnNewDelegationStart;
            handler?.Invoke(this, e);
        }
    }

    public List<CrewItem> CrewDelegatedFromOtherReports()
    {
        List<CrewItem> items = new List<CrewItem>();

        foreach(var del in DelegationsTo())
        {
            ReportType report = ReportType.GetReport(del.ReportFromId);
            if (report != null)
            {
                CrewItem cr = report.Crew.Items.Find(x=>x.Employee.Id==del.Employee.Id);
                if (cr != null)
                {
                    items.Add(cr);
                }
            }
        }

        return items;
    }
    public ReportModel GetModel()
    {
        return ReportModel.GetReportModel(ModelId);
    }
    private void SetNumber()
    {
        AppDBContext ad = new AppDBContext();
        int ct = ad.Reports.ToList().Count(x => x.Zone.Id == Zone.Id);

        string nb = (int)ct + 1 + "/" + DateStart.Month + "/" + DateStart.Year + "/" + Zone.Code;
        this.Number = nb;
    }
    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);

        EventHandler handler = OnSaved;
        handler?.Invoke(this, null);
    }

    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.RemovetObject(this);
    }

    public static ReportType GetReport(int id)
    {
        AppDBContext ad = new AppDBContext();
        ReportType rp = ad.Reports.Where(x => x.Id == id).FirstOrDefault();
        return rp;
    }


    public static ReportType GetReport(int modelid, int zoneid, bool closed)
    {
        AppDBContext ad = new AppDBContext();
        ReportType rp = ad.Reports.ToList().Where(x => x.ModelId == modelid && x.Zone.Id==zoneid && x.Closed==closed).FirstOrDefault();
        return rp;
    }


    public void EndReport()
    {
        this.Closed = true;
        this.Save();
    }

    public string PrintReport()
    {
        string filePath = @"wwwroot/pdfDocs/" + this.Id +".pdf";
        string html = "<h1 class='Header'>test</h1>";

        HtmlGenerator htmlGenerator = new HtmlGenerator();
        HtmlGenerator.HtmlElementSection section;

        section = new HtmlGenerator.HtmlElementSection();
        section.Elements.Add(new HtmlGenerator.HtmlElementHeader(2) { Text = "Raport " + this.Number });
        htmlGenerator.Elements.Add(section);

        htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementLabel() { Text = User.Employee.FullName(), Name = "Sporządzony przez" });

        htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementHeader(4) { Text = "Obsada" });
        HtmlGenerator.HtmlElementTable crewTable = new HtmlGenerator.HtmlElementTable();
        crewTable.Table = Crew.toTable();
        htmlGenerator.Elements.Add(crewTable);

        htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementHeader(4) { Text = "Delegowanie" });
        HtmlGenerator.HtmlElementTable delTable = new HtmlGenerator.HtmlElementTable();
        delTable.Table = DelegationsToTable();
        htmlGenerator.Elements.Add(delTable);


        foreach (var item in Items)
        {

            foreach(var field in item.BoolFields)
            {
                htmlGenerator.AddStartSection();
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementCheckBox() { Text = field.Name, Value = field.Value }) ;
                htmlGenerator.AddEndSection();
            }
            foreach (var field in item.TextFields)
            {
                htmlGenerator.AddStartSection();
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementLabel() { Text = field.Value, Name = field.Name });
                htmlGenerator.AddEndSection();
            }
            foreach (var field in item.NumberFields)
            {
                htmlGenerator.AddStartSection();
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementLabel() { Text = field.Value.ToString(), Name = field.Name });
                htmlGenerator.AddEndSection();
            }
            foreach (var field in item.ListFields)
            {
                htmlGenerator.AddStartSection();
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementLabel() { Text = field.Value, Name=field.Name });
                htmlGenerator.AddEndSection();
            }
            foreach (var field in item.TableFields)
            {
                htmlGenerator.AddStartSection();
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementLabel() { Text = Name = field.Name });
                htmlGenerator.Elements.Add(new HtmlGenerator.HtmlElementTable() { Table = field.toTable() });
                htmlGenerator.AddEndSection();
            }
        }






        FileHelper.GeneretePDFAsync(filePath, htmlGenerator.toHtmlString());
        return filePath;
    }

    private DataTable DelegationsToTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Pracownik");
        dt.Columns.Add("Ze strefy");
        dt.Columns.Add("Do strefy");
        dt.Columns.Add("Czas rozpoczęcia");
        dt.Columns.Add("Czas zakończenia");
        dt.Columns.Add("Uwagi");

        List<DelegationType> l = Delegations();

        foreach(var d in l)
        {
            dt.Rows.Add(d.Employee.FullName(), d.FromZone.Name, d.ToZone.Name, d.StartTime, d.EndTime, d.Description);
        }

        return dt;
    }
}

