using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionEventArgs<R> where R : SubscriptionEventResult
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008D66 File Offset: 0x00006F66
		public SubscriptionEventArgs(SyncLogSession syncLogSession, R result)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("result", result);
			this.result = result;
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008D97 File Offset: 0x00006F97
		public R Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008D9F File Offset: 0x00006F9F
		public SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x04000101 RID: 257
		private readonly R result;

		// Token: 0x04000102 RID: 258
		private readonly SyncLogSession syncLogSession;
	}
}
