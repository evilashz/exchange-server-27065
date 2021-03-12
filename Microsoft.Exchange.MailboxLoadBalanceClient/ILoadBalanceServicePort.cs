using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalanceClient
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILoadBalanceServicePort
	{
		// Token: 0x06000015 RID: 21
		BatchCapacityDatum GetBatchCapacityForForest(int numberOfMailboxes);

		// Token: 0x06000016 RID: 22
		BatchCapacityDatum GetBatchCapacityForForest(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize);

		// Token: 0x06000017 RID: 23
		CapacitySummary GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData);

		// Token: 0x06000018 RID: 24
		ADObjectId GetDatabaseForNewConsumerMailbox();
	}
}
