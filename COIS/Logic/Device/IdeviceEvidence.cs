using System;
using System.Collections.Generic;
using System.Text;


public interface IdeviceEvidence
{
    public int DeviceId { get; set; }
    public int OrgUnitId { get; set; }
    public DateTime GetTime { get; set; }
    public EmployeeType GetEmployee { get; set; }
    public EmployeeType GetManagerEmployee { get; set; }
    public ZoneType GetZone { get; set; }
    public DateTime PassTime { get; set; }
    public EmployeeType PassEmployee { get; set; }
    public EmployeeType PassManagerEmployee { get; set; }
    public ZoneType PassZone { get; set; }
    public bool Closed { get; set; }



    public IDevice Device();

}

