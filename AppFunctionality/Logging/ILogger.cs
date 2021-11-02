
namespace AppFunctionality.Logging
{
    public interface ILogger
    {
        public void Debug(string value);
        public void Info(string value);
        public void Warn(string value);
        public void Error(string value);
        public void Fatal(string value);
    }
}
