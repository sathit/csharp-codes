# PatchValue\<T\> for .NET API

1. [PatchValue.cs](PatchValue.cs) patch class
2. [PatchValueConverter.cs](PatchValueConverter.cs) json converter for Newtonsoft.Json
3. [Program.cs](Program.cs) setup json converter and swagger

## Usage

```c#
// request class
public class PatchExampleRequest
{
    public PatchValue<int>? IntField { get; set; }
    public PatchValue<bool>? BoolField { get; set; }
    public PatchValue<string>? StringField { get; set; }
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
