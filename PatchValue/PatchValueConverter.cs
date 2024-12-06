using Newtonsoft.Json;
using System.Globalization;

public class PatchValueConverter : JsonConverter
{
    private readonly Type _type = typeof(PatchValue<>);

    public override bool CanRead => true;
    public override bool CanWrite => false;

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == _type;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return Activator.CreateInstance(objectType, true);
        }
        if (reader.Value is DateTime dt)
        {
            var dataType = objectType.GenericTypeArguments[0];
            if (dataType == typeof(DateOnly))
            {
                var date = DateOnly.FromDateTime(dt);
                return Activator.CreateInstance(objectType, [date]);
            }
            return Activator.CreateInstance(objectType, [dt]);
        }
        if (reader.Value is string str)
        {
            var dataType = objectType.GenericTypeArguments[0];
            if (dataType == typeof(DateOnly))
            {
                var date = DateOnly.Parse(str, new CultureInfo("en-US"));
                return Activator.CreateInstance(objectType, [date]);
            }
            if (dataType == typeof(DateTime))
            {
                var date = DateTime.Parse(str, new CultureInfo("en-US"));
                return Activator.CreateInstance(objectType, [date]);
            }
            return Activator.CreateInstance(objectType, [str]);
        }
        return Activator.CreateInstance(objectType, [reader.Value]);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
