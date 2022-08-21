using System.Collections.Generic;

public class ReportModelField_Table:IReportmodelField,INoDatabase
{

    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }    
    [FrontEnd("Wartość domyślna", FrontendType.text, true)]
    public string DefaultValue { get; set; }    
    public List<ReportModelTableColumn> Columns { get; set; }
    public List<ReportModelTableRow> Rows { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }

    public ReportModelField_Table()
    {
        Guid = System.Guid.NewGuid().ToString();
        Columns = new List<ReportModelTableColumn>();
        Rows = new List<ReportModelTableRow>();
        InfoCategory = InfoCategory.Empty();
        EnabledAdWorkshifts = new List<WorkShift>();
    }

    public ReportField_Table NewItem()
    {
        ReportField_Table item = new ReportField_Table();

        item.EnabledAdWorkshifts = EnabledAdWorkshifts;
        item.InfoCategory = InfoCategory;
        item.Name = Name;
        item.Description = Description;

        foreach (var c in Columns)
        {
            item.Columns.Add(c.NewItem());
        }

        foreach(var  r in Rows)
        {
            item.Rows.Add(r.NewItem());
        }

        return item;
    }

    public void AddRow()
    {
        ReportModelTableRow row = new ReportModelTableRow();
        foreach (var c in Columns)
        {
            ReportModelRowCell cell = new ReportModelRowCell();

            switch (c.ColumnType)
            {
                case ReportModelItem.ReportFieldTypes.Text:
                    //cell.Field = new ReportModelField_Text();
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.Text;
                    break;
                case ReportModelItem.ReportFieldTypes.LongText:
                    //cell.Field = new ReportModelField_Text() { LongText =true };
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.LongText;
                    break;
                case ReportModelItem.ReportFieldTypes.Number:
                    //cell.Field = new ReportModelField_Number();
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.Number;
                    break;
                case ReportModelItem.ReportFieldTypes.Bool:
                    //cell.Field = new ReportModelField_Bool();
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.Bool;
                    break;
                case ReportModelItem.ReportFieldTypes.List:
                    //cell.Field = new ReportModelField_List();
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.List;
                    break;
                case ReportModelItem.ReportFieldTypes.Table:
                    //cell.Field = new ReportModelField_Table();
                    cell.Column = c;
                    cell.CellType = ReportModelItem.ReportFieldTypes.Table;
                    break;
                default:
                    break;
            }

            //cell.Field.Description = "dsc";
            row.Cells.Add(cell);            
        }

        this.Rows.Add(row);
    }

    public void ApplyChanges()
    {
        foreach (var row in Rows)
        {
            int i = 0;
            foreach (var c in Columns)
            {
                if (i < row.Cells.Count)
                {
                    switch (c.ColumnType)
                    {
                        case ReportModelItem.ReportFieldTypes.Text:
                            //cell.Field = new ReportModelField_Text();
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.Text;
                            break;
                        case ReportModelItem.ReportFieldTypes.LongText:
                            //cell.Field = new ReportModelField_Text() { LongText =true };
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.LongText;
                            break;
                        case ReportModelItem.ReportFieldTypes.Number:
                            //cell.Field = new ReportModelField_Number();
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.Number;
                            break;
                        case ReportModelItem.ReportFieldTypes.Bool:
                            //cell.Field = new ReportModelField_Bool();
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.Bool;
                            break;
                        case ReportModelItem.ReportFieldTypes.List:
                            //cell.Field = new ReportModelField_List();
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.List;
                            break;
                        case ReportModelItem.ReportFieldTypes.Table:
                            //cell.Field = new ReportModelField_Table();
                            row.Cells[i].Column = c;
                            row.Cells[i].CellType = ReportModelItem.ReportFieldTypes.Table;
                            break;
                        default:
                            break;
                    }
                }
                else
                {



                    ReportModelRowCell cell = new ReportModelRowCell();
                    switch (c.ColumnType)
                    {
                        case ReportModelItem.ReportFieldTypes.Text:
                            //cell.Field = new ReportModelField_Text();
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.Text;
                            break;
                        case ReportModelItem.ReportFieldTypes.LongText:
                            //cell.Field = new ReportModelField_Text() { LongText =true };
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.LongText;
                            break;
                        case ReportModelItem.ReportFieldTypes.Number:
                            //cell.Field = new ReportModelField_Number();
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.Number;
                            break;
                        case ReportModelItem.ReportFieldTypes.Bool:
                            //cell.Field = new ReportModelField_Bool();
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.Bool;
                            break;
                        case ReportModelItem.ReportFieldTypes.List:
                            //cell.Field = new ReportModelField_List();
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.List;
                            break;
                        case ReportModelItem.ReportFieldTypes.Table:
                            //cell.Field = new ReportModelField_Table();
                            cell.Column = c;
                            cell.CellType = ReportModelItem.ReportFieldTypes.Table;
                            break;
                        default:
                            break;
                    }
                    row.Cells.Add(cell);
                }


                i++;
            }

            if (row.Cells.Count > Columns.Count)
            {
                for (int x = Columns.Count-1; x < row.Cells.Count; x++)
                {
                    row.Cells.RemoveAt(x);
                }
            }
        }
    }

}