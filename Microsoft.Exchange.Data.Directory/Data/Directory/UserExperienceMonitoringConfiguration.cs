using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000291 RID: 657
	internal class UserExperienceMonitoringConfiguration
	{
		// Token: 0x06001EEB RID: 7915 RVA: 0x0008A5FA File Offset: 0x000887FA
		public UserExperienceMonitoringConfiguration()
		{
			this.registryWatcher = new RegistryWatcher(UserExperienceMonitoringConfiguration.RegistryPath, false);
			this.LoadConfiguration();
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0008A61C File Offset: 0x0008881C
		public bool Enabled
		{
			get
			{
				bool config = TenantRelocationConfigImpl.GetConfig<bool>("IsUserExperienceTestEnabled");
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool>((long)this.GetHashCode(), "Global Config: UserExperienceMonitoringConfiguration::Enabled: {0}.", config);
				return config;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0008A64C File Offset: 0x0008884C
		public int MonitorAccountExpiredDays
		{
			get
			{
				int config = TenantRelocationConfigImpl.GetConfig<int>("UXMonitorAccountExpiredDays");
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)this.GetHashCode(), "Global Config: UserExperienceMonitoringConfiguration::MonitorAccountExpiredDays: {0}.", config);
				return config;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0008A67C File Offset: 0x0008887C
		public int ReplicationTimeoutForRemoveMonitoringAccount
		{
			get
			{
				int config = TenantRelocationConfigImpl.GetConfig<int>("RemoveUXMonitorAccountWaitReplicationMinutes");
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)this.GetHashCode(), "Global Config: UserExperienceMonitoringConfiguration::ReplicationTimeoutForRemoveMonitoringAccount: {0}.", config);
				return config;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0008A6AC File Offset: 0x000888AC
		public int WaitProbeFailureReadableTimeout
		{
			get
			{
				int config = TenantRelocationConfigImpl.GetConfig<int>("WaitUXFailureResultSeconds");
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)this.GetHashCode(), "Global Config: UserExperienceMonitoringConfiguration::WaitProbeFailureReadableTimeout: {0}.", config);
				return config;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0008A6DC File Offset: 0x000888DC
		public bool IsLockdownOnly
		{
			get
			{
				this.DetechChangeAndReloadConfiguration();
				return this.isLockdownOnly;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0008A6EA File Offset: 0x000888EA
		public IList<UserExperienceMonitoringSupportedComponent> DisabledComponent
		{
			get
			{
				return UserExperienceMonitoringConfiguration.GetDisabledComponent(TenantRelocationConfigImpl.GetConfig<string>("DisabledUXProbes"));
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0008A6FB File Offset: 0x000888FB
		public int SupportedComponentCount
		{
			get
			{
				return Enum.GetValues(typeof(UserExperienceMonitoringSupportedComponent)).Length - this.DisabledComponent.Count;
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0008A720 File Offset: 0x00088920
		public static IList<UserExperienceMonitoringSupportedComponent> GetTenantUserExperienceTargetComponents(string tenant, string servicePlan)
		{
			IList<UserExperienceMonitoringSupportedComponent> supportedComponentInTenant = UserExperienceMonitoringConfiguration.GetSupportedComponentInTenant(tenant, servicePlan);
			if (supportedComponentInTenant == null)
			{
				return null;
			}
			if (UserExperienceMonitoringConfiguration.Config.Value.DisabledComponent == null)
			{
				return supportedComponentInTenant;
			}
			foreach (UserExperienceMonitoringSupportedComponent item in UserExperienceMonitoringConfiguration.Config.Value.DisabledComponent)
			{
				if (supportedComponentInTenant.Contains(item))
				{
					supportedComponentInTenant.Remove(item);
				}
			}
			return supportedComponentInTenant;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0008A7A4 File Offset: 0x000889A4
		private static IList<UserExperienceMonitoringSupportedComponent> GetSupportedComponentInTenant(string tenant, string servicePlan)
		{
			List<UserExperienceMonitoringSupportedComponent> list = new List<UserExperienceMonitoringSupportedComponent>();
			list.Add(UserExperienceMonitoringSupportedComponent.Owa);
			list.Add(UserExperienceMonitoringSupportedComponent.Outlook);
			list.Add(UserExperienceMonitoringSupportedComponent.ActiveSync);
			list.Add(UserExperienceMonitoringSupportedComponent.AutoDiscover);
			list.Add(UserExperienceMonitoringSupportedComponent.Transport);
			if (servicePlan.StartsWith("BPOS_Basic_E"))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug(0L, "Skip components {0}, {1} and {2} for BPOS_Basic tenants {3}.", new object[]
				{
					UserExperienceMonitoringSupportedComponent.Outlook,
					UserExperienceMonitoringSupportedComponent.ActiveSync,
					UserExperienceMonitoringSupportedComponent.AutoDiscover,
					tenant
				});
				list.Remove(UserExperienceMonitoringSupportedComponent.Outlook);
				list.Remove(UserExperienceMonitoringSupportedComponent.ActiveSync);
				list.Remove(UserExperienceMonitoringSupportedComponent.AutoDiscover);
			}
			return list;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0008A838 File Offset: 0x00088A38
		private void DetechChangeAndReloadConfiguration()
		{
			if (this.registryWatcher.IsChanged())
			{
				this.LoadConfiguration();
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0008A84D File Offset: 0x00088A4D
		private void LoadConfiguration()
		{
			this.isLockdownOnly = (Globals.GetIntValueFromRegistry(UserExperienceMonitoringConfiguration.RegistryPath, UserExperienceMonitoringConfiguration.UserExperienceMonitoringLockdownOnlyRegistryName, 0, 0) > 0);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0008A86C File Offset: 0x00088A6C
		private static IList<UserExperienceMonitoringSupportedComponent> GetDisabledComponent(string globalConfigValue)
		{
			IList<UserExperienceMonitoringSupportedComponent> list = new List<UserExperienceMonitoringSupportedComponent>();
			if (globalConfigValue != null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>(0L, "Global Config: UserExperienceMonitoringConfiguration::GetDisabledComponent: {0}.", globalConfigValue);
				string[] array = globalConfigValue.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					UserExperienceMonitoringSupportedComponent item;
					if (Enum.TryParse<UserExperienceMonitoringSupportedComponent>(text, out item))
					{
						list.Add(item);
					}
					else
					{
						ExTraceGlobals.TenantRelocationTracer.TraceWarning((long)default(Guid).GetHashCode(), string.Format("Failed to parse the configured disabled component {0}, which will be ignored.", text));
					}
				}
			}
			return list;
		}

		// Token: 0x04001274 RID: 4724
		private static readonly string RegistryPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x04001275 RID: 4725
		private static readonly string UserExperienceMonitoringLockdownOnlyRegistryName = "TenantRelocationUserExperienceLockdownOnly";

		// Token: 0x04001276 RID: 4726
		private bool isLockdownOnly;

		// Token: 0x04001277 RID: 4727
		private RegistryWatcher registryWatcher;

		// Token: 0x04001278 RID: 4728
		public static readonly Lazy<UserExperienceMonitoringConfiguration> Config = new Lazy<UserExperienceMonitoringConfiguration>(() => new UserExperienceMonitoringConfiguration());
	}
}
