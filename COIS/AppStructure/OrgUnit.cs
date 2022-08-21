using System.Collections.Generic;
using System.Linq;

public class OrgUnit:IDatabase
{
    public int Id { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Kod", FrontendType.text, true)]
    public string Code { get; set; }
    [FrontEnd("Naprawa OZ", FrontendType.checkbox, false)]
    public bool EmptyBoxEnable { get; set; }
    [FrontEnd("Jednostka naprawy OZ", FrontendType.list, false,true, ObjectListNames.orgunits,"id","name")]
    public int EmptyBoxParentOrgUnitId { get; set; }

    public static List<OrgUnit> GetOrgUnits()
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.ToList();
    }
    public static OrgUnit GetOrgUnitById(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.Where(x => x.Id == id).FirstOrDefault();
    }


    public  List<OrgUnit> GetOrgEmptyBoxesSubOrgUnits()
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.ToList().Where(x=>x.EmptyBoxParentOrgUnitId==this.Id).ToList();
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