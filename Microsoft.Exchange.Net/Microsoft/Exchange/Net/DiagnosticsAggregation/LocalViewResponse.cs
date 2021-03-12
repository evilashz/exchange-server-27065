using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000832 RID: 2098
	[DataContract]
	internal class LocalViewResponse
	{
		// Token: 0x06002C80 RID: 11392 RVA: 0x00064E6C File Offset: 0x0006306C
		public LocalViewResponse(ServerSnapshotStatus serverSnapshotStatus)
		{
			this.ServerSnapshotStatus = serverSnapshotStatus;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x00064E7B File Offset: 0x0006307B
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x00064E83 File Offset: 0x00063083
		[DataMember(IsRequired = true)]
		public ServerSnapshotStatus ServerSnapshotStatus { get; private set; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x00064E8C File Offset: 0x0006308C
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x00064E94 File Offset: 0x00063094
		[DataMember]
		public QueueLocalViewResponse QueueLocalViewResponse { get; set; }
	}
}
