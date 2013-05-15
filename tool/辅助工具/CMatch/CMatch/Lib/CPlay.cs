using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Primitives;

namespace ZLib
{
    public enum EEasingFunction
    {
        CCircleEasing,
        CBackEasing,
        CNone
    }

    public enum ETransform3DDirection
    {
        CAxisX, CAxisY, CAxisZ
    }

    public enum ETransform3DStyle
    {
        CHide, CShow, CNormal
    }

    public static class CPlay
    {

        /************************************************************************/
        /* 动画                                                                 */
        /************************************************************************/
        /// <summary>
        /// 生成一个简单的动画
        /// </summary>
        /// <param name="aControl"></param>
        /// <param name="aBindingPropertyName"></param>
        /// <param name="aFrom"></param>
        /// <param name="aTo"></param>
        /// <param name="aDuration"></param>
        /// <returns></returns>
        public static Timeline GetDoubleAnimation(FrameworkElement aControl, string aBindingPropertyName, double aFrom, double aTo, EEasingFunction aEasing = EEasingFunction.CCircleEasing, int aDurationMS = 300)
        {
            if (aFrom == aTo)
            {
                return null;
            }
            DoubleAnimation lTimeline = new DoubleAnimation();
            lTimeline.From = aFrom;
            lTimeline.To = aTo;
            lTimeline.Duration = new Duration(new TimeSpan(0, 0, 0, 0, aDurationMS));
            switch (aEasing)
            {
                case EEasingFunction.CCircleEasing:
                    lTimeline.EasingFunction = new Windows.UI.Xaml.Media.Animation.CircleEase();
                    break;
                case EEasingFunction.CBackEasing:
                    lTimeline.EasingFunction = new Windows.UI.Xaml.Media.Animation.BackEase();
                    break;
                case EEasingFunction.CNone:
                    break;
            }
            lTimeline.EnableDependentAnimation = true;
            Storyboard.SetTarget(lTimeline, aControl);
            Storyboard.SetTargetProperty(lTimeline, aBindingPropertyName);
            if (aBindingPropertyName == "Opacity")
            {
                aControl.Opacity = aFrom;
            }
            if (aBindingPropertyName == "Height")
            {
                aControl.Height = aFrom;
            }
            if (aBindingPropertyName == "Width")
            {
                aControl.Width = aFrom;
            }
            if (aBindingPropertyName == "HorizontalOffset")
            {
                ((Popup)aControl).HorizontalOffset = aFrom;
            }
            if (aBindingPropertyName == "VerticalOffset")
            {
                ((Popup)aControl).VerticalOffset = aFrom;
            }
            return lTimeline;
        }

        /// <summary>
        /// 简单动画
        /// </summary>
        /// <param name="aTimeline"></param>
        public static void PlaySimpleStory(Timeline aTimeline, EventHandler<object> aCallBack = null)
        {
            if (aTimeline == null)
            {
                return;
            }
            Storyboard lStoryboard = new Storyboard();
            lStoryboard.Children.Add(aTimeline);
            lStoryboard.Completed += aCallBack;
            lStoryboard.Begin();
        }

        /// <summary>
        /// 控件缩放动画
        /// </summary>
        /// <param name="aControl"></param>
        /// <param name="aNewSize"></param>
        /// <param name="aCallBack"></param>
        /// <returns></returns>
        public static void PlayChangeSize(FrameworkElement aControl, double aNewWidth = -1, double aNewHeight = -1, EventHandler<object> aCallBack = null)
        {
            Storyboard lStoryboard = new Storyboard();
            if (aNewHeight != -1 && aNewHeight != aControl.ActualHeight)
            {
                lStoryboard.Children.Add(GetDoubleAnimation(aControl, "Height", aControl.ActualHeight, aNewHeight));
            }
            if (aNewWidth != -1 && aNewWidth != aControl.ActualWidth)
            {
                lStoryboard.Children.Add(GetDoubleAnimation(aControl, "Width", aControl.ActualWidth, aNewWidth));
            }
            lStoryboard.Completed += aCallBack;
            lStoryboard.Begin();
        }

