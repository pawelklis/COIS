using System.Collections.Generic;

public class ReportModelTableRow:INoDatabase
{

    public string Guid { get; set; }
    public List<ReportModelRowCell> Cells { get; set; }


    public ReportTableRow NewItem()
    {
        ReportTableRow item = new ReportTableRow();
        
        foreach(var  cell in Cells)
        {
            item.Cells.Add(cell.NewItem());
        }

        return item;
    }

    public ReportModelTableRow()
    {
        Guid = System.Guid.NewGuid().ToString();
        Cells = new List<ReportModelRowCell>();
    }

}