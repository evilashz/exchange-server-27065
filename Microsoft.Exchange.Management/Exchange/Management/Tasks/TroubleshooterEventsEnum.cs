using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000423 RID: 1059
	public enum TroubleshooterEventsEnum
	{
		// Token: 0x04001D10 RID: 7440
		TSNoProblemsDetected = 5000,
		// Token: 0x04001D11 RID: 7441
		MailboxAssistantsServiceStopped,
		// Token: 0x04001D12 RID: 7442
		MailboxAssistantsServiceStarted,
		// Token: 0x04001D13 RID: 7443
		TSMDBperformanceCounterNotLoaded = 5100,
		// Token: 0x04001D14 RID: 7444
		TSMinServerVersion,
		// Token: 0x04001D15 RID: 7445
		TSNotAMailboxServer = 5200,
		// Token: 0x04001D16 RID: 7446
		MailboxAssistantsServiceNotRunning,
		// Token: 0x04001D17 RID: 7447
		MailboxAssistantsServiceCouldNotBeStopped,
		// Token: 0x04001D18 RID: 7448
		MailboxAssistantsServiceCouldNotBeStarted,
		// Token: 0x04001D19 RID: 7449
		TSResolutionFailed,
		// Token: 0x04001D1A RID: 7450
		AIMDBLastEventPollingThreadHung,
		// Token: 0x04001D1B RID: 7451
		AIDatabaseStatusPollThreadHung,
		// Token: 0x04001D1C RID: 7452
		AIMDBWatermarkTooLow
	}
}
