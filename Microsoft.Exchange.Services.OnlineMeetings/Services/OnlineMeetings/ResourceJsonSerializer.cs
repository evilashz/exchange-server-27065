using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000020 RID: 32
	internal class ResourceJsonSerializer
	{
		// Token: 0x060000AF RID: 175 RVA: 0x0000332C File Offset: 0x0000152C
		public MemoryStream Serialize(object value, MemoryStream stream)
		{
			Resource resource = value as Resource;
			if (resource == null && value != null)
			{
				throw new ArgumentException("The object to be serialized is not derived from Resource.", "value");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			try
			{
				object obj = this.ConvertResourceToDictionary(resource, null);
				string text = ResourceJsonSerializer.jsSerializer.Serialize(obj);
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[OnlineMeetings][ResourceJsonSerializer.Serialize] Serialized object: {0}", text);
				StreamWriter streamWriter = new StreamWriter(stream);
				streamWriter.Write(text);
				streamWriter.Flush();
			}
			catch (Exception innerException)
			{
				throw new SerializationException("The object could not be serialized correctly. See InnerException for details.", innerException);
			}
			return stream;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033C8 File Offset: 0x000015C8
		public object Deserialize(Type type, Stream stream)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!typeof(Resource).IsAssignableFrom(type))
			{
				throw new ArgumentException("The requested Type needs to be derived from ResourceBase", "type");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object result;
			try
			{
				StreamReader streamReader = new StreamReader(stream);
				string text = streamReader.ReadToEnd();
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[OnlineMeetings][ResourceJsonSerializer.Deserialize] Response Body:{0}", text);
				Dictionary<string, object> dictionary = ResourceJsonSerializer.jsSerializer.Deserialize<Dictionary<string, object>>(text);
				Resource resource = Resource.FromDictionary(type, dictionary);
				result = resource;
			}
			catch (Exception innerException)
			{
				throw new SerializationException("The object could not be deserialized correctly. See InnerException for details.", innerException);
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003474 File Offset: 0x00001674
		public object ConvertResourceToDictionary(Resource value, List<EmbeddedPart> mimeParts)
		{
			if (value == null)
			{
				return new Dictionary<string, string>();
			}
			return value.ToDictionary(mimeParts);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003486 File Offset: 0x00001686
		public string GetRequestContentType(Type requestType)
		{
			if (typeof(Resource).IsAssignableFrom(requestType))
			{
				return "application/vnd.microsoft.com.ucwa+json";
			}
			throw new ArgumentException("Unsupported request type. This Transformer only supports Resource classes.", "requestType");
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000034AF File Offset: 0x000016AF
		public string GetResponseContentType(Type responseType)
		{
			if (typeof(Resource).IsAssignableFrom(responseType))
			{
				return "application/vnd.microsoft.com.ucwa+json";
			}
			throw new ArgumentException("Unsupported response type. This Transformer only supports ConferenceServicesResponse.", "responseType");
		}

		// Token: 0x040000ED RID: 237
		private static readonly JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
	}
}
