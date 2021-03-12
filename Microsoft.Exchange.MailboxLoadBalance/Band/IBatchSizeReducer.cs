using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001B RID: 27
	internal interface IBatchSizeReducer
	{
		// Token: 0x060000F9 RID: 249
		IEnumerable<BandMailboxRebalanceData> ReduceBatchSize(IEnumerable<BandMailboxRebalanceData> results);
	}
}
