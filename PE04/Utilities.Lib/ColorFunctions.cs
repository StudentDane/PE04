using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Reflection;

namespace Utilities.Lib
{
    public class ColorFunctions
    {

        private static Random random = new Random();

        public static Brush GetRandomBrush()
        {
            Brush brush;
            string colorRGB = "#";
            for (int i = 0; i <= 3; i++)
            {
                int rgb = random.Next(0, 256);

                string hexRGB = "0" + rgb.ToString("X");

                colorRGB += hexRGB.Substring(hexRGB.Length - 2, 2);
            }
            brush = new BrushConverter().ConvertFromString(colorRGB) as Brush;
            return brush;
        }

        public static Brush GetBrush(int[] kleurWaarden)
        {
            Brush brush;
            string colorRGB = "#";
            for (int i = 0; i <= 2; i++)
            {
                int rgb = kleurWaarden[i];
                string hexRGB = "0" + rgb.ToString("X");
                colorRGB += hexRGB.Substring(hexRGB.Length - 2, 2);
            }
            //colorRGB += "FF";
            brush = new BrushConverter().ConvertFromString(colorRGB) as Brush;
            return brush;
        }

        public static int[] GetRGB(Brush brush)
        {
            int[] rgb = new int[3];
            string colorCode = brush.ToString();
            rgb[0] = HexToInt(colorCode.Substring(colorCode.Length - 6, 2));
            rgb[1] = HexToInt(colorCode.Substring(colorCode.Length - 4, 2));
            rgb[2] = HexToInt(colorCode.Substring(colorCode.Length - 2, 2));
            return rgb;
        }

        static int HexToInt(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }

        public static String GetColorName(int[] rgb)
        {
            string colorName = null;
            Color color = Color.FromRgb((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]);
            PropertyInfo colorProperty = typeof(Colors).GetProperties()
                    .FirstOrDefault(p => Color.AreClose((Color)p.GetValue(null), color));
            if (colorProperty != null)
            {
                colorName = colorProperty.Name;
            }
            return colorName;
        }


    }
}