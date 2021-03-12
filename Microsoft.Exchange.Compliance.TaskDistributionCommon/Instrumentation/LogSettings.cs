using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x0200001B RID: 27
	internal static class LogSettings
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004D83 File Offset: 0x00002F83
		public static bool IsMonitored(string name)
		{
			return LogSettings.monitoredEvents.Contains(name);
		}

		// Token: 0x0400004A RID: 74
		public const string DefaultServiceName = "EDiscovery";

		// Token: 0x0400004B RID: 75
		public const string DefaultComponentName = "Unknown";

		// Token: 0x0400004C RID: 76
		private static HashSet<string> monitoredEvents = new HashSet<string>
		{
			NotificationItem.GenerateResultName("EDiscovery", "Error", "Error")
		};
	}
}
