// using...
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// services.Add...

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // ...
        options.SerializerSettings.Converters.Add(new PatchValueConverter());
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.MapType<PatchValue<DateOnly>>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
    });
    c.MapType<PatchValue<DateTime>>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date-time",
    });
    // ...
});