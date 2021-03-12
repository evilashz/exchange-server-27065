using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020001DA RID: 474
	public sealed class MaintenanceTimeoutMonitor : MaintenanceMonitor
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x0005825C File Offset: 0x0005645C
		internal static MonitorDefinition CreateDefinition(Component component)
		{
			return new MonitorDefinition
			{
				AssemblyPath = MaintenanceTimeoutMonitor.AssemblyPath,
				TypeName = MaintenanceTimeoutMonitor.TypeName,
				Name = string.Format("{0}.{1}", "MaintenanceTimeoutMonitor", component.Name),
				SampleMask = NotificationItem.GenerateResultName(component.Name, MonitoringNotificationEvent.MaintenanceTimeout.ToString(), null),
				ServiceName = component.Name,
				Component = component,
				Enabled = true,
				TimeoutSeconds = 30,
				MonitoringIntervalSeconds = 1800,
				RecurrenceIntervalSeconds = 300,
				SecondaryMonitoringThreshold = 15.0
			};
		}

		// Token: 0x040009E3 RID: 2531
		internal const string MaintenanceTimeoutMonitorName = "MaintenanceTimeoutMonitor";

		// Token: 0x040009E4 RID: 2532
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040009E5 RID: 2533
		private static readonly string TypeName = typeof(MaintenanceTimeoutMonitor).FullName;
	}
}
