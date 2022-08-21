

using System;
using System.Collections.Generic;

public class PermissionType:INoDatabase
{
    public PermissionType()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
    public string Guid { get; set; }
    public PermissionList Permission { get; set; }
    public bool Access { get; set; }


    public enum PermissionList
    {
        ReportModelEdit,
        ReportEdit,
        OrgUnitsEdit,
        ZonesEdit,
        UsersEdit,
        EmployeesEdit,
        ScannersEdit
    }
    public static List<Tuple<PermissionList,string>> PermissionSelects()
    {
        List<Tuple<PermissionList, string>> l = new List<Tuple<PermissionList, string>>();

        l.Add(new Tuple<PermissionList, string>(PermissionList.ReportEdit, "Edycja raportów"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.ReportModelEdit, "Edycja modeli raportów"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.OrgUnitsEdit, "Edycja jednostek organizacyjnych"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.ZonesEdit, "Edycja stref"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.UsersEdit, "Edycja użytkowników"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.EmployeesEdit, "Edycja pracowników"));
        l.Add(new Tuple<PermissionList, string>(PermissionList.ScannersEdit, "Edycja ewidencji skanerów"));


        return l;
    }


    public static PermissionList GetPermission(string s)
    {
        foreach(var i in PermissionSelects())
        {
            if (i.Item2 == s)
                return i.Item1;
        }
        return 0;
    }
    public static string GetPermission(PermissionList s)
    {
        foreach (var i in PermissionSelects())
        {
            if (i.Item1 == s)
                return i.Item2;
        }
        return "";
    }


}