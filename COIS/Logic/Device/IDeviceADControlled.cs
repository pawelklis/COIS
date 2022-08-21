using System;
using System.Collections.Generic;
using System.Text;


public interface IDeviceADControlled
{
    public string IP { get; set; }
    public string ADLogin { get; set; }
    public bool ADControlled { get; set; }
    public bool ADStatusAccountActive { get; set; }

    public void ActivateAD();
    public void DeactivateAD();
    public bool CheckADStatus();

}

