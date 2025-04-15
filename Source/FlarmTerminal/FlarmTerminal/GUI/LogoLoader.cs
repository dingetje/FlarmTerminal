using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlarmTerminal.GUI
{
    internal static class LogoLoader
    {
        internal static string LoadFlarmLogo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream("FlarmTerminal.Resources.FLARM LOGO RGB.svg"))
            {
                if (stream == null)
                {
                    return string.Empty;
                }
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        internal static Bitmap? LoadLogo()
        {
            var logo = LoadFlarmLogo();
            if (string.IsNullOrEmpty(logo))
            {
                return null;
            }

            try
            {
                // Parse the SVG content into an SvgDocument
                var svgDoc = SvgDocument.FromSvg<SvgDocument>(logo);

                // Render the SVG into a Bitmap
                return svgDoc.Draw();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
