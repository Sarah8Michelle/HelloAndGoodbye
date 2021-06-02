using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace HelloAndGoodbye
{
    partial class HelloAndGoodbyeService : ServiceBase
    {
        #region Declaration of instances
        private int eventId = 1;
        Timer timer;
        EventLog eventLog;
        #endregion

        public HelloAndGoodbyeService()
        {
            InitializeComponent();

            eventLog = new EventLog();
            if (!EventLog.SourceExists("HelloAndGoodbyeService"))
            {
                EventLog.CreateEventSource("HelloAndGoodbyeService", "HelloAndGoodbyeLog");
            }
            eventLog.Source = "HelloAndGoodbyeService";
            eventLog.Log = "HelloAndGoodbyeLog";
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            try
            {
                eventLog.WriteEntry("Good Morning! Starting the day...", EventLogEntryType.Information);
                timer = new Timer();
                timer.Interval = 10000; //10 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
                timer.Start();
            }

            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error: {ex.Message}; StackTrace: {ex.StackTrace};", EventLogEntryType.Error);
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                var time = DateTime.Now;

                eventLog.WriteEntry("Log said hello! at: " + time.ToString(), EventLogEntryType.Information, eventId++);
            }

            catch (Exception ex)
            {
                eventLog.WriteEntry($"Error: {ex.Message}; StackTrace: {ex.StackTrace};", EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            eventLog.WriteEntry("Goodbye! Stopping the day...", EventLogEntryType.Warning);
            timer.Stop();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.IContainer components = new System.ComponentModel.Container();
            this.ServiceName = "HelloAndGoodbye";
        }
    }
}
