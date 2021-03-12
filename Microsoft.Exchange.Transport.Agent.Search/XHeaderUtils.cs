using System;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Transport.Agent.Search
{
	// Token: 0x02000006 RID: 6
	internal static class XHeaderUtils
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002F18 File Offset: 0x00001118
		internal static string GetProperty(HeaderList mimeHeaders, string name)
		{
			Header header = mimeHeaders.FindFirst(name);
			if (header == null || header.Value == null)
			{
				return null;
			}
			return header.Value;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F40 File Offset: 0x00001140
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
