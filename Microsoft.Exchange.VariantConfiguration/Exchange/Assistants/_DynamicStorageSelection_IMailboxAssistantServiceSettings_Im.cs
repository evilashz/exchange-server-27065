using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000077 RID: 119
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IMailboxAssistantServiceSettings_Implementation_ : IMailboxAssistantServiceSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00005823 File Offset: 0x00003A23
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000582B File Offset: 0x00003A2B
		_DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00005833 File Offset: 0x00003A33
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00005843 File Offset: 0x00003A43
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00005850 File Offset: 0x00003A50
		public TimeSpan EventPollingInterval
		{
			get
			{
				if (this.dataAccessor._EventPollingInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._EventPollingInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EventPollingInterval_MaterializedValue_;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00005881 File Offset: 0x00003A81
		public TimeSpan ActiveWatermarksSaveInterval
		{
			get
			{
				if (this.dataAccessor._ActiveWatermarksSaveInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._ActiveWatermarksSaveInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ActiveWatermarksSaveInterval_MaterializedValue_;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000058B2 File Offset: 0x00003AB2
		public TimeSpan IdleWatermarksSaveInterval
		{
			get
			{
				if (this.dataAccessor._IdleWatermarksSaveInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._IdleWatermarksSaveInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._IdleWatermarksSaveInterval_MaterializedValue_;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000058E3 File Offset: 0x00003AE3
		public TimeSpan WatermarkCleanupInterval
		{
			get
			{
				if (this.dataAccessor._WatermarkCleanupInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._WatermarkCleanupInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._WatermarkCleanupInterval_MaterializedValue_;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00005914 File Offset: 0x00003B14
		public int MaxThreadsForAllTimeBasedAssistants
		{
			get
			{
				if (this.dataAccessor._MaxThreadsForAllTimeBasedAssistants_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxThreadsForAllTimeBasedAssistants_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxThreadsForAllTimeBasedAssistants_MaterializedValue_;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00005945 File Offset: 0x00003B45
		public int MaxThreadsPerTimeBasedAssistantType
		{
			get
			{
				if (this.dataAccessor._MaxThreadsPerTimeBasedAssistantType_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxThreadsPerTimeBasedAssistantType_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxThreadsPerTimeBasedAssistantType_MaterializedValue_;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00005976 File Offset: 0x00003B76
		public TimeSpan HangDetectionTimeout
		{
			get
			{
				if (this.dataAccessor._HangDetectionTimeout_ValueProvider_ != null)
				{
					return this.dataAccessor._HangDetectionTimeout_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._HangDetectionTimeout_MaterializedValue_;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000059A7 File Offset: 0x00003BA7
		public TimeSpan HangDetectionPeriod
		{
			get
			{
				if (this.dataAccessor._HangDetectionPeriod_ValueProvider_ != null)
				{
					return this.dataAccessor._HangDetectionPeriod_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._HangDetectionPeriod_MaterializedValue_;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000059D8 File Offset: 0x00003BD8
		public int MaximumEventQueueSize
		{
			get
			{
				if (this.dataAccessor._MaximumEventQueueSize_ValueProvider_ != null)
				{
					return this.dataAccessor._MaximumEventQueueSize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaximumEventQueueSize_MaterializedValue_;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00005A09 File Offset: 0x00003C09
		public bool MemoryMonitorEnabled
		{
			get
			{
				if (this.dataAccessor._MemoryMonitorEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryMonitorEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryMonitorEnabled_MaterializedValue_;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00005A3A File Offset: 0x00003C3A
		public int MemoryBarrierNumberOfSamples
		{
			get
			{
				if (this.dataAccessor._MemoryBarrierNumberOfSamples_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryBarrierNumberOfSamples_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryBarrierNumberOfSamples_MaterializedValue_;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00005A6B File Offset: 0x00003C6B
		public TimeSpan MemoryBarrierSamplingInterval
		{
			get
			{
				if (this.dataAccessor._MemoryBarrierSamplingInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryBarrierSamplingInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryBarrierSamplingInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00005A9C File Offset: 0x00003C9C
		public TimeSpan MemoryBarrierSamplingDelay
		{
			get
			{
				if (this.dataAccessor._MemoryBarrierSamplingDelay_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryBarrierSamplingDelay_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryBarrierSamplingDelay_MaterializedValue_;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00005ACD File Offset: 0x00003CCD
		public long MemoryBarrierPrivateBytesUsageLimit
		{
			get
			{
				if (this.dataAccessor._MemoryBarrierPrivateBytesUsageLimit_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryBarrierPrivateBytesUsageLimit_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryBarrierPrivateBytesUsageLimit_MaterializedValue_;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00005AFE File Offset: 0x00003CFE
		public TimeSpan WorkCycleUpdatePeriod
		{
			get
			{
				if (this.dataAccessor._WorkCycleUpdatePeriod_ValueProvider_ != null)
				{
					return this.dataAccessor._WorkCycleUpdatePeriod_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._WorkCycleUpdatePeriod_MaterializedValue_;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00005B2F File Offset: 0x00003D2F
		public TimeSpan BatchDuration
		{
			get
			{
				if (this.dataAccessor._BatchDuration_ValueProvider_ != null)
				{
					return this.dataAccessor._BatchDuration_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BatchDuration_MaterializedValue_;
			}
		}

		// Token: 0x0400021A RID: 538
		private _DynamicStorageSelection_IMailboxAssistantServiceSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400021B RID: 539
		private VariantContextSnapshot context;
	}
}
