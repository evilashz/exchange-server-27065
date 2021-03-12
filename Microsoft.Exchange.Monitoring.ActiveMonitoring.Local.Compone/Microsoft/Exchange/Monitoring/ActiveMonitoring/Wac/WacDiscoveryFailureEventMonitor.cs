using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Wac
{
	// Token: 0x0200051D RID: 1309
	public class WacDiscoveryFailureEventMonitor : OverallXFailuresMonitor
	{
		// Token: 0x06002043 RID: 8259 RVA: 0x000C56C4 File Offset: 0x000C38C4
		public new static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, int monitoringInterval, int recurrenceInterval, int numberOfFailures, bool enabled = true)
		{
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(name, sampleMask, serviceName, component, monitoringInterval, recurrenceInterval, numberOfFailures, enabled);
			monitorDefinition.AssemblyPath = WacDiscoveryFailureEventMonitor.AssemblyPath;
			monitorDefinition.TypeName = WacDiscoveryFailureEventMonitor.TypeName;
			return monitorDefinition;
		}

		// Token: 0x040017B6 RID: 6070
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040017B7 RID: 6071
		private static readonly string TypeName = typeof(WacDiscoveryFailureEventMonitor).FullName;
	}
}
