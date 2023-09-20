using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RemotePC
{
    public partial class Form1 : Form
    {
        SocketIO client;
        public Form1()
        {
            InitializeComponent();
            InitSocket();
        }

        void DisplayText(string text)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = text;
            }
            ));
        }

        async void InitSocket()
        {
            client = new SocketIO("https://remotepc-3ipwxc6egq-em.a.run.app");

            client.On("key", response =>
            {
                string key = response.GetValue<string>();
                SendKeys.SendWait(key);
                Console.WriteLine(key);
            });


            //client.OnConnected += async (sender, e) =>
            client.OnConnected +=  (sender, e) =>
            {
                //await client.EmitAsync("hi", "socket.io");
                DisplayText("Connected - RemotePC");
            };

            client.OnDisconnected += (sender, e) =>
            {
                DisplayText("Disconnected - RemotePC");
            };

            await client.ConnectAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.DisconnectAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
