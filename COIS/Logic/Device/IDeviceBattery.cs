using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceBattery
{
    public string BatNumber { get; set; }
    public string Description { get; set; }
 
    public bool IsOnCharge();
}

