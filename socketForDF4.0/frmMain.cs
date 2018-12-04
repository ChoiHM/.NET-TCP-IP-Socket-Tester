using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mklib;

namespace socketForDF4._0
{
    public partial class frmMain : Form
    {
        exSocket40 socket1 = new exSocket40();
        System.Timers.Timer tmr = new System.Timers.Timer(500);
        public frmMain()
        {
            InitializeComponent();
            socket1.onConnect += Socket1_onConnect;
            socket1.onDisconnect += Socket1_onDisconnect;
            socket1.onDataArrival += Socket1_onDataArrival;
            socket1.onError += Socket1_onError;
            tmr.Elapsed += Tmr_Elapsed;
            tmr.Enabled = true;
            comboMode.SelectedIndex = 0;
        }

        private void Socket1_onError(int errCode, string Description)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lbMsg.Text = $"errCode:{errCode}, Desc:{Description}";
            }));
        }

        private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lbEnum.Text = socket1.State.ToString();
            }));
        }

        private void Socket1_onDataArrival(object sender, byte[] Data, int bytesRead)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                txtRcv.AppendText(Encoding.UTF8.GetString(Data) + "\r\n");
            }));
        }

        private void Socket1_onDisconnect(object sender)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lbStatus.BackColor = Color.Red;
                lbStatus.Text = "Disconnected";
            }));
        }

        private void Socket1_onConnect(object sender)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lbStatus.BackColor = Color.Lime;
                lbStatus.Text = "Connected";
                lbMsg.Text = "";
            }));
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            socket1.remoteIP = txtIP.Text;
            socket1.remotePort = int.Parse(txtPort.Text);
            socket1.Connect();

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            socket1.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            socket1.LocalPort = int.Parse(txtPort.Text);
            socket1.Listen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboMode.SelectedIndex == 0)
            {
                socket1.SendData(txtSendTo.Text);
            }
            else if (comboMode.SelectedIndex == 1)
            {
                socket1.SendData(txtSendTo.Text);
            }
        }

        private void txtSendTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                socket1.SendData(txtSendTo.Text);
                txtSendTo.Text = "";
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                foreach (var item in Application.OpenForms)
                {
                    if (item.GetType() == typeof(frmAsc))
                    {
                        (item as frmAsc).BringToFront();
                        (item as frmAsc).WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmAsc frm = new frmAsc();
                frm.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
