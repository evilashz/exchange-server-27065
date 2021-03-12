using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200020A RID: 522
	[Flags]
	internal enum ReadJobFlags
	{
		// Token: 0x04000AFE RID: 2814
		None = 0,
		// Token: 0x04000AFF RID: 2815
		SkipValidation = 1,
		// Token: 0x04000B00 RID: 2816
		SkipReadingMailboxRequestIndexEntries = 2
	}
}
