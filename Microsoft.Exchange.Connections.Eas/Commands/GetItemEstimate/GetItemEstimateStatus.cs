using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate
{
	// Token: 0x0200004E RID: 78
	[Flags]
	public enum GetItemEstimateStatus
	{
		// Token: 0x04000157 RID: 343
		Success = 1,
		// Token: 0x04000158 RID: 344
		InvalidCollection = 2050,
		// Token: 0x04000159 RID: 345
		SyncNotPrimed = 1027,
		// Token: 0x0400015A RID: 346
		InvalidSyncKey = 1028
	}
}
