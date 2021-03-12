using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000088 RID: 136
	[Flags]
	internal enum SaveStateFlags
	{
		// Token: 0x040002D1 RID: 721
		Regular = 0,
		// Token: 0x040002D2 RID: 722
		Lazy = 1,
		// Token: 0x040002D3 RID: 723
		DontSaveRequestJob = 2,
		// Token: 0x040002D4 RID: 724
		DontReportSyncStage = 4,
		// Token: 0x040002D5 RID: 725
		RelinquishLongRunningJob = 8
	}
}
