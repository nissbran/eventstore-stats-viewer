using System;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventStore.Monitoring.Infrastructure.Serialization
{
    public class DrivesJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var drives = new Drives();

            if (reader.TokenType == JsonToken.Null)
            {
                reader.Skip();
                return drives;
            }
            
            var jsonObject = JObject.Load(reader);

            foreach (var driveJsonObject in jsonObject)
            {
                drives.Add(ParseDrive(driveJsonObject.Key, driveJsonObject.Value));
            }
            
            return drives;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Drives);
        }

        public override bool CanWrite { get; } = false;
        
        private Drive ParseDrive(string name, JToken linkToken)
        {
            return new Drive
            {
                Name = name,
                AvailableBytes = Convert.ToInt64(linkToken["availableBytes"]),
                TotalBytes = Convert.ToInt64(linkToken["totalBytes"]),
                Usage = Convert.ToString(linkToken["usage"]),
                UsedBytes = Convert.ToInt64(linkToken["usedBytes"])
            };
        }
    }
}