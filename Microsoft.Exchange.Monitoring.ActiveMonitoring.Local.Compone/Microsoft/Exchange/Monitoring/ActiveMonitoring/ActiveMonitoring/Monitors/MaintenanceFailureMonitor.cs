using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020001D9 RID: 473
	public sealed class MaintenanceFailureMonitor : MaintenanceMonitor
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x00058184 File Offset: 0x00056384
		internal static MonitorDefinition CreateDefinition(Component component)
		{
			return new MonitorDefinition
			{
				AssemblyPath = MaintenanceFailureMonitor.AssemblyPath,
				TypeName = MaintenanceFailureMonitor.TypeName,
				Name = string.Format("{0}.{1}", "MaintenanceFailureMonitor", component.Name),
				SampleMask = NotificationItem.GenerateResultName(component.Name, MonitoringNotificationEvent.MaintenanceFailure.ToString(), null),
				ServiceName = component.Name,
				Component = component,
				Enabled = true,
				TimeoutSeconds = 30,
				MonitoringIntervalSeconds = 1800,
				RecurrenceIntervalSeconds = 300,
				SecondaryMonitoringThreshold = 15.0
			};
		}

		// Token: 0x040009E0 RID: 2528
		internal const string MaintenanceFailureMonitorName = "MaintenanceFailureMonitor";

		// Token: 0x040009E1 RID: 2529
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040009E2 RID: 2530
		private static readonly string TypeName = typeof(MaintenanceFailureMonitor).FullName;
	}
}
