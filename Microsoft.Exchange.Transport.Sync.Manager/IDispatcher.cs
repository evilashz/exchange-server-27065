using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDispatcher
	{
		// Token: 0x06000259 RID: 601
		void Shutdown();

		// Token: 0x0600025A RID: 602
		DispatchResult DispatchSubscription(DispatchEntry dispatchEntry, ISubscriptionInformation subscriptionInformation);

		// Token: 0x0600025B RID: 603
		XElement GetDiagnosticInfo(SyncDiagnosticMode mode);
	}
}
