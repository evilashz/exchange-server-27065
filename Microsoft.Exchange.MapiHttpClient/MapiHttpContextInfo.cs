using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MapiHttpContextInfo
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005AAC File Offset: 0x00003CAC
		internal MapiHttpContextInfo(ClientSessionContext clientSessionContext)
		{
			this.Cookies = clientSessionContext.Cookies;
			this.RequestPath = clientSessionContext.RequestPath;
			this.ExpirationPeriod = clientSessionContext.ActualExpiration;
			this.LastCall = clientSessionContext.LastCall;
			this.LastElapsedTime = clientSessionContext.LastElapsedTime;
			this.Expires = clientSessionContext.Expires;
			this.RequestGroupId = clientSessionContext.RequestGroupId;
		}

		// Token: 0x04000047 RID: 71
		public readonly Dictionary<string, string> Cookies;

		// Token: 0x04000048 RID: 72
		public readonly string RequestPath;

		// Token: 0x04000049 RID: 73
		public readonly TimeSpan? ExpirationPeriod;

		// Token: 0x0400004A RID: 74
		public readonly ExDateTime? Expires;

		// Token: 0x0400004B RID: 75
		public readonly ExDateTime? LastCall;

		// Token: 0x0400004C RID: 76
		public readonly TimeSpan? LastElapsedTime;

		// Token: 0x0400004D RID: 77
		public readonly string RequestGroupId;
	}
}
