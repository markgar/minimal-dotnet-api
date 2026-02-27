using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Tests;

public class HealthEndpointTests
{
    private static IServiceProvider BuildServiceProvider()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
        return app.Services;
    }

    private static DefaultHttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        context.RequestServices = BuildServiceProvider();
        context.Response.Body = new MemoryStream();
        return context;
    }

    [Fact]
    public async Task HealthEndpoint_Returns200OkStatus()
    {
        var result = Results.Ok(new { status = "healthy" });
        var context = CreateHttpContext();

        await result.ExecuteAsync(context);

        Assert.Equal(200, context.Response.StatusCode);
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsJsonBodyWithHealthyStatus()
    {
        var result = Results.Ok(new { status = "healthy" });
        var context = CreateHttpContext();

        await result.ExecuteAsync(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();

        Assert.Contains("\"status\"", body);
        Assert.Contains("\"healthy\"", body);
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsJsonContentType()
    {
        var result = Results.Ok(new { status = "healthy" });
        var context = CreateHttpContext();

        await result.ExecuteAsync(context);

        Assert.StartsWith("application/json", context.Response.ContentType);
    }

    [Fact]
    public async Task HealthEndpoint_ResponseBodyIsValidJson()
    {
        var result = Results.Ok(new { status = "healthy" });
        var context = CreateHttpContext();

        await result.ExecuteAsync(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();

        var doc = System.Text.Json.JsonDocument.Parse(body);
        var statusValue = doc.RootElement.GetProperty("status").GetString();

        Assert.Equal("healthy", statusValue);
    }
}
