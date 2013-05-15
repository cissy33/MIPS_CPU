using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;

namespace ZLib
{
    public enum EPopupPosition
    {
        CLess = 0,
        CMin = 1,
        CMid = 2,
        CMax = 3,
        CExceed = 4
    }
    public static class CWinPos
    {
        /************************************************************************/
        /* 视窗位置、大小                                                       */
        /************************************************************************/
        /// <summary>
        /// 常用控件名
        /// </summary>
        public const string CMAINPAGE = "MainPage";
        
        /// <summary>
        /// 注册控件信息
        /// </summary>
        static Dictionary<string, FrameworkElement> iControls = new Dictionary<string, FrameworkElement>();

        /// <summary>
        /// 获得一个控件相对于另一个控件的偏移位置
        /// </summary>
        /// <param name="aControl"></param>
        /// <param name="aBase"></param>
        /// <returns></returns>
        static Point GetOffset(UIElement aControl, UIElement aBase)
        {
                return aControl.TransformToVisual(aBase).TransformPoint(new Point(0, 0));
        }

        /// <summary>
        /// 读取一个控件的尺寸
        /// </summary>
        /// <param name="aControl"></param>
        /// <returns></returns>
        public static Point GetSize(FrameworkElement aControl, bool aUpdateLayout = true)
        {
            //判断是否要更新界面
            if (aUpdateLayout)
            {
                aControl.UpdateLayout();
            }
            try
            {
                if (aControl.Height + aControl.Width > 0)
                {
                    return new Point(aControl.Width, aControl.Height);
                }
                else
                if (aControl.ActualHeight + aControl.ActualWidth > 0)
                {
                    return new Point(aControl.ActualWidth, aControl.ActualHeight);
                }
                 
            }
            catch (Exception) { }
            return new Point(0, 0);
        }

        /// <summary>
        /// 计算移动距离
        /// </summary>
        /// <param name="aDistance">popup到主控件的距离</param>
        /// <param name="aBaseLen">主控件宽度</param>
        /// <param name="aPopupLen">popup宽度</param>
        /// <param name="aPosition">对齐位置</param>
        /// <param name="aOffset">额外偏移</param>
        /// <returns>移动距离</returns>
        static double CalcPos(double aDistance, double aBaseLen, double aPopupLen, EPopupPosition aPosition, double aOffset = 0)
        {
            double lRet = 0;
            switch (aPosition)
            {
                case EPopupPosition.CLess:
                    lRet = -aDistance - aPopupLen;
                    break;
                case EPopupPosition.CMin:
                    lRet = -aDistance;
                    break;
                case EPopupPosition.CMid:
                    lRet = -aPopupLen / 2 - (aDistance - aBaseLen / 2);
                    break;
                case EPopupPosition.CMax:
                    lRet = -(aDistance - aBaseLen) - aPopupLen;
                    break;
                case EPopupPosition.CExceed:
                    lRet = -(aDistance - aBaseLen);
                    break;
                default:
                    break;
            }
            return lRet + aOffset;
        }

        /// <summary>
        /// 设置popup相对于另一个控件的位置
        /// </summary>
        /// <param name="aPopup">popup控件</param>
        /// <param name="aControl">主对齐控件</param>
        /// <param name="aHorizontal">水平对齐方式</param>
        /// <param name="aVertical">垂直对齐方式</param>
        /// <param name="aHorizontalOffset">水平对齐偏移</param>
        /// <param name="aVerticalOffset">垂直对齐偏移</param>
        public static void SetPopupPos(Popup aPopup, FrameworkElement aControl, EPopupPosition aHorizontal, EPopupPosition aVertical, bool aCheckOutSide = false, double aHorizontalOffset = 0, double aVerticalOffset = 0)
        {
            Point lPopupSize = GetSize(aPopup, false);
            Point lBaseSize = GetSize(aControl, false);
            Point lOffset = GetOffset(aPopup, aControl);
            aPopup.HorizontalOffset = CalcPos(lOffset.X, lBaseSize.X, lPopupSize.X, aHorizontal, aHorizontalOffset);
            aPopup.VerticalOffset = CalcPos(lOffset.Y, lBaseSize.Y, lPopupSize.Y, aVertical, aVerticalOffset);

            //如果参考对象也是popup，则需要考虑参考对象本身的偏移
            if (aControl.GetType().Name == "Popup")
            {
                aPopup.HorizontalOffset += ((Popup)aControl).HorizontalOffset;
                aPopup.VerticalOffset += ((Popup)aControl).VerticalOffset;
            }

            if (aCheckOutSide)
            {
                aPopup.HorizontalOffset = Math.Min(Math.Max(0, aPopup.HorizontalOffset), GetWindowSize().Right - GetWindowSize().Left - lPopupSize.X);
                aPopup.VerticalOffset = Math.Min(Math.Max(0, aPopup.VerticalOffset), GetWindowSize().Bottom - GetWindowSize().Top - lPopupSize.Y);
            }
        }

        /// <summary>
        /// 注册一个控件
        /// </summary>
        /// <param name="aControl"></param>
        public static void RegControl(string aName, FrameworkElement aControl)
        {
            iControls.Remove(aName);
            iControls.Add(aName, aControl);
        }

        /// <summary>
        /// 获得一个已注册的控件
        /// </summary>
        /// <param name="aName"></param>
        /// <returns></returns>
        public static FrameworkElement GetControl(string aName)
        {
            FrameworkElement lControl;
            try
            {
                iControls.TryGetValue(aName, out lControl);
                return lControl;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得主页控件
        /// </summary>
        /// <returns></returns>
        public static FrameworkElement GetMainPage()
        {
            return GetControl(CMAINPAGE);
        }

        /// <summary>
        /// 获得窗口大小
        /// </summary>
        /// <returns></returns>
        public static Rect GetWindowSize()
        {
            return Window.Current.Bounds;
        }
    }
}
