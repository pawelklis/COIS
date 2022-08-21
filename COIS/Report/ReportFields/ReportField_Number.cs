

using System.Collections.Generic;

public class ReportField_Number:IReportField, INoDatabase
{
    public ReportField_Number()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double DefaultValue { get; set; }
    public string? Placeholder { get; set; }
    public double Value { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public string ValueToString()
    {
        if (Value == null)
            return "";
        else
            return Value.ToString();
    }
}