
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class UserType:IDatabase
{
    public int Id { get; set; }
    [FrontEnd("Login", FrontendType.text, true)]
    public string Login { get; set; }
    [FrontEnd("Utworzono", FrontendType.text, true,false)]
    public DateTime CreateTime { get; set; }
    public EmployeeType Employee { get; set; }
    [FrontEnd("Strefa", FrontendType.listmulti, false, true, ObjectListNames.zones, "id", "name")]
    public List<ZoneType> Zones { get; set; }
    public List<PermissionType> Permissions { get; set; }

    [NotMapped]
    public List<Tuple<string, string>> UserAdInfo;

    public UserType()
    {
        Login = "";
        Employee=new EmployeeType();
        CreateTime = DateTime.Now;
        Zones = new List<ZoneType>();
        Permissions = new List<PermissionType>();
    }

    public bool TryLogin()
    {
        return true;
    }
    public bool Can(PermissionType.PermissionList permission)
    {
        foreach(var x in Permissions)        
        {
            if (x.Permission == permission)
                return true;
        }
        return false;
    }
    public void ChangePermission(PermissionType.PermissionList permission)
    {
        if(Permissions.Find(x=>x.Permission == permission) == null)
        {
            Permissions.Add(new PermissionType() { Permission=permission, Access=true });
        }
        else
        {
            if (Permissions.Find(x => x.Permission == permission).Access == true)
                Permissions.Find(x => x.Permission == permission).Access = false;
            else
                Permissions.Find(x => x.Permission == permission).Access = true;
        }
    }
    public List<OrgUnit> GetEnabledOrgUnits()
    {
        List<OrgUnit> l = new List<OrgUnit>();

        foreach(var z in Zones)
        {
            if(l.Find(x=>x.Id==z.OrganizationUnitID)==null)
                l.Add(OrgUnit.GetOrgUnitById(z.OrganizationUnitID));
        }

        return l;
    }

    public async Task<bool> CheckPasswordAsync(string pwd)
    {
        await GetUserADDAta();
        return A_Directory.ValidateActiveDirectoryLogin(this.Login, pwd, 0);
    }
    public  bool CheckPassword(string pwd)
    {
        GetUserADDAta();
        return A_Directory.ValidateActiveDirectoryLogin(this.Login, pwd, 0);
    }


    public async System.Threading.Tasks.Task GetUserADDAta()
    {
        await System.Threading.Tasks.Task.Run(() => {

            List<A_Directory.UserInfoProps> props = new List<A_Directory.UserInfoProps>();

            props.Add(A_Directory.UserInfoProps.displayName);
            props.Add(A_Directory.UserInfoProps.employeeNumber);
            props.Add(A_Directory.UserInfoProps.title);
            props.Add(A_Directory.UserInfoProps.givenName);
            props.Add(A_Directory.UserInfoProps.mail);
            props.Add(A_Directory.UserInfoProps.mobile);
            props.Add(A_Directory.UserInfoProps.manager);
            props.Add(A_Directory.UserInfoProps.telephoneNumber);
            props.Add(A_Directory.UserInfoProps.name);
            props.Add(A_Directory.UserInfoProps.streetAddress);
            props.Add(A_Directory.UserInfoProps.sn);
            props.Add(A_Directory.UserInfoProps.targetAddress);


            UserAdInfo =  A_Directory.GetuserAdInformation(Login, props);
        
        });
    }
    public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

  
    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.RemovetObject(this);
    }

    public string GetADInfo(A_Directory.UserInfoProps info)
    {
        if (UserAdInfo == null)
            return "";
        return (UserAdInfo.Single(x => x.Item1 == info.ToString()).Item2);
    }

    public void Save()
    {
        if (Employee == null)
        {
            if (Zones.Count > 0)
            {
                Employee = EmployeeType.GetOrAddEmployee(
                    GetADInfo(A_Directory.UserInfoProps.employeeNumber),
                    GetADInfo(A_Directory.UserInfoProps.givenName),
                    GetADInfo(A_Directory.UserInfoProps.sn),
                    Zones[0]);
            }
        }

        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);
    }

    public static UserType GetUser(int id)
    {
        AppDBContext ad = new AppDBContext();
        return ad.Users.Single(x => x.Id == id);
    }

    public static UserType GetUser(string login)
    {
        AppDBContext ad = new AppDBContext();
        return ad.Users.Where(x => x.Login.ToLower() == login).FirstOrDefault();
    }

    public static List<UserType> GetAllUsers()
    {
        AppDBContext ad = new AppDBContext();
        return ad.Users.ToList();
    }
}

