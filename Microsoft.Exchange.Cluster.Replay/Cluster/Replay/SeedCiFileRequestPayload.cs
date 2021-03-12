using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000299 RID: 665
	[DataContract]
	internal sealed class SeedCiFileRequestPayload
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x0006C6E3 File Offset: 0x0006A8E3
		internal SeedCiFileRequestPayload(string endpoint, string reason)
		{
			this.Endpoint = endpoint;
			this.Reason = reason;
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0006C6F9 File Offset: 0x0006A8F9
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x0006C701 File Offset: 0x0006A901
		[DataMember]
		internal string Endpoint { get; private set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0006C70A File Offset: 0x0006A90A
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0006C712 File Offset: 0x0006A912
		[DataMember]
		internal string Reason { get; private set; }
	}
}
