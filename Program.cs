using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WheelFindLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread mReadTd = new Thread(Reading);
            mReadTd.Start();
            mReadTd.Join();

            //第一条有效数据
            foreach (KeyValuePair<int, string[]> kv in arigithm.FirstValidRaw())
            {
                Console.WriteLine(kv.Key);
            }

            foreach (KeyValuePair<int, string[]> kv in arigithm.LastValidRaw())
            {
                Console.WriteLine(kv.Key);
            }

            foreach (KeyValuePair<int, string[]> kv in arigithm.MaxValidRaw())
            {
                Console.WriteLine(kv.Key);
            }
            Console.ReadLine();
        }

        static void Reading()
        {
            DataMannger.Read("7080.txt");
        }
    }
}
