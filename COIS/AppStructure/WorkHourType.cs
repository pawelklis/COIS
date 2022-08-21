

using System.Linq;

public class WorkHourType:IDatabase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }


    public static List<WorkHourType> GetWorkHourTypes()
    {
        AppDBContext db = new AppDBContext();
        return db.WorkHours.ToList();
    }
    public static WorkHourType GetWorkHourType(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.WorkHours.ToList().Find(x=>x.Id==id);
    }
    public static WorkHourType GetOrAdd(string name)
    {
        AppDBContext db = new AppDBContext();
        WorkHourType o;

        o = db.WorkHours.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();

        if (o == null)
        {
            o = new WorkHourType() { Name = name, Description = "" };
            o.Save();
        }

        return o;
    }

    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.RemovetObject(this);
    }

    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);
    }
}