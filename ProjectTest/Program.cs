using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ProjectTest
{
    class Program
    {
        static void Main(string[] args)
        {

            FileHandler.WriteAsync(
               "Base Directory : " + Project.BaseDirectory +
               "Base Address : " + Project.BaseAddress +
               "Connection String : " + Project.ConnectionString);

            Project.GetRegKeys();

            FileHandler.WriteAsync(
                "Base Directory : " + Project.BaseDirectory +
                "Base Address : " + Project.BaseAddress +
                "Connection String : " + Project.ConnectionString);
        }
    }
}
