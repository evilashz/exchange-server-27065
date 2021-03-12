using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000018 RID: 24
	internal sealed class CiHealthMonitorConfiguration : ResourceHealthMonitorConfiguration<CiHealthMonitorConfigurationSetting>
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004774 File Offset: 0x00002974
		private CiHealthMonitorConfiguration()
		{
			this.numberOfHealthyCopiesRequired = this.ConfigSettings.DefaultNumberOfHealthyCopiesRequired;
			this.rpcTimeout = this.ConfigSettings.DefaultRpcTimeout;
			this.failedCatalogStatusThreshold = this.ConfigSettings.DefaultFailedCatalogStatusThreshold;
			this.mdbCopyUpdateInterval = this.ConfigSettings.DefaultMdbCopyUpdateInterval;
			this.mdbCopyUpdateDelay = this.ConfigSettings.DefaultMdbCopyUpdateDelay;
			using (RegistryKey registryKey = ResourceHealthMonitorConfiguration<CiHealthMonitorConfigurationSetting>.OpenConfigurationKey())
			{
				if (registryKey != null)
				{
					this.numberOfHealthyCopiesRequired = (int)registryKey.GetValue(this.ConfigSettings.NumberOfHealthyCopiesRequiredRegistryValueName, this.ConfigSettings.DefaultNumberOfHealthyCopiesRequired);
					if (this.numberOfHealthyCopiesRequired < 1 || this.numberOfHealthyCopiesRequired > 4)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] NumberOfHealthyCopiesRequired '{0}' set in registry is not in range [1, 4]. Using '{1}' instead.", this.numberOfHealthyCopiesRequired, this.ConfigSettings.DefaultNumberOfHealthyCopiesRequired);
						this.numberOfHealthyCopiesRequired = this.ConfigSettings.DefaultNumberOfHealthyCopiesRequired;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] NumberOfHealthyCopiesRequired = '{0}'.", this.numberOfHealthyCopiesRequired);
					int num = (int)registryKey.GetValue(this.ConfigSettings.RpcTimeoutRegistryValueName, (int)this.ConfigSettings.DefaultRpcTimeout.TotalSeconds);
					if (num <= 0 || num > 60)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, double>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] RpcTimeout '{0}' set in registry is not in range [1, 60]. Using '{1}' instead.", num, this.ConfigSettings.DefaultRpcTimeout.TotalSeconds);
						num = (int)this.ConfigSettings.DefaultRpcTimeout.TotalSeconds;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] RpcTimeout = '{0}'.", num);
					this.rpcTimeout = TimeSpan.FromSeconds((double)num);
					int num2 = (int)registryKey.GetValue(this.ConfigSettings.MdbCopyUpdateDelayRegistryValueName, (int)this.ConfigSettings.DefaultMdbCopyUpdateDelay.TotalSeconds);
					if (num2 <= 0 || num2 > 30)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, double>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] MDB refresh delay '{0}' set in registry is not in range [1, 30]. Using '{1}' instead.", num2, this.ConfigSettings.DefaultMdbCopyUpdateDelay.TotalSeconds);
						num2 = (int)this.ConfigSettings.DefaultMdbCopyUpdateDelay.TotalSeconds;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] MDB refresh delay = '{0}'.", num2);
					this.mdbCopyUpdateDelay = TimeSpan.FromSeconds((double)num2);
					int num3 = (int)registryKey.GetValue(this.ConfigSettings.MdbCopyUpdateIntervalRegistryValueName, (int)this.ConfigSettings.DefaultMdbCopyUpdateInterval.TotalSeconds);
					if (num3 <= 60 || num3 > 3600)
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceError<int, double>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] MDB refresh interval '{0}' set in registry is not in range [60, 3600]. Using '{1}' instead.", num3, this.ConfigSettings.DefaultMdbCopyUpdateInterval.TotalSeconds);
						num3 = (int)this.ConfigSettings.DefaultMdbCopyUpdateInterval.TotalSeconds;
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] MDB refresh interval = '{0}'.", num3);
					this.mdbCopyUpdateInterval = TimeSpan.FromSeconds((double)num3);
					this.failedCatalogStatusThreshold = (int)registryKey.GetValue(this.ConfigSettings.FailedCatalogStatusThresholdRegistryValueName, this.ConfigSettings.DefaultFailedCatalogStatusThreshold);
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)this.GetHashCode(), "[CiHealthMonitorConfiguration::ctor] FailedCatalogStatusThreshold = '{0}'.", this.failedCatalogStatusThreshold);
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004ACC File Offset: 0x00002CCC
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public static TimeSpan RefreshInterval
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.refreshInterval;
			}
			set
			{
				CiHealthMonitorConfiguration.Instance.refreshInterval = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004AE5 File Offset: 0x00002CE5
		public static bool Enabled
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.enabled;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004AF1 File Offset: 0x00002CF1
		public static int? OverrideMetricValue
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.overrideMetricValue;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004AFD File Offset: 0x00002CFD
		public static int NumberOfHealthyCopiesRequired
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.numberOfHealthyCopiesRequired;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004B09 File Offset: 0x00002D09
		public static int FailedCatalogStatusThreshold
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.failedCatalogStatusThreshold;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004B15 File Offset: 0x00002D15
		public static TimeSpan RpcTimeout
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.rpcTimeout;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004B21 File Offset: 0x00002D21
		public static TimeSpan MdbCopyUpdateInterval
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.mdbCopyUpdateInterval;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004B2D File Offset: 0x00002D2D
		public static TimeSpan MdbCopyUpdateDelay
		{
			get
			{
				return CiHealthMonitorConfiguration.Instance.mdbCopyUpdateDelay;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004B39 File Offset: 0x00002D39
		internal static CiHealthMonitorConfiguration Instance
		{
			get
			{
				if (CiHealthMonitorConfiguration.instance == null)
				{
					CiHealthMonitorConfiguration.instance = new CiHealthMonitorConfiguration();
				}
				return CiHealthMonitorConfiguration.instance;
			}
		}

		// Token: 0x04000061 RID: 97
		private static CiHealthMonitorConfiguration instance;

		// Token: 0x04000062 RID: 98
		private readonly int numberOfHealthyCopiesRequired;

		// Token: 0x04000063 RID: 99
		private readonly int failedCatalogStatusThreshold;

		// Token: 0x04000064 RID: 100
		private readonly TimeSpan rpcTimeout;

		// Token: 0x04000065 RID: 101
		private readonly TimeSpan mdbCopyUpdateInterval;

		// Token: 0x04000066 RID: 102
		private readonly TimeSpan mdbCopyUpdateDelay;
	}
}
