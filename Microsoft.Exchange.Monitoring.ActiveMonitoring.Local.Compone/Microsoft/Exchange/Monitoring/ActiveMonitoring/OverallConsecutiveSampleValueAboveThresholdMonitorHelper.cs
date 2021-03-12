using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008B RID: 139
	internal class OverallConsecutiveSampleValueAboveThresholdMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0001E350 File Offset: 0x0001C550
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueAboveThresholdMonitor.CreateDefinition(base.Name, base.SampleMask, base.ServiceName, base.Component, base.MonitoringThreshold, (int)base.SecondaryMonitoringThreshold, base.Enabled);
			base.GetAdditionalProperties(monitorDefinition);
			monitorDefinition.RecurrenceIntervalSeconds = base.RecurrenceIntervalSeconds;
			return monitorDefinition;
		}
	}
}
