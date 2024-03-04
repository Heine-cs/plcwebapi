using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActUtlTypeLib;

namespace plcwebapi.Controllers
{
    public class PlcController : ApiController
    {
        ActMLUtlTypeClass plc = new ActMLUtlTypeClass();
        [Route("plcOpen")]
        [HttpPost]
        public string plcOpen(int logicalStationNumber)
        {
            plc.ActLogicalStationNumber = logicalStationNumber;
            plc.ActPassword = "";
            int plcStationStatus = (int)plc.Open();
            if (plcStationStatus == 0)
            {
                return "Plc is alive";
            }
            return "Plc is dead";
        }

        [Route("plcWrite")]
        [HttpPost]
        public string plcWrite(int logicalStationNumber,string address,int value)
        {
            plc.ActLogicalStationNumber = logicalStationNumber;
            plc.ActPassword = "";
            string plcStatus = this.plcOpen(logicalStationNumber);
            if (plcStatus == "Plc is dead")
            {
                return plcStatus;
            }
            int iReturnCode= (int)plc.SetDevice(address, value);
            if(iReturnCode == 0)
            {
                return plcStatus + ",Function SetDevice Success!SetDevice address is " + address + ",device value is " + value;
            }
            return "寫入站點" + address + "失敗";
        }
    }
}
