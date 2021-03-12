using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200009E RID: 158
	public static class JsonConverter
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x0001248E File Offset: 0x0001068E
		public static string ToJSON(object instance)
		{
			return JsonConverter.ToJSON(instance, Array<Type>.Empty);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001249C File Offset: 0x0001069C
		public static string ToJSON(object instance, IEnumerable<Type> knownTypes)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (knownTypes == null)
			{
				throw new ArgumentNullException("knownTypes");
			}
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(instance.GetType(), knownTypes);
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, instance);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001252C File Offset: 0x0001072C
		public static T FromJSON<T>(string jsonString)
		{
			return JsonConverter.FromJSON<T>(jsonString, Array<Type>.Empty);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001253C File Offset: 0x0001073C
		public static T FromJSON<T>(string jsonString, IEnumerable<Type> knownTypes)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			if (knownTypes == null)
			{
				throw new ArgumentNullException("knownTypes");
			}
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), knownTypes);
			T result;
			using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
			{
				result = (T)((object)dataContractJsonSerializer.ReadObject(stream));
			}
			return result;
		}
	}
}
