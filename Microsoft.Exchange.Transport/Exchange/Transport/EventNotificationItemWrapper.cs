using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200001E RID: 30
	internal class EventNotificationItemWrapper : IEventNotificationItem
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003CB9 File Offset: 0x00001EB9
		public void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity, bool throwOnError)
		{
			EventNotificationItem.Publish(serviceName, component, tag, notificationReason, severity, throwOnError);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public void Publish(string serviceName, string component, string tag, string notificationReason, string stateAttribute1, ResultSeverityLevel severity, bool throwOnError)
		{
			EventNotificationItem.Publish(serviceName, component, tag, notificationReason, stateAttribute1, severity, throwOnError);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003CDB File Offset: 0x00001EDB
		public void PublishPeriodic(string serviceName, string component, string tag, string notificationReason, string periodicKey, TimeSpan period, ResultSeverityLevel severity, bool throwOnError)
		{
			EventNotificationItem.PublishPeriodic(serviceName, component, tag, notificationReason, periodicKey, period, severity, throwOnError);
		}
	}
}
