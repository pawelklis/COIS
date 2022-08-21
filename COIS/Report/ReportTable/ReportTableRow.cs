

using System.Collections.Generic;

public class ReportTableRow:INoDatabase
{

    public string Guid { get; set; }
    public List<ReportRowCell> Cells { get; set; }

    public ReportTableRow()
    {
        Guid = System.Guid.NewGuid().ToString();
        Cells = new List<ReportRowCell>();
    }

}