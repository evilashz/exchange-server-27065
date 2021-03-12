using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002F0 RID: 752
	internal sealed class PerfmonCounters : IPerfmonCounters
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x00089FE8 File Offset: 0x000881E8
		internal PerfmonCounters(ReplayServicePerfmonInstance perfmonInstance)
		{
			this.m_instance = perfmonInstance;
			this.m_copyQueueLength = new SafeCounter(perfmonInstance.CopyQueueLength);
			this.m_rawCopyQueueLength = new SafeCounter(perfmonInstance.RawCopyQueueLength);
			this.m_replayQueueLength = new SafeCounter(perfmonInstance.ReplayQueueLength);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0008A035 File Offset: 0x00088235
		public void Reset()
		{
			this.m_copyQueueLength.Reset();
			this.m_rawCopyQueueLength.Reset();
			this.m_replayQueueLength.Reset();
			this.m_instance.Reset();
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x0008A063 File Offset: 0x00088263
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x0008A075 File Offset: 0x00088275
		public long CopyNotificationGenerationNumber
		{
			get
			{
				return this.m_instance.CopyNotificationGenerationNumber.RawValue;
			}
			set
			{
				this.m_instance.CopyNotificationGenerationNumber.RawValue = value;
				this.m_instance.CopyNotificationGenerationsPerSecond.RawValue = value;
				this.UpdateCopyQueueLength();
				this.UpdateRawCopyQueueLength();
			}
		}

		// Token: 0x17000809 RID: 2057
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0008A0A5 File Offset: 0x000882A5
		public long CopyGenerationNumber
		{
			set
			{
				this.m_instance.CopyGenerationNumber.RawValue = value;
				this.UpdateRawCopyQueueLength();
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0008A0BE File Offset: 0x000882BE
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x0008A0D0 File Offset: 0x000882D0
		public long InspectorGenerationNumber
		{
			get
			{
				return this.m_instance.InspectorGenerationNumber.RawValue;
			}
			set
			{
				this.m_instance.InspectorGenerationNumber.RawValue = value;
				this.m_instance.InspectorGenerationsPerSecond.RawValue = value;
				this.UpdateReplayQueueLength();
				this.UpdateCopyQueueLength();
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0008A100 File Offset: 0x00088300
		public float CopyNotificationGenerationsPerSecond
		{
			get
			{
				return this.m_instance.CopyNotificationGenerationsPerSecond.NextValue();
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0008A112 File Offset: 0x00088312
		public float InspectorGenerationsPerSecond
		{
			get
			{
				return this.m_instance.InspectorGenerationsPerSecond.NextValue();
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0008A124 File Offset: 0x00088324
		public long CopyQueueLength
		{
			get
			{
				return this.m_copyQueueLength.Value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0008A131 File Offset: 0x00088331
		public long RawCopyQueueLength
		{
			get
			{
				return this.m_rawCopyQueueLength.Value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0008A13E File Offset: 0x0008833E
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0008A150 File Offset: 0x00088350
		public long CopyQueueNotKeepingUp
		{
			get
			{
				return this.m_instance.CopyQueueNotKeepingUp.RawValue;
			}
			set
			{
				this.m_instance.CopyQueueNotKeepingUp.RawValue = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0008A163 File Offset: 0x00088363
		public long ReplayNotificationGenerationNumber
		{
			set
			{
				this.m_instance.ReplayNotificationGenerationNumber.RawValue = value;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0008A176 File Offset: 0x00088376
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x0008A188 File Offset: 0x00088388
		public long ReplayGenerationNumber
		{
			get
			{
				return this.m_instance.ReplayGenerationNumber.RawValue;
			}
			set
			{
				this.m_instance.ReplayGenerationNumber.RawValue = value;
				this.m_instance.ReplayGenerationsPerSecond.RawValue = value;
				this.UpdateReplayQueueLength();
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0008A1B2 File Offset: 0x000883B2
		public float ReplayGenerationsPerSecond
		{
			get
			{
				return this.m_instance.ReplayGenerationsPerSecond.NextValue();
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0008A1C4 File Offset: 0x000883C4
		public long ReplayQueueLength
		{
			get
			{
				return this.m_replayQueueLength.Value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0008A1D1 File Offset: 0x000883D1
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x0008A1E3 File Offset: 0x000883E3
		public long ReplayQueueNotKeepingUp
		{
			get
			{
				return this.m_instance.ReplayQueueNotKeepingUp.RawValue;
			}
			set
			{
				this.m_instance.ReplayQueueNotKeepingUp.RawValue = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0008A1F6 File Offset: 0x000883F6
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0008A208 File Offset: 0x00088408
		public long Failed
		{
			get
			{
				return this.m_instance.Failed.RawValue;
			}
			set
			{
				this.m_instance.Failed.RawValue = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0008A21B File Offset: 0x0008841B
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0008A22D File Offset: 0x0008842D
		public long Initializing
		{
			get
			{
				return this.m_instance.Initializing.RawValue;
			}
			set
			{
				this.m_instance.Initializing.RawValue = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0008A240 File Offset: 0x00088440
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0008A252 File Offset: 0x00088452
		public long Resynchronizing
		{
			get
			{
				return this.m_instance.Resynchronizing.RawValue;
			}
			set
			{
				this.m_instance.Resynchronizing.RawValue = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0008A265 File Offset: 0x00088465
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0008A277 File Offset: 0x00088477
		public long ActivationSuspended
		{
			get
			{
				return this.m_instance.ActivationSuspended.RawValue;
			}
			set
			{
				this.m_instance.ActivationSuspended.RawValue = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0008A28A File Offset: 0x0008848A
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0008A29C File Offset: 0x0008849C
		public long ReplayLagDisabled
		{
			get
			{
				return this.m_instance.ReplayLagDisabled.RawValue;
			}
			set
			{
				this.m_instance.ReplayLagDisabled.RawValue = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0008A2AF File Offset: 0x000884AF
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0008A2C1 File Offset: 0x000884C1
		public long ReplayLagPercentage
		{
			get
			{
				return this.m_instance.ReplayLagPercentage.RawValue;
			}
			set
			{
				this.m_instance.ReplayLagPercentage.RawValue = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0008A2D4 File Offset: 0x000884D4
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0008A2E6 File Offset: 0x000884E6
		public long FailedSuspended
		{
			get
			{
				return this.m_instance.FailedSuspended.RawValue;
			}
			set
			{
				this.m_instance.FailedSuspended.RawValue = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0008A2F9 File Offset: 0x000884F9
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0008A30B File Offset: 0x0008850B
		public long SinglePageRestore
		{
			get
			{
				return this.m_instance.SinglePageRestore.RawValue;
			}
			set
			{
				this.m_instance.SinglePageRestore.RawValue = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0008A31E File Offset: 0x0008851E
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x0008A330 File Offset: 0x00088530
		public long Disconnected
		{
			get
			{
				return this.m_instance.Disconnected.RawValue;
			}
			set
			{
				this.m_instance.Disconnected.RawValue = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0008A343 File Offset: 0x00088543
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x0008A355 File Offset: 0x00088555
		public long Suspended
		{
			get
			{
				return this.m_instance.Suspended.RawValue;
			}
			set
			{
				this.m_instance.Suspended.RawValue = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0008A368 File Offset: 0x00088568
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x0008A37A File Offset: 0x0008857A
		public long SuspendedAndNotSeeding
		{
			get
			{
				return this.m_instance.SuspendedAndNotSeeding.RawValue;
			}
			set
			{
				this.m_instance.SuspendedAndNotSeeding.RawValue = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x0008A38D File Offset: 0x0008858D
		// (set) Token: 0x06001E51 RID: 7761 RVA: 0x0008A39F File Offset: 0x0008859F
		public long Seeding
		{
			get
			{
				return this.m_instance.Seeding.RawValue;
			}
			set
			{
				this.m_instance.Seeding.RawValue = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0008A3B2 File Offset: 0x000885B2
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x0008A3C4 File Offset: 0x000885C4
		public long CompressionEnabled
		{
			get
			{
				return this.m_instance.CompressionEnabled.RawValue;
			}
			set
			{
				this.m_instance.CompressionEnabled.RawValue = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0008A3D7 File Offset: 0x000885D7
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0008A3E9 File Offset: 0x000885E9
		public long EncryptionEnabled
		{
			get
			{
				return this.m_instance.EncryptionEnabled.RawValue;
			}
			set
			{
				this.m_instance.EncryptionEnabled.RawValue = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0008A3FC File Offset: 0x000885FC
		public long TruncatedGenerationNumber
		{
			set
			{
				this.m_instance.TruncatedGenerationNumber.RawValue = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0008A40F File Offset: 0x0008860F
		public long IncReseedDBPagesReadNumber
		{
			set
			{
				this.m_instance.IncReseedDBPagesReadNumber.RawValue = value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0008A422 File Offset: 0x00088622
		public long IncReseedLogCopiedNumber
		{
			set
			{
				this.m_instance.IncReseedLogCopiedNumber.RawValue = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0008A435 File Offset: 0x00088635
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x0008A447 File Offset: 0x00088647
		public long GranularReplication
		{
			get
			{
				return this.m_instance.GranularReplication.RawValue;
			}
			set
			{
				this.m_instance.GranularReplication.RawValue = value;
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0008A45A File Offset: 0x0008865A
		public void RecordLogCopierNetworkReadLatency(long tics)
		{
			this.m_instance.AvgLogCopyNetReadLatency.IncrementBy(tics);
			this.m_instance.AvgLogCopyNetReadLatencyBase.Increment();
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0008A47F File Offset: 0x0008867F
		public void RecordBlockModeConsumerWriteLatency(long tics)
		{
			this.m_instance.AvgBlockModeConsumerWriteTime.IncrementBy(tics);
			this.m_instance.AvgBlockModeConsumerWriteTimeBase.Increment();
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0008A4A4 File Offset: 0x000886A4
		public void RecordFileModeWriteLatency(long tics)
		{
			this.m_instance.AvgFileModeWriteTime.IncrementBy(tics);
			this.m_instance.AvgFileModeWriteTimeBase.Increment();
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0008A4C9 File Offset: 0x000886C9
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0008A4DB File Offset: 0x000886DB
		public long PassiveSeedingSource
		{
			get
			{
				return this.m_instance.PassiveSeedingSource.RawValue;
			}
			set
			{
				this.m_instance.PassiveSeedingSource.RawValue = value;
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0008A4EE File Offset: 0x000886EE
		public void RecordLogCopyThruput(long bytesCopied)
		{
			this.m_instance.LogCopyThruput.IncrementBy(bytesCopied / 1024L);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0008A50C File Offset: 0x0008870C
		public void RecordGranularBytesReceived(long bytesCopied, bool logIsComplete)
		{
			this.m_instance.LogCopyThruput.IncrementBy(bytesCopied / 1024L);
			this.m_instance.TotalGranularBytesReceived.IncrementBy(bytesCopied);
			if (logIsComplete)
			{
				this.m_granularLogsReceived += 1L;
				this.m_instance.AverageGranularBytesPerLog.RawValue = this.m_instance.TotalGranularBytesReceived.RawValue / this.m_granularLogsReceived;
			}
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0008A57D File Offset: 0x0008877D
		public void RecordOneGetCopyStatusCall()
		{
			this.m_instance.GetCopyStatusInstanceCalls.Increment();
			this.m_instance.GetCopyStatusInstanceCallsPerSec.Increment();
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0008A5A4 File Offset: 0x000887A4
		private void UpdateCopyQueueLength()
		{
			long num = Math.Max(this.m_instance.InspectorGenerationNumber.RawValue - 1L, this.m_instance.CopyNotificationGenerationNumber.RawValue) - this.m_instance.InspectorGenerationNumber.RawValue;
			this.m_copyQueueLength.Value = ((num > 0L) ? num : 0L);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0008A600 File Offset: 0x00088800
		private void UpdateRawCopyQueueLength()
		{
			long num = this.m_instance.CopyNotificationGenerationNumber.RawValue - this.m_instance.CopyGenerationNumber.RawValue;
			this.m_rawCopyQueueLength.Value = ((num > 0L) ? num : 0L);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0008A644 File Offset: 0x00088844
		private void UpdateReplayQueueLength()
		{
			this.m_replayQueueLength.Value = Math.Max(0L, this.m_instance.InspectorGenerationNumber.RawValue - this.m_instance.ReplayGenerationNumber.RawValue);
		}

		// Token: 0x04000CBE RID: 3262
		private long m_granularLogsReceived;

		// Token: 0x04000CBF RID: 3263
		private ReplayServicePerfmonInstance m_instance;

		// Token: 0x04000CC0 RID: 3264
		private SafeCounter m_copyQueueLength;

		// Token: 0x04000CC1 RID: 3265
		private SafeCounter m_rawCopyQueueLength;

		// Token: 0x04000CC2 RID: 3266
		private SafeCounter m_replayQueueLength;
	}
}
