using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000075 RID: 117
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IMailboxAssistantServiceSettings : ISettings
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060002A7 RID: 679
		TimeSpan EventPollingInterval { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060002A8 RID: 680
		TimeSpan ActiveWatermarksSaveInterval { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060002A9 RID: 681
		TimeSpan IdleWatermarksSaveInterval { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060002AA RID: 682
		TimeSpan WatermarkCleanupInterval { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060002AB RID: 683
		int MaxThreadsForAllTimeBasedAssistants { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060002AC RID: 684
		int MaxThreadsPerTimeBasedAssistantType { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060002AD RID: 685
		TimeSpan HangDetectionTimeout { get; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060002AE RID: 686
		TimeSpan HangDetectionPeriod { get; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060002AF RID: 687
		int MaximumEventQueueSize { get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060002B0 RID: 688
		bool MemoryMonitorEnabled { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060002B1 RID: 689
		int MemoryBarrierNumberOfSamples { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060002B2 RID: 690
		TimeSpan MemoryBarrierSamplingInterval { get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060002B3 RID: 691
		TimeSpan MemoryBarrierSamplingDelay { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060002B4 RID: 692
		long MemoryBarrierPrivateBytesUsageLimit { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060002B5 RID: 693
		TimeSpan WorkCycleUpdatePeriod { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060002B6 RID: 694
		TimeSpan BatchDuration { get; }
	}
}
