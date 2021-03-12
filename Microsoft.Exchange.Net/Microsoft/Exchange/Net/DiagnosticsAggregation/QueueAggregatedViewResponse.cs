using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000839 RID: 2105
	[DataContract]
	internal class QueueAggregatedViewResponse
	{
		// Token: 0x06002CAC RID: 11436 RVA: 0x000650FA File Offset: 0x000632FA
		public QueueAggregatedViewResponse(List<AggregatedQueueInfo> aggregatedQueues)
		{
			this.AggregatedQueues = aggregatedQueues;
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x00065109 File Offset: 0x00063309
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x00065111 File Offset: 0x00063311
		[DataMember(IsRequired = true)]
		public List<AggregatedQueueInfo> AggregatedQueues { get; private set; }
	}
}
