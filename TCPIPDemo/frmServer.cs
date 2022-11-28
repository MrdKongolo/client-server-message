using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace TCPIPDemo
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        private void frmServer_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//Enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtEtat.Invoke((MethodInvoker)delegate ()
            {
                txtEtat.Text += e.MessageString;
                e.ReplyLine(string.Format("Vous avez dit : {0} ", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtEtat.Text = "Server starting...";

            IPAddress ip = new IPAddress(long.Parse(txtHote.Text));
            server.Start(ip, Convert.ToInt32(txtPort.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }
}
