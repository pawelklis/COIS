using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using static ReportItem;
using static ReportModelItem;

public class ReportRowCell:INoDatabase
{

    public ReportRowCell()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public ReportFieldTypes CellType { get; set; }   
    public string Value { get; set; }
    public ReportModelTableColumn Column { get; set; }
    public string Height { get; set; } = "38px";
    public string ValueToString()
    {
        if (Value == null)
            return "";
        else
            return Value.ToString();
    }



}
