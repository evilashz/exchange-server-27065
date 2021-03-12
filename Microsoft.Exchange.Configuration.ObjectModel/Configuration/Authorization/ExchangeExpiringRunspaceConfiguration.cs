using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000227 RID: 551
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeExpiringRunspaceConfiguration : ExchangeRunspaceConfiguration, IExpiringRunspaceConfiguration
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00045C7C File Offset: 0x00043E7C
		public ExchangeExpiringRunspaceConfiguration(IIdentity identity, ExchangeRunspaceConfigurationSettings settings) : this(identity, settings, false)
		{
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00045C88 File Offset: 0x00043E88
		public ExchangeExpiringRunspaceConfiguration(IIdentity identity, ExchangeRunspaceConfigurationSettings settings, bool isPowerShellWebService) : base(identity, null, settings, null, null, null, false, isPowerShellWebService, false, SnapinSet.Default)
		{
			this.SetMaxAgeLimit(ExpirationLimit.RunspaceRefresh);
			this.SetMaxAgeLimit(ExpirationLimit.ExternalAccountRunspaceTermination);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00045CBF File Offset: 0x00043EBF
		private bool IsMaximumAgeLimitExceeded(ExpirationLimit limit)
		{
			return DateTime.UtcNow >= this.maxAgeLimits[(int)limit];
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00045CDC File Offset: 0x00043EDC
		public static TimeSpan GetMaximumAgeLimit(ExpirationLimit limit)
		{
			ExchangeExpiringRunspaceConfiguration.RefreshLimitsFromRegistryIfNeeded();
			return ExchangeExpiringRunspaceConfiguration.ExpiryPeriods[(int)limit];
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00045CF4 File Offset: 0x00043EF4
		private static void RefreshLimitsFromRegistryIfNeeded()
		{
			if (DateTime.UtcNow >= ExchangeExpiringRunspaceConfiguration.ExpiryRefreshTime)
			{
				lock (ExchangeExpiringRunspaceConfiguration.syncRoot)
				{
					if (DateTime.UtcNow >= ExchangeExpiringRunspaceConfiguration.ExpiryRefreshTime)
					{
						ExchangeExpiringRunspaceConfiguration.ExpiryPeriods[0] = ExchangeExpiringRunspaceConfiguration.GetMaximumAgeLimitFromRegistry(ExpirationLimit.RunspaceRefresh);
						ExchangeExpiringRunspaceConfiguration.ExpiryPeriods[1] = ExchangeExpiringRunspaceConfiguration.GetMaximumAgeLimitFromRegistry(ExpirationLimit.ExternalAccountRunspaceTermination);
						ExchangeExpiringRunspaceConfiguration.ExpiryRefreshTime = DateTime.UtcNow.AddMinutes(5.0);
					}
				}
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00045D98 File Offset: 0x00043F98
		internal override ScopeSet CalculateScopeSetForExchangeCmdlet(string exchangeCmdletName, IList<string> parameters, OrganizationId organizationId, Task.ErrorLoggerDelegate writeError)
		{
			if (this.IsMaximumAgeLimitExceeded(ExpirationLimit.RunspaceRefresh))
			{
				base.LoadRoleCmdletInfo();
				this.SetMaxAgeLimit(ExpirationLimit.RunspaceRefresh);
				base.RefreshProvisioningBroker();
			}
			return base.CalculateScopeSetForExchangeCmdlet(exchangeCmdletName, parameters, organizationId, writeError);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00045DC1 File Offset: 0x00043FC1
		public bool ShouldCloseDueToExpiration()
		{
			return (base.HasLinkedRoleGroups || base.DelegatedPrincipal != null) && this.IsMaximumAgeLimitExceeded(ExpirationLimit.ExternalAccountRunspaceTermination);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00045DDC File Offset: 0x00043FDC
		private static TimeSpan GetMaximumAgeLimitFromRegistry(ExpirationLimit limit)
		{
			TimeSpan result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange RBAC", false))
			{
				int num;
				string name;
				switch (limit)
				{
				case ExpirationLimit.RunspaceRefresh:
					num = 60;
					name = "MaximumAge";
					break;
				case ExpirationLimit.ExternalAccountRunspaceTermination:
					num = 1440;
					name = "MaximumAgeExternalAccount";
					break;
				default:
					throw new ArgumentOutOfRangeException("limit");
				}
				if (registryKey != null)
				{
					object value = registryKey.GetValue(name);
					if (value != null && value is int)
					{
						int num2 = (int)value;
						if (num2 > 0)
						{
							num = num2;
						}
					}
				}
				result = TimeSpan.FromMinutes((double)num);
			}
			return result;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00045E84 File Offset: 0x00044084
		private void SetMaxAgeLimit(ExpirationLimit limit)
		{
			this.maxAgeLimits[(int)limit] = DateTime.UtcNow.Add(ExchangeExpiringRunspaceConfiguration.GetMaximumAgeLimit(limit));
		}

		// Token: 0x0400052C RID: 1324
		private const string RbacConfigurationKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchange RBAC";

		// Token: 0x0400052D RID: 1325
		private const string MaximumAgeKey = "MaximumAge";

		// Token: 0x0400052E RID: 1326
		private const string MaximumAgeExternalAccountKey = "MaximumAgeExternalAccount";

		// Token: 0x0400052F RID: 1327
		private const int DefaultMaximumAge = 60;

		// Token: 0x04000530 RID: 1328
		private const int DefaultMaximumAgeExternalAccount = 1440;

		// Token: 0x04000531 RID: 1329
		private static TimeSpan[] ExpiryPeriods = new TimeSpan[2];

		// Token: 0x04000532 RID: 1330
		private static DateTime ExpiryRefreshTime = DateTime.UtcNow;

		// Token: 0x04000533 RID: 1331
		private static object syncRoot = new object();

		// Token: 0x04000534 RID: 1332
		private DateTime[] maxAgeLimits = new DateTime[2];
	}
}
