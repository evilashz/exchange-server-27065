using System;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000076 RID: 118
	internal enum DirectoryObjectType
	{
		// Token: 0x04000156 RID: 342
		Unknown,
		// Token: 0x04000157 RID: 343
		Forest,
		// Token: 0x04000158 RID: 344
		DatabaseAvailabilityGroup,
		// Token: 0x04000159 RID: 345
		Server,
		// Token: 0x0400015A RID: 346
		Database,
		// Token: 0x0400015B RID: 347
		Mailbox,
		// Token: 0x0400015C RID: 348
		CloudArchive,
		// Token: 0x0400015D RID: 349
		NonConnectedMailbox,
		// Token: 0x0400015E RID: 350
		ConstraintSet,
		// Token: 0x0400015F RID: 351
		ConsumerMailbox
	}
}
