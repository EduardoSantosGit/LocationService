using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Domain.Common
{
    
    public enum ResultStatusCode
    {
        UnprocessableEntity = -12,
        NotImplemented = -11,
        InvalidPage = -10,
        InvalidSession = -9,
        ProxyBlocked = -8,
        BadCaptcha = -7,
        Unavailable = -6,
        TimedOut = -5,
        BadRequest = -4,
        Duplicated = -3,
        NotFound = -2,
        Error = -1,
        OK = 1,
        None = 0
    }

    public class Result<T>
    {



    }
}
