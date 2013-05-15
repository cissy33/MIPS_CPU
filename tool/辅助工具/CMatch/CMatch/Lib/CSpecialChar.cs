using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace ZLib
{
    public enum ESpecialCharType
    {
        BS_None,
        //箭头
        BS_SingleDownBold,
        BS_SingleUp,
        BS_UpWithBlock,
        BS_UpArrow,
        BS_DoubleUp,
        BS_DoubleDown,
        BS_Right,
        //动作按钮
        BS_ZoomIn,
        BS_ZoomOut,
        BS_SizeDown,
        BS_SizeUp,
        BS_Shrink,
        BS_Expand,
        BS_Plus,
        BS_Minus,
        BS_CheckEmpty,
        BS_CheckClicked,
        BS_CheckCross,
        BS_ScrollUp,
        BS_Scroll,
        BS_CircleRight,
        BS_CircleLeft,
        BS_SmallBlock,
        BS_MiddleBlock,
        BS_LargeBlock,
        BS_Top,
        BS_Mid,
        BS_Bottom,
        //基本操作
        BS_Yes,
        BS_No,
        BS_Del,
        BS_Save,
        BS_SaveDown,
        BS_NewFile,
        BS_NewFileSolid,
        BS_UploadFile,
        BS_Setting,
        BS_Setting2,
        BS_Setting3,
        BS_Setting4,
        BS_Repeat,
        BS_Send,
        //特殊操作
        BS_Clock,
        BS_Search,
        BS_Pin,
        BS_Pen,
        BS_Write,
        BS_Keyboard,
        BS_Star,
        BS_Text,
        BS_Lock,
        BS_Key,
        BS_PowerOff,
        BS_Message,
        BS_MessageBlack
    }

    public static class CSpecialChar
    {
        /// <summary>
        /// 获得一个特殊字符
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public static string GetChar(ESpecialCharType aType)
        {
            string lChar = "";
            switch (aType)
            {
                case ESpecialCharType.BS_Yes:
                    lChar = "✔";
                    break;
                case ESpecialCharType.BS_No:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Del:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SingleDownBold:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SingleUp:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_UpWithBlock:
                    lChar = "⏏";
                    break;
                case ESpecialCharType.BS_UpArrow:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_DoubleUp:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_DoubleDown:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Right:
                    lChar = "➤";
                    break;
                case ESpecialCharType.BS_Clock:
                    lChar = "⏰";
                    break;
                case ESpecialCharType.BS_Search:
                    lChar = "🔍";
                    break;
                case ESpecialCharType.BS_Pin:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Pen:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Write:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_NewFile:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_NewFileSolid:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Keyboard:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Save:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SaveDown:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_UploadFile:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Plus:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Minus:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Star:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Shrink:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Expand:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_ZoomIn:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_ZoomOut:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SizeDown:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SizeUp:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Lock:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Key:
                    lChar = "⚿";
                    break;
                case ESpecialCharType.BS_PowerOff:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_CheckEmpty:
                    lChar = "☐";
                    break;
                case ESpecialCharType.BS_CheckClicked:
                    lChar = "☑";
                    break;
                case ESpecialCharType.BS_CheckCross:
                    lChar = "☒";
                    break;
                case ESpecialCharType.BS_Message:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_MessageBlack:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Setting:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Setting2:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Setting3:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Setting4:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_ScrollUp:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Scroll:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Repeat:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Send:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_CircleRight:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_CircleLeft:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_SmallBlock:
                    lChar = "▁";
                    break;
                case ESpecialCharType.BS_MiddleBlock:
                    lChar = "▅";
                    break;
                case ESpecialCharType.BS_LargeBlock:
                    lChar = "█";
                    break;
                case ESpecialCharType.BS_Top:
                    lChar = "";
                    break;
                case ESpecialCharType.BS_Mid:
                    lChar = "⬓";
                    break;
                case ESpecialCharType.BS_Bottom:
                    lChar = "";
                    break;

            }
            return lChar;
        }

        /// <summary>
        /// 获得字符集的字体
        /// </summary>
        /// <returns></returns>
        public static FontFamily GetSpecialFontFamily(bool aIsSpecial = true)
        {
            if (aIsSpecial)
            {
                return new FontFamily("Segoe UI Symbol");
            }
            else
            {
                return new FontFamily("Cambria");
            }
        }

        /// <summary>
        /// 像素转换字体大小，默认DPI=96
        /// </summary>
        /// <param name="aWidth"></param>
        /// <param name="aHeight"></param>
        /// <param name="aMinFontSize"></param>
        /// <returns></returns>
        public static double PixelToFontSize(double aWidth, double aHeight, double aMinFontSize = 10, int aDPI = 96)
        {
            return Math.Max(Math.Min(aWidth, aHeight) / aDPI * 72, aMinFontSize);
        }

        /// <summary>
        /// 根据父控件自动设置字体大小
        /// textblock不要设置stretch
        /// </summary>
        /// <param name="aTextBlock"></param>
        /// <param name="aBind"></param>
        public static void AutoChangeFontSize(TextBlock aTextBlock, bool aBind = true)
        {
            aTextBlock.SizeChanged -= ChangeFontSize;
            if (aBind) aTextBlock.SizeChanged += ChangeFontSize;
        }

        const int CTextPadding = 3;
        /// <summary>
        /// 循环调整字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ChangeFontSize(object sender, object e)
        {
            
            TextBlock lTextBlock = (sender as TextBlock);
            FrameworkElement lParent = lTextBlock.Parent as FrameworkElement;
            if (lParent == null) return;
            if (lParent.ActualHeight == 0) return;

            double pw = lParent.ActualWidth - CTextPadding;
            double ph = lParent.ActualHeight - CTextPadding;

            var d = lTextBlock.DesiredSize;

            while (d.Width < pw && d.Height < ph)
            {
                lTextBlock.FontSize++;
                lTextBlock.Measure(new Size(double.MaxValue, double.MaxValue));
                d = lTextBlock.DesiredSize;
            }

            while (d.Width > pw || d.Height > ph)
            {
                lTextBlock.FontSize--;
                lTextBlock.Measure(new Size(double.MaxValue, double.MaxValue));
                d = lTextBlock.DesiredSize;
            }
        }

        /// <summary>
        /// 转换文本，todo格式无法正常工作，怀疑是RTF文本为后台刷新
        /// </summary>
        /// <param name="aStr"></param>
        /// <param name="aFromFormat"></param>
        /// <param name="aToFormat"></param>
        /// <returns></returns>
        public static string ConvertTextFormat(string aStr, Windows.UI.Text.TextSetOptions aFromFormat = Windows.UI.Text.TextSetOptions.FormatRtf, Windows.UI.Text.TextGetOptions aToFormat = Windows.UI.Text.TextGetOptions.None)
        {
            string lStr;
            var lText = new RichEditBox();
            lText.IsSpellCheckEnabled = false;
            lText.Document.SetText(aFromFormat, aStr);
            lText.Measure(new Size(double.MaxValue, double.MaxValue));
            lText.Document.GetText(aToFormat, out lStr);
            return lStr;
        }
    }
}
