using System.Collections.Generic;

namespace FlarmTerminal
{
    public static class FlarmProperties
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

        private static Dictionary<ConfigurationItems, string> _configNameLookup = new Dictionary<ConfigurationItems, string>
        {
            { ConfigurationItems.DEVTYPE, "Device Type" },
            { ConfigurationItems.DEVICEID, "Device ID" },
            { ConfigurationItems.CAP, "Capabilities" },
            { ConfigurationItems.SWVER, "Firmware Version" },
            { ConfigurationItems.SWEXP, "Software Expiration" },
            { ConfigurationItems.FLARMVER, "Flarm Version" },
            { ConfigurationItems.BUILD, "Build" },
            { ConfigurationItems.SER, "Serial Number" },
            { ConfigurationItems.REGION, "Region" },
            { ConfigurationItems.OBSTDB, "Obstacle Database" },
            { ConfigurationItems.OBSTEXP, "Obstacle Expiration" },
            { ConfigurationItems.ID, "ID" },
            { ConfigurationItems.NMEAOUT, "NMEA Output" },
            { ConfigurationItems.BAUD, "Baud Rate" },
            { ConfigurationItems.ACFT, "Aircraft Type" },
            { ConfigurationItems.RANGE, "Range" },
            { ConfigurationItems.VRANGE, "Vertical Range" },
            { ConfigurationItems.PRIV, "Privacy Mode" },
            { ConfigurationItems.NOTRACK, "No Track Mode" },
            { ConfigurationItems.THRE, "Threshold" },
            { ConfigurationItems.LOGINT, "Log Interval" },
            { ConfigurationItems.PILOT, "Pilot Name" },
            { ConfigurationItems.COPIL, "Co-Pilot Name" },
            { ConfigurationItems.GLIDERID, "Glider ID" },
            { ConfigurationItems.GLIDERTYPE, "Glider Type" },
            { ConfigurationItems.COMPID, "Competition ID" },
            { ConfigurationItems.COMPCLASS, "Competition Class" },
            { ConfigurationItems.CFLAGS, "Configuration Flags" },
            { ConfigurationItems.UI, "User Interface" },
            { ConfigurationItems.AUDIOOUT, "Audio Output" },
            { ConfigurationItems.AUDIOVOLUME, "Audio Volume" },
            { ConfigurationItems.VOL, "Volume Level" },
            { ConfigurationItems.BATTERYTYPE, "Battery Type" },
            { ConfigurationItems.BRIGHTNESS, "Brightness Level" }
        };

        public static string GetConfigName(ConfigurationItems item)
        {
            if (!_configNameLookup.TryGetValue(item, out string value))
            {
                return item.ToString();
            }
            return value;
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
