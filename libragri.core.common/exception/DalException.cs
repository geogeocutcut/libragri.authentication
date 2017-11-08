using System;
namespace libragri.core.common
{
    public class DalException : Exception
    {

        public string Error;
        

        public DalException(string error,string message):base(message)
        {
            Error = error;
        }

        public DalException(string error,string message,Exception ex):base(message,ex)
        {
            Error = error;
        }
    }
}