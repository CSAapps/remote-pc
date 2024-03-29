﻿using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Rx

namespace RemotePC
{
    public partial class Form1 : Form
    {
        SocketIO client;
        //string socketUrl = "https://www.remote.linkpc.net";
        string socketUrl = "https://remotepc.onrender.com";
        //string socketUrl = "http://localhost:3000";
        string roomId = "";
        public Form1(string roomId="")
        {
            InitializeComponent();
            this.roomId = roomId;
            txtRoomId.Text = roomId;
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
            
            client = new SocketIO(socketUrl, new SocketIOOptions
            {
                Query = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Rx","1"),
                        new KeyValuePair<string, string>("roomId",roomId)
                    }
            });

            client.On("key", response =>
            {
                string key = response.GetValue<string>();
                SendKeys.SendWait(key);
                Console.WriteLine(key);
            });

            client.On("roomId", response =>
            {
                roomId = response.GetValue<string>();
                txtRoomId.Invoke(new Action(() => txtRoomId.Text = roomId));
                lblInfo.Invoke(new Action(() => lblInfo.Text = "Share this code"));
            });


            //client.OnConnected += async (sender, e) =>
            client.OnConnected += (sender, e) =>
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

        async void Exit()
        {
            await client.EmitAsync("exit",roomId);
            await client.DisconnectAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtRoomId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtRoomId_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtRoomId.Text);
            lblInfo.Text = "Coppied";
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(socketUrl+"?"+txtRoomId.Text);
            lblInfo.Text = "Coppied as URL";
        }

        private void txtRoomId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {

            }
        }
    }
}
