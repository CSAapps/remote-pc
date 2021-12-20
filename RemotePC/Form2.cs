using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketIOClient;

namespace RemotePC
{
    public partial class Form2 : Form
    {
        SocketIO client;
        public Form2()
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
            client = new SocketIO("https://z53861ec8-z3e2c9533-gtw.qovery.io");           

            //client.OnConnected += async (sender, e) =>
            client.OnConnected += (sender, e) =>
            {
                //await client.EmitAsync("hi", "socket.io");
                DisplayText("RemotePC - Connected");
            };

            client.OnDisconnected += (sender, e) =>
            {
                DisplayText("RemotePC - Disconnected");
            };

            await client.ConnectAsync();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.DisconnectAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.EmitAsync("key", "{LEFT}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.EmitAsync("key", "{RIGHT}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.EmitAsync("key", " ");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.EmitAsync("key", "~");
        }
    }
}
