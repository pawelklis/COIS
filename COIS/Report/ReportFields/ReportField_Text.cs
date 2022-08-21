

using System.Collections.Generic;

public class ReportField_Text : IReportField, INoDatabase
{
    private string value;

    public ReportField_Text()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? DefaultValue { get; set; }
    public string? Placeholder { get; set; }
    public bool LongText { get; set; }
    public string? Value { get; set; }
    public string Height { get; set; } = "38px";
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public string ValueToString()
    {
        if (value == null)
            return "";
        else
            return Value.ToString();
    }


}