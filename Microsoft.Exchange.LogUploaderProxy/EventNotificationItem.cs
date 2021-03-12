using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000006 RID: 6
	public class EventNotificationItem
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000211F File Offset: 0x0000031F
		public static void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			EventNotificationItem.Publish(serviceName, component, tag, notificationReason, (ResultSeverityLevel)severity, throwOnError);
		}
	}
}
