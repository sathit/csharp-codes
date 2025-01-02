using Newtonsoft.Json;
using System.Globalization;

namespace PatchValue
{
    public class PatchValueConverter : JsonConverter
    {
        private readonly Type _stringType = typeof(PatchValue);
        private readonly Type _structType = typeof(PatchValue<>);

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == _structType || objectType == _stringType;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return Activator.CreateInstance(objectType, true);
            }

            if (objectType == _stringType)
            {
                return Activator.CreateInstance(objectType, [reader.Value]);
            }

            var dataType = objectType.GenericTypeArguments[0];
            if (dataType.IsEnum)
            {
                return Activator.CreateInstance(objectType, [Enum.ToObject(dataType, reader.Value)]);
            }
            if (reader.Value is DateTime dt)
            {
                if (dataType == typeof(DateOnly))
                {
                    var date = DateOnly.FromDateTime(dt);
                    return Activator.CreateInstance(objectType, [date]);
                }
                if (dataType == typeof(DateTimeOffset))
                {
                    var date = new DateTimeOffset(dt, TimeSpan.Zero);
                    return Activator.CreateInstance(objectType, [date]);
                }
                return Activator.CreateInstance(objectType, [dt]);
            }
            if (reader.Value is string str)
            {
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
                if (dataType == typeof(TimeOnly))
                {
                    var time = TimeOnly.Parse(str, new CultureInfo("en-US"));
                    return Activator.CreateInstance(objectType, [time]);
                }
                if (dataType == typeof(TimeSpan))
                {
                    var timeSpan = TimeSpan.Parse(str, new CultureInfo("en-US"));
                    return Activator.CreateInstance(objectType, [timeSpan]);
                }
            }

            var newValue = Convert.ChangeType(reader.Value, dataType);
            return Activator.CreateInstance(objectType, [newValue]);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
