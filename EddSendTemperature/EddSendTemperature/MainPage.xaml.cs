using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EddSendTemperature
{
    public partial class MainPage : ContentPage
    {
        private readonly DeviceClient _deviceClient;
        public MainPage()
        {
            InitializeComponent();
            _deviceClient = DeviceClient.Create("KirkSampleIotHub.azure-devices.net",
                new DeviceAuthenticationWithRegistrySymmetricKey("Device1", "oMFgJYz+R1K4p8hPnBIFaKeDRtdBaOYmDS9kGp1xqNg="));
        }

        private async void SendData_OnClicked(object sender, EventArgs e)
        {
            Random randomTemp = new Random();

            var telemetryDataPoint = new
            {
                DeviceName = "Device1",
                CurrentTemperature = randomTemp.Next(0,100)
            };

            var messagingString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messagingString));
            await _deviceClient.SendEventAsync(message);
        }
    }
}
