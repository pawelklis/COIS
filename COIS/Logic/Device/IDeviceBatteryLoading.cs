using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceBatteryLoading
{
    public int IdBattery { get; set; }
    public IDevice GetBattery();
}

