using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DeviceCondition : IDatabase, IDeviceCondition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool CanBeUse { get; set; }
    public bool Default { get; set; }

    public int UsedInDeviceTypeId { get; set; }

    public void SetAsDefault()
    {
        Default = true;
        foreach (var c in DeviceCondition.GetDeviceConditions())
        {
            if (c.Id != this.Id)
            {
                c.Default = false;
                c.Save();
            }
        }
        Save();
    }
    public static DeviceCondition DefaultCondition()
    {
        return GetDeviceConditions().Find(x => x.Default == true);
    }
    public DeviceCondition()
    {
        Name = "";
        Description = "";
    }
    public static List<DeviceCondition> GetDeviceConditions()
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceConditions.ToList();
    }
    public static DeviceCondition GetDeviceCondition(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceConditions.ToList().Where(x => x.Id == id).FirstOrDefault();
    }
    public DeviceType GetUsedInDeviceType()
    {
        throw new NotImplementedException();
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

