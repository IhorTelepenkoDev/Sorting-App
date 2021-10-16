using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AppFunctionality.Logging
{
    public class Logger
    {
        private readonly log4net.ILog _log;
        private const string configFileLocalPath = @"\Logging\log4net.config";
        private const string loggerName = "log4netFileLogger";

        public Logger()
        {
            var baseProjectDirectory = Path.GetFullPath(@"..\..\");
            var configFileDirectory = baseProjectDirectory + configFileLocalPath;

            FileInfo configFileInfo = new FileInfo( configFileDirectory);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);

            _log = log4net.LogManager.GetLogger(loggerName);
        }

        public void Debug(string value)
        {
            _log.Debug(value);
        }

        public void Info(string value)
        {
            _log.Info(value);
        }

        public void Warn(string value)
        {
            _log.Warn(value);
        }

        public void Error(string value)
        {
            _log.Error(value);
        }

        public void Fatal(string value)
        {
            _log.Fatal(value);
        }
    }
}
