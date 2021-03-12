using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILoadBalance
	{
		// Token: 0x06000017 RID: 23
		IEnumerable<BandMailboxRebalanceData> BalanceForest(LoadContainer forest);
	}
}
