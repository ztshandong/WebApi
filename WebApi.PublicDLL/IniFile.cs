using System;
using System.IO;
using System.Runtime.InteropServices;

namespace WebApi.Public
{
    public class IniFile
    {
        private string _path; //INI档案名 
        public string IniPath { get { return _path; } set { _path = value; } }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct STRINGBUFFER
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szText;
        }

        //读写INI文件的API函数 
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileString(string section, string key, string def, out STRINGBUFFER retVal, int size, string filePath);

        //类的构造函数，传递INI档案名 
        public IniFile(string INIPath)
        {
            _path = INIPath;
            if (!File.Exists(_path)) CreateIniFile();
            SetFileAttributeNormal(_path);
        }

        /// <summary>
        /// 去掉文件只读属性
        /// </summary>
        /// <param name="aFilePath"></param>
        public static void SetFileAttributeNormal(string aFilePath)
        {
            if (File.GetAttributes(aFilePath).ToString().IndexOf("ReadOnly") != -1)
            {
                File.SetAttributes(aFilePath, FileAttributes.Normal);
            }
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this._path);
        }

        /// <summary>
        /// 读取INI文件指定关键字的值
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            int i;
            STRINGBUFFER RetVal;
            i = GetPrivateProfileString(Section, Key, null, out RetVal, 255, this._path);
            string temp = RetVal.szText;
            return temp.Trim();
        }

        /// <summary>
        /// 读取INI文件指定关键字的值
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string defaultValue)
        {
            int i;
            STRINGBUFFER RetVal;
            i = GetPrivateProfileString(Section, Key, null, out RetVal, 255, this._path);
            string temp = RetVal.szText.Trim();
            return String.IsNullOrEmpty(temp) ? defaultValue : temp;
        }

        /// <summary>
        /// 创建INI文件
        /// </summary>
        public void CreateIniFile()
        {
            string dir = Path.GetDirectoryName(_path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            FileStream fs = File.Create(_path);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
}
