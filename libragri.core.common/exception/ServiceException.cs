using System;
namespace libragri.core.common
{
    public class ServiceException : Exception
    {

        public string Error;
        

        public ServiceException(string error,string message):base(message)
        {
            Error = error;
        }

        public ServiceException(string error,string message,Exception ex):base(message,ex)
        {
            Error = error;
        }
    }
}