using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeviceEvidence : IDatabase, IdeviceEvidence
{
    public static event EventHandler<DeviceEvidence> OnDeviceGet;
    public static event EventHandler<DeviceEvidence> OnDevicePass;
    public int Id { get; set; }
    public int OrgUnitId { get; set; }
    public int DeviceId { get; set; }
    public DateTime GetTime { get; set; }    
    public EmployeeType? GetEmployee { get; set; }
    public EmployeeType? GetManagerEmployee { get; set; }
    public ZoneType? GetZone { get; set; }
    public DateTime PassTime { get; set; }
    public EmployeeType? PassEmployee { get; set; }
    public EmployeeType? PassManagerEmployee { get; set; }
    public ZoneType? PassZone { get; set; }
    public bool Closed { get; set; }
    public deviceclass DeviceClass { get; set; }
    public string? Remark { get; set; }

    private IDevice _device;
    public static List<DeviceEvidence> GetDevicesToPass(deviceclass deviceclass,int orgunit, int zoneid=0)
    {
        AppDBContext db = new AppDBContext();
        List<DeviceEvidence> l = new List<DeviceEvidence>();

        if(zoneid==0)
            l = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == deviceclass && x.OrgUnitId==orgunit && x.Closed == false).ToList();
        else
            l = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == deviceclass && x.GetZone.Id == zoneid && x.Closed == false).ToList();

        return l;
    }
    public static List<DeviceEvidence> GetDevicesToPass(int orgunit, int zoneid)
    {
        AppDBContext db = new AppDBContext();
        List<DeviceEvidence> l = new List<DeviceEvidence>();

        if(zoneid==0)
            l = db.DeviceEvidences.ToList().Where(x => x.OrgUnitId==orgunit && x.Closed == false).ToList();
        else
            l = db.DeviceEvidences.ToList().Where(x => x.GetZone.Id == zoneid && x.Closed == false).ToList();

        return l;
    }

    public static async Task<List<DeviceEvidence>> GetDevicesToPassAsync(int EmployeeID)
    {
        List<DeviceEvidence> l = await Task.FromResult(GetDevicesToPass(EmployeeID));
        return l;
    }
    public static List<DeviceEvidence> GetDevicesToPass(int EmployeeID)
    {
        AppDBContext db = new AppDBContext();
        List<DeviceEvidence> l = new List<DeviceEvidence>();

            l = db.DeviceEvidences.ToList().Where(x => x.Closed==false && x.GetEmployee.Id==EmployeeID).ToList();

        return l;
    }
    public static DeviceEvidence GetDeviceEvidenc(int id)
    {
        AppDBContext db = new AppDBContext();

        return db.DeviceEvidences.ToList().Find(x => x.Id==id);


    }

    public enum deviceclass
    {
        Skaner=0,Elektrowózek=1,Bateria=2,Rekwizyt=3
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
    public static DeviceEvidence GetDevice(int deviceid,deviceclass deviceClass, int employeeid, int managerEmployeeid, int zoneid)
    {
        IDevice device=null;
        switch (deviceClass)
        {
            case deviceclass.Skaner:
                device = ScannerType.GetScanner(deviceid);
                break;
            case deviceclass.Elektrowózek:
                device = ForkliftType.GetForkLift(deviceid);
                break;
            case deviceclass.Bateria:
                device = BatteryType.GetBattery(deviceid);
                break;
            case deviceclass.Rekwizyt:
                device = RequisiteType.GetRequisite(deviceid);
                break;
            default:
                break;
        }

        EmployeeType employee = EmployeeType.GetEmployee(employeeid);
        EmployeeType employeemanager = EmployeeType.GetEmployee(managerEmployeeid);
        ZoneType zone = ZoneType.GetZoneById(zoneid);
        return GetDevice(device, DateTime.Now, employee, employeemanager, zone);
    }

    public static DeviceEvidence GetDevice(IDevice device, DateTime time, EmployeeType employee, EmployeeType managerEmployee, ZoneType zone)
    {
        DeviceEvidence dv = new DeviceEvidence()
        {
            DeviceId = device.Id,
            GetEmployee = employee,
            GetManagerEmployee = managerEmployee,
            GetTime = time,
            GetZone = zone,
            Closed = false,
            OrgUnitId = zone.OrganizationUnit().Id,
            PassEmployee = EmployeeType.Empty(),
            PassManagerEmployee = EmployeeType.Empty(),
            PassZone = ZoneType.Empty()
        };
        if (device.GetType() == typeof(ScannerType))
            dv.DeviceClass = deviceclass.Skaner;
        if (device.GetType() == typeof(ForkliftType))
            dv.DeviceClass = deviceclass.Elektrowózek;
        if (device.GetType() == typeof(BatteryType))
            dv.DeviceClass = deviceclass.Bateria;
        if (device.GetType() == typeof(RequisiteType))
            dv.DeviceClass = deviceclass.Rekwizyt;

        DeviceOperation.GetDevice(dv);

        EventHandler<DeviceEvidence> handler = OnDeviceGet;
        handler?.Invoke(dv, dv);

        return dv;
    }

    public static DeviceEvidence PassDevice(int deviceevidenceid, int employeeid,
                               int managerid, int zoneid, int conditionid, string damageDescription)
    {
        DeviceEvidence evidenc = DeviceEvidence.GetDeviceEvidenc(deviceevidenceid);
        EmployeeType employee = EmployeeType.GetEmployee(employeeid);
        UserType manager = UserType.GetUser(managerid);
        ZoneType zone = ZoneType.GetZoneById(zoneid);
        DeviceCondition condition = DeviceCondition.GetDeviceCondition(conditionid);
        return PassDevice(evidenc, DateTime.Now, employee, manager, zone, condition, damageDescription);
    }

    public static DeviceEvidence PassDevice(DeviceEvidence deviceevidence, DateTime time, EmployeeType employee,
                                   UserType manager, ZoneType zone,DeviceCondition condition, string damageDescription)
    {
        DeviceEvidence dv = deviceevidence;
        dv.PassTime = time;
        dv.PassEmployee = employee;
        dv.PassManagerEmployee = manager.Employee;
        dv.PassZone = zone;
        dv.Remark = deviceevidence.Device().PassDescription + $"\n{DateTime.Now}\n{employee.FullName()}";
        dv.Closed = true;

        deviceevidence.Device().Condition = condition;
        deviceevidence.Device().Save();

        if (condition.CanBeUse == false)
        {
            //Register new damage
            DeviceOperation.RegisterDamage(deviceevidence.Device(), damageDescription, manager, dv.PassEmployee);
        }

        DeviceOperation.PassDevice(dv);

        EventHandler<DeviceEvidence> handler = OnDevicePass;
        handler?.Invoke(dv, dv);

        return dv;
    }

}

