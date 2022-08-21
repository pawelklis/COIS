using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DeviceOperation
{



    public static List<DeviceEvidence> GetHistory(int year,int month, int zoneid, DeviceEvidence.deviceclass deviceclass)
    {
        AppDBContext db = new AppDBContext();
        List<RequisiteType> l = new List<RequisiteType>();
        return db.DeviceEvidences.ToList().Where(x => x.DeviceClass == deviceclass &&
                                                    x.GetTime.Month == month && x.GetTime.Year == year &&
                                                    x.GetZone.Id == zoneid).ToList().OrderBy(x => x.Closed).ThenByDescending(x => x.GetTime).ToList();
    }


    public static List<RequisiteType> GetRequisitesToGet(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        List<RequisiteType> l = new List<RequisiteType>();

        l = db.Requisites.ToList().Where(x => x.OrgUnitId == orgunitid && x.Condition.CanBeUse == true).ToList();

        List<DeviceEvidence> ev = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == DeviceEvidence.deviceclass.Rekwizyt &&
                                                                        x.Closed == false && x.OrgUnitId == orgunitid).ToList();

        List<RequisiteType> lf = new List<RequisiteType>();

        ev.ForEach(x => {
            lf.Add(x.Device() as RequisiteType);
        });

        if (lf == null)
            lf = new List<RequisiteType>();


        return l.Except(lf).ToList();

    }

    public static List<ScannerType> GetScannersToGet(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        List<ScannerType> l = new List<ScannerType>();

            l = db.Scanners.ToList().Where(x=>x.OrgUnitId==orgunitid && x.Condition.CanBeUse==true).ToList();

        List<DeviceEvidence> ev = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == DeviceEvidence.deviceclass.Skaner &&
                                                                        x.Closed == false && x.OrgUnitId == orgunitid).ToList();

        List<ScannerType> lf = new List<ScannerType>();

        ev.ForEach(x => {
            lf.Add(x.Device() as ScannerType);
        });

        if (lf == null)
            lf = new List<ScannerType>();

      
        return l.Except(lf).ToList();

    }

    public static List<BatteryType> GetBateriesToGet(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        List<BatteryType> l = new List<BatteryType>();

        l = db.Batteries.ToList().Where(x => x.OrgUnitId == orgunitid && x.Condition.CanBeUse == true).ToList();

        List<DeviceEvidence> ev = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == DeviceEvidence.deviceclass.Bateria &&
                                                                        x.Closed == false && x.OrgUnitId == orgunitid).ToList();

        List<BatteryType> lf = new List<BatteryType>();

        ev.ForEach(x => {
            lf.Add(x.Device() as BatteryType);
        });

        if (lf == null)
            lf = new List<BatteryType>();

        return l.Except(lf).ToList();
    }

    public static List<ForkliftType> GetForkliftsToGet(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        List<ForkliftType> l = new List<ForkliftType>();

        l = db.Forklifts.ToList().Where(x => x.OrgUnitId == orgunitid && x.Condition.CanBeUse == true).ToList();

        List<DeviceEvidence> ev = db.DeviceEvidences.ToList().Where(x => x.DeviceClass == DeviceEvidence.deviceclass.Elektrowózek &&
                                                                        x.Closed == false && x.OrgUnitId == orgunitid).ToList();

        List<ForkliftType> lf = new List<ForkliftType>();

        ev.ForEach(x => {
            lf.Add(x.Device() as ForkliftType);
        });

        if (lf == null)
            lf = new List<ForkliftType>();

        return l.Except(lf).ToList();
    }

    public static void GetDevice(DeviceEvidence deviceEvidence)
    {
        deviceEvidence.Save();
    }

    public static void PassDevice(DeviceEvidence deviceEvidence)
    {
        deviceEvidence.Save();
    }

    public static void RegisterDamage(IDevice device, string damageDescription, UserType user, EmployeeType employee)
    {
        DeviceDamage damage = new DeviceDamage(device, damageDescription, user, employee);
    }
}

