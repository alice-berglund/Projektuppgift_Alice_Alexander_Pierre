using System;

namespace Logic.Exceptions
{
    public class DateOutOfReachException: ArgumentException
    {
        public DateOutOfReachException(): base("Date out of reach")
        {
            
        }
    }
}
