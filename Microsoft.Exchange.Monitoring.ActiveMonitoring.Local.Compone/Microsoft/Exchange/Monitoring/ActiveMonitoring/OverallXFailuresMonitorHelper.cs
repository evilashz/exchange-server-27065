using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008E RID: 142
	internal class OverallXFailuresMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x0001E464 File Offset: 0x0001C664
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(base.Name, base.SampleMask, base.ServiceName, base.Component, base.MonitoringIntervalSeconds, base.RecurrenceIntervalSeconds, (int)base.MonitoringThreshold, base.Enabled);
			base.GetAdditionalProperties(monitorDefinition);
			return monitorDefinition;
		}
	}
}
