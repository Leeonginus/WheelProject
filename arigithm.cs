using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelFindLine
{
    static class arigithm
    {
        /// <summary>
        /// 获得制定的行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string[] FindRow(int row)
        {
            string[] tmp ={};
            int count = 0;
            foreach (string[] str in GlobalData.allData)
            {
                if (count == row)
                {
                    tmp = str;
                }
                count++;
            }
            return tmp;
        }

        /// <summary>
        /// 获得指定的列
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static List<string> FindCol(int col)
        {
            List<string> ColList = new List<string>();
            foreach (string[] str in GlobalData.allData)
            {
                for (int i = 0; i<str.Length; i++)
                {
                    if(i ==col)
                        ColList.Add(str[i]);
                }
            }
            return ColList;
        }

        /// <summary>
        /// 获得所有需要的列
        /// </summary>
        /// <returns></returns>
        public static List<string[]> ReturnAllNeedLine()
        {
            List<string[]> validLine = new List<string[]>();

            return validLine;
        }

        /// <summary>
        /// 获得第一条有效数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string[]> FirstValidRaw()
        {
            Dictionary<int, string[]> dic = new Dictionary<int, string[]>();
            int count = 1;
            foreach (string[] str in GlobalData.allData)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i % 2 != 0 && str[i] != GlobalData.Invaliddata)
                    {
                        dic.Add(count, FindRow(count));
                        return dic;
                    }
                }
                count++;
            }
            return dic;
        }

        /// <summary>
        /// 寻找最后一条有效数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string[]> LastValidRaw()
        {
            Dictionary<int, string[]> dic = new Dictionary<int, string[]>();
            int count = 1;
            List<string[]> tmp = GlobalData.allData;
            tmp.Reverse();
            foreach (string[] str in GlobalData.allData)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i % 2 != 0 && str[i] != GlobalData.Invaliddata)
                    {
                        dic.Add(GlobalData.allData.Count - count + 1, FindRow(GlobalData.allData.Count - count + 1));
                        return dic;
                    }
                }
                count++;
            }
            return dic;
        }

        /// <summary>
        /// 寻找最大值所在的数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string[]> MaxValidRaw()
        {
            Dictionary<int, string[]> dic = new Dictionary<int, string[]>();
            List<string[]> tmp = GlobalData.allData;
            int MaxRowNum = 0;
            int tmpMaxValue=0;
            int count = 1;
            foreach (string[] str in GlobalData.allData)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i % 2 != 0 && str[i] != GlobalData.Invaliddata)
                    {
                        if (tmpMaxValue < int.Parse(str[i]))
                        {
                            tmpMaxValue = int.Parse(str[i]);
                            MaxRowNum = count;
                        }
                    }
                }
                count++;
            }
            dic.Add(MaxRowNum, FindRow(MaxRowNum));
            return dic;
        }

        /// <summary>
        /// 7200算法
        /// </summary>
        /// <returns></returns>
        public static int Find_7200()
        {
            int coreLine = 0;

            return coreLine;
        }
    }
}