        /// <summary>
        /// 设置透明度变化，如果aNewOpacity为2，则由0到1,若3，则1到0
        /// </summary>
        /// <param name="aControl"></param>
        /// <param name="aNewOpacity"></param>
        /// <param name="aCallBack"></param>
        public static void PlayChangeOpacity(FrameworkElement aControl, double aNewOpacity, EventHandler<object> aCallBack = null)
        {
            if (aNewOpacity == 2)
            {
                aControl.Opacity = 0;
                aNewOpacity = 1;
            }
            if (aNewOpacity == 3)
            {
                aControl.Opacity = 1;
                aNewOpacity = 0;
            }
            PlaySimpleStory(GetDoubleAnimation(aControl, "Opacity", aControl.Opacity, aNewOpacity, EEasingFunction.CNone), aCallBack);
        }

        /// <summary>
        /// 3D翻转动画
        /// </summary>
        /// <param name="aControl"></param>
        /// <param name="IsHide"></param>
        public static void PlayTransform3D(FrameworkElement aControl, ETransform3DDirection aDirc, ETransform3DStyle aStyle, double aCenter = 0.5, double aFrom = 0, double aTo = 90, EventHandler<object> aCallBack = null)
        {
            string s = "";
            switch (aDirc)
            {
                case ETransform3DDirection.CAxisX:
                    s = "RotationX";
                    break;
                case ETransform3DDirection.CAxisY:
                    s = "RotationY";
                    break;
                case ETransform3DDirection.CAxisZ:
                    s = "RotationZ";
                    break;
            }

            aControl.Projection = new PlaneProjection();
            (aControl.Projection as PlaneProjection).CenterOfRotationX = aCenter;
            (aControl.Projection as PlaneProjection).CenterOfRotationY = aCenter;
            (aControl.Projection as PlaneProjection).CenterOfRotationZ = aCenter;

            switch (aStyle)
            {
                case ETransform3DStyle.CHide:
                    aFrom = 0;
                    aTo = 90;
                    break;
                case ETransform3DStyle.CShow:
                    aFrom = -90;
                    aTo = 0;
                    break;
            }
            Storyboard lStoryboard = new Storyboard();
            lStoryboard.Children.Add(GetDoubleAnimation(aControl, "(UIElement.Projection).(PlaneProjection." + s + ")", aFrom, aTo, EEasingFunction.CNone));
            lStoryboard.Completed += aCallBack;
            lStoryboard.Begin();
        }

        /// <summary>
        /// 获得一个双色刷子
        /// </summary>
        /// <param name="aColorFrom"></param>
        /// <param name="aColorTo"></param>
        /// <param name="aOffset"></param>
        /// <returns></returns>
        public static Brush TwoColorBrush(Color aColorFrom, Color aColorTo, Double aOffset)
        {
            GradientStop lStop1 = new GradientStop(){Color = aColorFrom, Offset = 0};
            GradientStop lStop2 = new GradientStop(){Color = aColorFrom, Offset = aOffset};
            GradientStop lStop3 = new GradientStop(){Color = aColorTo, Offset = aOffset};
            GradientStop lStop4 = new GradientStop(){Color = aColorTo, Offset = 1};
            GradientStopCollection lCollection = new GradientStopCollection();
            lCollection.Add(lStop1);
            lCollection.Add(lStop2);
            lCollection.Add(lStop3);
            lCollection.Add(lStop4);
            return new LinearGradientBrush(lCollection, 0);
        }

        public static void Wait(int aMilliseconds, EventHandler<object> aCallBack)
        {
            DispatcherTimer lTimer = new DispatcherTimer();
            lTimer.Interval = TimeSpan.FromMilliseconds(aMilliseconds);
            lTimer.Tick += (s, e) => { ((DispatcherTimer)s).Stop(); };
            lTimer.Tick += aCallBack;
            lTimer.Start();
        }
    }
}
