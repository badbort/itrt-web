using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Irtl.Bff.Links.Model;
using Irtl.Bff.WebPreview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Irtl.Bff.Links;

public class CreateLink
{
    private readonly ILinksStore _linkStore;

    public CreateLink(ILinksStore linkStore)
    {
        _linkStore = linkStore;
    }
    
    [Function(nameof(AddLink))]
    [OpenApiOperation("add-link", Description = "Add a link")]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, typeof(UrlLink), 
        Required = true,
        Description = "Contains the url link to be added")]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK, Description = "Url was sent and added successfully")]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Description = "Invalid url object was sent in the request body")]
    public async Task<HttpResponseData> AddLink([HttpTrigger("post", Route = "links")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(AddLink));
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var requestData = await req.ReadFromJsonAsync<UrlLink>();

        if (requestData?.Url == null || Uri.TryCreate(requestData.Url, UriKind.Absolute, out var uri))
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        await _linkStore.AddLink(requestData, CancellationToken.None);
        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
        
    [Function(nameof(GetLinks))]
    [OpenApiOperation(Description = "Gets all links")]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, typeof(UrlLink), 
        Required = true,
        Description = "Contains the url link to be added")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(UrlLink[]), Description = "Response containing all links")]
    public async Task<HttpResponseData> GetLinks([HttpTrigger("get", Route = "links")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("CreateLink");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        int.TryParse(req.Query["page"], out int page);
        int.TryParse(req.Query["pageSize"], out int pageSize);

        var links = await _linkStore.GetLinks(CancellationToken.None).ToListAsync();
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(links.ToArray());
        
        return response;
    }
}