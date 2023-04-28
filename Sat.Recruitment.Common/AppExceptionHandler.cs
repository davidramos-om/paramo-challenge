using System.Globalization;

namespace Sat.Recruitment.Common
{
    public class AppExceptionHandler : Exception
    {
        public AppExceptionHandler() : base() { }

        public AppExceptionHandler(string message) : base(message) { }

        public AppExceptionHandler(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
