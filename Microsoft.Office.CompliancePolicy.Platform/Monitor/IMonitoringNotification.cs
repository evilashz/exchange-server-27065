using System;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x0200007C RID: 124
	public interface IMonitoringNotification
	{
		// Token: 0x0600033D RID: 829
		void PublishEvent(string componentName, string organization, string context, Exception exception);
	}
}
