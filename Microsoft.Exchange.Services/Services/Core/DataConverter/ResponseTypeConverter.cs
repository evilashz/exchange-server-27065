using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F9 RID: 505
	internal class ResponseTypeConverter : EnumConverter
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x00043094 File Offset: 0x00041294
		public static ResponseType Parse(string stringResponseType)
		{
			if (stringResponseType != null)
			{
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c92-1 == null)
				{
					<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c92-1 = new Dictionary<string, int>(6)
					{
						{
							"Accept",
							0
						},
						{
							"Decline",
							1
						},
						{
							"Unknown",
							2
						},
						{
							"NoResponseReceived",
							3
						},
						{
							"Organizer",
							4
						},
						{
							"Tentative",
							5
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c92-1.TryGetValue(stringResponseType, out num))
				{
					ResponseType result;
					switch (num)
					{
					case 0:
						result = ResponseType.Accept;
						break;
					case 1:
						result = ResponseType.Decline;
						break;
					case 2:
						result = ResponseType.None;
						break;
					case 3:
						result = ResponseType.NotResponded;
						break;
					case 4:
						result = ResponseType.Organizer;
						break;
					case 5:
						result = ResponseType.Tentative;
						break;
					default:
						goto IL_B1;
					}
					return result;
				}
			}
			IL_B1:
			throw new FormatException(string.Format(CultureInfo.InvariantCulture, "ResponseType type not supported '{0}'", new object[]
			{
				stringResponseType
			}));
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00043174 File Offset: 0x00041374
		public static string ToString(ResponseType responseType)
		{
			string result = "Unknown";
			switch (responseType)
			{
			case ResponseType.None:
				result = "Unknown";
				break;
			case ResponseType.Organizer:
				result = "Organizer";
				break;
			case ResponseType.Tentative:
				result = "Tentative";
				break;
			case ResponseType.Accept:
				result = "Accept";
				break;
			case ResponseType.Decline:
				result = "Decline";
				break;
			case ResponseType.NotResponded:
				result = "NoResponseReceived";
				break;
			}
			return result;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000431D8 File Offset: 0x000413D8
		public override object ConvertToObject(string propertyString)
		{
			return ResponseTypeConverter.Parse(propertyString);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000431E5 File Offset: 0x000413E5
		public override string ConvertToString(object propertyValue)
		{
			return ResponseTypeConverter.ToString((ResponseType)propertyValue);
		}

		// Token: 0x04000A84 RID: 2692
		private const string Accept = "Accept";

		// Token: 0x04000A85 RID: 2693
		private const string Decline = "Decline";

		// Token: 0x04000A86 RID: 2694
		private const string Unknown = "Unknown";

		// Token: 0x04000A87 RID: 2695
		private const string NoResponseReceived = "NoResponseReceived";

		// Token: 0x04000A88 RID: 2696
		private const string Organizer = "Organizer";

		// Token: 0x04000A89 RID: 2697
		private const string Tentative = "Tentative";
	}
}
