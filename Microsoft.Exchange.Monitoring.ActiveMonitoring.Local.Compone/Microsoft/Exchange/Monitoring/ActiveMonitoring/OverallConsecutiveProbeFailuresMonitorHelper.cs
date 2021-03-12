using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008A RID: 138
	internal class OverallConsecutiveProbeFailuresMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(base.Name, base.SampleMask, base.ServiceName, base.Component, (int)base.MonitoringThreshold, base.Enabled, (base.MonitoringIntervalSeconds > 0) ? base.MonitoringIntervalSeconds : 300);
			base.GetAdditionalProperties(monitorDefinition);
			return monitorDefinition;
		}
	}
}
