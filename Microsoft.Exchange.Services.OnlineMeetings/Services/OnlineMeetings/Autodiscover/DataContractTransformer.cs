using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover.DataContract;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x02000033 RID: 51
	internal static class DataContractTransformer
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00007758 File Offset: 0x00005958
		public static AutodiscoverResponse TransformResponse(string responseBody)
		{
			AutodiscoverResponse result;
			try
			{
				Dictionary<string, object> dictionary = DataContractTransformer.jsSerializer.Deserialize<Dictionary<string, object>>(responseBody);
				AutodiscoverResponse autodiscoverResponse = AutodiscoverResponse.FromDictionary(dictionary);
				result = autodiscoverResponse;
			}
			catch (InvalidOperationException innerException)
			{
				throw new SerializationException("An exception occurred during response deserialization.", innerException);
			}
			return result;
		}

		// Token: 0x0400015B RID: 347
		private static readonly JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
	}
}
