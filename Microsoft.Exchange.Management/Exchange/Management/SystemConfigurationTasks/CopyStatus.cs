using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000891 RID: 2193
	public enum CopyStatus
	{
		// Token: 0x04002D9C RID: 11676
		[LocDescription(Strings.IDs.CopyStatusUnknown)]
		Unknown,
		// Token: 0x04002D9D RID: 11677
		[LocDescription(Strings.IDs.CopyStatusFailed)]
		Failed = 3,
		// Token: 0x04002D9E RID: 11678
		[LocDescription(Strings.IDs.CopyStatusSeeding)]
		Seeding,
		// Token: 0x04002D9F RID: 11679
		[LocDescription(Strings.IDs.CopyStatusSuspended)]
		Suspended,
		// Token: 0x04002DA0 RID: 11680
		[LocDescription(Strings.IDs.CopyStatusHealthy)]
		Healthy,
		// Token: 0x04002DA1 RID: 11681
		[LocDescription(Strings.IDs.CopyStatusServiceDown)]
		ServiceDown,
		// Token: 0x04002DA2 RID: 11682
		[LocDescription(Strings.IDs.CopyStatusInitializing)]
		Initializing,
		// Token: 0x04002DA3 RID: 11683
		[LocDescription(Strings.IDs.CopyStatusResynchronizing)]
		Resynchronizing,
		// Token: 0x04002DA4 RID: 11684
		[LocDescription(Strings.IDs.CopyStatusMounted)]
		Mounted = 11,
		// Token: 0x04002DA5 RID: 11685
		[LocDescription(Strings.IDs.CopyStatusDismounted)]
		Dismounted,
		// Token: 0x04002DA6 RID: 11686
		[LocDescription(Strings.IDs.CopyStatusMounting)]
		Mounting,
		// Token: 0x04002DA7 RID: 11687
		[LocDescription(Strings.IDs.CopyStatusDismounting)]
		Dismounting,
		// Token: 0x04002DA8 RID: 11688
		[LocDescription(Strings.IDs.CopyStatusDisconnectedAndHealthy)]
		DisconnectedAndHealthy,
		// Token: 0x04002DA9 RID: 11689
		[LocDescription(Strings.IDs.CopyStatusFailedAndSuspended)]
		FailedAndSuspended,
		// Token: 0x04002DAA RID: 11690
		[LocDescription(Strings.IDs.CopyStatusDisconnectedAndResynchronizing)]
		DisconnectedAndResynchronizing,
		// Token: 0x04002DAB RID: 11691
		[LocDescription(Strings.IDs.CopyStatusNonExchangeReplication)]
		NonExchangeReplication,
		// Token: 0x04002DAC RID: 11692
		[LocDescription(Strings.IDs.CopyStatusSeedingSource)]
		SeedingSource,
		// Token: 0x04002DAD RID: 11693
		[LocDescription(Strings.IDs.CopyStatusMisconfigured)]
		Misconfigured
	}
}
