using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000184 RID: 388
	public enum SplitProgressState
	{
		// Token: 0x040009CD RID: 2509
		SplitNotStarted,
		// Token: 0x040009CE RID: 2510
		SplitNeeded,
		// Token: 0x040009CF RID: 2511
		IdentifyTargetMailboxStarted,
		// Token: 0x040009D0 RID: 2512
		IdentifyTargetMailboxCompleted,
		// Token: 0x040009D1 RID: 2513
		PrepareTargetMailboxStarted,
		// Token: 0x040009D2 RID: 2514
		PrepareTargetMailboxCompleted,
		// Token: 0x040009D3 RID: 2515
		PrepareSplitPlanStarted,
		// Token: 0x040009D4 RID: 2516
		PrepareSplitPlanCompleted,
		// Token: 0x040009D5 RID: 2517
		MoveContentStarted,
		// Token: 0x040009D6 RID: 2518
		MoveContentCompleted,
		// Token: 0x040009D7 RID: 2519
		SplitCompleted
	}
}
