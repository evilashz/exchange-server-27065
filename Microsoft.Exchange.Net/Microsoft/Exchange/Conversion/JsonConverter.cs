using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x0200005B RID: 91
	internal class JsonConverter
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000C440 File Offset: 0x0000A640
		public static string Serialize<T>(T obj, IEnumerable<Type> types = null)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				JsonConverter.Serialize<T>(obj, memoryStream, types);
				byte[] array = memoryStream.ToArray();
				string @string = Encoding.UTF8.GetString(array, 0, array.Length);
				result = @string;
			}
			return result;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C494 File Offset: 0x0000A694
		public static string Serialize<T>(T obj, IEnumerable<Type> types, DateTimeFormat dateTimeFormat)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				JsonConverter.Serialize<T>(obj, memoryStream, types, dateTimeFormat);
				byte[] array = memoryStream.ToArray();
				string @string = Encoding.UTF8.GetString(array, 0, array.Length);
				result = @string;
			}
			return result;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public static void Serialize<T>(T obj, Stream stream, IEnumerable<Type> types = null)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), types);
			dataContractJsonSerializer.WriteObject(stream, obj);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C514 File Offset: 0x0000A714
		public static void Serialize<T>(T obj, Stream stream, IEnumerable<Type> types, DateTimeFormat dateTimeFormat)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
			{
				DateTimeFormat = dateTimeFormat,
				KnownTypes = types
			};
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), settings);
			dataContractJsonSerializer.WriteObject(stream, obj);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C558 File Offset: 0x0000A758
		public static T Deserialize<T>(string json, IEnumerable<Type> types = null)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				result = JsonConverter.Deserialize<T>(memoryStream, types);
			}
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C59C File Offset: 0x0000A79C
		public static T Deserialize<T>(string json, IEnumerable<Type> types, DateTimeFormat dateTimeFormat)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				result = JsonConverter.Deserialize<T>(memoryStream, types, dateTimeFormat);
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public static T Deserialize<T>(Stream stream, IEnumerable<Type> types = null)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), types);
			return (T)((object)dataContractJsonSerializer.ReadObject(stream));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C60C File Offset: 0x0000A80C
		public static T Deserialize<T>(Stream stream, IEnumerable<Type> types, DateTimeFormat dateTimeFormat)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
			{
				DateTimeFormat = dateTimeFormat,
				KnownTypes = types
			};
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), settings);
			return (T)((object)dataContractJsonSerializer.ReadObject(stream));
		}

		// Token: 0x0400018E RID: 398
		public static readonly DateTimeFormat RoundTripDateTimeFormat = new DateTimeFormat("O");
	}
}
