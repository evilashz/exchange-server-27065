using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAutoProvision
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000325 RID: 805
		string[] Hostnames { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000326 RID: 806
		int[] ConnectivePorts { get; }

		// Token: 0x06000327 RID: 807
		DiscoverSettingsResult DiscoverSetting(SyncLogSession syncLogSession, bool testOnlyInsecure, Dictionary<Authority, bool> connectiveAuthority, AutoProvisionProgress progressCallback, out PimSubscriptionProxy sub);
	}
}
