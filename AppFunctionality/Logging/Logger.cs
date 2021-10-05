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

        public Logger()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configFileDirectory = Path.Combine(baseDirectory, "log4net.config");

            FileInfo configFileInfo = new FileInfo(@"C:\My Projects\Sort Project\Main Project\SortApp\WinFormsApp\log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);

            _log = log4net.LogManager.GetLogger("log4netFileLogger");
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
