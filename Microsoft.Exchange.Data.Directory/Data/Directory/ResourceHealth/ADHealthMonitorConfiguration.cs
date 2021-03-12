using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009E8 RID: 2536
	internal sealed class ADHealthMonitorConfiguration : ResourceHealthMonitorConfiguration<ADHealthMonitorConfigurationSetting>
	{
		// Token: 0x17002A25 RID: 10789
		// (get) Token: 0x060075A7 RID: 30119 RVA: 0x001828C2 File Offset: 0x00180AC2
		internal static ADHealthMonitorConfiguration Instance
		{
			get
			{
				if (ADHealthMonitorConfiguration.instance == null)
				{
					ADHealthMonitorConfiguration.instance = new ADHealthMonitorConfiguration();
				}
				return ADHealthMonitorConfiguration.instance;
			}
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x001828DC File Offset: 0x00180ADC
		private ADHealthMonitorConfiguration()
		{
			this.enabled = (this.enabled && Globals.IsMicrosoftHostedOnly);
			ADHealthMonitorConfiguration.Tracer.TraceDebug<bool>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] Enabled={0}.", this.enabled);
			if (this.refreshInterval < this.ConfigSettings.DefaultRefreshInterval)
			{
				ADHealthMonitorConfiguration.Tracer.TraceError<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] Refresh interval '{0}' set in registry is less than '{1}'. Using '{1}' instead.", this.refreshInterval, this.ConfigSettings.DefaultRefreshInterval);
				this.refreshInterval = this.ConfigSettings.DefaultRefreshInterval;
			}
			using (RegistryKey registryKey = ResourceHealthMonitorConfiguration<ADHealthMonitorConfigurationSetting>.OpenConfigurationKey())
			{
				if (registryKey != null)
				{
					this.overrideHealthMeasure = (registryKey.GetValue(this.ConfigSettings.OverrideHealthMeasureRegistryValueName) as int?);
					if (this.overrideHealthMeasure > 100 || this.overrideHealthMeasure < -1)
					{
						ADHealthMonitorConfiguration.Tracer.TraceError<int?>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] OverrideHealthMeasure '{0}' set in registry is not in range [-1, 100]. Using '<null>' instead.", this.overrideHealthMeasure);
					}
					ADHealthMonitorConfiguration.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] OverrideHealthMeasure={0}.", (this.overrideHealthMeasure != null) ? this.overrideHealthMeasure.Value.ToString() : "<null>");
					this.healthyCutoff = ResourceHealthMonitorConfiguration<ADHealthMonitorConfigurationSetting>.ReadTimeSpan(registryKey, this.ConfigSettings.HealthyCutoffRegistryValueName, this.ConfigSettings.HealthyCutoffDefault);
					if (this.healthyCutoff < this.refreshInterval)
					{
						ADHealthMonitorConfiguration.Tracer.TraceError<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] Healthy cutoff '{0}' set in registry is less than refresh interval '{1}'. Using '{1}' instead.", this.healthyCutoff, this.refreshInterval);
						this.healthyCutoff = this.refreshInterval;
					}
					ADHealthMonitorConfiguration.Tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] HealthyCutoff = '{0}'.", this.healthyCutoff);
					this.fairCutoff = ResourceHealthMonitorConfiguration<ADHealthMonitorConfigurationSetting>.ReadTimeSpan(registryKey, this.ConfigSettings.FairCutoffRegistryValueName, this.ConfigSettings.FairCutoffDefault);
					if (this.fairCutoff < this.healthyCutoff + this.refreshInterval)
					{
						ADHealthMonitorConfiguration.Tracer.TraceError((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] Fair cutoff '{0}' set in registry is less than healthy cutoff '{1}' plus refresh interval '{2}'. Using '{3}' instead.", new object[]
						{
							this.fairCutoff,
							this.healthyCutoff,
							this.refreshInterval,
							this.healthyCutoff + this.refreshInterval
						});
						this.fairCutoff = this.healthyCutoff + this.refreshInterval;
					}
					ADHealthMonitorConfiguration.Tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "[ADHealthMonitorConfiguration::ctor] FairCutoff = '{0}'.", this.fairCutoff);
				}
			}
		}

		// Token: 0x17002A26 RID: 10790
		// (get) Token: 0x060075A9 RID: 30121 RVA: 0x00182BC0 File Offset: 0x00180DC0
		public static int? OverrideHealthMeasure
		{
			get
			{
				return ADHealthMonitorConfiguration.Instance.overrideHealthMeasure;
			}
		}

		// Token: 0x17002A27 RID: 10791
		// (get) Token: 0x060075AA RID: 30122 RVA: 0x00182BCC File Offset: 0x00180DCC
		public static TimeSpan HealthyCutoff
		{
			get
			{
				return ADHealthMonitorConfiguration.Instance.healthyCutoff;
			}
		}

		// Token: 0x17002A28 RID: 10792
		// (get) Token: 0x060075AB RID: 30123 RVA: 0x00182BD8 File Offset: 0x00180DD8
		public static TimeSpan FairCutoff
		{
			get
			{
				return ADHealthMonitorConfiguration.Instance.fairCutoff;
			}
		}

		// Token: 0x17002A29 RID: 10793
		// (get) Token: 0x060075AC RID: 30124 RVA: 0x00182BE4 File Offset: 0x00180DE4
		public static TimeSpan RefreshInterval
		{
			get
			{
				return ADHealthMonitorConfiguration.Instance.refreshInterval;
			}
		}

		// Token: 0x17002A2A RID: 10794
		// (get) Token: 0x060075AD RID: 30125 RVA: 0x00182BF0 File Offset: 0x00180DF0
		public static bool Enabled
		{
			get
			{
				return ADHealthMonitorConfiguration.Instance.enabled;
			}
		}

		// Token: 0x04004B76 RID: 19318
		private readonly int? overrideHealthMeasure = null;

		// Token: 0x04004B77 RID: 19319
		private readonly TimeSpan healthyCutoff;

		// Token: 0x04004B78 RID: 19320
		private readonly TimeSpan fairCutoff;

		// Token: 0x04004B79 RID: 19321
		private static readonly Trace Tracer = ExTraceGlobals.ResourceHealthManagerTracer;

		// Token: 0x04004B7A RID: 19322
		private static ADHealthMonitorConfiguration instance;
	}
}
