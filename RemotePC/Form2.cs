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
using Microsoft.VisualBasic;

namespace RemotePC
{
    public partial class Form2 : Form
    {
        SocketIO client;
        string socketUrl = "https://www.remote.linkpc.net";
        //string socketUrl = "http://localhost:3000";
        public Form2()
        {
            InitializeComponent();            
        }
        void DisplayText(string text)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = text;
            }
            ));
        }

        async void InitSocket(string roomId)
        {
            client = new SocketIO(socketUrl, new SocketIOOptions
            {
                Query = new List<KeyValuePair<string, string>>
                    {                        
                        new KeyValuePair<string, string>("roomId",roomId)
                    }
            });

            //client.OnConnected += async (sender, e) =>
            client.OnConnected += (sender, e) =>
            {
                //await client.EmitAsync("hi", "socket.io");
                DisplayText(roomId+" Connected - RemotePC");
            };

            client.OnDisconnected += (sender, e) =>
            {
                DisplayText("Disconnected - RemotePC");
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

        private void Form2_Load(object sender, EventArgs e)
        {
            string roomId = Interaction.InputBox("Enter roomId");
            InitSocket(roomId);
        }
    }
}
