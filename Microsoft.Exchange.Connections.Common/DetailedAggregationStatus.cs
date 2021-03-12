using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200000B RID: 11
	public enum DetailedAggregationStatus
	{
		// Token: 0x04000018 RID: 24
		None,
		// Token: 0x04000019 RID: 25
		AuthenticationError,
		// Token: 0x0400001A RID: 26
		ConnectionError,
		// Token: 0x0400001B RID: 27
		CommunicationError,
		// Token: 0x0400001C RID: 28
		RemoteMailboxQuotaWarning,
		// Token: 0x0400001D RID: 29
		LabsMailboxQuotaWarning,
		// Token: 0x0400001E RID: 30
		MaxedOutSyncRelationshipsError,
		// Token: 0x0400001F RID: 31
		Corrupted,
		// Token: 0x04000020 RID: 32
		LeaveOnServerNotSupported,
		// Token: 0x04000021 RID: 33
		RemoteAccountDoesNotExist,
		// Token: 0x04000022 RID: 34
		RemoteServerIsSlow,
		// Token: 0x04000023 RID: 35
		TooManyFolders,
		// Token: 0x04000024 RID: 36
		Finalized,
		// Token: 0x04000025 RID: 37
		RemoteServerIsBackedOff,
		// Token: 0x04000026 RID: 38
		RemoteServerIsPoisonous,
		// Token: 0x04000027 RID: 39
		SyncStateSizeError,
		// Token: 0x04000028 RID: 40
		ConfigurationError,
		// Token: 0x04000029 RID: 41
		RemoveSubscription,
		// Token: 0x0400002A RID: 42
		ProviderException
	}
}
