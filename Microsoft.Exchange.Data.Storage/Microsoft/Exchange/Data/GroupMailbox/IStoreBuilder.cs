using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200080B RID: 2059
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStoreBuilder
	{
		// Token: 0x06004CBD RID: 19645
		IAssociationStore Create(IMailboxLocator targetMailbox, IMailboxAssociationPerformanceTracker performanceTracker);
	}
}
