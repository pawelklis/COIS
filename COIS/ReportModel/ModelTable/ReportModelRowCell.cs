using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static ReportModelItem;

public class ReportModelRowCell:INoDatabase
{
    public ReportModelRowCell()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public ReportFieldTypes CellType { get; set; }
    private string _Field { get; set; }
    public string? Value { get; set; }
    public string Height { get; set; } = "38px";
    public ReportModelTableColumn Column { get; set; }




    public ReportRowCell NewItem()
    {
        ReportRowCell item = new ReportRowCell();
        item.CellType = CellType;
        item.Value = Value;
        item.Column = Column;

        if (item.CellType == ReportFieldTypes.Bool)
            item.Value = false.ToString();
        if (item.CellType == ReportFieldTypes.Number)
            item.Value = 0.ToString();

     

        return item;
    }
}