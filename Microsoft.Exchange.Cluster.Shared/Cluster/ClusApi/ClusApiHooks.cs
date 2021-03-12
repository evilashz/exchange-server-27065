using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000015 RID: 21
	internal enum ClusApiHooks
	{
		// Token: 0x04000024 RID: 36
		OpenCluster = 1,
		// Token: 0x04000025 RID: 37
		CloseCluster,
		// Token: 0x04000026 RID: 38
		GetClusterKey,
		// Token: 0x04000027 RID: 39
		ClusterRegBatchAddCommand,
		// Token: 0x04000028 RID: 40
		ClusterRegCreateBatch,
		// Token: 0x04000029 RID: 41
		ClusterRegCloseBatch,
		// Token: 0x0400002A RID: 42
		ClusterRegOpenKey,
		// Token: 0x0400002B RID: 43
		ClusterRegCreateKey,
		// Token: 0x0400002C RID: 44
		ClusterRegQueryValue,
		// Token: 0x0400002D RID: 45
		ClusterRegSetValue,
		// Token: 0x0400002E RID: 46
		ClusterRegDeleteValue,
		// Token: 0x0400002F RID: 47
		ClusterRegDeleteKey,
		// Token: 0x04000030 RID: 48
		ClusterRegCloseKey
	}
}
