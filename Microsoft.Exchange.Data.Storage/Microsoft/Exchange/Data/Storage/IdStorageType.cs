using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D87 RID: 3463
	internal enum IdStorageType : byte
	{
		// Token: 0x04005296 RID: 21142
		MailboxItemSmtpAddressBased,
		// Token: 0x04005297 RID: 21143
		PublicFolder,
		// Token: 0x04005298 RID: 21144
		PublicFolderItem,
		// Token: 0x04005299 RID: 21145
		MailboxItemMailboxGuidBased,
		// Token: 0x0400529A RID: 21146
		ConversationIdMailboxGuidBased,
		// Token: 0x0400529B RID: 21147
		ActiveDirectoryObject
	}
}
