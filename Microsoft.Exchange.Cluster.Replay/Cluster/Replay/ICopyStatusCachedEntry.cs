using System;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E0 RID: 480
	internal interface ICopyStatusCachedEntry
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600131C RID: 4892
		RpcDatabaseCopyStatus2 CopyStatus { get; }
	}
}
