using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200002E RID: 46
	internal class ClusterNetworkDeletedException : ClusterException
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000853B File Offset: 0x0000673B
		private ClusterNetworkDeletedException() : base("Internal exception due to network deletion")
		{
		}
	}
}
