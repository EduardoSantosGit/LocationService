using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Domain.Common
{

    public enum ResultCode
    {
        GatewayTimeout = 7,
        ServiceUnavailable = 6,
        Conflict = 5,
        BadRequest = 4,
        BadGateway = 3,
        NotFound = 2,
        OK = 1,
        Error = -1
    }

    public class Result<T>
    {
        public ResultCode Status { get; set; }
        public object Value { get; set; }

        public T ValueType
        {
            get { return (T)Value; }
        }

        public Result(ResultCode status)
        {
            this.Status = status;
        }

        public Result(ResultCode status, object value)
            : this(status)
        {
            this.Value = value;
        }
    }


}
