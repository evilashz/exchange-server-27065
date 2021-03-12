using System;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A3 RID: 163
	[Flags]
	public enum MailboxRights
	{
		// Token: 0x0400023C RID: 572
		DeleteItem = 65536,
		// Token: 0x0400023D RID: 573
		ReadPermission = 131072,
		// Token: 0x0400023E RID: 574
		ChangePermission = 262144,
		// Token: 0x0400023F RID: 575
		ChangeOwner = 524288,
		// Token: 0x04000240 RID: 576
		FullAccess = 1,
		// Token: 0x04000241 RID: 577
		ExternalAccount = 4,
		// Token: 0x04000242 RID: 578
		SendAs = 2
	}
}
