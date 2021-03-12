using System;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000E RID: 14
	internal enum RpcRequestType
	{
		// Token: 0x04000030 RID: 48
		NotifyTaskStoreChange,
		// Token: 0x04000031 RID: 49
		EnsureTenantMonitoring,
		// Token: 0x04000032 RID: 50
		GetDarTask,
		// Token: 0x04000033 RID: 51
		SetDarTask,
		// Token: 0x04000034 RID: 52
		GetDarTaskAggregate,
		// Token: 0x04000035 RID: 53
		SetDarTaskAggregate,
		// Token: 0x04000036 RID: 54
		RemoveCompletedDarTasks,
		// Token: 0x04000037 RID: 55
		RemoveDarTaskAggregate,
		// Token: 0x04000038 RID: 56
		GetDarInfo
	}
}
