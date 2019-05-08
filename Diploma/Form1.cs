using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Diploma
{
    public partial class Form1 : Form
    {

        class Server
        {
            private static byte[] _buffer = new byte[1024];
            private static List<Socket> _clientSockets = new List<Socket>();
            private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            private void SetupServer()
            {
                Console.WriteLine("SettingUpServer");
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));
                _serverSocket.Listen(5);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }

            private static void AcceptCallback(IAsyncResult AR)
            {
                Socket socket = _serverSocket.EndAccept(AR);
                _clientSockets.Add(socket);
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReciveCallback),socket);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }

            private static void ReciveCallback(IAsyncResult AR)
            {
                Socket socket = (Socket)AR.AsyncState;
                int received = socket.EndReceive(AR);
                byte[] dateBuf = new byte[received];
                Array.Copy(_buffer, dateBuf, received);

                string text = Encoding.ASCII.GetString(dateBuf);
                Console.WriteLine("Text received" + text);

                if (text.ToLower() == "Get Time")
                {
                    byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
                    socket.BeginSend(data, 0 ,data.Length, SocketFlags.None, new AsyncCallback (), socket);
                }
            }

            private static void SendCallBack(IAsyncResult AR)
            {
                Socket socket = (Socket)AR.AsyncState;
            }
        }

       
        public Form1()
        {
            InitializeComponent();
            Product pr = new Product("Kaustik",2);
            int parametr = pr.quality();
            Sertification(parametr);
        }

        class Product
        {
            int ProductionCount;
            int BigBagValue;
            enum ProductionTypes
            {
                PolivilHlorid,
                CarbonatNatria
            }

           public Product(string ProductStatus, int ProductionCount)
            {
            }


            public int quality ()
            {
                int qualityValue;
                return qualityValue = 99;
            }

        }


        //Sacha huy
        class ProductParty
        {

        }

        public void Sertification (int v)
        {
            if (v<100)
            {
                Console.WriteLine("invalid");
            }
        }
        class Storage
        {
            int StorageCapasity = 10000;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
