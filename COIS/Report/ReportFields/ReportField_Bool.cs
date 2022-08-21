

using System.Collections.Generic;

public class ReportField_Bool:IReportField, INoDatabase
{
    public ReportField_Bool()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool DefaultValue { get; set; }
    public string? Placeholder { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public int? ReportModelItemId { get; set; }
    public bool Value { get; set; }
    public string ValueToString()
    {
        if (Value == null)
            return "";
        else
            return Value.ToString();
    }
}