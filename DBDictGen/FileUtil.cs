using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DBDictGen
{
    public class FileUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public FileUtil() { }

        /// <summary>
        /// Create a file with utf-8 encoding
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        public static void CreateFile(string filename, string content)
        {
            CreateFile(filename, content, "UTF-8", false);
        }

        /// <summary>
        /// Create a file with charset you want
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="charset"></param>
        public static void CreateFile(string filename, string content, string charset)
        {
            CreateFile(filename, content, charset, false);
        }

        /// <summary>
        /// Create or append a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="isappend"></param>
        public static void CreateFile(string filename, string content, bool isappend)
        {
            CreateFile(filename, content, "UTF-8", isappend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="charset"></param>
        /// <param name="isappend"></param>
        public static void CreateFile(string filename, string content, string charset, bool isappend)
        {
            CreateDirectorys(filename);
            using (StreamWriter sw = new StreamWriter(filename, isappend, System.Text.Encoding.GetEncoding(charset)))
            {
                sw.WriteLine(content);
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// Read file with utf-8 encoding
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string ReadFile(string filename)
        {
            return ReadFile(filename, "UTF-8");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string ReadFile(string filename, string charset)
        {
            string file_content = "";
            try
            {
                using (StreamReader reader = new StreamReader(filename, System.Text.Encoding.GetEncoding(charset)))
                {
                    file_content = reader.ReadToEnd();
                    reader.Dispose();
                }
            }
            catch { return ""; }
            return file_content;
        }

        /// <summary>
        /// Create Directorys By Given Path
        /// </summary>
        /// <param name="dirname"></param>
        public static void CreateDirectorys(string dirname)
        {
            dirname = dirname.Replace("\\", "/");
            string[] tmp = dirname.Split("/".ToCharArray());
            string dirfile = tmp[0];
            for (byte i = 1; i < (byte)(tmp.Length - 1); i++)
            {
                dirfile += "/" + tmp[i];
                if (!Directory.Exists(dirfile))
                {
                    Directory.CreateDirectory(dirfile);
                }
            }
        }
    }
}
