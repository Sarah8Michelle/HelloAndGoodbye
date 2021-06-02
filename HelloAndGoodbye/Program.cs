using System;
using System.ServiceProcess;

namespace HelloAndGoodbye
{
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HelloAndGoodbyeService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
