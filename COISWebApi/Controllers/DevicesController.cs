using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COISWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {

        public DevicesController()
        {
            DeviceEvidence.OnDeviceGet += OnDeviceGet;
            DeviceEvidence.OnDevicePass += OnDevicePass;
        }
        void OnDeviceGet(object? sender, DeviceEvidence e)
        { 
        }
        void OnDevicePass(object? sender, DeviceEvidence e)
        {
        }

        // GET: api/<DevicesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("GetForklifts")]
        public IEnumerable<ForkliftType> GetForklifts()
        {
            return ForkliftType.GetForklifts();
        }

        // GET api/<DevicesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DevicesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        } 
        
        // POST api/<DevicesController>
        [HttpPost]
        [Route("GetDevice")]
        public DeviceEvidence GetDevice(int deviceid,DeviceEvidence.deviceclass deviceClass, int employeeid, int managerEmployeeid, int zoneid)
        {
           DeviceEvidence dv =  DeviceEvidence.GetDevice(deviceid,deviceClass, employeeid, managerEmployeeid, zoneid);

           return dv;
        }  
        // POST api/<DevicesController>
        [HttpPost]
        [Route("PassDevice")]
        public DeviceEvidence PassDevice(int deviceevidenceid, int employeeid,
                               int managerid, int zoneid, int conditionid, string damageDescription)
        {
            DeviceEvidence dv = DeviceEvidence.PassDevice(deviceevidenceid,employeeid,managerid,zoneid,conditionid,damageDescription);
          
            return dv;
        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
