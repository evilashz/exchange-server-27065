using System;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000035 RID: 53
	internal class AmDbAttemptInfo
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		internal AmDbAttemptInfo(Guid guid, AmDbActionCode actionCode, DateTime attemptTime)
		{
			this.Guid = guid;
			this.ActionCode = actionCode;
			this.LastAttemptTime = attemptTime;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000EF07 File Offset: 0x0000D107
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000EF0F File Offset: 0x0000D10F
		internal Guid Guid { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000EF18 File Offset: 0x0000D118
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000EF20 File Offset: 0x0000D120
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000EF29 File Offset: 0x0000D129
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000EF31 File Offset: 0x0000D131
		internal DateTime LastAttemptTime { get; set; }
	}
}
