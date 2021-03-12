using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000218 RID: 536
	public enum RequestJobTimestamp
	{
		// Token: 0x04000BFA RID: 3066
		None,
		// Token: 0x04000BFB RID: 3067
		Creation,
		// Token: 0x04000BFC RID: 3068
		Start,
		// Token: 0x04000BFD RID: 3069
		InitialSeedingCompleted,
		// Token: 0x04000BFE RID: 3070
		FinalSync,
		// Token: 0x04000BFF RID: 3071
		Completion,
		// Token: 0x04000C00 RID: 3072
		Suspended,
		// Token: 0x04000C01 RID: 3073
		LastUpdate,
		// Token: 0x04000C02 RID: 3074
		DoNotPickUntil,
		// Token: 0x04000C03 RID: 3075
		Failure,
		// Token: 0x04000C04 RID: 3076
		MailboxLocked,
		// Token: 0x04000C05 RID: 3077
		FailedDataGuarantee,
		// Token: 0x04000C06 RID: 3078
		StartAfter,
		// Token: 0x04000C07 RID: 3079
		CompleteAfter,
		// Token: 0x04000C08 RID: 3080
		LastProgressCheckpoint,
		// Token: 0x04000C09 RID: 3081
		DomainControllerUpdate,
		// Token: 0x04000C0A RID: 3082
		RequestCanceled,
		// Token: 0x04000C0B RID: 3083
		LastSuccessfulSourceConnection,
		// Token: 0x04000C0C RID: 3084
		LastSuccessfulTargetConnection,
		// Token: 0x04000C0D RID: 3085
		SourceConnectionFailure,
		// Token: 0x04000C0E RID: 3086
		TargetConnectionFailure,
		// Token: 0x04000C0F RID: 3087
		IsIntegStarted,
		// Token: 0x04000C10 RID: 3088
		LastServerBusyBackoff,
		// Token: 0x04000C11 RID: 3089
		ServerBusyBackoffUntil,
		// Token: 0x04000C12 RID: 3090
		MaxTimestamp
	}
}
