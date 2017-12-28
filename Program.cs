using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
namespace WheelFindLine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread mReadTd = new Thread(Reading);
            //mReadTd.Start();
            //mReadTd.Join();

            ////第一条有效数据
            //foreach (KeyValuePair<int, string[]> kv in arigithm.FirstValidRaw())
            //{
            //    Console.WriteLine(kv.Key);
            //}

            //foreach (KeyValuePair<int, string[]> kv in arigithm.LastValidRaw())
            //{
            //    Console.WriteLine(kv.Key);
            //}

            //foreach (KeyValuePair<int, string[]> kv in arigithm.MaxValidRaw())
            //{
            //    Console.WriteLine(kv.Key);
            //}
            byte[] bytes = BitConverter.GetBytes(123);
            UdpManger udp = new UdpManger();
            string[] str = "123".Split('1');
            udp.SendMsg(UdpManger.SendUdpMsg(1, 1, str));
            Console.WriteLine(bytes.Length);
            
            Console.ReadLine();
        }

        static void Reading()
        {
            DataMannger.Read("7080.txt");
        }
    }
}
