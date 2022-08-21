

using System.Collections.Generic;
using System.Linq;
using static ReportItem;
using static ReportModelItem;

public class ReportTableColumn:INoDatabase
{
    public ReportTableColumn()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Lock { get; set; }
    public ReportFieldTypes ColumnType { get; set; }
    public bool OrgUnitFilter { get; set; }
    public bool ZoneFilter { get; set; }
    public string PossibleValues { get; set; }
    public string BindingValue { get; set; }
    public bool UseBindingValues { get; set; }

    public List<string> PossibleValuesList()
    {
        if (PossibleValues == null)
            PossibleValues = "";
        return PossibleValues.Split(',').ToList();
    }
}