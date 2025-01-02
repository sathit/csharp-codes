# PatchValue\<T\> for .NET API

Patch payload to distinct between null and undefined values.

## Setup

```cs
// Program.cs

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
    c.MapType<PatchValue<DateTimeOffset>>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date-time",
    });
    c.MapType<PatchValue<TimeOnly>>(() => new OpenApiSchema
    {
        Type = "string",
        Example = "00:00:00",
    });
    c.MapType<PatchValue<TimeSpan>>(() => new OpenApiSchema
    {
        Type = "string",
        Example = "0.00:00:00",
    });
    // ...
});
```

## Usage

```c#
// request class
public class PatchExampleRequest
{
    public PatchValue<int>? IntField { get; set; }
    public PatchValue<bool>? BoolField { get; set; }
    public PatchValue? StringField { get; set; }
    public PatchValue<DateOnly>? DateOnlyField { get; set; }
}

// sample request
PATCH /examples/1
payload
{
  "intField": 11,     // update value
  "boolField": null, // clear value
  //"stringField" is not in payload (do nothing)
  "dateOnlyField": "2024-01-15" // update value
}


// integration
[HttpPatch("{id}")]
public async Task<ApiResult> PatchExample(int id, [FromBody] PatchExampleRequest request)
{
    var src = new ExampleModel
    {
        IntField = 1,
        BoolField = true,
        StringField = "my string",
        DateOnlyField = null,
    };

    src.IntField = request.IntField ?? src.IntField; // 11
    src.BoolField = request.BoolField ?? src.BoolField; // null
    src.StringField = request.StringField ?? src.StringField; // my string
    src.DateOnlyField = request.DateOnlyField ?? src.DateOnlyField; // 2024-01-15
}
```
