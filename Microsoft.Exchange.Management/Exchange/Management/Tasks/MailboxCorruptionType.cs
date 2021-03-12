using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D97 RID: 3479
	public enum MailboxCorruptionType
	{
		// Token: 0x04004086 RID: 16518
		None,
		// Token: 0x04004087 RID: 16519
		SearchFolder,
		// Token: 0x04004088 RID: 16520
		FolderView,
		// Token: 0x04004089 RID: 16521
		AggregateCounts,
		// Token: 0x0400408A RID: 16522
		ProvisionedFolder,
		// Token: 0x0400408B RID: 16523
		ReplState,
		// Token: 0x0400408C RID: 16524
		MessagePtagCn,
		// Token: 0x0400408D RID: 16525
		MessageId,
		// Token: 0x0400408E RID: 16526
		RuleMessageClass = 100,
		// Token: 0x0400408F RID: 16527
		RestrictionFolder,
		// Token: 0x04004090 RID: 16528
		FolderACL,
		// Token: 0x04004091 RID: 16529
		UniqueMidIndex,
		// Token: 0x04004092 RID: 16530
		CorruptJunkRule,
		// Token: 0x04004093 RID: 16531
		MissingSpecialFolders,
		// Token: 0x04004094 RID: 16532
		DropAllLazyIndexes,
		// Token: 0x04004095 RID: 16533
		ImapId,
		// Token: 0x04004096 RID: 16534
		LockedMoveTarget = 4096,
		// Token: 0x04004097 RID: 16535
		ScheduledCheck = 8192,
		// Token: 0x04004098 RID: 16536
		Extension1 = 32768,
		// Token: 0x04004099 RID: 16537
		Extension2,
		// Token: 0x0400409A RID: 16538
		Extension3,
		// Token: 0x0400409B RID: 16539
		Extension4,
		// Token: 0x0400409C RID: 16540
		Extension5
	}
}
