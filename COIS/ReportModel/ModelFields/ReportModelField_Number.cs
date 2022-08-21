using System;
using System.Collections.Generic;
using System.Text;


public class ReportModelField_Number : IReportmodelField,INoDatabase
{
    public ReportModelField_Number()
    {
        Guid = System.Guid.NewGuid().ToString();
        EnabledAdWorkshifts = new List<WorkShift>();
        InfoCategory = InfoCategory.Empty();
    }
    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }
    [FrontEnd("Wartość domyślna", FrontendType.text, true)]
    public double DefaultValue { get; set; }
    public string Placeholder { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }



    public ReportField_Number NewItem()
    {
        ReportField_Number item = new ReportField_Number();
        item.Name = Name;
        item.Description = Description;
        item.Value = DefaultValue;
        item.DefaultValue = DefaultValue;
        item.Placeholder = Placeholder;
        item.EnabledAdWorkshifts = EnabledAdWorkshifts;
        item.InfoCategory = InfoCategory;
        return item;
    }
}

