using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000019 RID: 25
	[DataContract]
	public enum SyncStage
	{
		// Token: 0x040000D8 RID: 216
		[EnumMember]
		Error = -1,
		// Token: 0x040000D9 RID: 217
		[EnumMember]
		None,
		// Token: 0x040000DA RID: 218
		[EnumMember]
		CreatingMailbox,
		// Token: 0x040000DB RID: 219
		[EnumMember]
		MailboxCreated,
		// Token: 0x040000DC RID: 220
		[EnumMember]
		CreatingFolderHierarchy,
		// Token: 0x040000DD RID: 221
		[EnumMember]
		CreatingInitialSyncCheckpoint,
		// Token: 0x040000DE RID: 222
		[EnumMember]
		LoadingMessages,
		// Token: 0x040000DF RID: 223
		[EnumMember]
		CopyingMessages,
		// Token: 0x040000E0 RID: 224
		[EnumMember]
		IncrementalSync = 10,
		// Token: 0x040000E1 RID: 225
		[EnumMember]
		FinalIncrementalSync,
		// Token: 0x040000E2 RID: 226
		[EnumMember]
		Cleanup,
		// Token: 0x040000E3 RID: 227
		[EnumMember]
		SyncFinished = 100,
		// Token: 0x040000E4 RID: 228
		CleanupResetTargetMailbox = 1001,
		// Token: 0x040000E5 RID: 229
		CleanupSeedTargetMBICache,
		// Token: 0x040000E6 RID: 230
		CleanupDeleteSourceMailbox,
		// Token: 0x040000E7 RID: 231
		CleanupUnableToRehomeRelatedRequests,
		// Token: 0x040000E8 RID: 232
		CleanupUpdateMovedMailboxWarning,
		// Token: 0x040000E9 RID: 233
		CleanupUnableToComputeTargetAddress,
		// Token: 0x040000EA RID: 234
		CleanupUnableToUpdateSourceMailbox,
		// Token: 0x040000EB RID: 235
		BadItem,
		// Token: 0x040000EC RID: 236
		CleanupUnableToGuaranteeUnlock,
		// Token: 0x040000ED RID: 237
		CleanupUnableToLoadTargetMailbox
	}
}
