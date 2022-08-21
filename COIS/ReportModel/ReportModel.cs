using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ReportModel:IDatabase
{

    public int Id { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string? Description { get; set; }
    public List<ReportModelItem> Items { get; set; }
    public List<ZoneType> EnableZones { get; set; }
    public bool HasCrew { get; set; }
    public bool HasDelegations { get; set; }

    public bool IsOpen(int zoneId)
    {
        AppDBContext db = new AppDBContext();
        try
        {
            int i = db.Reports.ToList().Where(x => x.ModelId == this.Id && x.Zone.Id==zoneId && x.Closed == false).Count();
            if (i == 0)
                return false;
        }
        catch (Exception ex)
        {

        }


        return true;
    }

    public ReportModel()
    {
        Items = new List<ReportModelItem>();
        Name = "";
        Description = "";
        EnableZones = new List<ZoneType>();
    }
    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);            

    }



    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.RemovetObject(this);
    }

    public static ReportModel GetReportModel(int id)
    {
        AppDBContext ad = new AppDBContext();
        ReportModel m = ad.ReportModels.Where(x => x.Id == id).FirstOrDefault();

        return m;
    }

    public static List<ReportModel> GetReportModels(int OrgUnitId)
    {
        AppDBContext ad = new AppDBContext();
        List<ReportModel> m = (from o in ad.ReportModels.ToList()
                               from z in o.EnableZones.ToList()
                               where z.OrganizationUnit().Id == OrgUnitId
                               select o).Distinct().ToList();
        
        return m;
    }
    public static List<ReportModel> GetReportModels(int OrgUnitId, int zoneId)
    {
        AppDBContext ad = new AppDBContext();
        List<ReportModel> m = (from o in ad.ReportModels.ToList()
                               from z in o.EnableZones.ToList()
                               where z.OrganizationUnit().Id == OrgUnitId && z.Id==zoneId
                               select o).ToList();

        return m;
    }
}

