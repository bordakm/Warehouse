using Converter.Temperature.Extensions.From;
using Converter.Temperature.Extensions.To;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Warehouse.Services
{
    public class TcpTemperatureService : ITemperatureService
    {
        public double GetTemperatureCelsius()
        {
            return GetTempFromTcp();
        }

        public double GetTemperatureFahreinheit()
        {
            return GetTemperatureCelsius().FromCelsius().ToFahrenheit();
        }

        private double GetTempFromTcp()
        {
            try
            {
                int port = 80;
                TcpClient client = new TcpClient("13.90.133.26", port);
                string message = "gettemp";
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                data = new byte[256];
                string responseData = string.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                stream.Close();
                client.Close();
                double responseDouble;
                bool success = double.TryParse(responseData, out responseDouble);             
                if(!success) double.TryParse(responseData.Replace('.', ','), out responseDouble);  
                return responseDouble;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            return 0;
        }

    }
}
