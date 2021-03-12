using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Logging
{
	// Token: 0x0200001F RID: 31
	internal static class LogSettings
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public static bool IsMonitored(string name)
		{
			return LogSettings.monitoredEvents.Contains(name);
		}

		// Token: 0x040000E2 RID: 226
		public const string DefaultServiceName = "EDiscovery";

		// Token: 0x040000E3 RID: 227
		public const string DefaultComponentName = "Unknown";

		// Token: 0x040000E4 RID: 228
		private static HashSet<string> monitoredEvents = new HashSet<string>
		{
			NotificationItem.GenerateResultName("EDiscovery", "Error", "Error"),
			NotificationItem.GenerateResultName(ExchangeComponent.EdiscoveryProtocol.Name, "Search.FailureMonitor", null),
			NotificationItem.GenerateResultName(ExchangeComponent.EdiscoveryProtocol.Name, "Mailbox.FailureMonitor", null)
		};
	}
}
