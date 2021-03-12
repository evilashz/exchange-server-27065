using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000043 RID: 67
	public class JsonHelpers
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public static string SerializeJson(IEnumerable<Type> knownTypes, Type t, object o)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(t, knownTypes);
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, o);
				memoryStream.Position = 0L;
				result = new StreamReader(memoryStream).ReadToEnd();
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public static object DeserializeJson(IEnumerable<Type> knownTypes, Type t, string s)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(t, knownTypes);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(s)))
			{
				result = dataContractJsonSerializer.ReadObject(memoryStream);
			}
			return result;
		}
	}
}
