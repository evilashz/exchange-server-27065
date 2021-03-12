using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009BB RID: 2491
	public enum AsyncOperationStatus
	{
		// Token: 0x040032AF RID: 12975
		[SeverityLevel(SeverityLevel.Information)]
		[LocDescription(ServerStrings.IDs.RequestStateQueued)]
		Queued,
		// Token: 0x040032B0 RID: 12976
		[LocDescription(ServerStrings.IDs.RequestStateInProgress)]
		[SeverityLevel(SeverityLevel.Information)]
		InProgress,
		// Token: 0x040032B1 RID: 12977
		[LocDescription(ServerStrings.IDs.RequestStateSuspended)]
		[SeverityLevel(SeverityLevel.Information)]
		Suspended,
		// Token: 0x040032B2 RID: 12978
		[LocDescription(ServerStrings.IDs.RequestStateCompleted)]
		[SeverityLevel(SeverityLevel.Information)]
		Completed,
		// Token: 0x040032B3 RID: 12979
		[LocDescription(ServerStrings.IDs.RequestStateFailed)]
		[SeverityLevel(SeverityLevel.Error)]
		Failed,
		// Token: 0x040032B4 RID: 12980
		[LocDescription(ServerStrings.IDs.RequestStateCertExpiring)]
		[SeverityLevel(SeverityLevel.Warning)]
		CertExpiring,
		// Token: 0x040032B5 RID: 12981
		[SeverityLevel(SeverityLevel.Error)]
		[LocDescription(ServerStrings.IDs.RequestStateCertExpired)]
		CertExpired,
		// Token: 0x040032B6 RID: 12982
		[LocDescription(ServerStrings.IDs.RequestStateWaitingForFinalization)]
		[SeverityLevel(SeverityLevel.Information)]
		WaitingForFinalization,
		// Token: 0x040032B7 RID: 12983
		[LocDescription(ServerStrings.IDs.RequestStateCreated)]
		[SeverityLevel(SeverityLevel.Information)]
		Created,
		// Token: 0x040032B8 RID: 12984
		[LocDescription(ServerStrings.IDs.RequestStateCompleting)]
		[SeverityLevel(SeverityLevel.Information)]
		Completing,
		// Token: 0x040032B9 RID: 12985
		[LocDescription(ServerStrings.IDs.RequestStateRemoving)]
		[SeverityLevel(SeverityLevel.Information)]
		Removing
	}
}
