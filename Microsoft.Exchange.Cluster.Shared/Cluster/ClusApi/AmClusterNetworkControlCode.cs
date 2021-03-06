using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200005A RID: 90
	internal enum AmClusterNetworkControlCode : uint
	{
		// Token: 0x0400012D RID: 301
		CLUSCTL_NETWORK_GET_RO_COMMON_PROPERTIES = 83886165U,
		// Token: 0x0400012E RID: 302
		CLUSCTL_NETWORK_GET_COMMON_PROPERTIES = 83886169U,
		// Token: 0x0400012F RID: 303
		CLUSCTL_NETWORK_SET_COMMON_PROPERTIES = 88080478U,
		// Token: 0x04000130 RID: 304
		CLUSCTL_NETWORK_GET_PRIVATE_PROPERTIES = 83886209U,
		// Token: 0x04000131 RID: 305
		CLUSCTL_NETWORK_SET_PRIVATE_PROPERTIES = 88080518U
	}
}
