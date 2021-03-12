using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000019 RID: 25
	internal enum AssistantsEventType
	{
		// Token: 0x040000BE RID: 190
		ServiceStarted,
		// Token: 0x040000BF RID: 191
		ServiceStopped,
		// Token: 0x040000C0 RID: 192
		SuspendActivity,
		// Token: 0x040000C1 RID: 193
		EndActivity,
		// Token: 0x040000C2 RID: 194
		StartProcessingMailbox,
		// Token: 0x040000C3 RID: 195
		EndProcessingMailbox,
		// Token: 0x040000C4 RID: 196
		FilterMailbox,
		// Token: 0x040000C5 RID: 197
		ErrorProcessingMailbox,
		// Token: 0x040000C6 RID: 198
		SucceedOpenMailboxStoreSession,
		// Token: 0x040000C7 RID: 199
		FailedOpenMailboxStoreSession,
		// Token: 0x040000C8 RID: 200
		MailboxInteresting,
		// Token: 0x040000C9 RID: 201
		MailboxNotInteresting,
		// Token: 0x040000CA RID: 202
		FolderSyncOperation,
		// Token: 0x040000CB RID: 203
		FolderSyncException,
		// Token: 0x040000CC RID: 204
		ReceivedQueriedMailboxes,
		// Token: 0x040000CD RID: 205
		EndGetMailboxes,
		// Token: 0x040000CE RID: 206
		NoMailboxes,
		// Token: 0x040000CF RID: 207
		NotStarted,
		// Token: 0x040000D0 RID: 208
		NoJobs,
		// Token: 0x040000D1 RID: 209
		BeginJob,
		// Token: 0x040000D2 RID: 210
		EndJob,
		// Token: 0x040000D3 RID: 211
		DriverNotStarted,
		// Token: 0x040000D4 RID: 212
		JobAlreadyRunning,
		// Token: 0x040000D5 RID: 213
		ErrorEnumeratingMailbox
	}
}
