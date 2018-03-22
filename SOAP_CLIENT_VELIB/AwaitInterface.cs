using SOAP_CLIENT_VELIB.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAP_CLIENT_VELIB
{
    class AwaitInterface
    {
        public static async Task getCitiesAsync(ServiceVelibClient client, ComboBox c)
        {
            string[] res = await client.getCitiesAsync();

            c.Items.AddRange(res);
        }

        public static async Task getStationsAsync(ServiceVelibClient client, string city, ComboBox c)
        {
            string[] res = await client.getStationsAsync(city);

            c.Items.AddRange(res);
        }

        public static async Task getAvailableBikesAsync(ServiceVelibClient client, string city, string station, int delay, Label l)
        {
            int res = await client.getAvailableBikesAsync(city, station, delay);

            l.Text = res.ToString();
        }
    }
}
