﻿using System;

namespace NET5Academy.Web.Infrastructure.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base()
        {
        }

        public UnAuthorizedException(string message) : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
