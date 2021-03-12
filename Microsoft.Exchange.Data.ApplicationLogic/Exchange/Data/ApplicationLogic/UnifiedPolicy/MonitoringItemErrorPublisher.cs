using System;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Data.ApplicationLogic.UnifiedPolicy
{
	// Token: 0x020001C3 RID: 451
	internal sealed class MonitoringItemErrorPublisher : IMonitoringNotification
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000477EE File Offset: 0x000459EE
		public static MonitoringItemErrorPublisher Instance
		{
			get
			{
				return MonitoringItemErrorPublisher.instance;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000477F8 File Offset: 0x000459F8
		public void PublishEvent(string componentName, string organization, string context, Exception exception)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("componentName", componentName);
			ArgumentValidator.ThrowIfNullOrEmpty("organization", organization);
			string arg = (exception != null) ? exception.ToString() : "<>";
			EventNotificationItem.Publish(ExchangeComponent.UnifiedPolicy.Name, componentName, null, string.Format("Policy sync issues identified for Tenant {0}.\r\nContext: {1}.\r\nError: {2}.", organization, context, arg), ResultSeverityLevel.Error, false);
		}

		// Token: 0x0400092A RID: 2346
		private static readonly MonitoringItemErrorPublisher instance = new MonitoringItemErrorPublisher();
	}
}
