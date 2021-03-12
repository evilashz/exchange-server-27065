using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000834 RID: 2100
	[DataContract]
	internal class AggregatedViewResponse
	{
		// Token: 0x06002C90 RID: 11408 RVA: 0x00064F2F File Offset: 0x0006312F
		public AggregatedViewResponse(List<ServerSnapshotStatus> snapshotStatusOfServers)
		{
			this.SnapshotStatusOfServers = snapshotStatusOfServers;
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x00064F3E File Offset: 0x0006313E
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x00064F46 File Offset: 0x00063146
		[DataMember(IsRequired = true)]
		public List<ServerSnapshotStatus> SnapshotStatusOfServers { get; private set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x00064F4F File Offset: 0x0006314F
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x00064F57 File Offset: 0x00063157
		[DataMember]
		public QueueAggregatedViewResponse QueueAggregatedViewResponse { get; set; }
	}
}
