using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BatteryType : IDatabase, IDevice, IDeviceBattery, IEquatable<BatteryType>,IDeviceType
{
    public int Id { get; set; }
    public string BarCode { get; set; }
    public string? Name { get; set; }
    public string BatNumber { get; set; }
    public string? Description { get; set; }
    public int DeviceTypeId { get; set; }
    public DeviceCondition Condition { get; set; }
    public int OrgUnitId { get; set; }
    public bool IsBattery { get; set ; }
    public string? PassDescription { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }

    public BatteryType()
    {
        Name = "";
        Description = "";
        BarCode = "";
        BatNumber = "";
        DeviceClass = DeviceEvidence.deviceclass.Bateria;
        try
        {
            Condition = DeviceCondition.GetDeviceConditions().First();
        }
        catch (Exception)
        {
        }
    }

    public OrgUnit GetOrgUnit()
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.ToList().Where(x => x.Id == OrgUnitId).FirstOrDefault();
    }
    public DeviceType GetDeviceType()
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceTypes.ToList().Where(x => x.Id == DeviceTypeId).FirstOrDefault();
    }
    public static List<BatteryType> GetBatteries()
    {
        AppDBContext db = new AppDBContext();
        return db.Batteries.ToList();
    }
    public static BatteryType GetBattery(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.Batteries.ToList().Where(x => x.Id == id).FirstOrDefault();
    }
    public bool IsOnCharge()
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

    //excepts

    public bool Equals(BatteryType other)
    {
        if (other is null)
            return false;

        return this.Id == other.Id;
    }

    public override bool Equals(object obj) => Equals(obj as BatteryType);
    public override int GetHashCode() => (Id).GetHashCode();

    //excepts end

}

