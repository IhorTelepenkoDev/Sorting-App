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
        private static Logger instance; //Logger is "Singleton"

        private readonly log4net.ILog _log;
        private const string configFileLocalPath = @"AppFunctionality\Logging\log4net.config";
        private const string loggerName = "log4netFileLogger";

        private Logger()
        {
            var baseSolutionDirectory = Path.GetFullPath(@"..\..\..\");
            var configFileDirectory = baseSolutionDirectory + configFileLocalPath;

            FileInfo configFileInfo = new FileInfo( configFileDirectory);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);

            _log = log4net.LogManager.GetLogger(loggerName);
        }

        public static Logger GetInstance()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
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
