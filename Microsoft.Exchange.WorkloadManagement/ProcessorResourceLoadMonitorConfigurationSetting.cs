using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000028 RID: 40
	internal sealed class ProcessorResourceLoadMonitorConfigurationSetting : ResourceHealthMonitorConfigurationSetting
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006616 File Offset: 0x00004816
		internal override string DisabledRegistryValueName
		{
			get
			{
				return "DisableCPUHealthCollection";
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000661D File Offset: 0x0000481D
		internal override string RefreshIntervalRegistryValueName
		{
			get
			{
				return "CPUHealthRefreshInterval";
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006624 File Offset: 0x00004824
		internal override string OverrideMetricValueRegistryValueName
		{
			get
			{
				return "CPUMetricValue";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000662B File Offset: 0x0000482B
		internal string CPUAverageTimeWindowRegistryValueName
		{
			get
			{
				return "CPUAverageTimeWindowInSeconds";
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006632 File Offset: 0x00004832
		internal string CPUHealthyFairThresholdConfigValueName
		{
			get
			{
				return "CPUHealthyFairThreshold";
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006639 File Offset: 0x00004839
		internal string CPUFairUnhealthyThresholdConfigValueName
		{
			get
			{
				return "CPUFairUnhealthyThreshold";
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006640 File Offset: 0x00004840
		internal string CPUMaxDelayConfigValueName
		{
			get
			{
				return "CPUMaxDelayInMilliseconds";
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006647 File Offset: 0x00004847
		internal override TimeSpan DefaultRefreshInterval
		{
			get
			{
				return this.refreshIntervalDefault;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000664F File Offset: 0x0000484F
		internal int DefaultCPUAverageTimeWindowInSeconds
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006653 File Offset: 0x00004853
		internal int DefaultHealthyFairThreshold
		{
			get
			{
				return 70;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006657 File Offset: 0x00004857
		internal int DefaultFairUnhealthyThreshold
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000665B File Offset: 0x0000485B
		internal int DefaultCPUMaxDelayInMilliseconds
		{
			get
			{
				return 15000;
			}
		}

		// Token: 0x040000B7 RID: 183
		private const string DisabledValueName = "DisableCPUHealthCollection";

		// Token: 0x040000B8 RID: 184
		private const string RefreshIntervalValueName = "CPUHealthRefreshInterval";

		// Token: 0x040000B9 RID: 185
		private const string OverrideMetricValueName = "CPUMetricValue";

		// Token: 0x040000BA RID: 186
		private const string CPUAverageTimeWindowValueName = "CPUAverageTimeWindowInSeconds";

		// Token: 0x040000BB RID: 187
		private const string CPUHealthyFairThresholdValueName = "CPUHealthyFairThreshold";

		// Token: 0x040000BC RID: 188
		private const string CPUFairUnhealthyThresholdValueName = "CPUFairUnhealthyThreshold";

		// Token: 0x040000BD RID: 189
		private const string CPUMaxDelayValueName = "CPUMaxDelayInMilliseconds";

		// Token: 0x040000BE RID: 190
		private readonly TimeSpan refreshIntervalDefault = TimeSpan.FromSeconds(1.0);
	}
}
