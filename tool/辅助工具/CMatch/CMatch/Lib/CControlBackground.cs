using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Input;
using System.Threading;

namespace ZLib
{
    public enum EShapeStyle
    {
        SS_Circle,
        SS_Rectage,
    }

    public enum EOpacityStyle
    {
        OS_NoPlay,
        OS_AutoHide,
        OS_ManualHide
    }

    public class CControlBackground : UserControl
    {
        public delegate void RepeatClickHandle(object sender, RoutedEventArgs e);
        public event RepeatClickHandle RepeatClick;

        public static EOpacityStyle iOpacityStyle = EOpacityStyle.OS_NoPlay;

        Ellipse iBorder_C;
        Border iBorder_R;
        Grid iMain;
        FrameworkElement iMask;
        FrameworkElement iBorder;

        bool iIsLoaded = false;
        bool iIsShow = false;

        DispatcherTimer iTimer = new DispatcherTimer();

        public int TimerInterval
        {
            get { return (int)GetValue(TimerIntervalProperty); }
            set { SetValue(TimerIntervalProperty, value); }
        }
        public static readonly DependencyProperty TimerIntervalProperty =
            DependencyProperty.Register("TimerInterval", typeof(int), typeof(CControlBackground), new PropertyMetadata(300));

        public EShapeStyle ShapeStyle
        {
            get { return (EShapeStyle)GetValue(ShapeStyleProperty); }
            set { SetValue(ShapeStyleProperty, value); }
        }
        public static readonly DependencyProperty ShapeStyleProperty =
            DependencyProperty.Register("ShapeStyle", typeof(EShapeStyle), typeof(CControlBackground), new PropertyMetadata(EShapeStyle.SS_Rectage, new PropertyChangedCallback(OnMaskChanged)));

