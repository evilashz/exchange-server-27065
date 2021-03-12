using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000027 RID: 39
	internal sealed class ProcessorResourceLoadMonitorConfiguration : ResourceHealthMonitorConfiguration<ProcessorResourceLoadMonitorConfigurationSetting>
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00006158 File Offset: 0x00004358
		private ProcessorResourceLoadMonitorConfiguration()
		{
			if (this.overrideMetricValue < 0 || this.overrideMetricValue > 100)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int?>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] OverrideMetricValue '{0}' set in registry is not in the correct range (0-100). Ignore this setting.", this.overrideMetricValue);
				this.overrideMetricValue = null;
			}
			this.cpuAverageTimeWindow = this.ConfigSettings.DefaultCPUAverageTimeWindowInSeconds;
			this.healthyFairThreshold = this.ConfigSettings.DefaultHealthyFairThreshold;
			this.fairUnhealthyThreshold = this.ConfigSettings.DefaultFairUnhealthyThreshold;
			this.maxDelay = this.ConfigSettings.DefaultCPUMaxDelayInMilliseconds;
			using (RegistryKey registryKey = ResourceHealthMonitorConfiguration<ProcessorResourceLoadMonitorConfigurationSetting>.OpenConfigurationKey())
			{
				if (registryKey != null)
				{
					this.cpuAverageTimeWindow = (int)registryKey.GetValue(this.ConfigSettings.CPUAverageTimeWindowRegistryValueName, this.ConfigSettings.DefaultCPUAverageTimeWindowInSeconds);
					if (this.cpuAverageTimeWindow < 1 || this.cpuAverageTimeWindow > 60)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUAverageTimeWindow '{0}' set in registry is not in range [1, 60]. Using '{1}' instead.", this.cpuAverageTimeWindow, this.ConfigSettings.DefaultCPUAverageTimeWindowInSeconds);
						this.cpuAverageTimeWindow = this.ConfigSettings.DefaultCPUAverageTimeWindowInSeconds;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUAverageTimeWindow = '{0}'.", this.cpuAverageTimeWindow);
				}
			}
			IntAppSettingsEntry intAppSettingsEntry = new IntAppSettingsEntry(this.ConfigSettings.CPUHealthyFairThresholdConfigValueName, this.ConfigSettings.DefaultHealthyFairThreshold, ExTraceGlobals.ResourceHealthManagerTracer);
			this.healthyFairThreshold = intAppSettingsEntry.Value;
			if (this.healthyFairThreshold < 0 || this.healthyFairThreshold > 100)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUHealthyFairThreshold '{0}' set in App.Config file is not in range [0, 100]. Using '{1}' instead.", this.healthyFairThreshold, this.ConfigSettings.DefaultHealthyFairThreshold);
				this.healthyFairThreshold = this.ConfigSettings.DefaultHealthyFairThreshold;
			}
			IntAppSettingsEntry intAppSettingsEntry2 = new IntAppSettingsEntry(this.ConfigSettings.CPUFairUnhealthyThresholdConfigValueName, this.ConfigSettings.DefaultFairUnhealthyThreshold, ExTraceGlobals.ResourceHealthManagerTracer);
			this.fairUnhealthyThreshold = intAppSettingsEntry2.Value;
			if (this.fairUnhealthyThreshold < 0 || this.fairUnhealthyThreshold > 100)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUFairUnhealthyThreshold '{0}' set in App.Config file is not in range [0, 100]. Using '{1}' instead.", this.fairUnhealthyThreshold, this.ConfigSettings.DefaultFairUnhealthyThreshold);
				this.fairUnhealthyThreshold = this.ConfigSettings.DefaultFairUnhealthyThreshold;
			}
			if (this.healthyFairThreshold >= this.fairUnhealthyThreshold)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUHealthyFairThreshold '{0}' set in App.Config file is not smaller than CPUFairUnhealthyThreshold '{1}'. Using default values CPUHealthyFairThreshold='{2}' and CPUFairUnhealthyThreshold='{3}' instead.", new object[]
				{
					this.healthyFairThreshold,
					this.fairUnhealthyThreshold,
					this.ConfigSettings.DefaultHealthyFairThreshold,
					this.ConfigSettings.DefaultFairUnhealthyThreshold
				});
				this.healthyFairThreshold = this.ConfigSettings.DefaultHealthyFairThreshold;
				this.fairUnhealthyThreshold = this.ConfigSettings.DefaultFairUnhealthyThreshold;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUHealthyFairThreshold = '{0}'.", this.healthyFairThreshold);
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUFairUnhealthyThreshold = '{0}'.", this.fairUnhealthyThreshold);
			IntAppSettingsEntry intAppSettingsEntry3 = new IntAppSettingsEntry(this.ConfigSettings.CPUMaxDelayConfigValueName, this.ConfigSettings.DefaultCPUMaxDelayInMilliseconds, ExTraceGlobals.ResourceHealthManagerTracer);
			this.maxDelay = intAppSettingsEntry3.Value;
			if (this.maxDelay < 0 || this.maxDelay > 60000)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUMaxDelay '{0}' set in App.Config file is not in range [0, 60000]. Using '{1}' instead.", this.maxDelay, this.ConfigSettings.DefaultCPUMaxDelayInMilliseconds);
				this.maxDelay = this.ConfigSettings.DefaultCPUMaxDelayInMilliseconds;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[ProcessorResourceLoadMonitorConfiguration::ctor] CPUMaxDelay = '{0}'.", this.maxDelay);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006528 File Offset: 0x00004728
		public static TimeSpan RefreshInterval
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.refreshInterval;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006534 File Offset: 0x00004734
		public static bool Enabled
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.enabled;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006540 File Offset: 0x00004740
		public static int? OverrideMetricValue
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.overrideMetricValue;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000654C File Offset: 0x0000474C
		public static int CPUAverageTimeWindow
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.cpuAverageTimeWindow;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006558 File Offset: 0x00004758
		public static int HealthyFairThreshold
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.healthyFairThreshold;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006564 File Offset: 0x00004764
		public static int FairUnhealthyThreshold
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.fairUnhealthyThreshold;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006570 File Offset: 0x00004770
		public static int MaxDelay
		{
			get
			{
				return ProcessorResourceLoadMonitorConfiguration.Instance.maxDelay;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000657C File Offset: 0x0000477C
		internal static ProcessorResourceLoadMonitorConfiguration Instance
		{
			get
			{
				if (ProcessorResourceLoadMonitorConfiguration.instance == null)
				{
					ProcessorResourceLoadMonitorConfiguration.instance = new ProcessorResourceLoadMonitorConfiguration();
				}
				return ProcessorResourceLoadMonitorConfiguration.instance;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006594 File Offset: 0x00004794
		internal static void SetConfigurationValuesForTest(TimeSpan? refreshInterval, bool? enabled, int? overrideMetricValue, int? cpuAverageTimeWindow)
		{
			if (refreshInterval != null)
			{
				ProcessorResourceLoadMonitorConfiguration.Instance.refreshInterval = refreshInterval.Value;
			}
			if (enabled != null)
			{
				ProcessorResourceLoadMonitorConfiguration.Instance.enabled = enabled.Value;
			}
			if (overrideMetricValue != null)
			{
				ProcessorResourceLoadMonitorConfiguration.Instance.overrideMetricValue = new int?(overrideMetricValue.Value);
			}
			if (cpuAverageTimeWindow != null)
			{
				ProcessorResourceLoadMonitorConfiguration.Instance.cpuAverageTimeWindow = cpuAverageTimeWindow.Value;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000660E File Offset: 0x0000480E
		internal static void ReloadConfigurationValues()
		{
			ProcessorResourceLoadMonitorConfiguration.instance = null;
		}

		// Token: 0x040000AF RID: 175
		internal const int UnknownHealthMeasure = -1;

		// Token: 0x040000B0 RID: 176
		internal const int UnhealthyHealthMeasure = 0;

		// Token: 0x040000B1 RID: 177
		internal const int HealthyHealthMeasure = 100;

		// Token: 0x040000B2 RID: 178
		private static ProcessorResourceLoadMonitorConfiguration instance;

		// Token: 0x040000B3 RID: 179
		private readonly int healthyFairThreshold;

		// Token: 0x040000B4 RID: 180
		private readonly int fairUnhealthyThreshold;

		// Token: 0x040000B5 RID: 181
		private readonly int maxDelay;

		// Token: 0x040000B6 RID: 182
		private int cpuAverageTimeWindow;
	}
}
