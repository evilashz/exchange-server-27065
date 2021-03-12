using System;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000A9 RID: 169
	public class OverallConsecutiveSumAboveThresholdMonitor : OverallConsecutiveAggregationMonitor
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x000231C0 File Offset: 0x000213C0
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string targetResouce, Component component, int failureCount, int monitoringInterval, int recurrenceInterval)
		{
			return new MonitorDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				TypeName = typeof(OverallConsecutiveSumAboveThresholdMonitor).FullName,
				Name = name,
				SampleMask = sampleMask,
				TargetResource = targetResouce,
				Component = component,
				MonitoringThreshold = (double)failureCount,
				MonitoringIntervalSeconds = monitoringInterval,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = recurrenceInterval * 2,
				ServiceName = component.Name,
				MaxRetryAttempts = 3
			};
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002324C File Offset: 0x0002144C
		protected override bool ShouldAlert()
		{
			int num = base.ProbeValueHistory.Sum();
			if (base.ProbeValueHistory.Length > base.NumberOfValuesToStore)
			{
				num -= base.ProbeValueHistory[0];
			}
			base.Result.NewValue = (double)base.LastProbeValue;
			base.Result.TotalValue = (double)num;
			return (double)num >= base.Definition.MonitoringThreshold;
		}
	}
}