        public Visibility HasBorder
        {
            get { return (Visibility)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }
        public static readonly DependencyProperty HasBorderProperty =
            DependencyProperty.Register("HasBorder", typeof(Visibility), typeof(CControlBackground), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnStyleChanged)));

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(int), typeof(CControlBackground), new PropertyMetadata(2, new PropertyChangedCallback(OnStyleChanged)));

        public Brush MaskColor
        {
            get { return (Brush)GetValue(MaskColorProperty); }
            set { SetValue(MaskColorProperty, value); }
        }
        public static readonly DependencyProperty MaskColorProperty =
            DependencyProperty.Register("MaskColor", typeof(Brush), typeof(CControlBackground), new PropertyMetadata(new SolidColorBrush(CColors.ColorMask), new PropertyChangedCallback(OnStyleChanged)));

        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(CControlBackground), new PropertyMetadata(new SolidColorBrush(Colors.White), new PropertyChangedCallback(OnStyleChanged)));

        public double BaseOpacity
        {
            get { return (double)GetValue(BaseOpacityProperty); }
            set { SetValue(BaseOpacityProperty, value); }
        }
        public static readonly DependencyProperty BaseOpacityProperty =
            DependencyProperty.Register("BaseOpacity", typeof(double), typeof(CControlBackground), new PropertyMetadata(0.0, new PropertyChangedCallback(OnMaskChanged)));

        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CControlBackground).RefreshStyle();
        }

        private static void OnMaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CControlBackground).ResetMask();
        }

        public CControlBackground()
        {
            Init();
        }

        /// <summary>
        /// 如果要设置的遮蔽层不是控件默认值，则需要手动设置
        /// </summary>
        /// <param name="aGrid"></param>
        public void SetMaskGrid(Grid aGrid)
        {
            if (aGrid != null)
            {
                iMain = aGrid;
                ReBind();
            }
        }

        void Init()
        {
            if (iMask == null)
            {
                ResetMask();
            }
            if (iBorder_C == null) iBorder_C = new Ellipse();
            if (iBorder_R == null) iBorder_R = new Border();
            
            if (BaseOpacity > 0.3)
            {
                BaseOpacity = 0.3;
            }

            iBorder_C.StrokeThickness = Thickness;
            iBorder_R.BorderThickness = new Windows.UI.Xaml.Thickness(Thickness);

            SizeChanged += (s, e) =>
            {
                if (ShapeStyle == EShapeStyle.SS_Circle)
                {
                    iBorder_C.Height = iBorder_C.Width = iMask.Height = iMask.Width =
                        ActualWidth < ActualHeight ? ActualWidth : ActualHeight;
                }
                else
                {
                    iMask.Height = ActualHeight;
                    iMask.Width = ActualWidth;
                }
            };
    
            Loaded += (s, e) =>
            {
                ReBind();
            };
            Unloaded += (s, e) =>
            {
                UnBind();
            };
            
            iIsShow = true;
        }

        /// <summary>
        /// 重新绑定控件效果
        /// </summary>
        void ReBind()
        {
            UnBind();

            if (iMain == null)
            {
                //从自己的子控件开始向上扫描，一旦找到有Grid类型的，就选取为遮蔽层
                //自身如果是UserControl，则下面两句无效
                try
                {
                    if (this.Content.GetType() == typeof(Grid))
                    {
                        iMain = this.Content as Grid;
                    }
                    else if (this.Parent.GetType() == typeof(Grid))
                    {
                        iMain = this.Parent as Grid;
                    }
                }
                catch (Exception) { }
            }

            if (!iIsLoaded)
            {
                if (iMain == null)
                {
                    return;
                }
                iMain.Children.Add(iBorder_C);
                iMain.Children.Add(iBorder_R);
                iMain.Children.Add(iMask);
                iIsLoaded = true;
            }
            RefreshStyle();
        }

        /// <summary>
        /// 取消绑定控件效果
        /// </summary>
        void UnBind()
        {
            if (iIsLoaded)
            {
                iMain.Children.Remove(iBorder_C);
                iMain.Children.Remove(iBorder_R);
                iMain.Children.Remove(iMask);
                iIsLoaded = false;
            }
        }

        /// <summary>
        /// 重新生成iMask形状
        /// </summary>
        void ResetMask()
        {
            if (iMask != null && iMain != null)
            {
                iMain.Children.Remove(iMask);
            }
            switch (ShapeStyle)
            {
                case EShapeStyle.SS_Circle:
                    iMask = new Ellipse();// { Fill = BorderColor };
                    break;
                case EShapeStyle.SS_Rectage:
                    iMask = new Grid();// { Background = BorderColor };
                    break;
            }
            if (iMain != null)
            {
                iMain.Children.Add(iMask);
            }

            iMask.Opacity = BaseOpacity;
            iMask.Height = iMask.Width = ActualWidth < ActualHeight ? ActualWidth : ActualHeight;

            AddOpacityMask(iMask, BaseOpacity, HasBorder == Visibility.Visible);
            iMask.PointerCanceled += (s, e) => { iTimer.Stop(); };
            iMask.PointerReleased += (s, e) => { iTimer.Stop(); };
            iMask.PointerExited += (s, e) => { iTimer.Stop(); };
            iMask.PointerCaptureLost += (s, e) => { iTimer.Stop(); };
            iMask.PointerPressed += (s, e) =>
            {
                if (RepeatClick != null)
                {
                    RepeatClick(this, null);
                    iTimer = new DispatcherTimer();
                    iTimer.Interval = TimeSpan.FromMilliseconds(300);
                    iTimer.Tick += (at, ae) =>
                    {
                        RepeatClick(this, null);
                    };
                    iTimer.Start();
                }
            };
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        void RefreshStyle()
        {
            if (!iIsShow) return;
            iBorder_R.Visibility = iBorder_C.Visibility = Visibility.Collapsed;
            switch (ShapeStyle)
            {
                case EShapeStyle.SS_Circle:
                    iBorder_C.Stroke = BorderColor;
                    (iMask as Ellipse).Fill = MaskColor;
                    iBorder = iBorder_C;
                    break;
                case EShapeStyle.SS_Rectage:
                    iBorder_R.BorderBrush = BorderColor;
                    (iMask as Grid).Background = MaskColor;
                    iBorder = iBorder_R;
                    break;
            }
            iMask.Opacity = BaseOpacity;
            iBorder.Visibility = HasBorder;
        }

        static void SetNoPlayOpacity(FrameworkElement aControl, double aNewOpacity, int aDuration = 500)
        {
            aControl.Opacity = aNewOpacity;
        }

        static void SetManualHideOpacity(FrameworkElement aControl, double aNewOpacity, int aDuration = 500)
        {
            CPlay.PlaySimpleStory(CPlay.GetDoubleAnimation(aControl, "Opacity", aControl.Opacity, aNewOpacity, EEasingFunction.CCircleEasing, aDuration));
        }

        static void SetAutoHideOpacity(FrameworkElement aControl, double aBaseOpacity, double aAddOpacity, int aShowDuration = 300, int aHideDuration = 1000)
        {
            CPlay.PlaySimpleStory(CPlay.GetDoubleAnimation(aControl, "Opacity", aControl.Opacity, aBaseOpacity + aAddOpacity, EEasingFunction.CNone, aShowDuration),
                (s, e) =>
                {
                    CPlay.PlaySimpleStory(CPlay.GetDoubleAnimation(aControl, "Opacity", aControl.Opacity, aBaseOpacity, EEasingFunction.CNone, aHideDuration));
                });
        }

        static void AddNoPlayOpacityMask(FrameworkElement aControl, double aBaseOpacity = 0)
        {
            aControl.Opacity = aBaseOpacity;
            PointerEventHandler lExit = (s, e) => { SetNoPlayOpacity(aControl, aBaseOpacity); };
            PointerEventHandler lMoveIn = (s, e) => { SetNoPlayOpacity(aControl, aBaseOpacity + 0.3); };
            PointerEventHandler lPressed = (s, e) => { SetNoPlayOpacity(aControl, aBaseOpacity + 0.6); };
            aControl.PointerEntered += lMoveIn;
            aControl.PointerReleased += lMoveIn;
            aControl.PointerCanceled += lExit;
            aControl.PointerCaptureLost += lExit;
            aControl.PointerExited += lExit;
            aControl.PointerPressed += lPressed;
        }

        static void AddManualHideOpacityMask(FrameworkElement aControl, double aBaseOpacity = 0)
        {
            aControl.Opacity = aBaseOpacity;
            PointerEventHandler lExit = (s, e) => { SetManualHideOpacity(aControl, aBaseOpacity); };
            PointerEventHandler lMoveIn = (s, e) => { SetManualHideOpacity(aControl, aBaseOpacity + 0.3); };
            PointerEventHandler lPressed = (s, e) => { SetManualHideOpacity(aControl, aBaseOpacity + 0.6); };
            aControl.PointerEntered += lMoveIn;
            aControl.PointerReleased += lMoveIn;
            aControl.PointerCanceled += lExit;
            aControl.PointerCaptureLost += lExit;
            aControl.PointerExited += lExit;
            aControl.PointerPressed += lPressed;
        }

        static void AddAutoHideOpacityMask(FrameworkElement aControl, double aBaseOpacity = 0)
        {
            aControl.Opacity = aBaseOpacity;
            PointerEventHandler lMoveIn = (s, e) => { SetAutoHideOpacity(aControl, aBaseOpacity, 0.3); };
            PointerEventHandler lPressed = (s, e) => { SetAutoHideOpacity(aControl, aBaseOpacity, 0.6); };
            aControl.PointerEntered += lMoveIn;
            aControl.PointerPressed += lPressed;
        }

        static void SetOpactiy(FrameworkElement aControl, double aBaseOpacity, double aAddOpacity, int aDuration = 300, int aHideDuration = 1000)
        {
            switch (iOpacityStyle)
            {
                case EOpacityStyle.OS_NoPlay:
                    SetNoPlayOpacity(aControl, aBaseOpacity + aAddOpacity, aDuration);
                    break;
                case EOpacityStyle.OS_AutoHide:
                    SetAutoHideOpacity(aControl, aBaseOpacity, aAddOpacity, aDuration, aHideDuration);
                    break;
                case EOpacityStyle.OS_ManualHide:
                    SetManualHideOpacity(aControl, aBaseOpacity + aAddOpacity, aDuration);
                    break;
            }
        }

        /// <summary>
        /// 设置控件背景自动变化
        /// 如果aContent是grid，注意设置background不为空
        /// </summary>
        /// <param name="aControl"></param>
        public static void AddOpacityMask(FrameworkElement aControl, double aBaseOpacity = 0, bool aAutoBorder = false)
        {
            if (aControl.GetType() == typeof(Grid))
            {
                if ((aControl as Grid).Background == null)
                {
                    try
                    {
                        (aControl as Grid).Background = (Brush)aControl.Resources["ResMask"];
                    }
                    catch (System.Exception)
                    {
                        (aControl as Grid).Background = new SolidColorBrush(CColors.ColorMask);
                    }
                }
                if (aAutoBorder)
                {
                    Border lBorder = new Border();
                    lBorder.BorderThickness = new Windows.UI.Xaml.Thickness(2);
                    lBorder.BorderBrush = new SolidColorBrush(Colors.DarkSlateGray);
                    (aControl as Grid).Children.Add(lBorder);
                }
            }
            if (aControl.GetType() == typeof(Ellipse))
            {
                if ((aControl as Ellipse).Fill == null)
                {
                    try
                    {
                        (aControl as Ellipse).Fill = (Brush)aControl.Resources["ResMask"];
                    }
                    catch (System.Exception)
                    {
                        (aControl as Ellipse).Fill = new SolidColorBrush(CColors.ColorMask);
                    }
                }
                if (aAutoBorder)
                {
                    (aControl as Ellipse).StrokeThickness = 2;
                    (aControl as Ellipse).Stroke = new SolidColorBrush(Colors.DarkSlateGray);
                }
            }
            switch (iOpacityStyle)
            {
                case EOpacityStyle.OS_NoPlay:
                    AddNoPlayOpacityMask(aControl, aBaseOpacity);
                    break;
                case EOpacityStyle.OS_AutoHide:
                    AddAutoHideOpacityMask(aControl, aBaseOpacity);
                    break;
                case EOpacityStyle.OS_ManualHide:
                    AddManualHideOpacityMask(aControl, aBaseOpacity);
                    break;
            }
        }
    }
}
