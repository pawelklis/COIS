using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeviceEvidence;

public class DeviceDamage:IDatabase
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    private IDevice _device;
    public string? DamageDescription { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool Closed { get; set; }
    public UserType? User { get; set; }
    public EmployeeType? Employee { get; set; }
    public DateTime CloseTime { get; set; }
    public List<DeviceDamageHistory> History { get; set; }
    public deviceclass DeviceClass { get; set; }

    public DeviceDamage() { }
    public DeviceDamage(IDevice device, string damageDescription, UserType user, EmployeeType employee)
    {
        History = new List<DeviceDamageHistory>();
        DeviceId=device.Id;

        if (device.GetType() == typeof(ScannerType))
            DeviceClass = deviceclass.Skaner;
        if (device.GetType() == typeof(ForkliftType))
            DeviceClass = deviceclass.Elektrowózek;
        if (device.GetType() == typeof(BatteryType))
            DeviceClass = deviceclass.Bateria;
        if (device.GetType() == typeof(RequisiteType))
            DeviceClass = deviceclass.Rekwizyt;

        DamageDescription = damageDescription;
        User=user;
        Employee = employee;
        RegisterDate = DateTime.Now;

        Save();

        AddHistory("", user, DeviceDamageLevel.Zarejestrowano);
    }

    public IDevice Device()
    {
        if (_device == null)
        {
            AppDBContext db = new AppDBContext();
            if (DeviceClass == deviceclass.Skaner)
                _device = db.Scanners.ToList().Find(x => x.Id == DeviceId);
            if (DeviceClass == deviceclass.Elektrowózek)
                _device = db.Forklifts.ToList().Find(x => x.Id == DeviceId);
            if (DeviceClass == deviceclass.Bateria)
                _device = db.Batteries.ToList().Find(x => x.Id == DeviceId);
            if (DeviceClass == deviceclass.Rekwizyt)
                _device = db.Requisites.ToList().Find(x => x.Id == DeviceId);
        }
        return _device;
    }
    public static List<DeviceDamage> GetDamages(bool closed)
    {
        AppDBContext db = new AppDBContext();
        return db.DeviceDamages.ToList().Where(x => x.Closed == closed).ToList();
    }
    public void AddHistory(string description, UserType user,  DeviceDamageLevel damageLevel)
    {
        History.Add(new DeviceDamageHistory(description, user, damageLevel));

        if(damageLevel== DeviceDamageLevel.Zakończono)
        {
            Closed = true;
            Device().Condition = DeviceCondition.DefaultCondition();
            Device().Save();
        }

        Save();
    }

    public void Close(string description,UserType user)
    {
        History.Add(new DeviceDamageHistory(description, user, DeviceDamageLevel.Zakończono));
        Closed = true;
        Save();
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

