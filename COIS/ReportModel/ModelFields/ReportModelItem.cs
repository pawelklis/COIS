

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class ReportModelItem:INoDatabase
{

    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    public List<ReportModelField_Text> TextFields { get; set; }
    public List<ReportModelField_Number> NumberFields { get; set; }
    public List<ReportModelField_Bool> BoolFields { get; set; }
    public List<ReportModelField_List> ListFields { get; set; }
    public List<ReportModelField_Table> TableFields { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }

    public enum ReportFieldTypes
    {
        Text,LongText,Number,Bool,List,Table
    }
    public static List<string> ReportFieldTypesList()
    {
        return new string[] { "Text", "LongText", "Number", "Bool", "List", "Table" }.ToList();
    }
    public List<IReportmodelField> Fields()
    {
        List<IReportmodelField> f = new List<IReportmodelField>();
        f.AddRange(TextFields);
        f.AddRange(NumberFields);
        f.AddRange(BoolFields);
        f.AddRange(ListFields);
        f.AddRange(TableFields);
        return f;
    }

    public ReportItem NewItem(WorkShift workShift, ProgramInstance cois)
    {
        ReportItem item = new ReportItem();
        item.Name = Name;
        item.EnabledAdWorkshifts = EnabledAdWorkshifts;
        

        TextFields.ForEach(x =>
        {
            if(x.EnabledAdWorkshifts==null|| x.EnabledAdWorkshifts.Find(x=>x.Id==workShift.Id)!=null)
                item.TextFields.Add(x.NewItem());
        });
        NumberFields.ForEach(x =>
        {
            if (x.EnabledAdWorkshifts == null || x.EnabledAdWorkshifts.Find(x => x.Id == workShift.Id) != null)
                item.NumberFields.Add(x.NewItem());
        });
        BoolFields.ForEach(x =>
        {
            if (x.EnabledAdWorkshifts == null || x.EnabledAdWorkshifts.Find(x => x.Id == workShift.Id) != null)
                item.BoolFields.Add(x.NewItem());
        });
        ListFields.ForEach(x =>
        {
            if (x.EnabledAdWorkshifts == null || x.EnabledAdWorkshifts.Find(x => x.Id == workShift.Id) != null)
                item.ListFields.Add(x.NewItem(cois));
        });
        TableFields.ForEach(x =>
        {
            if (x.EnabledAdWorkshifts == null || x.EnabledAdWorkshifts.Find(x => x.Id == workShift.Id) != null)
                item.TableFields.Add(x.NewItem());
        });
        return item;
    }

    public ReportModelItem()
    {
        Guid = System.Guid.NewGuid().ToString();
        TextFields = new List<ReportModelField_Text>();
        NumberFields = new List<ReportModelField_Number>();
        BoolFields = new List<ReportModelField_Bool>();
        ListFields = new List<ReportModelField_List>();
        TableFields = new List<ReportModelField_Table>();
        Name = "";
        EnabledAdWorkshifts = new List<WorkShift>();
    }

  
}