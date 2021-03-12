using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200001D RID: 29
	internal interface IEventNotificationItem
	{
		// Token: 0x060000A4 RID: 164
		void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity, bool throwOnError);

		// Token: 0x060000A5 RID: 165
		void Publish(string serviceName, string component, string tag, string notificationReason, string stateAttribute1, ResultSeverityLevel severity, bool throwOnError);

		// Token: 0x060000A6 RID: 166
		void PublishPeriodic(string serviceName, string component, string tag, string notificationReason, string periodicKey, TimeSpan period, ResultSeverityLevel severity, bool throwOnError);
	}
}
