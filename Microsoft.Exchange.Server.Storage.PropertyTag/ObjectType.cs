using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000002 RID: 2
	public enum ObjectType : byte
	{
		// Token: 0x04000002 RID: 2
		Invalid,
		// Token: 0x04000003 RID: 3
		Mailbox,
		// Token: 0x04000004 RID: 4
		Folder,
		// Token: 0x04000005 RID: 5
		Message,
		// Token: 0x04000006 RID: 6
		Attachment,
		// Token: 0x04000007 RID: 7
		EmbeddedMessage,
		// Token: 0x04000008 RID: 8
		Recipient,
		// Token: 0x04000009 RID: 9
		Conversation,
		// Token: 0x0400000A RID: 10
		FolderView,
		// Token: 0x0400000B RID: 11
		AttachmentView,
		// Token: 0x0400000C RID: 12
		PermissionView,
		// Token: 0x0400000D RID: 13
		Event,
		// Token: 0x0400000E RID: 14
		LocalDirectory,
		// Token: 0x0400000F RID: 15
		InferenceLog,
		// Token: 0x04000010 RID: 16
		ViewDefinition,
		// Token: 0x04000011 RID: 17
		IcsState,
		// Token: 0x04000012 RID: 18
		ResourceDigest,
		// Token: 0x04000013 RID: 19
		ProcessInfo,
		// Token: 0x04000014 RID: 20
		FastTransferStream,
		// Token: 0x04000015 RID: 21
		IsIntegJob,
		// Token: 0x04000016 RID: 22
		UserInfo,
		// Token: 0x04000017 RID: 23
		RestrictionView,
		// Token: 0x04000018 RID: 24
		Count
	}
}
