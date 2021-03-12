using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000029 RID: 41
	[Flags]
	internal enum GetFolderRecFlags
	{
		// Token: 0x04000164 RID: 356
		None = 0,
		// Token: 0x04000165 RID: 357
		PromotedProperties = 1,
		// Token: 0x04000166 RID: 358
		Views = 2,
		// Token: 0x04000167 RID: 359
		Restrictions = 4,
		// Token: 0x04000168 RID: 360
		NoProperties = 8
	}
}
