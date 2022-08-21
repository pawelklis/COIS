using System.Collections.Generic;

public class ReportModelField_Bool:IReportmodelField,INoDatabase
{
    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }
    [FrontEnd("Wartość domyślna", FrontendType.text, true)]
    public bool DefaultValue { get; set; }
    public string Placeholder { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }

    public ReportModelField_Bool()
    {
        Guid = System.Guid.NewGuid().ToString();
        InfoCategory = InfoCategory.Empty();
        EnabledAdWorkshifts = new List<WorkShift>();
    }


    public ReportField_Bool NewItem()
    {
     
            ReportField_Bool item = new ReportField_Bool();
            item.Name = Name;
            item.Description = Description;
            item.Value = DefaultValue;
            item.DefaultValue = DefaultValue;
            item.Placeholder = Placeholder;
            //item.ReportModelItemId = ReportModelItemId;
            item.EnabledAdWorkshifts = EnabledAdWorkshifts;
            item.InfoCategory = InfoCategory;
            return item;
        

    }
}