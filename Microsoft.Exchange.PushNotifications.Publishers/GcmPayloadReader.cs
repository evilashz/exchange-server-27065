using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A2 RID: 162
	internal class GcmPayloadReader
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00012D91 File Offset: 0x00010F91
		public Dictionary<string, string> Read(string payload)
		{
			return GcmPayloadReader.PropertyReader.Read(payload);
		}

		// Token: 0x040002C4 RID: 708
		private static readonly PropertyReader PropertyReader = new PropertyReader(new string[]
		{
			"&",
			"\n",
			"\r\n"
		}, new string[]
		{
			"="
		});
	}
}
