using System;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009E7 RID: 2535
	internal abstract class ResourceHealthMonitorConfiguration<T> where T : ResourceHealthMonitorConfigurationSetting, new()
	{
		// Token: 0x060075A3 RID: 30115 RVA: 0x0018269C File Offset: 0x0018089C
		protected ResourceHealthMonitorConfiguration()
		{
			this.ConfigSettings = Activator.CreateInstance<T>();
			this.refreshInterval = this.ConfigSettings.DefaultRefreshInterval;
			using (RegistryKey registryKey = ResourceHealthMonitorConfiguration<T>.OpenConfigurationKey())
			{
				if (registryKey != null)
				{
					this.enabled = ((int)registryKey.GetValue(this.ConfigSettings.DisabledRegistryValueName, 0) == 0);
					ResourceHealthMonitorConfiguration<T>.Tracer.TraceDebug<bool>((long)this.GetHashCode(), "[ResourceHealthMonitorConfiguration::ctor] Enabled = '{0}'.", this.enabled);
					this.refreshInterval = ResourceHealthMonitorConfiguration<T>.ReadTimeSpan(registryKey, this.ConfigSettings.RefreshIntervalRegistryValueName, this.ConfigSettings.DefaultRefreshInterval);
					ResourceHealthMonitorConfiguration<T>.Tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "[ResourceHealthMonitorConfiguration::ctor] RefreshInterval = '{0}'.", this.refreshInterval);
					this.overrideMetricValue = (registryKey.GetValue(this.ConfigSettings.OverrideMetricValueRegistryValueName) as int?);
					ResourceHealthMonitorConfiguration<T>.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[ResourceHealthMonitorConfiguration::ctor] OverrideMetricValue = '{0}'.", (this.overrideMetricValue != null) ? this.overrideMetricValue.Value.ToString() : "<null>");
				}
			}
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x00182804 File Offset: 0x00180A04
		protected static RegistryKey OpenConfigurationKey()
		{
			try
			{
				return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth", false);
			}
			catch (SecurityException arg)
			{
				ResourceHealthMonitorConfiguration<T>.Tracer.TraceError<SecurityException>(0L, "[ResourceHealthMonitorConfiguration::OpenConfigurationKey] Security exception encountered while reading Resource Health Monitor configuration: {0}", arg);
			}
			catch (UnauthorizedAccessException arg2)
			{
				ResourceHealthMonitorConfiguration<T>.Tracer.TraceError<UnauthorizedAccessException>(0L, "[ResourceHealthMonitorConfiguration::OpenConfigurationKey] Security exception encountered while reading Resource Health Monitor configuration: {0}", arg2);
			}
			return null;
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x00182870 File Offset: 0x00180A70
		protected static TimeSpan ReadTimeSpan(RegistryKey key, string valueName, TimeSpan defaultValue)
		{
			TimeSpan result = defaultValue;
			object value = key.GetValue(valueName);
			if (value != null)
			{
				if (key.GetValueKind(valueName) != RegistryValueKind.DWord)
				{
					ResourceHealthMonitorConfiguration<T>.Tracer.TraceError<string>(0L, "[ResourceHealthMonitorConfiguration::ReadTimeSpan] {0} should be of type DWORD", valueName);
				}
				else
				{
					result = TimeSpan.FromSeconds((double)((int)value));
				}
			}
			return result;
		}

		// Token: 0x04004B70 RID: 19312
		private const int DefaultDisabledRegistryValue = 0;

		// Token: 0x04004B71 RID: 19313
		protected readonly T ConfigSettings;

		// Token: 0x04004B72 RID: 19314
		protected bool enabled = true;

		// Token: 0x04004B73 RID: 19315
		protected TimeSpan refreshInterval;

		// Token: 0x04004B74 RID: 19316
		protected int? overrideMetricValue = null;

		// Token: 0x04004B75 RID: 19317
		private static readonly Trace Tracer = ExTraceGlobals.ResourceHealthManagerTracer;
	}
}
