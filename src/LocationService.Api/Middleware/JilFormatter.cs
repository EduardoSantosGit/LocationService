using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace LocationService.Api.Middleware
{
    public static class JilOptions
    {
        public static Options DefaultOptions = new Options(
                prettyPrint: false,
                excludeNulls: true,
                jsonp: false,
                dateFormat: DateTimeFormat.ISO8601,
                includeInherited: true,
                unspecifiedDateTimeKindBehavior: UnspecifiedDateTimeKindBehavior.IsUTC,
                serializationNameFormat: SerializationNameFormat.CamelCase);
    }

    internal class JilInputFormatter : TextInputFormatter
    {
        private readonly ILogger<JilInputFormatter> _logger;
        private readonly Options _options;

        public JilInputFormatter(Options options = null)
        {
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationJsonMediaType);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.TextJsonMediaType);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationAnyJsonSyntaxMediaType);

            _options = options ?? JilOptions.DefaultOptions;
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.HttpContext.Request;
            using (var reader = context.ReaderFactory(request.Body, encoding))
            {
                try
                {
                    return InputFormatterResult.SuccessAsync(JSON.Deserialize(reader, context.ModelType, _options));
                }
                catch (DeserializationException ex)
                {
                    context.ModelState.AddModelError(context.ModelType.Name, ex.Message);
                }
                catch (Exception ex)
                {
                }

                return InputFormatterResult.FailureAsync();
            }
        }
    }

    internal class JilOutputFormatter : TextOutputFormatter
    {
        private readonly ILogger<JilOutputFormatter> _logger;
        private readonly Options _options;
        private readonly MethodInfo _serializeGenericDefinition;

        public JilOutputFormatter(Options options = null)
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationJsonMediaType);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.TextJsonMediaType);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationAnyJsonSyntaxMediaType);

            _options = options ?? JilOptions.DefaultOptions;

            Expression<Action<object, TextWriter, Options>> fakeSerializeCall =
                (data, writer, settings) => JSON.Serialize(data, writer, settings);
            _serializeGenericDefinition = ((MethodCallExpression)fakeSerializeCall.Body).Method.GetGenericMethodDefinition();
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding encoding)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            using (var writer = context.WriterFactory(context.HttpContext.Response.Body, encoding))
            {
                try
                {
                    _serializeGenericDefinition
                        .MakeGenericMethod(context.ObjectType)
                        .Invoke(null, new[] { context.Object, writer, _options });
                }
                catch (TargetInvocationException exception)
                {
                    _logger?.LogDebug(exception.InnerException, "Exception was occurred during serialization");
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
                await writer.FlushAsync();
            }
        }
    }
    public class MediaTypeHeaderValues
    {
        public const string ApplicationJson = "application/json";

        public static readonly Microsoft.Net.Http.Headers.MediaTypeHeaderValue ApplicationJsonMediaType
            = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse(ApplicationJson).CopyAsReadOnly();

        public static readonly Microsoft.Net.Http.Headers.MediaTypeHeaderValue TextJsonMediaType
            = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/json").CopyAsReadOnly();

        public static readonly Microsoft.Net.Http.Headers.MediaTypeHeaderValue ApplicationJsonPatchMediaType
            = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse($"{ApplicationJson}-patch+json").CopyAsReadOnly();

        public static readonly Microsoft.Net.Http.Headers.MediaTypeHeaderValue ApplicationAnyJsonSyntaxMediaType
            = Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/*+json").CopyAsReadOnly();
    }

}
