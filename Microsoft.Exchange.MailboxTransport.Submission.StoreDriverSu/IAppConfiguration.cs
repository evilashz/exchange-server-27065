using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200003F RID: 63
	internal interface IAppConfiguration
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000247 RID: 583
		bool IsWriteToPickupFolderEnabled { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000248 RID: 584
		TimeSpan HangDetectionInterval { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000249 RID: 585
		TimeSpan SmtpOutWaitTimeOut { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600024A RID: 586
		int MaxConcurrentSubmissions { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600024B RID: 587
		bool ShouldLogTemporaryFailures { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600024C RID: 588
		bool ShouldLogNotifyEvents { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600024D RID: 589
		bool UseLocalHubOnly { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600024E RID: 590
		bool EnableCalendarHeaderCreation { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600024F RID: 591
		bool EnableMailboxQuarantine { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000250 RID: 592
		int MailboxQuarantineCrashCountThreshold { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000251 RID: 593
		TimeSpan MailboxQuarantineCrashCountWindow { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000252 RID: 594
		TimeSpan MailboxQuarantineSpan { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000253 RID: 595
		TimeSpan PoisonRegistryEntryExpiryWindow { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000254 RID: 596
		string PoisonRegistryEntryLocation { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000255 RID: 597
		int PoisonRegistryEntryMaxCount { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000256 RID: 598
		bool SenderRateDeprioritizationEnabled { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000257 RID: 599
		bool EnableSendNdrForPoisonMessage { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000258 RID: 600
		int SenderRateDeprioritizationThreshold { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000259 RID: 601
		bool SenderRateThrottlingEnabled { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600025A RID: 602
		int SenderRateThrottlingThreshold { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600025B RID: 603
		TimeSpan SenderRateThrottlingRetryInterval { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600025C RID: 604
		bool EnableSeriesMessageProcessing { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600025D RID: 605
		TimeSpan ServiceHeartbeatPeriodicLoggingInterval { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600025E RID: 606
		bool EnableUnparkedMessageRestoring { get; }

		// Token: 0x0600025F RID: 607
		void AddDiagnosticInfo(XElement parent);
	}
}
