using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAP_LIB_VELIB
{
    class StationProperties
    {
        private int stationNumber;
        private DateTime? lastUpdate;
        private int aviableBikes;

        public StationProperties(int stationNumber, DateTime? lastUpdate, int aviableBikes)
        {
            this.stationNumber = stationNumber;
            this.lastUpdate = lastUpdate;
            this.aviableBikes = aviableBikes;
        }

        public int StationNumber { get => stationNumber; }
        public DateTime? LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public int AviableBikes { get => aviableBikes; set => aviableBikes = value; }

        
    }
}
