

public interface IReportField
{
    public string Name { get; set; }
    public string Description { get; set; }
    public InfoCategory InfoCategory { get; set; }
    public string ValueToString();
}