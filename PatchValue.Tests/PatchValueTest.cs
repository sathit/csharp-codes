using Newtonsoft.Json;

namespace PatchValue.Tests
{
    public class PatchValueTest
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public PatchValueTest()
        {
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.Converters.Add(new PatchValueConverter());
        }

        [Fact]
        public void EmptyPayload()
        {
            var payload = "{}";
            var result = JsonConvert.DeserializeObject<AllTypesModel>(payload, _serializerSettings);

            Assert.NotNull(result);
            Assert.Null(result.BoolField);
            Assert.Null(result.CharField);
            Assert.Null(result.IntField);
            Assert.Null(result.LongField);
            Assert.Null(result.DoubleField);
            Assert.Null(result.DecimalField);
            Assert.Null(result.StringField);
            Assert.Null(result.DateOnlyField);
            Assert.Null(result.TimeOnlyField);
            Assert.Null(result.DateTimeField);
            Assert.Null(result.DateTimeOffsetField);
            Assert.Null(result.EnumField);
        }

        [Fact]
        public void AllAreNull()
        {
            var payload = "{\"BoolField\":null,\"CharField\":null,\"IntField\":null,\"LongField\":null,\"DoubleField\":null,\"DecimalField\":null,\"StringField\":null,\"DateOnlyField\":null,\"TimeOnlyField\":null,\"DateTimeField\":null,\"DateTimeOffsetField\":null,\"TimeSpanField\":null,\"EnumField\":null}";
            var result = JsonConvert.DeserializeObject<AllTypesModel>(payload, _serializerSettings);

            Assert.NotNull(result);
            Assert.NotNull(result.BoolField);
            Assert.Null(result.BoolField.Value);
            Assert.NotNull(result.CharField);
            Assert.Null(result.CharField.Value);
            Assert.NotNull(result.IntField);
            Assert.Null(result.IntField.Value);
            Assert.NotNull(result.LongField);
            Assert.Null(result.LongField.Value);
            Assert.NotNull(result.DoubleField);
            Assert.Null(result.DoubleField.Value);
            Assert.NotNull(result.DecimalField);
            Assert.Null(result.DecimalField.Value);
            Assert.NotNull(result.StringField);
            Assert.Null(result.StringField.Value);
            Assert.NotNull(result.DateOnlyField);
            Assert.Null(result.DateOnlyField.Value);
            Assert.NotNull(result.TimeOnlyField);
            Assert.Null(result.TimeOnlyField.Value);
            Assert.NotNull(result.DateTimeField);
            Assert.Null(result.DateTimeField.Value);
            Assert.NotNull(result.DateTimeOffsetField);
            Assert.Null(result.DateTimeOffsetField.Value);
            Assert.NotNull(result.EnumField);
            Assert.Null(result.EnumField.Value);
        }

        [Fact]
        public void AllAreNotNull()
        {
            var payload = "{\"BoolField\":true,\"CharField\":\"a\",\"IntField\":1,\"LongField\":2,\"DoubleField\":3,\"DecimalField\":4,\"StringField\":\"bbb\",\"DateOnlyField\":\"2024-01-16\",\"TimeOnlyField\":\"14:45:59\",\"DateTimeField\":\"2024-01-16T14:45:59Z\",\"DateTimeOffsetField\":\"2024-01-16T14:45:59Z\",\"TimeSpanField\":\"180.14:45:59\",\"EnumField\":2}";
            var result = JsonConvert.DeserializeObject<AllTypesModel>(payload, _serializerSettings);

            Assert.NotNull(result);
            Assert.NotNull(result.BoolField);
            Assert.NotNull(result.BoolField.Value);
            Assert.NotNull(result.CharField);
            Assert.NotNull(result.CharField.Value);
            Assert.NotNull(result.IntField);
            Assert.NotNull(result.IntField.Value);
            Assert.NotNull(result.LongField);
            Assert.NotNull(result.LongField.Value);
            Assert.NotNull(result.DoubleField);
            Assert.NotNull(result.DoubleField.Value);
            Assert.NotNull(result.DecimalField);
            Assert.NotNull(result.DecimalField.Value);
            Assert.NotNull(result.StringField);
            Assert.NotNull(result.StringField.Value);
            Assert.NotNull(result.DateOnlyField);
            Assert.NotNull(result.DateOnlyField.Value);
            Assert.NotNull(result.TimeOnlyField);
            Assert.NotNull(result.TimeOnlyField.Value);
            Assert.NotNull(result.DateTimeField);
            Assert.NotNull(result.DateTimeField.Value);
            Assert.NotNull(result.DateTimeOffsetField);
            Assert.NotNull(result.DateTimeOffsetField.Value);
            Assert.NotNull(result.TimeSpanField);
            Assert.NotNull(result.TimeSpanField.Value);
            Assert.NotNull(result.EnumField);
            Assert.NotNull(result.EnumField.Value);
        }

        [Fact]
        public void GetValue()
        {
            var value = new PatchValue<bool>(true);

            Assert.NotNull(value);
            Assert.IsType<PatchValue<bool>>(value);
            Assert.True(value);
            Assert.True(value.Value);
        }

        [Fact]
        public void ImplicitCasting()
        {
            bool? value = new PatchValue<bool>(true);

            Assert.True(value);
        }

        [Fact]
        public void PatchWithValue_IfShorthand()
        {
            bool? oldValue = null;
            var newValue = new PatchValue<bool>(true);
            var value = newValue == null ? oldValue : newValue;

            Assert.True(oldValue ?? value);
        }

        [Fact]
        public void PatchWithValue_NullCoalescing()
        {
            bool? oldValue = null;
            var newValue = new PatchValue<bool>(true);
            var value = oldValue ?? newValue;

            Assert.True(value);
        }

        [Fact]
        public void PatchNullValue_IfShorthand()
        {
            bool? oldValue = null;
            var newValue = new PatchValue<bool>(null);
            var value = newValue == null ? oldValue : newValue;

            Assert.Null(value);
        }

        [Fact]
        public void PatchNullValue_NullCoalescing()
        {
            bool? oldValue = null;
            var newValue = new PatchValue<bool>(null);
            var value = oldValue ?? newValue;

            Assert.Null(value);
        }
    }

    public class AllTypesModel
    {
        public PatchValue<bool>? BoolField { get; set; }
        public PatchValue<char>? CharField { get; set; }
        public PatchValue<int>? IntField { get; set; }
        public PatchValue<long>? LongField { get; set; }
        public PatchValue<double>? DoubleField { get; set; }
        public PatchValue<decimal>? DecimalField { get; set; }
        public PatchValue? StringField { get; set; }
        public PatchValue<DateOnly>? DateOnlyField { get; set; }
        public PatchValue<TimeOnly>? TimeOnlyField { get; set; }
        public PatchValue<DateTime>? DateTimeField { get; set; }
        public PatchValue<DateTimeOffset>? DateTimeOffsetField { get; set; }
        public PatchValue<TimeSpan>? TimeSpanField { get; set; }
        public PatchValue<EnumType>? EnumField { get; set; }
    }

    public enum EnumType
    {
        None = 0,
        Value1 = 1,
        Value2 = 2,
    }
}