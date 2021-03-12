using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002A RID: 42
	[Flags]
	internal enum ExportFoldersDataToCopyFlags
	{
		// Token: 0x0400016A RID: 362
		None = 0,
		// Token: 0x0400016B RID: 363
		OutputCreateMessages = 1,
		// Token: 0x0400016C RID: 364
		IncludeCopyToStream = 2
	}
}
