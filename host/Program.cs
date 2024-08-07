using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Data.SqlClient;

namespace host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ChatLaba3.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Start host");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
