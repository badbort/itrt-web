using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Irtl.Bff.WebPreview.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Irtl.Bff.WebPreview;

public class WebPreviewFunctions
{
    private readonly IHttpClientFactory _clientFactory;

    public WebPreviewFunctions(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    public class GetUrlPreviewParams
    {
        [Required]
        public string? Url { get; set; }
    }

    [Function("get-url-preview")]
    [OpenApiOperation("get-url-preview", Summary = "Gets a preview of a url", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, typeof(GetUrlPreviewParams), 
        Required = true,
        Description = "Contains the url to be retrieved", 
        Example = typeof(RequestExample))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(UrlPreview), 
        Summary = "The response",
        Description = "This returns the response containing immediate url preview values, without any image", 
        Example = typeof(ResponseExample))]
    public async Task<HttpResponseData> Run([HttpTrigger("post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var requestData = await req.ReadFromJsonAsync<GetUrlPreviewParams>();

        if (string.IsNullOrEmpty(requestData?.Url))
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var logger = executionContext.GetLogger(nameof(WebPreviewFunctions));
        var response = req.CreateResponse(HttpStatusCode.OK);

        var httpClient = _clientFactory.CreateClient();

        var htmlContent = await httpClient.GetStringAsync(requestData.Url);

        var m = Regex.Match(htmlContent, @"<title>\s*(.+?)\s*</title>");
        var title = m.Success ? m.Groups[1].Value : "Unknown Title";

        await response.WriteAsJsonAsync(new UrlPreview
        {
            Title = title,
            Summary = "Blah blah blah website"
        });

        return response;
    }

    private class ResponseExample : OpenApiExample<UrlPreview>
    {
        public override IOpenApiExample<UrlPreview> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "sample1",
                    new UrlPreview { Title = "Hello World Website", Summary = "Welcome the whole new world!" },
                    namingStrategy
                ));

            return this;
        }
    }

    private class RequestExample : OpenApiExample<GetUrlPreviewParams>
    {
        public override IOpenApiExample<GetUrlPreviewParams> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "sample1",
                    new GetUrlPreviewParams() { Url = "www.rac.com.au" },
                    namingStrategy
                ));
            
            Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "sample2",
                    new GetUrlPreviewParams() { Url = "www.google.com.au" },
                    namingStrategy
                ));

            return this;
        }
    }
}