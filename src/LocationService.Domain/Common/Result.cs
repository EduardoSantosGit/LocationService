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

    /// <summary>
    /// Represents the outcome of an operation, which includes the result value,
    /// status code and optionally other metadata.
    /// </summary>
    [DebuggerDisplay("{Status}")]
    public class Result
    {
        /// <summary>
        /// The status of the result.
        /// </summary>
        public ResultStatusCode Status { get; set; }

        /// <summary>
        /// The result value of the operation.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="Result"/> object using the provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <returns>
        /// A new <see cref="Result"/> object whose <see cref="Status"/> property
        /// is initialized with the <paramref name="status"/> parameter.
        /// </returns>
        public static implicit operator Result(ResultStatusCode status)
        {
            return new Result(status);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> object using the <see cref="ResultStatusCode.Error"/>
        /// status code and the provided value.
        /// </summary>
        /// <param name="value">The result value of the operation.</param>
        /// <returns>
        /// A new <see cref="Result"/> object whose <see cref="Status"/> property
        /// is set to <see cref="ResultStatusCode.Error"/>, and whose <see cref="Value"/> property
        /// is initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static implicit operator Result(ErrorBuilder value)
        {
            return new Result(ResultStatusCode.Error, (value != null) ? value.GetErrors() : null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class, using the provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        public Result(ResultStatusCode status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class, using the provided status code
        /// and value.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <param name="value">The result value.</param>
        public Result(ResultStatusCode status, object value)
            : this(status)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{{status:'{this.Status}',value:'{this.Value}'}}";
        }
    }

    /// <summary>
    /// An <see cref="Result"/> with a fixed successful result type.
    /// </summary>
    /// <typeparam name="TSuccess">The result type when the operation outcome is successful.</typeparam>
    public class Result<TSuccess> : Result
    {
        /// <summary>
        /// The result value when the operation outcome is successful.
        /// </summary>
        public TSuccess ValueAsSuccess
        {
            get { return (TSuccess)Value; }
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess>"/> object using the provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess>"/> object whose <see cref="Result.Status"/> property
        /// is initialized with the <paramref name="status"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess>(ResultStatusCode status)
        {
            return new Result<TSuccess>(status);
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess>"/> object using the <see cref="ResultStatusCode.OK"/>
        /// status code and the provided value.
        /// </summary>
        /// <param name="value">The result value of the operation.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess>"/> object whose <see cref="Result.Status"/> property
        /// is set to <see cref="ResultStatusCode.OK"/>, and whose <see cref="Result.Value"/> property
        /// is initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess>(TSuccess value)
        {
            return new Result<TSuccess>(ResultStatusCode.OK, value);
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess>"/> object using the <see cref="ResultStatusCode.Error"/>
        /// status code and the provided value.
        /// </summary>
        /// <param name="value">The result value of the operation.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess>"/> object whose <see cref="Result.Status"/> property
        /// is initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess>(ErrorBuilder value)
        {
            return new Result<TSuccess>(ResultStatusCode.Error,
                                       (value != null)
                                           ? value.GetErrors()
                                           : null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result&lt;TSuccess>"/> class, using the
        /// provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        public Result(ResultStatusCode status)
            : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result&lt;TSuccess>"/> class, using the
        /// provided status code and value.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <param name="value">The result value.</param>
        public Result(ResultStatusCode status, object value)
            : base(status, value) { }
    }

    /// <summary>
    /// An <see cref="Result&lt;TSuccess>"/> with a fixed error result type.
    /// </summary>
    /// <typeparam name="TSuccess">The result type when the operation outcome is successful.</typeparam>
    /// <typeparam name="TError">The result type when the operation outcome is not successful.</typeparam>
    public class Result<TSuccess, TError> : Result<TSuccess>
    {
        /// <summary>
        /// The result value when the operation outcome is not successful.
        /// </summary>
        public TError ValueAsError
        {
            get { return (TError)Value; }
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess, TError>"/> object using the provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess, TError>"/> object whose <see cref="Result.Status"/> property
        /// is initialized with the <paramref name="status"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess, TError>(ResultStatusCode status)
        {
            return new Result<TSuccess, TError>(status);
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess, TError>"/> object using the <see cref="ResultStatusCode.OK"/>
        /// status code and the provided value.
        /// </summary>
        /// <param name="value">The result value of the operation.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess, TError>"/> object whose <see cref="Result.Status"/> property
        /// is set to <see cref="ResultStatusCode.OK"/>, and whose <see cref="Result.Value"/> property
        /// is initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess, TError>(TSuccess value)
        {
            return new Result<TSuccess, TError>(ResultStatusCode.OK, value);
        }

        /// <summary>
        /// Creates a new <see cref="Result&lt;TSuccess, TError>"/> object using the <see cref="ResultStatusCode.Error"/>
        /// status code and the provided value.
        /// </summary>
        /// <param name="value">The result value of the operation.</param>
        /// <returns>
        /// A new <see cref="Result&lt;TSuccess, TError>"/> object whose <see cref="Result.Status"/> property
        /// is set to <see cref="ResultStatusCode.Error"/>, and whose <see cref="Result.Value"/> property
        /// is initialized with the <paramref name="value"/> parameter.
        /// </returns>
        public static implicit operator Result<TSuccess, TError>(TError value)
        {
            return new Result<TSuccess, TError>(ResultStatusCode.Error, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result&lt;TSuccess, TError>"/> class, using the
        /// provided status code.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        public Result(ResultStatusCode status)
            : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result&lt;TSuccess, TError>"/> class, using the
        /// provided status code and value.
        /// </summary>
        /// <param name="status">The status code of the result.</param>
        /// <param name="value">The result value.</param>
        public Result(ResultStatusCode status, object value)
            : base(status, value) { }
    }}
