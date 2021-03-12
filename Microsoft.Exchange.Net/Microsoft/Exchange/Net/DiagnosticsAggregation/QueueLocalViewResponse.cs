using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000837 RID: 2103
	[DataContract]
	internal class QueueLocalViewResponse
	{
		// Token: 0x06002CA0 RID: 11424 RVA: 0x0006505E File Offset: 0x0006325E
		public QueueLocalViewResponse(List<LocalQueueInfo> localQueues, DateTime timestamp)
		{
			this.LocalQueues = localQueues;
			this.Timestamp = timestamp;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x00065074 File Offset: 0x00063274
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x0006507C File Offset: 0x0006327C
		[DataMember(IsRequired = true)]
		public DateTime Timestamp { get; private set; }

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x00065085 File Offset: 0x00063285
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x0006508D File Offset: 0x0006328D
		[DataMember(IsRequired = true)]
		public List<LocalQueueInfo> LocalQueues { get; private set; }
	}
}
