
public class ActivityType : INoDatabase
{
    public ActivityType()
    {
        Guid = System.Guid.NewGuid().ToString();
        Time= System.DateTime.Now;
    }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Time { get; set; }

}
