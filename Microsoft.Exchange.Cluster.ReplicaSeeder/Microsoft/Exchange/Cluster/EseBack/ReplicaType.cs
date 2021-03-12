using System;

namespace Microsoft.Exchange.Cluster.EseBack
{
	// Token: 0x02000103 RID: 259
	public enum ReplicaType : uint
	{
		// Token: 0x0400023D RID: 573
		Unknown,
		// Token: 0x0400023E RID: 574
		StandbyReplica,
		// Token: 0x0400023F RID: 575
		ClusterReplica,
		// Token: 0x04000240 RID: 576
		LocalReplica = 4U
	}
}
