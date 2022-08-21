

using System.Collections.Generic;
using System.Data;

public class ReportField_Table:IReportField, INoDatabase
{

    public string Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<ReportTableColumn> Columns { get; set; }
    public List<ReportTableRow> Rows { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public ReportField_Table()
    {
        Guid = System.Guid.NewGuid().ToString();
        Columns = new List<ReportTableColumn>();
        Rows = new List<ReportTableRow>();
    }

    public string ValueToString()
    {
        string v = "";
        foreach(var r in Rows)
        {
            foreach (var c in r.Cells){
                v+=c.ValueToString() + " ; ";
            }
        }
        return v;
    }
    public void AddRow()
    {
        ReportTableRow row = new ReportTableRow();
        foreach (var c in Columns)
        {
            ReportRowCell cell = new ReportRowCell();
            cell.Column = ReportModelTableColumn.NewItem(c);
            cell.CellType = c.ColumnType;
            
            row.Cells.Add(cell);
        }

        this.Rows.Add(row);
    }
    public DataTable toTable()
    {
        DataTable dt = new DataTable();
        foreach (var col in Columns)
        {
            if (col.Name == null)
                col.Name = "";
            if(dt.Columns.Contains(col.Name))
                dt.Columns.Add(col.Name+"_"+dt.Columns.Count);
            else
                dt.Columns.Add(col.Name);
        }


        foreach (var row in Rows)
        {
            dt.Rows.Add();
            int x = 0;
            foreach(var cel in row.Cells)
            {
                dt.Rows[dt.Rows.Count - 1][x] = cel.Value;
                x++;
            }
        }

        return dt;
    }
}