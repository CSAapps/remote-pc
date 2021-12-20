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
        public Form1()
        {
            InitializeComponent();
            InitSocket();
        }

        void DisplayText( string text)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = text;
            }
            ));
        }
        
        async void InitSocket()
        {
            var client = new SocketIO("https://z53861ec8-z3e2c9533-gtw.qovery.io");

            client.On("key", response =>
            {    
                string key = response.GetValue<string>();
                Console.WriteLine(key);                
            });
           

            client.OnConnected += async (sender, e) =>
            {
                
                //await client.EmitAsync("hi", "socket.io");
              
                DisplayText("RemotePC - Connected");
                
            };
            await client.ConnectAsync();
        }
    }
}
