using Client.Model;
using Client.View;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        SettingServer setting;
        public Form1()
        {
            InitializeComponent();
            setting = new SettingServer();

            richTextBox1.BackColor = setting.ColorFrons;
            richTextBox1.ForeColor = setting.ColorText;
            richTextBox1.Font = new Font("Microsoft Sans Serif", setting.HigthText);

            timer.Tick += new EventHandler(InfoServer);
            timer.Interval += 10000;
            timer.Start();
        }

        private void InfoServer(object sender, EventArgs e)
        {
            labelNameClient.Text = Environment.MachineName;
            labelIPv4.Text = "IPv4: " + Dns.GetHostAddresses(Environment.MachineName)[0].ToString();
            labelIPv6.Text = "IPv6: " + Dns.GetHostAddresses(Environment.MachineName)[1].ToString();

            labelNameServer.Text = SendServer("name");
            labelIPv4Server.Text = "IPv4: " + SendServer("ipv4");
            labelIPv6Server.Text = "IPv6: " + SendServer("ipv6");
        }

        private string SendServer(string code)
        {
            string configserver = "";
            try
            {
                TCPClient client = new TCPClient();
                client.CreateClient(setting.IP, setting.Host);
                client.StartClient(code);
                configserver = client.GetMessage();
            }

            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return configserver;
        }

        private void ClearMenu_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void SettingMenu_Click(object sender, EventArgs e)
        {
            Setting formSetting = new Setting(setting);
            formSetting.ShowDialog();
            setting = formSetting.newSetting;
            richTextBox1.BackColor = setting.ColorFrons;
            richTextBox1.ForeColor = setting.ColorText;
            richTextBox1.Font = new Font("Microsoft Sans Serif", setting.HigthText);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                richTextBox1.Text = SendServer("ipconfig");
            }
            if (comboBox1.SelectedIndex == 1)
            {
                richTextBox1.Text = SendServer("ipconfig/all");
            }
            if (comboBox1.SelectedIndex == 2)
            {
                richTextBox1.Text = SendServer("ipv4Interface");
            }
            if (comboBox1.SelectedIndex == 3)
            {
                richTextBox1.Text = SendServer("ipstatistics");
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}