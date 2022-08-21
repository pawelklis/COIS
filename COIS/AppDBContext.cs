using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AppDBContext : DbContext
{
    public static AppDBContext DB()
    {
        return new AppDBContext();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
       // string mySqlConnectionStr = "Server=localhost;Database=cois;Uid=root;Pwd=mayday1";
       // options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));

        string sqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pawel.klis\source\repos\coisCore\coisCore\DB\Database1.mdf;Integrated Security=True";
        options.UseSqlServer(sqlConnectionString);

    }
    public AppDBContext() : base()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        //Entities.Add(tblEmployee);    
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region OrgUnit
        #endregion

        #region CrewModel
        modelBuilder.Entity<CrewModel>().Property(p => p.Employee)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<EmployeeType>(v)
        );
        modelBuilder.Entity<CrewModel>().Property(p => p.Zone)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<ZoneType>(v)
        );
        modelBuilder.Entity<CrewModel>().Property(p => p.WorkHourTyp)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<WorkHourType>(v)
        );
        #endregion

        #region InfoCategory
        #endregion

        #region WorkHourType
        #endregion

        #region EmployeeType
        //EmployeeType
        modelBuilder.Entity<EmployeeType>().Property(p => p.Zone)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<ZoneType>(v)
        );
        #endregion

        #region WorkShift
        //WorkShift
        modelBuilder.Entity<WorkShift>().Property(p => p.Zone)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<ZoneType>(v)
        );
        #endregion

        #region ZoneType
        //ZoneType
        //modelBuilder.Entity<ZoneType>().Property(p => p.OrganizationUnit)
        //.HasConversion(
        //   v => JsonConvert.SerializeObject(v),
        //   v => JsonConvert.DeserializeObject<OrgUnit>(v)
        //);
        #endregion

        #region UserType
        //UserType
        modelBuilder.Entity<UserType>().Property(p => p.Employee)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<EmployeeType>(v)
        );
        modelBuilder.Entity<UserType>().Property(p => p.Zones)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<List<ZoneType>>(v)
        );

        modelBuilder.Entity<UserType>().Property(p => p.Permissions)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<List<PermissionType>>(v)
        );
        #endregion

        #region ReportModel
        //ReportModel
        modelBuilder.Entity<ReportModel>().Property(p => p.Items)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<List<ReportModelItem>>(v)
        );
        modelBuilder.Entity<ReportModel>().Property(p => p.EnableZones)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<List<ZoneType>>(v)
        );
        #endregion

        #region Report
        //Report
        modelBuilder.Entity<ReportType>().Property(p => p.Items)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<List<ReportItem>>(v)
        );

        modelBuilder.Entity<ReportType>().Property(p => p.WorkShift)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<WorkShift>(v)
        );

        modelBuilder.Entity<ReportType>().Property(p => p.Zone)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<ZoneType>(v)
        );

        modelBuilder.Entity<ReportType>().Property(p => p.User)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<UserType>(v)
        );

        modelBuilder.Entity<ReportType>().Property(p => p.Crew)
        .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<CrewType>(v)
        );
        #endregion

        #region Delegations
        modelBuilder.Entity<DelegationType>().Property(p => p.Employee)
.HasConversion(
   v => JsonConvert.SerializeObject(v),
   v => JsonConvert.DeserializeObject<EmployeeType>(v)
);

        modelBuilder.Entity<DelegationType>().Property(p => p.FromZone)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<ZoneType>(v)
);

        modelBuilder.Entity<DelegationType>().Property(p => p.ToZone)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<ZoneType>(v)
);

        #endregion

        #region DeviceConditions
        #endregion

        #region DeviceTypes
        #endregion

        #region BatteryTypes
        modelBuilder.Entity<BatteryType>().Property(p => p.Condition)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<DeviceCondition>(v)
);
        #endregion

        #region ForkLiftTypes
        modelBuilder.Entity<ForkliftType>().Property(p => p.Condition)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<DeviceCondition>(v)
);
        #endregion

        #region ScannerTypes
        modelBuilder.Entity<ScannerType>().Property(p => p.Condition)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<DeviceCondition>(v)
);
        #endregion

        #region DeviceEvidence
        modelBuilder.Entity<DeviceEvidence>().Property(p => p.GetEmployee)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmployeeType>(v)
);
        modelBuilder.Entity<DeviceEvidence>().Property(p => p.GetManagerEmployee)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmployeeType>(v)
);
        modelBuilder.Entity<DeviceEvidence>().Property(p => p.GetZone)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<ZoneType>(v)
);

        modelBuilder.Entity<DeviceEvidence>().Property(p => p.PassEmployee)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmployeeType>(v)
);
        modelBuilder.Entity<DeviceEvidence>().Property(p => p.PassManagerEmployee)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmployeeType>(v)
);
        modelBuilder.Entity<DeviceEvidence>().Property(p => p.PassZone)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<ZoneType>(v)
);
        #endregion

        #region EmptyBoxAdverts
        modelBuilder.Entity<EmptyBoxAdvert>().Property(p => p.Offerts)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<List<EmptyBoxOffert>>(v)
);

        modelBuilder.Entity<EmptyBoxAdvert>().Property(p => p.EmptyBox)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmptyBoxType>(v)
);
        modelBuilder.Entity<EmptyBoxAdvert>().Property(p => p.User)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<UserType>(v)
);
        #endregion

        #region DeviceDamages
        modelBuilder.Entity<DeviceDamage>().Property(p => p.History)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<List<DeviceDamageHistory>>(v)
);
        modelBuilder.Entity<DeviceDamage>().Property(p => p.Employee)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmployeeType>(v)
);
        modelBuilder.Entity<DeviceDamage>().Property(p => p.User)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<UserType>(v)
);
        #endregion

        #region EmptyBoxOrders
        modelBuilder.Entity<EmptyBoxOrderType>().Property(p => p.OrderUnit)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<OrgUnit>(v)
);
        modelBuilder.Entity<EmptyBoxOrderType>().Property(p => p.ParentOrgUnit)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<OrgUnit>(v)
);
        modelBuilder.Entity<EmptyBoxOrderType>().Property(p => p.EmptyBox)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<EmptyBoxType>(v)
);
        modelBuilder.Entity<EmptyBoxOrderType>().Property(p => p.History)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<List<EmptyBoxOrderHistory>>(v)
);
        #endregion

        #region Requisites
        modelBuilder.Entity<RequisiteType>().Property(p => p.Condition)
