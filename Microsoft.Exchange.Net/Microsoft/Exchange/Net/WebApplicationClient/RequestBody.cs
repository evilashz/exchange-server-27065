using System;
using System.IO;
using System.Net.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B1E RID: 2846
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class RequestBody
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000A028B File Offset: 0x0009E48B
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x000A0293 File Offset: 0x0009E493
		public ContentType ContentType { get; set; }

		// Token: 0x06003D7A RID: 15738
		public abstract void Write(Stream writeStream);

		// Token: 0x02000B1F RID: 2847
		public static class MediaTypes
		{
			// Token: 0x04003597 RID: 13719
			public const string FormUrlEncoded = "application/x-www-form-urlencoded";
		}
	}
}
