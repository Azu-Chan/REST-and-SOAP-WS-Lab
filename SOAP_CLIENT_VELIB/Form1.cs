using SOAP_CLIENT_VELIB.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAP_CLIENT_VELIB
{
    public partial class Form1 : Form
    {
        private static ServiceVelibClient client = new ServiceVelibClient();

        public Form1()
        {
            InitializeComponent();

            comboBox2.Items.AddRange(client.getCities().ToArray());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            label2.Text = "";

            comboBox1.Items.AddRange(client.getStations(comboBox2.SelectedItem.ToString()));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals(""))
            {
                int time;
                if (textBox1.Text.Equals(""))
                    time = 0;
                else
                    time = Convert.ToInt32(textBox1.Text);
                label2.Text = client.getAvailableBikes(comboBox2.Text, comboBox1.Text, time).ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Une valeur numérique est attendue");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }
    }
}
