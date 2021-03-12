using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadMetricRepository
	{
		// Token: 0x040000D1 RID: 209
		public static readonly IEnumerable<LoadMetric> DefaultMetrics = new LoadMetric[]
		{
			ConsumedCpu.Instance,
			ItemCount.Instance,
			LogicalSize.Instance,
			PhysicalSize.Instance,
			ConsumerMailboxCount.Instance,
			ConsumerMailboxSize.Instance,
			InProgressLoadBalancingMoveCount.Instance
		};
	}
}
