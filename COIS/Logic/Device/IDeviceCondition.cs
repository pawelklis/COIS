using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceCondition
{
    public string Name { get; set; }
    public bool CanBeUse { get; set; }
    public int UsedInDeviceTypeId { get; set; }
    public DeviceType GetUsedInDeviceType();

}

