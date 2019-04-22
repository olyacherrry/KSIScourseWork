using Client.Model;
using System;
using System.Net;
using System.Windows.Forms;

namespace Client.View
{
    public partial class Setting : Form
    {
        SettingServer copySetting;
        public SettingServer newSetting;
        public Setting(SettingServer setting)
        {
            InitializeComponent();
            maskedTextBox1.ValidatingType = typeof(System.Net.IPAddress);

            copySetting = newSetting = setting;
            maskedTextBox1.Text = newSetting.IP;
            maskedTextBox2.Text = newSetting.Host.ToString();

            size.Text = trackBarSize.Value.ToString();

            trackBarSize.Value = newSetting.HigthText;
            panel1.BackColor = newSetting.ColorText;
            panel2.BackColor = newSetting.ColorFrons;
        }

        private void trackBarSize_Scroll(object sender, EventArgs e)
        {
            size.Text = trackBarSize.Value.ToString();
        }

        private void buttonColorText_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            panel1.BackColor = colorDialog1.Color;
        }

        private void buttonColorFront_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            panel2.BackColor = colorDialog1.Color;
        }
        private void Save()
        {
            newSetting.IP = IPAddress.Parse(maskedTextBox1.Text).ToString();
            newSetting.Host = Convert.ToInt32(maskedTextBox2.Text);

            newSetting.HigthText = trackBarSize.Value;
            newSetting.ColorText = panel1.BackColor;
            newSetting.ColorFrons = panel2.BackColor;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            newSetting = copySetting;
            Close();
        }
    }
}
