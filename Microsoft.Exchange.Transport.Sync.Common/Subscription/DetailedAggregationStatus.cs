using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B5 RID: 181
	public enum DetailedAggregationStatus
	{
		// Token: 0x040002D9 RID: 729
		None,
		// Token: 0x040002DA RID: 730
		AuthenticationError,
		// Token: 0x040002DB RID: 731
		ConnectionError,
		// Token: 0x040002DC RID: 732
		CommunicationError,
		// Token: 0x040002DD RID: 733
		RemoteMailboxQuotaWarning,
		// Token: 0x040002DE RID: 734
		LabsMailboxQuotaWarning,
		// Token: 0x040002DF RID: 735
		MaxedOutSyncRelationshipsError,
		// Token: 0x040002E0 RID: 736
		Corrupted,
		// Token: 0x040002E1 RID: 737
		LeaveOnServerNotSupported,
		// Token: 0x040002E2 RID: 738
		RemoteAccountDoesNotExist,
		// Token: 0x040002E3 RID: 739
		RemoteServerIsSlow,
		// Token: 0x040002E4 RID: 740
		TooManyFolders,
		// Token: 0x040002E5 RID: 741
		Finalized,
		// Token: 0x040002E6 RID: 742
		RemoteServerIsBackedOff,
		// Token: 0x040002E7 RID: 743
		RemoteServerIsPoisonous,
		// Token: 0x040002E8 RID: 744
		SyncStateSizeError,
		// Token: 0x040002E9 RID: 745
		ConfigurationError,
		// Token: 0x040002EA RID: 746
		RemoveSubscription,
		// Token: 0x040002EB RID: 747
		ProviderException
	}
}
