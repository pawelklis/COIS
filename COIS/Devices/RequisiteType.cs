using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RequisiteType : IDevice, IDatabase,  IDeviceParenty, IEquatable<RequisiteType>
{

    public int Id { get; set; }
    public string? BarCode { get; set; }
    public string Name { get; set; }
    public int DeviceTypeId { get; set; }
    public string? Description { get; set; }
    public string? EvidenceNumber { get; set; }
    public int OrgUnitId { get; set; }
    public int ZoneId { get; set; }
    public DeviceCondition Condition { get; set; }
    public string? PassDescription { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }

    public RequisiteType()
    {
        BarCode = "";
        Name = "";
        Description = "";
        EvidenceNumber = "";
        DeviceClass = DeviceEvidence.deviceclass.Rekwizyt;
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
    public static List<RequisiteType> GetRequisiteTypes()
    {
        AppDBContext db = new AppDBContext();
        return db.Requisites.ToList();
    }
    public static RequisiteType GetRequisite (int id)
    {
        AppDBContext db = new AppDBContext();
        return db.Requisites.ToList().Where(x => x.Id == id).FirstOrDefault();
    }

    public OrgUnit GetOrgUnit()
    {
        AppDBContext db = new AppDBContext();
        return db.OrgUnits.ToList().Where(x => x.Id == OrgUnitId).FirstOrDefault();
    }



    public ZoneType GetZoneType()
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.ToList().Where(x => x.Id == ZoneId).FirstOrDefault();
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

    public bool Equals(RequisiteType other)
    {
        if (other is null)
            return false;

        return this.Id == other.Id;
    }

    public override bool Equals(object obj) => Equals(obj as RequisiteType);
    public override int GetHashCode() => (Id).GetHashCode();

    //excepts end

}


