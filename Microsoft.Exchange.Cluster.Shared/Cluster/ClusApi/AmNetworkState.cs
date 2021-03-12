using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200004A RID: 74
	public enum AmNetworkState
	{
		// Token: 0x040000D2 RID: 210
		StateUnknown = -1,
		// Token: 0x040000D3 RID: 211
		Unavailable,
		// Token: 0x040000D4 RID: 212
		Down,
		// Token: 0x040000D5 RID: 213
		Partitioned,
		// Token: 0x040000D6 RID: 214
		Up
	}
}
