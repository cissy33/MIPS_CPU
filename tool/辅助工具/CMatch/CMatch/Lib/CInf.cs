using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ZLib
{
    public static class CInf
    {
        //异步锁
        static bool iLock = false;
        
        //消息显示队列
        static List<string> iShowStrs = new List<string>();

        /// <summary>
        /// MessageDialog提示
        /// </summary>
        /// <param name="aStr"></param>
        public static void ShowMsg(object aStr)
        {
            iShowStrs.Add(aStr.ToString());
            ShowMsgsAsyn();
        }

        /// <summary>
        /// 异步循环显示队列中的所有数据
        /// </summary>
        static async void ShowMsgsAsyn()
        {
            if (iLock) return;
            iLock = true;
            do 
            {
                await (new MessageDialog(iShowStrs.First())).ShowAsync();
                iShowStrs.RemoveAt(0);
            } while ( iShowStrs.Count > 0);
            iLock = false;
        }
    }
}
