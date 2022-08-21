using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ReportModelField_List:IReportmodelField,INoDatabase
{
    public ReportModelField_List()
    {
        Guid = System.Guid.NewGuid().ToString();
        InfoCategory = InfoCategory.Empty();
        EnabledAdWorkshifts = new List<WorkShift>();
        PossibleValues = "";
        DefaultValue = "";
    }
    public string Guid { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }
    public string Value { get; private set; }
    [FrontEnd("Wartość domyślna", FrontendType.text, true)]
    public string DefaultValue { get; set; }
    public string Placeholder { get; set; }
    public string PossibleValues { get; set; }
    public bool UseBindingValues { get; set; }
    public string BindingValue { get; set; }
    public bool OrgUnitFilter { get; set; }
    public bool ZoneFilter { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }



    public static List<string> PossibleBindings()
    {
        //nalezy dodac w ReportFieldList
        List<string> vals = new List<string>();
        vals.Add("Pracownicy");
        vals.Add("Strefy");
        vals.Add("Jednostki");

        return vals;
    }
    public List<string> PossibleValuesList()
    {
        List<string> vals = new List<string>();
        vals.AddRange(PossibleValues.Split(','));
        return vals;
    }


    public ReportField_List NewItem(ProgramInstance cois)
    {


        ReportField_List item = new ReportField_List();

        if (BindingValue == null)
            try { DefaultValue = PossibleValuesList()[0]; } catch { }
        else
        {
            item.BindingValue = BindingValue;
            try { DefaultValue = item.GetBindingValues(cois)[0].Item2; } catch { }
        }
            


        item.Name = Name;
        item.Description = Description;
        item.Value = DefaultValue;
        item.DefaultValue = DefaultValue;
        item.PossibleValues = PossibleValues;
        item.Placeholder = Placeholder;
        item.EnabledAdWorkshifts = EnabledAdWorkshifts;
        item.InfoCategory = InfoCategory;
        item.UseBindingValues = UseBindingValues;
        item.OrgUnitFilter = OrgUnitFilter;
        item.ZoneFilter = ZoneFilter;

        
        return item;
    }
}