using LocationService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Common
{
    public class ResultOperations
    {

        public static async Task<Result<string>> ReadHttpResult(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                return new Result<string>(ResultCode.OK, 
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));

            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new Result<string>(ResultCode.NotFound,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));

            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadGateway)
                return new Result<string>(ResultCode.BadGateway,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));

            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return new Result<string>(ResultCode.BadRequest,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));

            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Conflict)
                return new Result<string>(ResultCode.Conflict,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));

            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                return new Result<string>(ResultCode.ServiceUnavailable,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));
            else
                return new Result<string>(ResultCode.Error,
                    HttpUtility.HtmlDecode(await httpResponseMessage.Content.ReadAsStringAsync()));
        }

    }
}
