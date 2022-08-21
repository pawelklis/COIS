using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScannerType : IDeviceType, IDatabase,  IDeviceADControlled,  IDeviceEvidenceNumbers, IDeviceParenty, IDevice, IEquatable<ScannerType>
{
    public int Id { get; set; }
    public string BarCode { get; set; }
    public string Name { get; set; }
    public int DeviceTypeId { get; set; }
    public string? IP { get; set; }
    public string? ADLogin { get; set; }
    public bool ADControlled { get; set; }
    public bool ADStatusAccountActive { get; set; }
    public string? Description { get; set; }
    public bool IsBattery { get; set; }
    public string? SerialNumber { get; set; }
    public string? EvidenceNumber { get; set; }
    public string? DeviceNumber { get; set; }
    public string ?MoneyNumber { get; set; }
    public int OrgUnitId { get; set; }
    public int ZoneId { get; set; }
    public DeviceCondition Condition { get; set; }
    public string? PassDescription { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }
    public ScannerType()
    {
        Name = "";
        IP = "";
        ADLogin = "";
        Description = "";
        SerialNumber = "";
        EvidenceNumber = "";
        MoneyNumber = "";
        BarCode = "";
        DeviceNumber = "";
        DeviceClass = DeviceEvidence.deviceclass.Skaner;
        try
        {
            Condition = DeviceCondition.GetDeviceConditions().First();
        }
        catch (Exception)
        {
        }
    }

    public static List<ScannerType> GetScannerTypes()
    {
        AppDBContext db = new AppDBContext();
        return db.Scanners.ToList();
    }

    public static ScannerType GetScanner(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.Scanners.ToList().Where(x=>x.Id==id).FirstOrDefault();
    }

    public Task<string> GetForecastAsync()
    {
        
        return Task.FromResult(TestStatus());
    }
    public async ValueTask<string> TestStatusAsync()
    {

        await Task.Run(() =>
        {
            return TestStatus();
        });
        return "a";
    }

    string TestStatus()
    {
        System.Threading.Thread.Sleep(2000);
        return "status";
    }

    public void ActivateAD()
    {
        throw new NotImplementedException();
    }

    public bool CheckADStatus()
    {
        throw new NotImplementedException();
    }

    public void DeactivateAD()
    {
        throw new NotImplementedException();
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

    public DeviceType GetDeviceType()
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceTypes.ToList().Where(x=>x.Id==DeviceTypeId).FirstOrDefault();
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

    public bool Equals(ScannerType other)
    {
        if (other is null)
            return false;

        return this.Id == other.Id;
    }

    public override bool Equals(object obj) => Equals(obj as ScannerType);
    public override int GetHashCode() => (Id).GetHashCode();

    //excepts end
}

