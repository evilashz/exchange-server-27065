using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000078 RID: 120
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IMailboxAssistantServiceSettings_Implementation_ : IMailboxAssistantServiceSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00005B68 File Offset: 0x00003D68
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00005B6B File Offset: 0x00003D6B
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00005B6E File Offset: 0x00003D6E
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00005B76 File Offset: 0x00003D76
		public TimeSpan EventPollingInterval
		{
			get
			{
				return this._EventPollingInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00005B7E File Offset: 0x00003D7E
		public TimeSpan ActiveWatermarksSaveInterval
		{
			get
			{
				return this._ActiveWatermarksSaveInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00005B86 File Offset: 0x00003D86
		public TimeSpan IdleWatermarksSaveInterval
		{
			get
			{
				return this._IdleWatermarksSaveInterval_MaterializedValue_;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00005B8E File Offset: 0x00003D8E
		public TimeSpan WatermarkCleanupInterval
		{
			get
			{
				return this._WatermarkCleanupInterval_MaterializedValue_;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00005B96 File Offset: 0x00003D96
		public int MaxThreadsForAllTimeBasedAssistants
		{
			get
			{
				return this._MaxThreadsForAllTimeBasedAssistants_MaterializedValue_;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00005B9E File Offset: 0x00003D9E
		public int MaxThreadsPerTimeBasedAssistantType
		{
			get
			{
				return this._MaxThreadsPerTimeBasedAssistantType_MaterializedValue_;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00005BA6 File Offset: 0x00003DA6
		public TimeSpan HangDetectionTimeout
		{
			get
			{
				return this._HangDetectionTimeout_MaterializedValue_;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00005BAE File Offset: 0x00003DAE
		public TimeSpan HangDetectionPeriod
		{
			get
			{
				return this._HangDetectionPeriod_MaterializedValue_;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00005BB6 File Offset: 0x00003DB6
		public int MaximumEventQueueSize
		{
			get
			{
				return this._MaximumEventQueueSize_MaterializedValue_;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00005BBE File Offset: 0x00003DBE
		public bool MemoryMonitorEnabled
		{
			get
			{
				return this._MemoryMonitorEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00005BC6 File Offset: 0x00003DC6
		public int MemoryBarrierNumberOfSamples
		{
			get
			{
				return this._MemoryBarrierNumberOfSamples_MaterializedValue_;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00005BCE File Offset: 0x00003DCE
		public TimeSpan MemoryBarrierSamplingInterval
		{
			get
			{
				return this._MemoryBarrierSamplingInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public TimeSpan MemoryBarrierSamplingDelay
		{
			get
			{
				return this._MemoryBarrierSamplingDelay_MaterializedValue_;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00005BDE File Offset: 0x00003DDE
		public long MemoryBarrierPrivateBytesUsageLimit
		{
			get
			{
				return this._MemoryBarrierPrivateBytesUsageLimit_MaterializedValue_;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00005BE6 File Offset: 0x00003DE6
		public TimeSpan WorkCycleUpdatePeriod
		{
			get
			{
				return this._WorkCycleUpdatePeriod_MaterializedValue_;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00005BEE File Offset: 0x00003DEE
		public TimeSpan BatchDuration
		{
			get
			{
				return this._BatchDuration_MaterializedValue_;
			}
		}

		// Token: 0x0400021C RID: 540
		internal string _Name_MaterializedValue_;

		// Token: 0x0400021D RID: 541
		internal TimeSpan _EventPollingInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400021E RID: 542
		internal TimeSpan _ActiveWatermarksSaveInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400021F RID: 543
		internal TimeSpan _IdleWatermarksSaveInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000220 RID: 544
		internal TimeSpan _WatermarkCleanupInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000221 RID: 545
		internal int _MaxThreadsForAllTimeBasedAssistants_MaterializedValue_;

		// Token: 0x04000222 RID: 546
		internal int _MaxThreadsPerTimeBasedAssistantType_MaterializedValue_;

		// Token: 0x04000223 RID: 547
		internal TimeSpan _HangDetectionTimeout_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000224 RID: 548
		internal TimeSpan _HangDetectionPeriod_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000225 RID: 549
		internal int _MaximumEventQueueSize_MaterializedValue_;

		// Token: 0x04000226 RID: 550
		internal bool _MemoryMonitorEnabled_MaterializedValue_;

		// Token: 0x04000227 RID: 551
		internal int _MemoryBarrierNumberOfSamples_MaterializedValue_;

		// Token: 0x04000228 RID: 552
		internal TimeSpan _MemoryBarrierSamplingInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000229 RID: 553
		internal TimeSpan _MemoryBarrierSamplingDelay_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400022A RID: 554
		internal long _MemoryBarrierPrivateBytesUsageLimit_MaterializedValue_;

		// Token: 0x0400022B RID: 555
		internal TimeSpan _WorkCycleUpdatePeriod_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400022C RID: 556
		internal TimeSpan _BatchDuration_MaterializedValue_ = default(TimeSpan);
	}
}
