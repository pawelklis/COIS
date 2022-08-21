using System;
using System.Collections.Generic;
using System.Text;


public interface IDevice
{
    public int Id { get; set; }
    public string BarCode { get; set; }
    public string Name { get; set; } 
    public DeviceCondition Condition { get; set; }
    public DeviceEvidence.deviceclass DeviceClass { get; set; }
    public string? PassDescription { get; set; }
    public void Save();
}

