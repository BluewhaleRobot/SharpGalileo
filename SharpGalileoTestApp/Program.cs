using SharpGalileo;
using SharpGalileo.models;
using SharpGalileoTest;
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

        public static void testKeepConnection()
        {
            GalileoSDK sdk = new GalileoSDK();
            sdk.Connect("8FB56D27D6C961E9036F62182ADE9544D71E23C31E5DF4C7DD692B9E4296A131434B1066D365", true, 10000, null, null);
            sdk.KeepConnection(true);
            int count = 0;
            while (count < 60)
            {
                GalileoStatus status = sdk.GetCurrentStatus();
                if (status != null)
                    Console.WriteLine("Power: " + status.power);
                else
                    Console.WriteLine("Get power failed");
                Thread.Sleep(1000);
                count += 1;
            }
        }

        static void Main(string[] args)
        {
            testKeepConnection();
        }
    }
}
