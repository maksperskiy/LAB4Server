using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB4Server
{
    public partial class Form1 : Form
    {
        TcpListener server;
        Boolean flg;
        void Init()
        {
            try
            {
                Int32 port = 1001;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1"); //'IPAddress.Any
                server = new TcpListener(localAddr, port);
                //' Начинаем слушать запросы клиентов
                server.Start();
                listBox1.Items.Add("Начинаем слушать!");
                flg = true;
            }
            catch (SocketException e1)
            {
                MessageBox.Show("Ошибка соединения: " + e1.ToString());
            }
        }
        void Endmain()
        {
            try
            {
                flg = false;
                //'Прекрашаем слушать запросы клиентов
                server.Stop();
                listBox1.Items.Add("Прекрашаем слушать!");
            }
            catch (SocketException e1)
            {
                MessageBox.Show("Ошибка соединения: " + e1.ToString());
            }


        }
        void main()
        {
            try
            {
                int i = 0;

                Byte[] bytes = new Byte[3];

                listBox1.Items.Add("Ожидаем соединений... ");

                while (flg == true)
                {
                    if (server.Pending() == true)
                    {
                        TcpClient client = server.AcceptTcpClient();

                        Application.DoEvents();

                        NetworkStream stream = client.GetStream();
                        i = stream.Read(bytes, 0, 3);
                        if (i != 0)
                        {
                            listBox1.Items.Add("Полученные данные: " + bytes[0]);
                            listBox1.Items.Add("Полученные данные: " + bytes[1]);
                            listBox1.Items.Add("Полученные данные: " + bytes[2]);

                            this.BackColor = Color.FromArgb(bytes[0], bytes[1], bytes[2]);

                            i = 0;
                        }
                        
                        Application.DoEvents();
                        stream.Close();
                        client.Close();
                    }
                    Application.DoEvents();
                }
            }
            catch (SocketException e1)
            {
                MessageBox.Show("Ошибка соединения: " + e1.ToString());
            }

        }
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            main();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            flg = false;
            Endmain();
        }
    }
}