using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Irtl.Bff.Content;

public static class GetContent
{
    [Function(nameof(GetContent))]
    public static async Task<HttpResponseData> Run([HttpTrigger("get", Route = "content/{*path}")] HttpRequestData req,
        FunctionContext executionContext, string path)
    {
        var logger = executionContext.GetLogger(nameof(GetContent));
        logger.LogInformation("C# HTTP trigger function processed a request.");

        // Local dev for now
        var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "content");
        var fullFilePath = Path.Combine(baseDirectory, path);

        if (File.Exists(fullFilePath))
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

            var mimeType = MimeTypes.GetMimeType(fullFilePath);
            response.Headers.Add("Content-Type", mimeType);

            await response.WriteBytesAsync(await File.ReadAllBytesAsync(fullFilePath), executionContext.CancellationToken);
            return response;
        }

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}