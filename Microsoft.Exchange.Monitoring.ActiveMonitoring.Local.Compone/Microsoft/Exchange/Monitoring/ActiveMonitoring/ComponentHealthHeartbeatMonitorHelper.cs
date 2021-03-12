using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000078 RID: 120
	internal class ComponentHealthHeartbeatMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0001AFF0 File Offset: 0x000191F0
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = ComponentHealthHeartbeatMonitor.CreateDefinition(base.Component, base.RecurrenceIntervalSeconds, base.TimeoutSeconds, base.MaxRetryAttempts, base.MonitoringIntervalSeconds, base.MonitoringThreshold);
			base.GetAdditionalProperties(monitorDefinition);
			return monitorDefinition;
		}
	}
}
