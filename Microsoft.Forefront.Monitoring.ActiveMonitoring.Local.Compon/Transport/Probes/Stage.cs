using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000271 RID: 625
	public enum Stage
	{
		// Token: 0x040009D6 RID: 2518
		None,
		// Token: 0x040009D7 RID: 2519
		LoadItem,
		// Token: 0x040009D8 RID: 2520
		SendAsCheck,
		// Token: 0x040009D9 RID: 2521
		CreateMailItem,
		// Token: 0x040009DA RID: 2522
		OnDemotedEvent,
		// Token: 0x040009DB RID: 2523
		SubmitNdrForInvalidRecipients,
		// Token: 0x040009DC RID: 2524
		CommitMailItem,
		// Token: 0x040009DD RID: 2525
		StartedSMTPOutOperation,
		// Token: 0x040009DE RID: 2526
		FinishedSMTPOutOperation,
		// Token: 0x040009DF RID: 2527
		SMTPOutThrewException,
		// Token: 0x040009E0 RID: 2528
		SubmitMailItem,
		// Token: 0x040009E1 RID: 2529
		DoneWithMessage,
		// Token: 0x040009E2 RID: 2530
		EventHandled
	}
}
