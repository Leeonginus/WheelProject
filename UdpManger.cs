using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WheelFindLine
{
     class UdpManger
    {
        public static bool messageSent = false;
        // Receive a message and write it to the console.
        // 定义端口
        private const int listenPort = 1101;
        private const int remotePort = 8888;
        // 定义节点
        private IPEndPoint localEP = null;
        private IPEndPoint remoteEP = null;
        // 定义UDP发送和接收
        private UdpClient udpReceive = null;
        private UdpClient udpSend = null;
        private UdpState udpSendState = null;
        private UdpState udpReceiveState = null;
        private int counter = 0;
        // 异步状态同步
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        // 定义 UdpState类
        public class UdpState
        {
            public UdpClient udpClient = null;
            public IPEndPoint ipEndPoint = null;
            public const int BufferSize = 1024;
            public byte[] buffer = new byte[BufferSize];
            public int counter = 0;
        }
        // 发送函数
        public void SendMsg(byte[] message)
        {
            remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            udpSend.Connect(remoteEP);
            //Thread t = new Thread(new ThreadStart(ReceiveMessages));
            //t.Start();
            while (true)
            {
                lock (this)
                {
                    udpSendState.counter = counter;
                    // 调用发送回调函数
                    udpSend.BeginSend(message, message.Length, new AsyncCallback(SendCallback), udpSendState);
                    sendDone.WaitOne();
                    ReceiveMessages();
                }
            }
        }

        public UdpManger()
        {
            // 本机节点
            localEP = new IPEndPoint(IPAddress.Any, listenPort);
            // 远程节点
            remoteEP = new IPEndPoint(IPAddress.Any, remotePort);
            // 实例化
            udpReceive = new UdpClient(localEP);
            udpSend = new UdpClient();

            // 分别实例化udpSendState、udpReceiveState
            udpSendState = new UdpState();
            udpSendState.ipEndPoint = remoteEP;
            udpSendState.udpClient = udpSend;

            udpReceiveState = new UdpState();
            udpReceiveState.ipEndPoint = remoteEP;
            udpReceiveState.udpClient = udpReceive;

            //receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //receiveSocket.Bind(localEP);

            //sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //sendSocket.Bind(remoteEP);
        }
        // 发送回调函数
        public void SendCallback(IAsyncResult iar)
        {
            UdpState udpState = iar.AsyncState as UdpState;
            if (iar.IsCompleted)
            {
                Console.WriteLine("第{0}个发送完毕！", udpState.counter);
                Console.WriteLine("number of bytes sent: {0}", udpState.udpClient.EndSend(iar));
                //if (udpState.counter == 10)
                //{
                //    udpState.udpClient.Close();
                //}
                sendDone.Set();
            }
        }
        // 接收函数
        public void ReceiveMessages()
        {
            lock (this)
            {
                udpReceive.BeginReceive(new AsyncCallback(ReceiveCallback), udpReceiveState);
                receiveDone.WaitOne();
            }
        }

        // 接收回调函数
        public void ReceiveCallback(IAsyncResult iar)
        {
            UdpState udpState = iar.AsyncState as UdpState;
            if (iar.IsCompleted)
            {
                Byte[] receiveBytes = udpState.udpClient.EndReceive(iar, ref udpReceiveState.ipEndPoint);
                string receiveString = Encoding.Unicode.GetString(receiveBytes);
                Console.WriteLine("Received: {0}", receiveString);
                receiveDone.Set();
            }
        }
        /// <summary>
        ///自定义协议发送数据
        /// </summary>
        /// <param name="headType">数据类型</param>
        /// <param name="equiNum">数据编号</param>
        /// <param name="message">数据内容</param>
        /// <returns></returns>
        public static byte[] SendUdpMsg(int headType, int equiNum, string[] message)
        {
            byte[] Headtype = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(headType));
            byte[] EquiNum = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(equiNum));
            byte[] MessageBodyByte = new byte[message.Length*4];

            int CopyIndex = 0;  
            for (int i = 0; i < message.Length; i++)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message[i]);
                bytes.CopyTo(MessageBodyByte, CopyIndex);
                CopyIndex += bytes.Length;  
            }

            //byte[] totalByte = new byte[];
            byte[] totalByte = new byte[12 + MessageBodyByte.Length];
            Headtype.CopyTo(totalByte, 0);
            BitConverter.GetBytes(MessageBodyByte.Length).CopyTo(totalByte, 4);
            EquiNum.CopyTo(totalByte, 8);
            MessageBodyByte.CopyTo(totalByte, 12);
            Console.WriteLine("all data length:" + totalByte.Length);
            return totalByte;
        }
    }
}
