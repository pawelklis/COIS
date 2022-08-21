using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceParenty
{
    public int OrgUnitId { get; set; }
    public int ZoneId { get; set; }


    public OrgUnit GetOrgUnit();
    public ZoneType GetZoneType();
}

