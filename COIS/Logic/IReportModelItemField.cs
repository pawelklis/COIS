using System.Collections.Generic;

public interface IReportmodelField
{
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string Description { get; set; }
    public List<WorkShift> EnabledAdWorkshifts { get; set; }
    public InfoCategory InfoCategory { get; set; }

}