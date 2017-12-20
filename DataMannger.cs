using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WheelFindLine
{
    static class DataMannger
    {
        public static string ReadTextPath;
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path"></param>
        public static void Read(string txtname)
        {
            TxtMessage tx = new TxtMessage();
            tx.GetTxtFile(txtname);
            StreamReader sr = new StreamReader(tx.TxtName, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                tx.row++;
                line=line.Replace(" ", "");
                string[] tmp = line.Split('\t');                 
                GlobalData.allData.Add(tmp);
            }
        }
        /// <summary>
        /// 获取当前执行路径
        /// </summary>
        /// <returns></returns>
        public static string GetExcutePath()
        {
            string str = System.Environment.CurrentDirectory;
            return str+@"\";
        }
    }

    class TxtMessage
    {
        public int row;
        public int col;
        public string TxtName;
            
        public TxtMessage()
        {
            row = 0;
            col = 0;
        }
        public void GetTxtFile(string name)
        {
            TxtName= DataMannger.GetExcutePath() + name;
        }
    }
}
