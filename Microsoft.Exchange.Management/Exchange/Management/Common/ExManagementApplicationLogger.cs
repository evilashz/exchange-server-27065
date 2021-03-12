using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000FB RID: 251
	internal static class ExManagementApplicationLogger
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x0001F756 File Offset: 0x0001D956
		public static void LogEvent(ExEventLog.EventTuple eventInfo, params string[] messageArguments)
		{
			ExManagementApplicationLogger.eventLogger.LogEvent(eventInfo, null, messageArguments);
			ExTraceGlobals.EventTracer.Information<string[]>(0L, eventInfo.ToString(), messageArguments);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001F780 File Offset: 0x0001D980
		public static void LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple eventInfo, params string[] messageArguments)
		{
			ExManagementApplicationLogger.eventLogger.LogEvent(organizationId, eventInfo, null, messageArguments);
			ExTraceGlobals.EventTracer.Information<string[]>(0L, eventInfo.ToString(), messageArguments);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001F7AB File Offset: 0x0001D9AB
		public static bool IsLowEventCategoryEnabled(short eventCategory)
		{
			return ExManagementApplicationLogger.eventLogger.IsEventCategoryEnabled(eventCategory, ExEventLog.EventLevel.Low);
		}

		// Token: 0x0400037E RID: 894
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.LogTracer.Category, "MSExchange Management Application");
	}
}
