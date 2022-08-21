

using System.Collections.Generic;
using System.Linq;

public class ReportItem:INoDatabase
{

    public string Guid { get; set; }
    public int OrderNumber { get; set; }
    public string Name { get; set; }
    public List<ReportField_Text> TextFields { get; set; }
    public List<ReportField_Number> NumberFields { get; set; }
    public List<ReportField_Bool> BoolFields { get; set; }
    public List<ReportField_List> ListFields { get; set; }
    public List<ReportField_Table> TableFields { get; set; }
    
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public ReportItem()
    {
        Guid = System.Guid.NewGuid().ToString();
        TextFields = new List<ReportField_Text>();
        NumberFields = new List<ReportField_Number>();
        BoolFields = new List<ReportField_Bool>();
        ListFields = new List<ReportField_List>();
        TableFields = new List<ReportField_Table>();
        
        Name = "";
    }

    public string QuerySelector()
    {
        return "z" + this.OrderNumber;
    }

    public List<string> CategoriesList()
    {
        List<string> l = new List<string>();

        l.AddRange((from o in TextFields.ToList()
                select o.InfoCategory?.Name).Distinct().ToArray());
        l.AddRange((from o in NumberFields.ToList()
                select o.InfoCategory?.Name).Distinct().ToArray());
        l.AddRange((from o in BoolFields.ToList()
                select o.InfoCategory?.Name).Distinct().ToArray());
        l.AddRange((from o in ListFields.ToList()
                select o.InfoCategory?.Name).Distinct().ToArray());
        l.AddRange((from o in TableFields.ToList()
                select o.InfoCategory?.Name).Distinct().ToArray());

        return l.Distinct().ToList();
    }


    public List<IReportField> FieldsByCategory(string cat)
    {
        List<IReportField> l = new List<IReportField>();

        l.AddRange((from o in TextFields.ToList()
                    where o.InfoCategory?.Name==cat
                    select o).ToArray());
        l.AddRange((from o in NumberFields.ToList()
                    where o.InfoCategory?.Name == cat
                    select o).ToArray());
        l.AddRange((from o in BoolFields.ToList()
                    where o.InfoCategory?.Name == cat
                    select o).ToArray());
        l.AddRange((from o in ListFields.ToList()
                    where o.InfoCategory?.Name == cat
                    select o).ToArray());
        l.AddRange((from o in TableFields.ToList()
                    where o.InfoCategory?.Name == cat
                    select o).ToArray());

        return l;
    }

    public List<IReportField> AllFields()
    {
        List<IReportField> l = new List<IReportField>();

        l.AddRange(TextFields.ToList());
        l.AddRange(NumberFields.ToList());
        l.AddRange(BoolFields.ToList());
        l.AddRange(ListFields.ToList());
        l.AddRange(TableFields.ToList());

        return l;
    }
}