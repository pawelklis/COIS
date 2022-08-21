using System;
using System.Collections.Generic;

public class ReportField_List:IReportField, INoDatabase
{

    public ReportField_List()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? DefaultValue { get; set; }
    public int ValueInt { get; set; }
    public string Value { get; set; }
    public string? Placeholder { get; set; }
    public string? PossibleValues { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public string? BindingValue { get; set; }
    public bool UseBindingValues { get; set; }
    public bool OrgUnitFilter { get; set; }
    public bool ZoneFilter { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public string ValueToString()
    {
        if (Value == null)
            return "";
        else
            return Value.ToString();
    }

    public List<Tuple<int, string>> GetBindingValues(ProgramInstance cois)
    {
        List<Tuple<int, string>> vals = new List<Tuple<int, string>>();

        AppDBContext db = new AppDBContext();

        switch (BindingValue)
        {
            case "Pracownicy":
                foreach (var o in GetEmployees(cois))
                {
                    vals.Add(new Tuple<int, string>(o.Id, o.FullName()));
                }
                break;
            case "Strefy":
                foreach (var o in GetZones(cois))
                {
                    vals.Add(new Tuple<int, string>(o.Id, o.Name));
                }
                break;
            case "Jednostki":
                foreach (var o in GetOrgs(cois))
                {
                    vals.Add(new Tuple<int, string>(o.Id, o.Name));
                }
                break;
            default:
                break;
        }

        return vals;
    }

    public List<string> PossibleValuesList()
    {
        List<string> vals = new List<string>();
        if(PossibleValues==null)
            PossibleValues = "";

        vals.AddRange(PossibleValues.Split(','));
        return vals;
    }


    public List<OrgUnit> GetOrgs(ProgramInstance cois)
    {
        AppDBContext db = new AppDBContext();
        if (this.OrgUnitFilter && this.ZoneFilter)
            return db.OrgUnits.ToList().Where(x => x.Id == cois.Zone.OrganizationUnit().Id).ToList();
        if (this.OrgUnitFilter)
            return db.OrgUnits.ToList().Where(x => x.Id == cois.Zone.OrganizationUnit().Id).ToList();
        if (this.ZoneFilter)
            return db.OrgUnits.ToList().Where(x => x.Id == cois.Zone.OrganizationUnit().Id).ToList();

        return db.OrgUnits.ToList();
    }

    public List<ZoneType> GetZones(ProgramInstance cois)
    {
        AppDBContext db = new AppDBContext();

        if (this.OrgUnitFilter && this.ZoneFilter)
            return db.Zones.ToList().Where(x => x.Id == cois.Zone.Id).ToList();
        if (this.OrgUnitFilter)
            return db.Zones.ToList().Where(x => x.OrganizationUnit().Id == cois.Zone.OrganizationUnit().Id).ToList();
        if (this.ZoneFilter)
            return db.Zones.ToList().Where(x => x.Id == cois.Zone.Id).ToList();


        return db.Zones.ToList();
    }

    public List<EmployeeType> GetEmployees(ProgramInstance cois)
    {
        AppDBContext db = new AppDBContext();

        if (this.OrgUnitFilter && this.ZoneFilter)
            return db.Employees.ToList().Where(x => x.Zone.Id == cois.Zone.Id && x.Zone.OrganizationUnit().Id == cois.Zone.OrganizationUnit().Id).ToList();
        if (this.OrgUnitFilter)
            return db.Employees.ToList().Where(x => x.Zone.OrganizationUnit().Id == cois.Zone.OrganizationUnit().Id).ToList();
        if (this.ZoneFilter)
            return db.Employees.ToList().Where(x => x.Zone.Id == cois.Zone.Id).ToList();


        return db.Employees.ToList();
    }

}