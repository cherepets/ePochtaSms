using System;

namespace EpochtaSms
{
    public class EpochtaException : Exception
    {
        public EpochtaException(int status)
            : base(((EpochtaExceptionKind)status).ToString()) { }

        private enum EpochtaExceptionKind
        {
            UnknownError = 0,
            AuthFailed = -1,
            XmlError = -2,
            NotEnoughCredits = -3,
            NoRecipients = -4,
            BadSenderName = -5
        }
    }
}
