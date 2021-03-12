using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Configuration.TenantMonitoring
{
	// Token: 0x0200000B RID: 11
	internal static class TenantMonitor
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003C89 File Offset: 0x00001E89
		public static void LogActivity(CounterType counterType, string organizationName)
		{
			if (TenantMonitor.counterCategoryExist)
			{
				IntervalCounterInstanceCache.IncrementIntervalCounter(organizationName ?? "First Organization", counterType);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003CA2 File Offset: 0x00001EA2
		private static MSExchangeTenantMonitoringInstance GetInstance(string instanceName)
		{
			return MSExchangeTenantMonitoring.GetInstance(instanceName);
		}

		// Token: 0x04000033 RID: 51
		public const string TenantMonitoringRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange Tenant Monitoring";

		// Token: 0x04000034 RID: 52
		public const string TenantMonitoringCounterCategory = "MSExchangeTenantMonitoring";

		// Token: 0x04000035 RID: 53
		public const string DefaultTenantName = "First Organization";

		// Token: 0x04000036 RID: 54
		private static bool counterCategoryExist = PerformanceCounterCategory.Exists("MSExchangeTenantMonitoring");
	}
}
