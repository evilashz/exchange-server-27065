using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000DF RID: 223
	internal interface IPerfmonCounters
	{
		// Token: 0x060008E5 RID: 2277
		void Reset();

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008E6 RID: 2278
		// (set) Token: 0x060008E7 RID: 2279
		long CopyNotificationGenerationNumber { get; set; }

		// Token: 0x170001C3 RID: 451
		// (set) Token: 0x060008E8 RID: 2280
		long CopyGenerationNumber { set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008E9 RID: 2281
		float CopyNotificationGenerationsPerSecond { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008EA RID: 2282
		float InspectorGenerationsPerSecond { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008EB RID: 2283
		// (set) Token: 0x060008EC RID: 2284
		long InspectorGenerationNumber { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008ED RID: 2285
		long CopyQueueLength { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008EE RID: 2286
		// (set) Token: 0x060008EF RID: 2287
		long CopyQueueNotKeepingUp { get; set; }

		// Token: 0x170001C9 RID: 457
		// (set) Token: 0x060008F0 RID: 2288
		long ReplayNotificationGenerationNumber { set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008F1 RID: 2289
		// (set) Token: 0x060008F2 RID: 2290
		long ReplayGenerationNumber { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008F3 RID: 2291
		float ReplayGenerationsPerSecond { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008F4 RID: 2292
		long ReplayQueueLength { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008F5 RID: 2293
		// (set) Token: 0x060008F6 RID: 2294
		long ReplayQueueNotKeepingUp { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008F7 RID: 2295
		// (set) Token: 0x060008F8 RID: 2296
		long Failed { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008F9 RID: 2297
		// (set) Token: 0x060008FA RID: 2298
		long Initializing { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008FB RID: 2299
		// (set) Token: 0x060008FC RID: 2300
		long FailedSuspended { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008FD RID: 2301
		// (set) Token: 0x060008FE RID: 2302
		long Resynchronizing { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008FF RID: 2303
		// (set) Token: 0x06000900 RID: 2304
		long ActivationSuspended { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000901 RID: 2305
		// (set) Token: 0x06000902 RID: 2306
		long ReplayLagDisabled { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000903 RID: 2307
		// (set) Token: 0x06000904 RID: 2308
		long ReplayLagPercentage { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000905 RID: 2309
		// (set) Token: 0x06000906 RID: 2310
		long Disconnected { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000907 RID: 2311
		// (set) Token: 0x06000908 RID: 2312
		long PassiveSeedingSource { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000909 RID: 2313
		// (set) Token: 0x0600090A RID: 2314
		long Seeding { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600090B RID: 2315
		// (set) Token: 0x0600090C RID: 2316
		long SinglePageRestore { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600090D RID: 2317
		// (set) Token: 0x0600090E RID: 2318
		long Suspended { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600090F RID: 2319
		// (set) Token: 0x06000910 RID: 2320
		long SuspendedAndNotSeeding { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000911 RID: 2321
		// (set) Token: 0x06000912 RID: 2322
		long CompressionEnabled { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000913 RID: 2323
		// (set) Token: 0x06000914 RID: 2324
		long EncryptionEnabled { get; set; }

		// Token: 0x170001DD RID: 477
		// (set) Token: 0x06000915 RID: 2325
		long TruncatedGenerationNumber { set; }

		// Token: 0x170001DE RID: 478
		// (set) Token: 0x06000916 RID: 2326
		long IncReseedDBPagesReadNumber { set; }

		// Token: 0x170001DF RID: 479
		// (set) Token: 0x06000917 RID: 2327
		long IncReseedLogCopiedNumber { set; }

		// Token: 0x06000918 RID: 2328
		void RecordOneGetCopyStatusCall();

		// Token: 0x06000919 RID: 2329
		void RecordLogCopyThruput(long bytesCopied);

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600091A RID: 2330
		long RawCopyQueueLength { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600091B RID: 2331
		// (set) Token: 0x0600091C RID: 2332
		long GranularReplication { get; set; }

		// Token: 0x0600091D RID: 2333
		void RecordGranularBytesReceived(long bytesCopied, bool logIsComplete);

		// Token: 0x0600091E RID: 2334
		void RecordLogCopierNetworkReadLatency(long tics);

		// Token: 0x0600091F RID: 2335
		void RecordBlockModeConsumerWriteLatency(long tics);

		// Token: 0x06000920 RID: 2336
		void RecordFileModeWriteLatency(long tics);
	}
}
