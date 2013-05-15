using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Globalization;
using Windows.Security.Cryptography;
using Windows.Storage;

namespace ZLib
{
    static class CData
    {
        static public string iFileName;
        static public ApplicationDataContainer iLocalSetting = ApplicationData.Current.RoamingSettings;
        static public StorageFolder iLocalPath = ApplicationData.Current.RoamingFolder;
        static Stream lSaveStream = null;

        static CData()
        {
            iFileName = "ZData.dat";
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="aName"></param>
        /// <param name="aValue"></param>
        public static void SaveSetting(string aName, object aValue)
        {
            iLocalSetting.Values[aName] = aValue;
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <param name="aName"></param>
        /// <returns></returns>
        public static object LoadSetting(string aName)
        {
            return iLocalSetting.Values[aName];
        }

        /// <summary>
        /// 序列化并保存一个类
        /// </summary>
        /// <param name="request"></param>
        public static async void SaveClass(object request)
        {
            try
            {
                lSaveStream = await iLocalPath.OpenStreamForWriteAsync(iFileName, CreationCollisionOption.ReplaceExisting);
                XmlSerializer lXS = new XmlSerializer(request.GetType());
                lXS.Serialize(lSaveStream, request);
                lSaveStream.Flush();
                lSaveStream.Dispose();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 从XML中反序列化一个类
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="aDefault"></param>
        /// <returns></returns>
        public static object LoadClass(Type aType, object aDefault = null)
        {
            try
            {
                var lTask = iLocalPath.OpenStreamForReadAsync(iFileName);
                lTask.Wait(-1);
                Stream lStream = lTask.Result;
                XmlSerializer lXS = new XmlSerializer(aType);
                var lResult = lXS.Deserialize(lStream);
                return lResult;
            }
            catch (System.Exception)
            {
                return aDefault;
            }

        }

        /// <summary>
        /// 判断文件是否存在（可能会卡在这）
        /// </summary>
        /// <param name="aFilePath"></param>
        /// <returns></returns>
        static async Task<bool> FileExist(string aFilePath)
        {
            try
            {
                StorageFile lFile = await StorageFile.GetFileFromPathAsync(aFilePath);
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 字符串编码为十六进制字符串
        /// </summary>
        /// <param name="aInput"></param>
        /// <returns></returns>
        public static string StringToHex(string aInput)
        {
            return CryptographicBuffer.EncodeToHexString(CryptographicBuffer.ConvertStringToBinary(aInput, BinaryStringEncoding.Utf8));
        }

        /// <summary>
        /// 解码十六进制字符串
        /// </summary>
        /// <param name="aInput"></param>
        /// <returns></returns>
        public static string HexToString(string aInput)
        {
            return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, CryptographicBuffer.DecodeFromHexString(aInput));
        }
    }
}
