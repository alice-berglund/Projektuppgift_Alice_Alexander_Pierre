using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.DAL
{
    public class CustomDictionaryJsonConverter<TKey, TValue> : JsonConverter<IDictionary<TKey, TValue>> where TKey : IConvertible
	{
		public override bool CanConvert(Type typeToConvert)
		{
			if (typeToConvert != typeof(Dictionary<TKey, TValue>))
			{
				return false;
			}
			else if (typeToConvert.GenericTypeArguments.First() == typeof(string))
			{
				return false;
			}
			return true;
		}
		public override IDictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionaryWithStringKey = (Dictionary<string, TValue>)JsonSerializer.Deserialize(ref reader, typeof(Dictionary<string, TValue>), options);

			var dictionary = new Dictionary<TKey, TValue>();

			foreach (var kvp in dictionaryWithStringKey)
			{
				dictionary.Add((TKey)Convert.ChangeType(kvp.Key, typeof(TKey)), kvp.Value);
			}

			return dictionary;
		}

		public override void Write(Utf8JsonWriter writer, IDictionary<TKey, TValue> value, JsonSerializerOptions options)
		{
			var dictionary = new Dictionary<string, TValue>(value.Count);

			foreach (var kvp in value)
			{
				dictionary.Add(kvp.Key.ToString(), kvp.Value);
			}

			JsonSerializer.Serialize(writer, dictionary, options);

		}
	}
}
