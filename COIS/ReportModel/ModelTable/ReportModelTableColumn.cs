using System.Collections.Generic;
using System.Linq;
using static ReportModelItem;

public class ReportModelTableColumn:INoDatabase
{
    public ReportModelTableColumn()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? DefaultValue { get; set; }
    public bool Lock { get; set; }
    public ReportFieldTypes ColumnType {get; set;}
    public string? PossibleValues { get; set; }
    public bool UseBindingValues { get; set; }
    public bool OrgUnitFilter { get; set; }
    public bool ZoneFilter { get; set; }
    public string? BindingValue { get; set; }
    public ReportTableColumn NewItem()
    {
        ReportTableColumn item = new ReportTableColumn();
        item.Name = Name;
        item.Description = Description;
        item.Lock = Lock;
        item.ColumnType = ColumnType;
        item.OrgUnitFilter = OrgUnitFilter;
        item.ZoneFilter = ZoneFilter;
        item.PossibleValues = PossibleValues;
        item.BindingValue = BindingValue;
        item.UseBindingValues = UseBindingValues;

        return item;
    }

    public static ReportModelTableColumn NewItem(ReportTableColumn mc)
    {
        ReportModelTableColumn item = new ReportModelTableColumn();
        item.Name = mc.Name;
        item.Description = mc.Description;
        item.Lock = mc.Lock;
        item.ColumnType = mc.ColumnType;
        item.OrgUnitFilter = mc.OrgUnitFilter;
        item.ZoneFilter = mc.ZoneFilter;
        item.PossibleValues = mc.PossibleValues;
        item.BindingValue = mc.BindingValue;
        item.UseBindingValues = mc.UseBindingValues;

        return item;
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
        if (PossibleValues == null)
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



    public static ReportModelItem.ReportFieldTypes GetFieldType(string v)
    {
        switch (v)
        {
            case "Text":
                return ReportFieldTypes.Text;                
            case "LongText":
                return ReportFieldTypes.LongText;
            case "Number":
                return ReportFieldTypes.Number;
            case "Bool":
                return ReportFieldTypes.Bool;
            case "List":
                return ReportFieldTypes.List;
            case "Table":
                return ReportFieldTypes.Table;
            default:
                return ReportFieldTypes.Text; 
        }
    }


}