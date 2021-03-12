using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000079 RID: 121
	internal class ComponentHealthPercentFailureMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0001B038 File Offset: 0x00019238
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = ComponentHealthPercentFailureMonitor.CreateDefinition(base.Component, base.RecurrenceIntervalSeconds, base.TimeoutSeconds, base.MaxRetryAttempts, base.MonitoringIntervalSeconds, base.MonitoringThreshold);
			base.GetAdditionalProperties(monitorDefinition);
			return monitorDefinition;
		}
	}
}
