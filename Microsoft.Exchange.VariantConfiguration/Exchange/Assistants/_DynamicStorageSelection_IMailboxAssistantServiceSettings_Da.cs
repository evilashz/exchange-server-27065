using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000076 RID: 118
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_ : VariantObjectDataAccessorBase<IMailboxAssistantServiceSettings, _DynamicStorageSelection_IMailboxAssistantServiceSettings_Implementation_, _DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_>
	{
		// Token: 0x040001F9 RID: 505
		internal string _Name_MaterializedValue_;

		// Token: 0x040001FA RID: 506
		internal TimeSpan _EventPollingInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x040001FB RID: 507
		internal ValueProvider<TimeSpan> _EventPollingInterval_ValueProvider_;

		// Token: 0x040001FC RID: 508
		internal TimeSpan _ActiveWatermarksSaveInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x040001FD RID: 509
		internal ValueProvider<TimeSpan> _ActiveWatermarksSaveInterval_ValueProvider_;

		// Token: 0x040001FE RID: 510
		internal TimeSpan _IdleWatermarksSaveInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x040001FF RID: 511
		internal ValueProvider<TimeSpan> _IdleWatermarksSaveInterval_ValueProvider_;

		// Token: 0x04000200 RID: 512
		internal TimeSpan _WatermarkCleanupInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000201 RID: 513
		internal ValueProvider<TimeSpan> _WatermarkCleanupInterval_ValueProvider_;

		// Token: 0x04000202 RID: 514
		internal int _MaxThreadsForAllTimeBasedAssistants_MaterializedValue_;

		// Token: 0x04000203 RID: 515
		internal ValueProvider<int> _MaxThreadsForAllTimeBasedAssistants_ValueProvider_;

		// Token: 0x04000204 RID: 516
		internal int _MaxThreadsPerTimeBasedAssistantType_MaterializedValue_;

		// Token: 0x04000205 RID: 517
		internal ValueProvider<int> _MaxThreadsPerTimeBasedAssistantType_ValueProvider_;

		// Token: 0x04000206 RID: 518
		internal TimeSpan _HangDetectionTimeout_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000207 RID: 519
		internal ValueProvider<TimeSpan> _HangDetectionTimeout_ValueProvider_;

		// Token: 0x04000208 RID: 520
		internal TimeSpan _HangDetectionPeriod_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000209 RID: 521
		internal ValueProvider<TimeSpan> _HangDetectionPeriod_ValueProvider_;

		// Token: 0x0400020A RID: 522
		internal int _MaximumEventQueueSize_MaterializedValue_;

		// Token: 0x0400020B RID: 523
		internal ValueProvider<int> _MaximumEventQueueSize_ValueProvider_;

		// Token: 0x0400020C RID: 524
		internal bool _MemoryMonitorEnabled_MaterializedValue_;

		// Token: 0x0400020D RID: 525
		internal ValueProvider<bool> _MemoryMonitorEnabled_ValueProvider_;

		// Token: 0x0400020E RID: 526
		internal int _MemoryBarrierNumberOfSamples_MaterializedValue_;

		// Token: 0x0400020F RID: 527
		internal ValueProvider<int> _MemoryBarrierNumberOfSamples_ValueProvider_;

		// Token: 0x04000210 RID: 528
		internal TimeSpan _MemoryBarrierSamplingInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000211 RID: 529
		internal ValueProvider<TimeSpan> _MemoryBarrierSamplingInterval_ValueProvider_;

		// Token: 0x04000212 RID: 530
		internal TimeSpan _MemoryBarrierSamplingDelay_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000213 RID: 531
		internal ValueProvider<TimeSpan> _MemoryBarrierSamplingDelay_ValueProvider_;

		// Token: 0x04000214 RID: 532
		internal long _MemoryBarrierPrivateBytesUsageLimit_MaterializedValue_;

		// Token: 0x04000215 RID: 533
		internal ValueProvider<long> _MemoryBarrierPrivateBytesUsageLimit_ValueProvider_;

		// Token: 0x04000216 RID: 534
		internal TimeSpan _WorkCycleUpdatePeriod_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000217 RID: 535
		internal ValueProvider<TimeSpan> _WorkCycleUpdatePeriod_ValueProvider_;

		// Token: 0x04000218 RID: 536
		internal TimeSpan _BatchDuration_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000219 RID: 537
		internal ValueProvider<TimeSpan> _BatchDuration_ValueProvider_;
	}
}
