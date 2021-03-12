using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Transport.Sync.Migration
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationStatusOverride
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00009793 File Offset: 0x00007993
		public MigrationStatusOverride(DetailedAggregationStatus detailedStatus, MigrationSubscriptionStatus migrationDetailedStatus)
		{
			this.DetailedAggregationStatus = detailedStatus;
			this.MigrationSubscriptionStatus = migrationDetailedStatus;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000097A9 File Offset: 0x000079A9
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000097B1 File Offset: 0x000079B1
		public DetailedAggregationStatus DetailedAggregationStatus { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000097BA File Offset: 0x000079BA
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000097C2 File Offset: 0x000079C2
		public MigrationSubscriptionStatus MigrationSubscriptionStatus { get; private set; }
	}
}
