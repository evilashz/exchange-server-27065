using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000F0 RID: 240
	internal interface IMessagingDatabaseConfig
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000942 RID: 2370
		string DatabasePath { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000943 RID: 2371
		string LogFilePath { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000944 RID: 2372
		uint LogFileSize { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000945 RID: 2373
		uint LogBufferSize { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000946 RID: 2374
		uint ExtensionSize { get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000947 RID: 2375
		uint MaxBackgroundCleanupTasks { get; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000948 RID: 2376
		int MaxConnections { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000949 RID: 2377
		DatabaseRecoveryAction DatabaseRecoveryAction { get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600094A RID: 2378
		TimeSpan MessagingGenerationCleanupAge { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600094B RID: 2379
		TimeSpan MessagingGenerationExpirationAge { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600094C RID: 2380
		TimeSpan MessagingGenerationLength { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600094D RID: 2381
		TimeSpan DefaultAsyncCommitTimeout { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600094E RID: 2382
		byte MaxMessageLoadTimePercentage { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600094F RID: 2383
		int RecentGenerationDepth { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000950 RID: 2384
		TimeSpan StatisticsUpdateInterval { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000951 RID: 2385
		bool CloneInOriginalGeneration { get; }
	}
}
