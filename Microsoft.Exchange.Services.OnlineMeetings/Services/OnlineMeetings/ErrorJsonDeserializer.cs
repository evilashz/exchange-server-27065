using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000011 RID: 17
	internal class ErrorJsonDeserializer
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000024DC File Offset: 0x000006DC
		public static ErrorInformation Deserialize(Stream stream)
		{
			ErrorInformation result;
			try
			{
				StreamReader streamReader = new StreamReader(stream);
				IDictionary dictionary = ErrorJsonDeserializer.jsSerializer.Deserialize<Dictionary<string, object>>(streamReader.ReadToEnd());
				ErrorCode code = ErrorCode.Unknown;
				if (dictionary.Contains("code"))
				{
					Enum.TryParse<ErrorCode>(dictionary["code"].ToString(), out code);
				}
				ErrorSubcode subcode = ErrorSubcode.None;
				if (dictionary.Contains("subcode"))
				{
					Enum.TryParse<ErrorSubcode>(dictionary["subcode"].ToString(), out subcode);
				}
				string message = null;
				if (dictionary.Contains("message"))
				{
					message = (dictionary["message"] as string);
				}
				ErrorInformation errorInformation = new ErrorInformation
				{
					Code = code,
					Subcode = subcode,
					Message = message
				};
				if (dictionary.Contains("debugInfo"))
				{
					IDictionary dictionary2 = dictionary["debugInfo"] as IDictionary;
					foreach (object obj in dictionary2.Keys)
					{
						string key = (string)obj;
						errorInformation.DebugInfo.Add(key, dictionary2[key] as string);
					}
				}
				if (dictionary.Contains("parameters"))
				{
					IEnumerable enumerable = dictionary["parameters"] as IEnumerable;
					foreach (object obj2 in enumerable)
					{
						IDictionary dictionary3 = (IDictionary)obj2;
						errorInformation.Parameters[dictionary3["name"] as string] = (dictionary3["reason"] as string);
					}
				}
				result = errorInformation;
			}
			catch (Exception innerException)
			{
				throw new SerializationException("Failed to deserialize a UCWA error response", innerException);
			}
			return result;
		}

		// Token: 0x040000B3 RID: 179
		private static readonly JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

		// Token: 0x02000012 RID: 18
		private static class StringConstants
		{
			// Token: 0x040000B4 RID: 180
			public const string Code = "code";

			// Token: 0x040000B5 RID: 181
			public const string Subcode = "subcode";

			// Token: 0x040000B6 RID: 182
			public const string Message = "message";

			// Token: 0x040000B7 RID: 183
			public const string DebugInfo = "debugInfo";

			// Token: 0x040000B8 RID: 184
			public const string Parameters = "parameters";

			// Token: 0x040000B9 RID: 185
			public const string Name = "name";

			// Token: 0x040000BA RID: 186
			public const string Reason = "reason";
		}
	}
}
