using System;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000087 RID: 135
	internal static class XHeaderUtils
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00018320 File Offset: 0x00016520
		internal static string GetProperty(HeaderList mimeHeaders, string name)
		{
			Header header = mimeHeaders.FindFirst(name);
			if (header == null || header.Value == null)
			{
				return null;
			}
			return header.Value;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00018348 File Offset: 0x00016548
		internal static void SetProperty(HeaderList mimeHeaders, string name, string value)
		{
			mimeHeaders.RemoveAll(name);
			if (!string.IsNullOrEmpty(value))
			{
				mimeHeaders.AppendChild(new TextHeader(name, value));
			}
		}
	}
}
