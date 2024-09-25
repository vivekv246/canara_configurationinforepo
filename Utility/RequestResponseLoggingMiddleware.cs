using ConfigurationInfo.Model;
using ConfigurationInfo.Repository.IRepository;
using ConfigurationInfo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationInfo.Utility
{

    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly ILogRepository _logRepository;

        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger, IConfiguration iConfig)
        {
            _next = next;
            _logger = logger;

            _config = iConfig;

        }


        public async Task InvokeAsync(HttpContext context, ILogRepository logRepository)
        {
            context.Request.EnableBuffering();

            LogMetaData metadata = new LogMetaData();
            metadata.TerminalId = "";
            metadata.ServerName = _config.GetSection("serverSetting").GetSection("ServerName").Value.ToString();

            var builder = new StringBuilder();

            var request = await FormatRequest(context.Request);
            if (context.Request.Method.Equals("GET"))
            {
                metadata.Request = "";
            }
    
            else
            {
                metadata.Request = AESOperation.DecryptString(request);
            }


            metadata.RequestTime = DateTime.Now;
            metadata.Url = context.Request.GetEncodedUrl();
            metadata.GeteWayUrl = "";
            metadata.microServiceName = "Configuration Info Service";
            metadata.MethodName = context.Request.Method;


            // builder.Append("Request: ").AppendLine(request);
            // builder.AppendLine("My Request headers:");
            foreach (var header in context.Request.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            // _logger.LogInformation(builder.ToString());

            metadata.RequestHeaders = builder.ToString();


            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using var responseBody = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //Format the response from the server

            var response = await FormatResponse(context.Response);
            metadata.ResponseHttpCode = context.Response.StatusCode.ToString();
            metadata.Response = AESOperation.DecryptString(response);
            metadata.ResponseTime = DateTime.Now;
            // builder2.Append("Response: ").AppendLine(response);
            var builder2 = new StringBuilder();
            //builder2.AppendLine("My Response headers: ");
            foreach (var header in context.Response.Headers)
            {
                builder2.Append(header.Key).Append(':').AppendLine(header.Value);
            }
            metadata.ResponseHeaders = builder2.ToString();
            await logRepository.LogInsertion(metadata);


            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            // Do some processing with body…

            var formattedRequest = body;
            var formattedRequest2 = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";

            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;

            return formattedRequest;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return text;
        }
    }


}
