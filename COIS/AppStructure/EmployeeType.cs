using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

public class EmployeeType:IDatabase, IEquatable<EmployeeType>
{
    public int Id { get; set; }
    [FrontEnd("Imię", FrontendType.text, true)]
    public string FirstName { get; set; }
    [FrontEnd("Nazwisko", FrontendType.text, true)]
    public string LastName { get; set; }
    [FrontEnd("Strefa", FrontendType.list, false, true, ObjectListNames.zones, "id", "name")]
    public ZoneType Zone { get; set; }
    [FrontEnd("Kod", FrontendType.text, true)]
    public string? EmployeeCode { get; set; }


    //excepts
    public static EmployeeType Empty()
    {
        EmployeeType employee = new EmployeeType()
        {
            Id = 0,
            FirstName = "",
            EmployeeCode = "",
            LastName = ""
        };
        return employee;
    }

    public List<DeviceEvidence> DevicesToPassAsync()
    {
        return DeviceEvidence.GetDevicesToPassAsync(Id).Result;
    }
    public bool Equals(EmployeeType other)
    {
        if (other is null)
            return false;

        return this.Id == other.Id;
    }

    public override bool Equals(object obj) => Equals(obj as EmployeeType);
    public override int GetHashCode() => (Id).GetHashCode();

    //excepts end


    public EmployeeType()
    {
        FirstName = "";
        LastName = "";
        EmployeeCode = "";
        Zone = new ZoneType();
    }
    public EmployeeType(string employeeFullName, ZoneType zone, string employeeCode)
    {
        LoadFirstAndLastName(employeeFullName);
        Zone = zone;
        EmployeeCode = employeeCode;
        SaveAsync();
    }

    public DelegationType ActiveDelegation()
    {
        AppDBContext db = new AppDBContext();
        return db.Delegations.ToList().Where(x => x.Employee.Id == this.Id).ToList().Where(x=>x.Closed==false).FirstOrDefault();
    }
    public static EmployeeType GetEmployee(int id)
    {
        AppDBContext db = new AppDBContext();

        EmployeeType o;
        o = db.Employees.Where(x => x.Id == id).FirstOrDefault();

        return o;
    }
    public static EmployeeType GetOrAddEmployee(string employeeCode, string employeeFullName, ZoneType zone)
    {
        AppDBContext db = new AppDBContext();

        EmployeeType o;
        o= db.Employees.Where(x => x.EmployeeCode == employeeCode).FirstOrDefault();

        if (o == null)
        {
            o = new EmployeeType(employeeFullName, zone, employeeCode);
        }

        return o;
    }
    public static EmployeeType GetOrAddEmployee(string employeeCode, string firstname,string lastname, ZoneType zone)
    {
        AppDBContext db = new AppDBContext();

        EmployeeType o;
        o = db.Employees.Where(x => x.EmployeeCode == employeeCode).FirstOrDefault();

        if (o == null)
        {
            o = new EmployeeType(firstname+ " " + lastname, zone, employeeCode);
        }

        return o;
    }
    public static List<EmployeeType> GetEmployees()
    {
        AppDBContext db = new AppDBContext();
        return db.Employees.ToList().OrderBy(x=>x.LastName).ToList();
    }
    public static List<EmployeeType> GetEmployees(int zoneid)
    {
        AppDBContext db = new AppDBContext();
        return db.Employees.ToList().Where(x => x.Zone?.Id == zoneid).ToList();
    }
    private void LoadFirstAndLastName(string employeeFullName)
    {
        var names = employeeFullName.Trim().Split(" ", 2);

        LastName = names.First();

        if (names.Count() > 1)
            FirstName = names.Last();
    }

    public string FullName()
    {
        return LastName + " " + FirstName;
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

    public void SaveAsync()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();

        ap.UpdateObjectAsync(this);
    }








    public class EmployeeReportView
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Zone { get; set; }

        public string? EmployeeCode { get; set; }
    }
}

