using SharpGalileo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpGalileoTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GalileoSDK sdk = new GalileoSDK();
            bool cbFlag = false;
            sdk.Connect("F9DF41E6CA1C41CD8ECB510C3EF84A4472191922695EBA5A7514D459FC919608A2EF4FB50622", true, 10000, (status, targetID) =>
            {
                Console.WriteLine("Server connected");
                Console.WriteLine(targetID);
                Console.WriteLine("status: " + status);
                cbFlag = true;
            }, (status, targetID) =>
            {
                Console.WriteLine("Server disconnected");
                Console.WriteLine(targetID);
                cbFlag = true;
            });
            while (!cbFlag)
                Thread.Sleep(1000);
            sdk.SetCurrentStatusCallback((status) => {
                Console.WriteLine(status.power);
            });
            int count = 0;
            while (count < 10)
            {
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
