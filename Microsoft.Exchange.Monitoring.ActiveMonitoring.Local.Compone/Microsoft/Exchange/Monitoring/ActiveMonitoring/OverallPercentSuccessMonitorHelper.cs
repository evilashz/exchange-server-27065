using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008D RID: 141
	internal class OverallPercentSuccessMonitorHelper : MonitorDefinitionHelper
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0001E408 File Offset: 0x0001C608
		internal override MonitorDefinition CreateDefinition()
		{
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition(base.Name, base.SampleMask, base.ServiceName, base.Component, base.MonitoringThreshold, TimeSpan.FromSeconds((double)base.MonitoringIntervalSeconds), base.MinimumErrorCount, base.Enabled);
			base.GetAdditionalProperties(monitorDefinition);
			return monitorDefinition;
		}
	}
}
