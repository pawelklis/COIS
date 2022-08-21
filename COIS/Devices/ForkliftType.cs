using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ForkliftType : IDatabase, IDevice, IDeviceBatteryLoading, IDeviceParenty, IDeviceEvidenceNumbers, IEquatable<ForkliftType>
{
    public int Id { get; set; }
    public string BarCode { get; set; }
    public string Name { get; set; }
    public int DeviceTypeId { get; set; }
    public DeviceCondition Condition { get; set; }
    public int IdBattery { get; set; }
    public int OrgUnitId { get; set; }
    public int ZoneId { get; set; }
    public string? SerialNumber { get; set; }
    public string? EvidenceNumber { get; set; }
    public string? DeviceNumber { get; set; }
    public string? MoneyNumber { get; set; }
    public string? Description { get; set; }
    public string? PassDescription { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }

    public ForkliftType()
    {
        Name = "";
        Description = "";
        SerialNumber = "";
        EvidenceNumber = "";
        MoneyNumber = "";
        BarCode = "";
        DeviceNumber = "";
        DeviceClass = DeviceEvidence.deviceclass.Elektrowózek;
        try
        {
            Condition = DeviceCondition.GetDeviceConditions().First();
        }
        catch (Exception)
        {
        }
    }
    public DeviceType GetDeviceType()
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceTypes.ToList().Where(x => x.Id == DeviceTypeId).FirstOrDefault();
    }
    public static ForkliftType GetForkLift(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.Forklifts.ToList().Where(x => x.Id == id).FirstOrDefault();
    }

    public static List<ForkliftType> GetForklifts()
    {
        AppDBContext db = new AppDBContext();
        return db.Forklifts.ToList();
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

    public IDevice GetBattery()
    {
        AppDBContext db = new AppDBContext();
        return db.Batteries.ToList().Where(x => x.Id == IdBattery).FirstOrDefault();
    }

    public OrgUnit GetOrgUnit()
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.ToList().Where(x => x.Id == OrgUnitId).FirstOrDefault();
    }

    public ZoneType GetZoneType()
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.ToList().Where(x=>x.Id==ZoneId).FirstOrDefault();
    }
    //excepts

    public bool Equals(ForkliftType other)
    {
        if (other is null)
            return false;

        return this.Id == other.Id;
    }

    public override bool Equals(object obj) => Equals(obj as ForkliftType);
    public override int GetHashCode() => (Id).GetHashCode();

    //excepts end

}



