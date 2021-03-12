using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200004D RID: 77
	public class OverallConsecutiveSampleValueAboveThresholdMonitor : OverallConsecutiveFailuresMonitor
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00013D8C File Offset: 0x00011F8C
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, double threshold, int numberOfSamples, bool enabled = true)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = OverallConsecutiveSampleValueAboveThresholdMonitor.AssemblyPath;
			monitorDefinition.TypeName = OverallConsecutiveSampleValueAboveThresholdMonitor.TypeName;
			monitorDefinition.Name = name;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.ServiceName = serviceName;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TimeoutSeconds = 30;
			monitorDefinition.Component = component;
			monitorDefinition.MonitoringThreshold = threshold;
			monitorDefinition.SecondaryMonitoringThreshold = (double)numberOfSamples;
			monitorDefinition.MonitoringIntervalSeconds = (numberOfSamples + 1) * 300;
			monitorDefinition.RecurrenceIntervalSeconds = monitorDefinition.MonitoringIntervalSeconds / 2;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, OverallConsecutiveSampleValueAboveThresholdMonitor.DefaultInsufficientSamplesIntervalSeconds);
			return monitorDefinition;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00013E30 File Offset: 0x00012030
		protected override bool ShouldAlert()
		{
			return base.Result.TotalValue >= base.Definition.SecondaryMonitoringThreshold;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00013E50 File Offset: 0x00012050
		protected override bool HaveInsufficientSamples()
		{
			double num = 0.0;
			if (base.Definition.Attributes.ContainsKey("MinimumSampleCount"))
			{
				num = double.Parse(base.Definition.Attributes["MinimumSampleCount"]);
			}
			return (double)base.Result.TotalSampleCount < num;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013EC8 File Offset: 0x000120C8
		protected override Task SetConsecutiveFailureNumbers(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveSampleValueAboveThresholdMonitor.SetConsecutiveFailureNumbers: Getting overall consecutive samples of: {0}.", base.Definition.SampleMask, null, "SetConsecutiveFailureNumbers", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallConsecutiveSampleValueAboveThresholdMonitor.cs", 113);
			Task consecutiveSampleValueAboveThresholdCounts = base.GetConsecutiveSampleValueAboveThresholdCounts(base.Definition.SampleMask, (double)((int)base.Definition.MonitoringThreshold), (int)base.Definition.SecondaryMonitoringThreshold, delegate(int newValue)
			{
				base.Result.NewValue = (double)newValue;
			}, delegate(int totalValue)
			{
				base.Result.TotalValue = (double)totalValue;
			}, cancellationToken);
			this.SetStateAttribute6ForScopeMonitoring(base.Result.NewValue);
			return consecutiveSampleValueAboveThresholdCounts;
		}

		// Token: 0x04000390 RID: 912
		private const string MinimumSampleCount = "MinimumSampleCount";

		// Token: 0x04000391 RID: 913
		private static readonly int DefaultInsufficientSamplesIntervalSeconds = Math.Max(Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]), (int)TimeSpan.FromHours(8.0).TotalSeconds);

		// Token: 0x04000392 RID: 914
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000393 RID: 915
		private static readonly string TypeName = typeof(OverallConsecutiveSampleValueAboveThresholdMonitor).FullName;
	}
}
