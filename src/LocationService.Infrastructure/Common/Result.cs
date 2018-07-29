using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Common
{

    public enum ResultCode
    {
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
