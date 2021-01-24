using System;

namespace Logic.Exceptions
{
    public class NotEnoughComponentsException : ArgumentException
    {
        public NotEnoughComponentsException() : base("Not enough components in stock")
        {
            try
            {

            }
            catch (NotEnoughComponentsException)
            {

            }
        }
    }
}
