using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C1 RID: 1217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPerformanceTracker
	{
		// Token: 0x06003578 RID: 13688
		void SetMailboxSessionToTrack(IMailboxSession session);

		// Token: 0x06003579 RID: 13689
		void Start();

		// Token: 0x0600357A RID: 13690
		void Stop();
	}
}
