using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200041A RID: 1050
	public enum ElcFolderType
	{
		// Token: 0x04001FE5 RID: 8165
		[LocDescription(DirectoryStrings.IDs.Calendar)]
		Calendar = 1,
		// Token: 0x04001FE6 RID: 8166
		[LocDescription(DirectoryStrings.IDs.Contacts)]
		Contacts,
		// Token: 0x04001FE7 RID: 8167
		[LocDescription(DirectoryStrings.IDs.DeletedItems)]
		DeletedItems,
		// Token: 0x04001FE8 RID: 8168
		[LocDescription(DirectoryStrings.IDs.Drafts)]
		Drafts,
		// Token: 0x04001FE9 RID: 8169
		[LocDescription(DirectoryStrings.IDs.Inbox)]
		Inbox,
		// Token: 0x04001FEA RID: 8170
		[LocDescription(DirectoryStrings.IDs.JunkEmail)]
		JunkEmail,
		// Token: 0x04001FEB RID: 8171
		[LocDescription(DirectoryStrings.IDs.Journal)]
		Journal,
		// Token: 0x04001FEC RID: 8172
		[LocDescription(DirectoryStrings.IDs.Notes)]
		Notes,
		// Token: 0x04001FED RID: 8173
		[LocDescription(DirectoryStrings.IDs.Outbox)]
		Outbox,
		// Token: 0x04001FEE RID: 8174
		[LocDescription(DirectoryStrings.IDs.SentItems)]
		SentItems,
		// Token: 0x04001FEF RID: 8175
		[LocDescription(DirectoryStrings.IDs.Tasks)]
		Tasks,
		// Token: 0x04001FF0 RID: 8176
		[LocDescription(DirectoryStrings.IDs.All)]
		All,
		// Token: 0x04001FF1 RID: 8177
		[LocDescription(DirectoryStrings.IDs.Organizational)]
		ManagedCustomFolder,
		// Token: 0x04001FF2 RID: 8178
		[LocDescription(DirectoryStrings.IDs.RssSubscriptions)]
		RssSubscriptions,
		// Token: 0x04001FF3 RID: 8179
		[LocDescription(DirectoryStrings.IDs.SyncIssues)]
		SyncIssues,
		// Token: 0x04001FF4 RID: 8180
		[LocDescription(DirectoryStrings.IDs.ConversationHistory)]
		ConversationHistory,
		// Token: 0x04001FF5 RID: 8181
		[LocDescription(DirectoryStrings.IDs.PersonalFolder)]
		Personal,
		// Token: 0x04001FF6 RID: 8182
		[LocDescription(DirectoryStrings.IDs.DumpsterFolder)]
		RecoverableItems,
		// Token: 0x04001FF7 RID: 8183
		[LocDescription(DirectoryStrings.IDs.NonIpmRoot)]
		NonIpmRoot,
		// Token: 0x04001FF8 RID: 8184
		[LocDescription(DirectoryStrings.IDs.LegacyArchiveJournals)]
		LegacyArchiveJournals
	}
}
