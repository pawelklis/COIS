using System;
using System.Collections.Generic;
using System.Text;


public class ReportModelField_Text : IReportmodelField,INoDatabase
{
    public ReportModelField_Text()
    {
        Guid = System.Guid.NewGuid().ToString();
        InfoCategory = InfoCategory.Empty();
        EnabledAdWorkshifts = new List<WorkShift>();
    }
    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }
    [FrontEnd("Wartość domyślna", FrontendType.text, true)]
    public string DefaultValue { get; set; }
    public string Placeholder { get; set; }
    public bool LongText { get; set; }
    public string Value { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }


    public ReportField_Text NewItem()
    {
        ReportField_Text item = new ReportField_Text();
        item.Name = Name;
        item.Description = Description;
        item.Value = DefaultValue;
        item.DefaultValue = DefaultValue;
        item.Placeholder = Placeholder;
        item.LongText = LongText;
        item.EnabledAdWorkshifts = EnabledAdWorkshifts;
        item.InfoCategory = InfoCategory;
        return item;
    }
}

