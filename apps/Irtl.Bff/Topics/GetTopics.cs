using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Irtl.Bff.WebPreview.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Irtl.Bff.Topics;

public class GetTopics
{
    [Function("topics")]
    // [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(string[]), 
        Description = "The set of known topics")]
    public async Task<HttpResponseData> Run([HttpTrigger("get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("GetTopics");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new[] { "tech", "electronics", "4wd" });

        return response;
    }
}