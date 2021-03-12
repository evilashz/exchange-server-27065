using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000210 RID: 528
	public enum RequestWorkloadType
	{
		// Token: 0x04000B1A RID: 2842
		[LocDescription(MrsStrings.IDs.WorkloadTypeNone)]
		None,
		// Token: 0x04000B1B RID: 2843
		[LocDescription(MrsStrings.IDs.WorkloadTypeLocal)]
		Local,
		// Token: 0x04000B1C RID: 2844
		[LocDescription(MrsStrings.IDs.WorkloadTypeOnboarding)]
		Onboarding,
		// Token: 0x04000B1D RID: 2845
		[LocDescription(MrsStrings.IDs.WorkloadTypeOffboarding)]
		Offboarding,
		// Token: 0x04000B1E RID: 2846
		[LocDescription(MrsStrings.IDs.WorkloadTypeTenantUpgrade)]
		TenantUpgrade,
		// Token: 0x04000B1F RID: 2847
		[LocDescription(MrsStrings.IDs.WorkloadTypeLoadBalancing)]
		LoadBalancing,
		// Token: 0x04000B20 RID: 2848
		[LocDescription(MrsStrings.IDs.WorkloadTypeEmergency)]
		Emergency,
		// Token: 0x04000B21 RID: 2849
		[LocDescription(MrsStrings.IDs.WorkloadTypeRemotePstIngestion)]
		RemotePstIngestion,
		// Token: 0x04000B22 RID: 2850
		[LocDescription(MrsStrings.IDs.WorkloadTypeSyncAggregation)]
		SyncAggregation,
		// Token: 0x04000B23 RID: 2851
		[LocDescription(MrsStrings.IDs.WorkloadTypeRemotePstExport)]
		RemotePstExport
	}
}
