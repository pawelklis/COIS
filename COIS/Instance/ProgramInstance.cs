using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ProgramInstance
{
    public ReportModel model { get; set; }
    public ReportType report { get; set; }
    public UserType User { get;private set; }
    public OrgUnit OrgUnit { get; private set; }
    public ZoneType Zone { get; private set; }
    public bool UserLoged { get; private set; }
    public LoggerType Logger { get; set; }

    public ProgramInstance()
    {
        Logger = new LoggerType();
    }
    public void Login(string login)
    {
        User=UserType.GetUser(login);
        if (User != null)
        {
            if (User.TryLogin())
            {
                UserLoged = true;
            }
            else
            {
                //bad credentials
                UserLoged = false;
                User = null;
            }
        }
        else
        {
            //user not exists
            UserLoged = false;
        }
    }
    public void SetOrgUnit(OrgUnit org)
    {
        OrgUnit= org;
    }
    public void SetZoneType(ZoneType zone)
    {
        Zone = zone;
    }

    public void LogOut()
    {
        UserLoged = false;
        User = null;
        OrgUnit = null;
        Zone = null;

    }

}