.HasConversion(
v => JsonConvert.SerializeObject(v),
v => JsonConvert.DeserializeObject<DeviceCondition>(v)
);
        #endregion


    }

    //public List<object> Entities = new List<object>();

    public DbSet<OrgUnit> OrgUnits { get; set; }
    public DbSet<WorkHourType> WorkHours { get; set; }
    public DbSet<EmployeeType> Employees { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<ZoneType> Zones { get; set; }
    public DbSet<UserType> Users { get; set; }
    public DbSet<ReportModel> ReportModels { get; set; }
    public DbSet<ReportType> Reports { get; set; }
    public DbSet<CrewModel> CrewModels { get; set; }
    public DbSet<InfoCategory> InfoCategories { get; set; }
    public DbSet<DelegationType> Delegations { get; set; }
    public DbSet<DeviceCondition> DeviceConditions { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<BatteryType> Batteries { get; set; }
    public DbSet<ForkliftType> Forklifts { get; set; }
    public DbSet<ScannerType> Scanners { get; set; }
    public DbSet<DeviceEvidence> DeviceEvidences { get; set; }
    public DbSet<RequisiteType> Requisites { get; set; }
    public DbSet<EmptyBoxType> EmptyBoxes { get; set; }
    public DbSet<EmptyBoxOrderType> BoxOrders { get; set; }
    public DbSet<EmptyBoxAdvert> BoxAdverts { get; set; }
    public DbSet<DeviceDamage> DeviceDamages { get; set; }
}



public class AppDataAccessLayer
{
    AppDBContext db = new AppDBContext(new DbContextOptions<AppDBContext>());




    public List<T> GetAllObject<T>(string[] include = null) where T : class
    {
        if (include == null)
            include = new string[0];

        switch (include.Length)
        {
            case 0:
                return db.Set<T>().ToListAsync<T>().Result;
            case 1:
                return db.Set<T>().Include(include[0]).ToListAsync<T>().Result;
            case 2:
                return db.Set<T>().Include(include[0]).Include(include[1]).ToListAsync<T>().Result;
            case 3:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).ToListAsync<T>().Result;
            case 4:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).ToListAsync<T>().Result;
            case 5:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).ToListAsync<T>().Result;
            case 6:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).Include(include[5]).ToListAsync<T>().Result;
            case 7:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).Include(include[5]).Include(include[6]).ToListAsync<T>().Result;
            case 8:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).Include(include[5]).Include(include[6]).Include(include[7]).ToListAsync<T>().Result;
            case 9:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).Include(include[5]).Include(include[6]).Include(include[7]).Include(include[8]).ToListAsync<T>().Result;
            case 10:
                return db.Set<T>().Include(include[0]).Include(include[1]).Include(include[2]).Include(include[3]).Include(include[4]).Include(include[5]).Include(include[6]).Include(include[7]).Include(include[8]).Include(include[9]).ToListAsync<T>().Result;
        }


        return db.Set<T>().ToListAsync<T>().Result;

    }

    public void UpdateObject<T>(T o) where T : class
    {
        try
        {
            this.db.Set<T>().Update(o);
            db.SaveChanges();
        }
        catch(Exception ex)
        {

        }
    }

    public void UpdateObjectAsync<T>(T o) where T : class
    {
        try
        {
            this.db.Set<T>().Update(o);
            db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }
    public void RemovetObject<T>(T o, bool async = false) where T : class
    {

        try
        {
            db.Set<T>().Remove((T)o);
            db.SaveChanges();
        }
        catch (Exception ex)
        {

        }


    }

}


//public static class classIs
//{
//    public static bool IsGenericList(this object o)
//    {
//        var oType = o.GetType();
//        return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
//    }
//}


