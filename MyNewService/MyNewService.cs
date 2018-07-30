using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Microsoft.Owin.Hosting;
using MyNewService.API;
using Common;
using SampleServer;

namespace MyNewService
{
    public partial class MyNewService : ServiceBase
    {
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        ServiceStatus serviceStatus = new ServiceStatus();

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        //private HttpSelfHostServer _server;
        //private readonly HttpSelfHostConfiguration _config;
        public const string ServiceAddress = "http://localhost:2345";
        public IDisposable _owinServer = null;

        public MyNewService(string[] args)
        {
            InitializeComponent();
            string eventSourceName = "MySource";
            string logName = "MyNewLog";
            if (args.Count() > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
            FileHandler.WriteAsync("Testing file handler");
            //_config = new HttpSelfHostConfiguration(ServiceAddress);
            //_config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        protected override void OnStart(string[] args)
        {
            //Update the service state to start pending
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart");
            FileHandler.WriteAsync("Starting service");
            Timer timer = new Timer
            {
                Interval = 60000
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            FileHandler.WriteAsync("Opening service ");
            //_server = new HttpSelfHostServer(_config);
            //_server.OpenAsync();
            _owinServer = WebApp.Start<StartUp>(url: ServiceAddress);

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

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            /// TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnContinue()
        {
            FileHandler.WriteAsync("Continuing service");
            eventLog1.WriteEntry("In onContinue");
            base.OnContinue();
        }

        protected override void OnStop()
        {
            FileHandler.WriteAsync("Stopping service");
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog1.WriteEntry("In onStop");

            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            if (_owinServer != null)
            {
                _owinServer.Dispose();
            }
            base.OnStop();
            //_server.CloseAsync().Wait();
            //_server.Dispose();
        }
    }
}
