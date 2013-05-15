using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ZLib
{
    public static class CColors
    {

        static byte CAlpha = 255;    //公共透明度

        private static List<Color> iColorList = new List<Color>();
        public static List<Color> ColorList
        {
            get { return iColorList; }
            set { iColorList = value;}
        }

        public static Dictionary<Color, string> ColorDetails = new Dictionary<Color, string>();

        static CColors()
        {
            AddColor(Yellow, "提醒");
            AddColor(Blue, "会议");
            AddColor(Pink, "生日");
            AddColor(Purple, "工作");
            AddColor(White, "技术");
            AddColor(Green, "收支");
            AddColorFromRGB(250, 215, 215, "生活");
        }

        public static void AddColor(Color aColor, string aDetail = "")
        {
            if (!iColorList.Contains(aColor))
            {
                iColorList.Add(aColor);
                ColorDetails.Add(aColor, aDetail);
            }
        }

        public static void AddColorFromRGB(byte r, byte g, byte b, string aDetail = "")
        {
            AddColor(Color.FromArgb(CAlpha, r, g, b), aDetail);
        }

        public static void SetColorDetail(Color aColor, string aDetail)
        {
            if (ColorDetails.ContainsKey(aColor))
            {
                ColorDetails[aColor] = aDetail;
            }
            else
            {
                ColorDetails.Add(aColor, aDetail);
            }
        }

        public static string GetColorDetail(Color aColor)
        {
            string lStr = "";
            ColorDetails.TryGetValue(aColor, out lStr);
            if (lStr == null)
            {
                lStr = "";
            }
            return lStr;
        }

        /// <summary>
        /// 颜色转为实体刷子
        /// </summary>
        /// <param name="aColor"></param>
        /// <returns></returns>
        public static SolidColorBrush ColorToBrush(Color aColor)
        {
            return new SolidColorBrush(aColor);
        }

        /// <summary>
        /// 实体刷子转为颜色
        /// </summary>
        /// <param name="aBrush"></param>
        /// <returns></returns>
        public static Color BrushToColor(Brush aBrush)
        {
            try
            {
                return (aBrush as SolidColorBrush).Color;
            }
            catch (System.Exception)
            {
                return Colors.White;
            }
        }

        static byte FixColor(int aValue)
        {
            return (byte)(aValue > 255 ? 255 : (aValue < 0 ? 0 : aValue));
        }

        /// <summary>
        /// 加减颜色偏移
        /// </summary>
        /// <param name="aColor"></param>
        /// <param name="aOffset"></param>
        /// <returns></returns>
        public static Color Darker(Color aColor, int aOffset = 0)
        {
            return Color.FromArgb(255, FixColor(aColor.R - aOffset), FixColor(aColor.G - aOffset), FixColor(aColor.B - aOffset));
        }
        public static Color Lighter(Color aColor, int aOffset = 0) { return Darker(aColor, -aOffset); }

        /// <summary>
        /// 常规颜色定义
        /// </summary>
        public static Color Gold = Color.FromArgb(CAlpha, 255, 210, 54);
        public static Color Blue = Color.FromArgb(CAlpha, 201, 236, 248);
        public static Color Green = Color.FromArgb(CAlpha, 197, 247, 193);
        public static Color Pink = Color.FromArgb(CAlpha, 241, 195, 241);
        public static Color Purple = Color.FromArgb(CAlpha, 212, 205, 243);
        public static Color White = Color.FromArgb(CAlpha, 245, 245, 245);
        public static Color Yellow = Color.FromArgb(CAlpha, 248, 248, 182);

        public static Color ColorMain = Color.FromArgb(CAlpha, 182, 23, 23);
        public static Color ColorMask = ColorMain;

        public static Brush BrushMain = new SolidColorBrush(ColorMain);
        public static Brush BrushWhite = new SolidColorBrush(CColors.White);
    }
}
