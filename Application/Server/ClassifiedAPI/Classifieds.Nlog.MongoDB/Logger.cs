using System;
using MongoDB.Driver;
using NLog;
using System.Configuration;

namespace Classifieds.NLog.MongoDB
{
    public class Logger : ILogger
    {
       

        NLog.MongoDB.MongoRepository objnlogmongo =
               new NLog.MongoDB.MongoRepository(MongoServerSettings.FromUrl(
                   new MongoUrl(ConfigurationManager.ConnectionStrings["SearchDBConnectionString"].ConnectionString)), ConfigurationManager.AppSettings["Logging"]);

        void ILogger.Log(Exception ex)
        {
            string LoggerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var stacktrace = ex.StackTrace.ToString();
            LogEventInfo eventdata = new LogEventInfo(LogLevel.Error, LoggerName, null, stacktrace, null, ex);
            eventdata.TimeStamp = DateTime.Now;
            objnlogmongo.Insert(eventdata);
        }
    }
}
