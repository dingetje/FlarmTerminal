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
            RADIOID
        }

        private static Dictionary<ConfigurationItems, string> _configNameLookup =
            new Dictionary<ConfigurationItems, string>
            {
                { ConfigurationItems.DEVTYPE, "Device Type" },
                { ConfigurationItems.DEVICEID, "Device ID" },
                { ConfigurationItems.CAP, "Capabilities" },
                { ConfigurationItems.SWVER, "Firmware Version" },
                { ConfigurationItems.SWEXP, "Expiration" },
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
                { ConfigurationItems.CFLAGS, "Config Flags" },
                { ConfigurationItems.UI, "User Interface" },
                { ConfigurationItems.AUDIOOUT, "Audio Output" },
                { ConfigurationItems.AUDIOVOLUME, "Audio Volume" },
                { ConfigurationItems.VOL, "Volume Level" },
                { ConfigurationItems.BATTERYTYPE, "Battery Type" },
                { ConfigurationItems.BRIGHTNESS, "Brightness Level" },
                { ConfigurationItems.RADIOID, "Radio ID" }
            };

        public static string GetConfigName(ConfigurationItems item)
        {
            if (!_configNameLookup.TryGetValue(item, out string value))
            {
                return item.ToString();
            }

            return value;
        }
        public static string GetIGCName(IGCSpecific item)
        {
            if (!_igcNameLookup.TryGetValue(item, out string value))
            {
                return item.ToString();
            }
            return value;
        }

        public static string GetPowerFlarmName(PowerFlarmSpecific item)
        {
            if (!_powerFlarmNameLookup.TryGetValue(item, out string value))
            {
                return item.ToString();
            }
            return value;
        }

        public enum IGCSpecific
        {
            IGCSER,
        }
        private static Dictionary<IGCSpecific, string> _igcNameLookup =
            new Dictionary<IGCSpecific, string>
            {
                { IGCSpecific.IGCSER, "IGC ID" },
            };

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

        private static Dictionary<PowerFlarmSpecific, string> _powerFlarmNameLookup =
            new Dictionary<PowerFlarmSpecific, string>
            {
                { PowerFlarmSpecific.BAUD1, "Baud Rate Port 1" },
                { PowerFlarmSpecific.BAUD2, "Baud Rate Port 2" },
                { PowerFlarmSpecific.NMEAOUT1, "NMEA Port 1" },
                { PowerFlarmSpecific.NMEAOUT2, "NMEA Port 2" },
                { PowerFlarmSpecific.LS, "Configuration List" },
                { PowerFlarmSpecific.LIC, "Licenses" },
            };
    }
}
