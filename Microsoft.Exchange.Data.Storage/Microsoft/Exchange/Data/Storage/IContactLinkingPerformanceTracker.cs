using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C4 RID: 1220
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IContactLinkingPerformanceTracker : IPerformanceTracker
	{
		// Token: 0x06003599 RID: 13721
		void IncrementContactsCreated();

		// Token: 0x0600359A RID: 13722
		void IncrementContactsUpdated();

		// Token: 0x0600359B RID: 13723
		void IncrementContactsRead();

		// Token: 0x0600359C RID: 13724
		void IncrementContactsProcessed();

		// Token: 0x0600359D RID: 13725
		ILogEvent GetLogEvent();
	}
}
