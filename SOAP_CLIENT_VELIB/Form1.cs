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

        private static Dictionary<string, Dictionary<string, int>> tampon = new Dictionary<string, Dictionary<string, int>>();

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
            if (!tampon.ContainsKey(comboBox2.SelectedItem.ToString()))
            {
                tampon.Add(comboBox2.SelectedItem.ToString(), client.getstationsAndBikes(comboBox2.SelectedItem.ToString()));
            }
            comboBox1.Items.AddRange(tampon[comboBox2.SelectedItem.ToString()].Keys.ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!comboBox1.Text.Equals(""))
                label2.Text = tampon[comboBox2.SelectedItem.ToString()][comboBox1.SelectedItem.ToString()].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            label2.Text = "";

            tampon.Clear();
        }
    }
}
