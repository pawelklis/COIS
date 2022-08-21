using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceType
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsBattery { get; set; }


}

