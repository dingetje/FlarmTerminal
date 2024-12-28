using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlarmTerminal
{
    internal class FlarmProperties
    {
        public enum ConfigurationItems
        {
            DEVTYPE,
            DEVICEID,
            CAP,
            SWVER,
            SWEXP,
            FLARMVER,
            BUILD,
            SER,
            REGION,
            OBSTDB,
            OBSTEXP,
            ID,
            NMEAOUT,
            BAUD,
            ACFT,
            RANGE,
            VRANGE,
            PRIV,
            NOTRACK,
            THRE,
            LOGINT,
            PILOT,
            COPIL,
            GLIDERID,
            GLIDERTYPE,
            COMPID,
            COMPCLASS,
            CFLAGS,
            UI,
            AUDIOOUT,
            AUDIOVOLUME,
            VOL,
            BATTERYTYPE,
            BRIGHTNESS,
        }

        public enum IGCSpecific
        {
            IGCSER,
        }
        // for dual port PowerFlarm
        public enum PowerFlarmSpecific
        {
            LIC,
            LS,
            NMEAOUT1,
            NMEAOUT2,
            BAUD1,
            BAUD2,
        }
    }
}
