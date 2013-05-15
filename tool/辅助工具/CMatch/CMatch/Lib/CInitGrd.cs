using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cal_Note.Lib
{
    class CInitGrd
    {
        /// <summary>
        /// 显示grid布局
        /// </summary>
        /// <param name="lLen">每月动态的日历行数</param>
        public static void showGrid(int aRow, int aCol, Grid grd)
        {
            //grid分行分列
            grd.ColumnDefinitions.Clear();
            grd.RowDefinitions.Clear();
            InitRows(aRow, grd);
            InitCols(aCol, grd);
            grd.Children.Clear();
        }

        /// <summary>
        /// 后台给出grid行数
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="g"></param>
        public static  void InitRows(int rowCount, Grid g)
        {
            while (rowCount > 0)
            {
                RowDefinition rd = new RowDefinition();
                g.RowDefinitions.Add(rd);
                rowCount--;
            }
        }

        /// <summary>
        /// 后台给出grid列数
        /// </summary>
        /// <param name="colCount"></param>
        /// <param name="g"></param>
        public static void InitCols(int colCount, Grid g)
        {
            while (colCount > 0)
            {
                ColumnDefinition cd = new ColumnDefinition();
                g.ColumnDefinitions.Add(cd);
                colCount--;
            }
        }

        /// <summary>
        /// 将一个控件加到一个Grid中
        /// </summary>
        /// <param name="aGrid"></param>
        /// <param name="aControl"></param>
        /// <param name="aRow"></param>
        /// <param name="aColumn"></param>
        /// <param name="aRowSpan"></param>
        /// <param name="aColumnSpan"></param>
        public static void AddToGrid(Grid aGrid, FrameworkElement aControl, int aRow = 0, int aColumn = 0, int aRowSpan = 1, int aColumnSpan = 1)
        {
            try
            {
                if (aControl.Parent == null && aGrid != null)
                {
                    aGrid.Children.Add(aControl);
                }
                aControl.SetValue(Grid.RowProperty, aRow);
                aControl.SetValue(Grid.ColumnProperty, aColumn);
                aControl.SetValue(Grid.RowSpanProperty, aRowSpan);
                aControl.SetValue(Grid.ColumnSpanProperty, aColumnSpan);
            }
            catch (System.Exception) { };
        }
    }
}
