using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013B RID: 315
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionStatistics : IStepSnapshot, IMigrationSerializable
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000FD7 RID: 4055
		ExDateTime? LastSyncTime { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000FD8 RID: 4056
		long NumItemsSkipped { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000FD9 RID: 4057
		long NumItemsSynced { get; }
	}
}
