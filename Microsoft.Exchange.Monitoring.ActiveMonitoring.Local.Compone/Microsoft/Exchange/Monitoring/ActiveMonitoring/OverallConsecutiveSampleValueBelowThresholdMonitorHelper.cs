using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008C RID: 140
	internal class OverallConsecutiveSampleValueBelowThresholdMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueBelowThresholdMonitor.CreateDefinition(base.Name, base.SampleMask, base.ServiceName, base.Component, base.MonitoringThreshold, (int)base.SecondaryMonitoringThreshold, base.Enabled);
			base.GetAdditionalProperties(monitorDefinition);
			monitorDefinition.RecurrenceIntervalSeconds = base.RecurrenceIntervalSeconds;
			return monitorDefinition;
		}
	}
}
