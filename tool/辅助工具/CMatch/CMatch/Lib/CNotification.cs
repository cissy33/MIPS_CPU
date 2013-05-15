using Cal_Note.Classes;
using NotificationsExtensions.ToastContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media.Imaging;

namespace ZLib
{
    class CNotification
    {
        /// <summary>
        /// 带定时的带图片的toast通知，如果指定时间已经过时，则立即显示
        /// </summary>
        /// <param name="textBody1Text">head，粗体</param>
        /// <param name="dt">延时</param>
        /// <param name="imgSrc">图片</param>
        /// <param name="altString">图片提示问题</param>
        /// <param name="durationProperty">延时时间</param>
        /// <param name="isSilent">有提示音</param>
        /// <param name="launchStr">返回时字符串</param>
        /// <param name="textBody2Text">第二行bodytext，常规字体</param>
        /// <param name="textBody3Text">第三行bodytext，常规字体</param>
        public static ScheduledToastNotification sendToast(string textBody1Text, string launchStr, DateTime dt, ToastDuration durationProperty = ToastDuration.Short, 
            ToastAudioContent audio = ToastAudioContent.Default, string textBody2Text = "", string textBody3Text = "")
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText04;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);


            //提供文字
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            //总共三行文字，加粗在第一行，正常在第二、三行
            //toastTextElements[0].AppendChild(toastXml.CreateTextNode(textBody1Text));
            //toastTextElements[1].AppendChild(toastXml.CreateTextNode(textBody2Text));
            //toastTextElements[2].AppendChild(toastXml.CreateTextNode(textBody3Text));
            toastTextElements[0].InnerText = textBody1Text;
            toastTextElements[1].InnerText = textBody2Text;
            toastTextElements[2].InnerText = textBody3Text;

            ////提供图像
            //XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ////如果使用包含在vs中的本地图片，通过"ms-appx:///"访问
            //(toastImageAttributes[0] as XmlElement).SetAttribute("src", imgSrc);
            ////图片提醒字串
            //(toastImageAttributes[0] as XmlElement).SetAttribute("alt", altString);

            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");

            //toast持续时间，默认情况是short
            (toastNode as XmlElement).SetAttribute("duration", durationProperty.ToString());



            //通知声音
            XmlElement toastAudio = toastXml.CreateElement("audio");
            toastAudio.SetAttribute("slient", audio.ToString());



            //应用启动参数，当点击toast将启动应用，并处理相关内容的视图
            (toastNode as XmlElement).SetAttribute("launch", launchStr);

            //定时发送toast
            if (dt != new DateTime())
            {
                var toastTime = new DateTimeOffset(dt);

                ////var toastTime = DateTimeOffset.Now.AddSeconds(2);

                //显示toast
                try
                {
                    var stn = new ScheduledToastNotification(toastXml, toastTime);
                    ToastNotificationManager.CreateToastNotifier().AddToSchedule(stn);
                    return stn;
                }
                catch (System.Exception) { }
            }
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
            return null;
             
        }

        
        /// <summary>
        /// 没有图片的toast,带延迟
        /// </summary>
        public static ScheduledToastNotification sendToast(string textBody1Text, string launchStr, DateTime dt)
        {
            return sendToast(textBody1Text, launchStr, dt, ToastDuration.Short, ToastAudioContent.Default, "", "");
        }

        /// <summary>
        /// 没有图片的toast，立即执行
        /// </summary>
        public static ScheduledToastNotification sendToast(string textBody1Text, string launchStr = "")
        {
            return sendToast(textBody1Text, launchStr, new DateTime(), ToastDuration.Short, ToastAudioContent.Default, "", "");
        }

        /// <summary>
        /// 添加一个tile
        /// </summary>
        /// <param name="textBody1Text"></param>
        /// <param name="textBody2Text"></param>
        /// <param name="textBody3Text"></param>
        /// <param name="textBody4Text"></param>
        /// <param name="dt"></param>
        public static void sendTile(string textBody1Text, string textBody2Text, string textBody3Text, string textBody4Text, DateTime dt)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideBlockAndText01);
            var tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = textBody1Text;
            tileTextAttributes[1].InnerText = textBody2Text;
            tileTextAttributes[2].InnerText = textBody3Text;
            tileTextAttributes[3].InnerText = textBody4Text;
            tileTextAttributes[4].InnerText = dt.Day.ToString();
            tileTextAttributes[5].InnerText = dt.Year + " " + dt.Month;
            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText01);
            var squareTextAttributes = squareTileXml.GetElementsByTagName("text");
            squareTextAttributes[0].InnerText = dt.Month + "月" + dt.Day + "日";
            squareTextAttributes[1].InnerText = textBody1Text;
            squareTextAttributes[2].InnerText = textBody2Text;
            squareTextAttributes[3].InnerText = textBody3Text;
            //添加方形磁铁
            var node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);
            DateTimeOffset dateoffset = new DateTimeOffset(new DateTime(dt.Year, dt.Month, dt.AddDays(1).Day, 0, 0, 0));
            tileNotification.ExpirationTime = dateoffset;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        /// <summary>
        /// 取消一个toast
        /// </summary>
        /// <param name="aToast"></param>
        public static void CancelScheduledToast(ScheduledToastNotification aToast)
        {
            if (aToast != null)
            {
                ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(aToast);
            }
        }

        /// <summary>
        /// 重置tile队列
        /// </summary>
        /// <param name="aNotes"></param>
        /// <param name="dt"></param>
        /// <param name="aCountInOneTile"></param>
        public static void sendTileQueue(CNotes aNotes, DateTime dt, int aCountInOneTile = 2)
        {
            ClearQueue();
            int l = aNotes.Count();
            while ( l > 0 )
            {
                int k = Math.Min(l, aCountInOneTile);
                var lList = new string[4]{"","","",""};
                for (int i = 0; i < k; i++ )
                {
                    lList[i] = (l - k + i + 1).ToString() + "." + aNotes.Notes[l - i - 1].Title;
                }
                CNotification.sendTile(lList[0], lList[1], lList[2], lList[3], dt);
                l -= k;
            }
        }

        /// <summary>
        /// 清空tile队列
        /// </summary>
        public static void ClearQueue()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }
    }
}
