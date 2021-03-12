using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E8 RID: 232
	[Flags]
	public enum MailboxAuditOperations
	{
		// Token: 0x0400047C RID: 1148
		None = 0,
		// Token: 0x0400047D RID: 1149
		Update = 1,
		// Token: 0x0400047E RID: 1150
		Copy = 2,
		// Token: 0x0400047F RID: 1151
		Move = 4,
		// Token: 0x04000480 RID: 1152
		MoveToDeletedItems = 8,
		// Token: 0x04000481 RID: 1153
		SoftDelete = 16,
		// Token: 0x04000482 RID: 1154
		HardDelete = 32,
		// Token: 0x04000483 RID: 1155
		FolderBind = 64,
		// Token: 0x04000484 RID: 1156
		SendAs = 128,
		// Token: 0x04000485 RID: 1157
		SendOnBehalf = 256,
		// Token: 0x04000486 RID: 1158
		MessageBind = 512,
		// Token: 0x04000487 RID: 1159
		Create = 1024,
		// Token: 0x04000488 RID: 1160
		MailboxLogin = 2048
	}
}
