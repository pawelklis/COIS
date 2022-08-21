using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DeviceType : IDatabase, IDeviceType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsBattery { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }

    public DeviceType()
    {
        Name = "";
        Description = "";
    }
    public int DeviceClassId()
    {
        return (int)DeviceClass;
    }
    public static List<DeviceType> GetDeviceTypes(DeviceEvidence.deviceclass DeviceClass)
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceTypes.ToList().Where(x=>x.DeviceClass== DeviceClass).ToList();
    }
    public static List<DeviceType> GetDeviceTypes()
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceTypes.ToList();
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

