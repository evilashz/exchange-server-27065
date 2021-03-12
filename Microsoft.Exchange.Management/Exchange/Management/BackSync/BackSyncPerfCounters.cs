using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x02000DAB RID: 3499
	internal static class BackSyncPerfCounters
	{
		// Token: 0x06008617 RID: 34327 RVA: 0x00224590 File Offset: 0x00222790
		public static void GetPerfCounterInfo(XElement element)
		{
			if (BackSyncPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in BackSyncPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04004104 RID: 16644
		public const string CategoryName = "MSExchange Back Sync";

		// Token: 0x04004105 RID: 16645
		public static readonly ExPerformanceCounter DeltaSyncTime = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004106 RID: 16646
		public static readonly ExPerformanceCounter DeltaSyncTimeSinceLast = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Time Since Last", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004107 RID: 16647
		public static readonly ExPerformanceCounter DeltaSyncResultSuccess = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Result Success", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004108 RID: 16648
		public static readonly ExPerformanceCounter DeltaSyncResultSystemError = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Result System Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004109 RID: 16649
		public static readonly ExPerformanceCounter DeltaSyncResultUserError = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Result User Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410A RID: 16650
		public static readonly ExPerformanceCounter DeltaSyncSuccessRate = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Success Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410B RID: 16651
		public static readonly ExPerformanceCounter DeltaSyncSuccessBase = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Success Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410C RID: 16652
		public static readonly ExPerformanceCounter DeltaSyncSystemErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync System Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410D RID: 16653
		public static readonly ExPerformanceCounter DeltaSyncSystemErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync System Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410E RID: 16654
		public static readonly ExPerformanceCounter DeltaSyncUserErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync User Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400410F RID: 16655
		public static readonly ExPerformanceCounter DeltaSyncUserErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync User Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004110 RID: 16656
		public static readonly ExPerformanceCounter DeltaSyncCount = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004111 RID: 16657
		public static readonly ExPerformanceCounter DeltaSyncChangeCount = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Change Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004112 RID: 16658
		public static readonly ExPerformanceCounter DeltaSyncRetryCookieCount = new ExPerformanceCounter("MSExchange Back Sync", "Delta Sync Retry Cookie Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004113 RID: 16659
		public static readonly ExPerformanceCounter ObjectFullSyncTime = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004114 RID: 16660
		public static readonly ExPerformanceCounter ObjectFullSyncTimeSinceLast = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Time Since Last", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004115 RID: 16661
		public static readonly ExPerformanceCounter ObjectFullSyncResultSuccess = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Result Success", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004116 RID: 16662
		public static readonly ExPerformanceCounter ObjectFullSyncResultSystemError = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Result System Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004117 RID: 16663
		public static readonly ExPerformanceCounter ObjectFullSyncResultUserError = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Result User Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004118 RID: 16664
		public static readonly ExPerformanceCounter ObjectFullSyncSuccessRate = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Success Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004119 RID: 16665
		public static readonly ExPerformanceCounter ObjectFullSyncSuccessBase = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Success Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411A RID: 16666
		public static readonly ExPerformanceCounter ObjectFullSyncSystemErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync System Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411B RID: 16667
		public static readonly ExPerformanceCounter ObjectFullSyncSystemErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync System Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411C RID: 16668
		public static readonly ExPerformanceCounter ObjectFullSyncUserErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync User Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411D RID: 16669
		public static readonly ExPerformanceCounter ObjectFullSyncUserErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync User Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411E RID: 16670
		public static readonly ExPerformanceCounter ObjectFullSyncCount = new ExPerformanceCounter("MSExchange Back Sync", "Object Full Sync Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400411F RID: 16671
		public static readonly ExPerformanceCounter TenantFullSyncTime = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004120 RID: 16672
		public static readonly ExPerformanceCounter TenantFullSyncTimeSinceLast = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Time Since Last", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004121 RID: 16673
		public static readonly ExPerformanceCounter TenantFullSyncResultSuccess = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Result Success", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004122 RID: 16674
		public static readonly ExPerformanceCounter TenantFullSyncResultSystemError = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Result System Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004123 RID: 16675
		public static readonly ExPerformanceCounter TenantFullSyncResultUserError = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Result User Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004124 RID: 16676
		public static readonly ExPerformanceCounter TenantFullSyncSuccessRate = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Success Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004125 RID: 16677
		public static readonly ExPerformanceCounter TenantFullSyncSuccessBase = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Success Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004126 RID: 16678
		public static readonly ExPerformanceCounter TenantFullSyncSystemErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync System Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004127 RID: 16679
		public static readonly ExPerformanceCounter TenantFullSyncSystemErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync System Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004128 RID: 16680
		public static readonly ExPerformanceCounter TenantFullSyncUserErrorRate = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync User Error Rate", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004129 RID: 16681
		public static readonly ExPerformanceCounter TenantFullSyncUserErrorBase = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync User Error Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400412A RID: 16682
		public static readonly ExPerformanceCounter TenantFullSyncCount = new ExPerformanceCounter("MSExchange Back Sync", "Tenant Full Sync Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400412B RID: 16683
		public static readonly ExPerformanceCounter BackLogCount = new ExPerformanceCounter("MSExchange Back Sync", "Back Log Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400412C RID: 16684
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			BackSyncPerfCounters.DeltaSyncTime,
			BackSyncPerfCounters.DeltaSyncTimeSinceLast,
			BackSyncPerfCounters.DeltaSyncResultSuccess,
			BackSyncPerfCounters.DeltaSyncResultSystemError,
			BackSyncPerfCounters.DeltaSyncResultUserError,
			BackSyncPerfCounters.DeltaSyncSuccessRate,
			BackSyncPerfCounters.DeltaSyncSuccessBase,
			BackSyncPerfCounters.DeltaSyncSystemErrorRate,
			BackSyncPerfCounters.DeltaSyncSystemErrorBase,
			BackSyncPerfCounters.DeltaSyncUserErrorRate,
			BackSyncPerfCounters.DeltaSyncUserErrorBase,
			BackSyncPerfCounters.DeltaSyncCount,
			BackSyncPerfCounters.DeltaSyncChangeCount,
			BackSyncPerfCounters.DeltaSyncRetryCookieCount,
			BackSyncPerfCounters.ObjectFullSyncTime,
			BackSyncPerfCounters.ObjectFullSyncTimeSinceLast,
			BackSyncPerfCounters.ObjectFullSyncResultSuccess,
			BackSyncPerfCounters.ObjectFullSyncResultSystemError,
			BackSyncPerfCounters.ObjectFullSyncResultUserError,
			BackSyncPerfCounters.ObjectFullSyncSuccessRate,
			BackSyncPerfCounters.ObjectFullSyncSuccessBase,
			BackSyncPerfCounters.ObjectFullSyncSystemErrorRate,
			BackSyncPerfCounters.ObjectFullSyncSystemErrorBase,
			BackSyncPerfCounters.ObjectFullSyncUserErrorRate,
			BackSyncPerfCounters.ObjectFullSyncUserErrorBase,
			BackSyncPerfCounters.ObjectFullSyncCount,
			BackSyncPerfCounters.TenantFullSyncTime,
			BackSyncPerfCounters.TenantFullSyncTimeSinceLast,
			BackSyncPerfCounters.TenantFullSyncResultSuccess,
			BackSyncPerfCounters.TenantFullSyncResultSystemError,
			BackSyncPerfCounters.TenantFullSyncResultUserError,
			BackSyncPerfCounters.TenantFullSyncSuccessRate,
			BackSyncPerfCounters.TenantFullSyncSuccessBase,
			BackSyncPerfCounters.TenantFullSyncSystemErrorRate,
			BackSyncPerfCounters.TenantFullSyncSystemErrorBase,
			BackSyncPerfCounters.TenantFullSyncUserErrorRate,
			BackSyncPerfCounters.TenantFullSyncUserErrorBase,
			BackSyncPerfCounters.TenantFullSyncCount,
			BackSyncPerfCounters.BackLogCount
		};
	}
}
