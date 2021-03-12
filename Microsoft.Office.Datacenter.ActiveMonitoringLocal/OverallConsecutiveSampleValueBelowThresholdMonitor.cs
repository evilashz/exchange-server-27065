using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200004E RID: 78
	public class OverallConsecutiveSampleValueBelowThresholdMonitor : OverallConsecutiveFailuresMonitor
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00013FC8 File Offset: 0x000121C8
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, double threshold, int numberOfSamples, bool enabled = true)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = OverallConsecutiveSampleValueBelowThresholdMonitor.AssemblyPath;
			monitorDefinition.TypeName = OverallConsecutiveSampleValueBelowThresholdMonitor.TypeName;
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
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, OverallConsecutiveSampleValueBelowThresholdMonitor.DefaultInsufficientSamplesIntervalSeconds);
			return monitorDefinition;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001406C File Offset: 0x0001226C
		protected override bool ShouldAlert()
		{
			return base.Result.TotalValue >= base.Definition.SecondaryMonitoringThreshold;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001408C File Offset: 0x0001228C
		protected override bool HaveInsufficientSamples()
		{
			double num = 0.0;
			if (base.Definition.Attributes.ContainsKey("MinimumSampleCount"))
			{
				num = double.Parse(base.Definition.Attributes["MinimumSampleCount"]);
			}
			return (double)base.Result.TotalSampleCount < num;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00014104 File Offset: 0x00012304
		protected override Task SetConsecutiveFailureNumbers(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveSampleValueBelowThresholdMonitor.SetConsecutiveFailureNumbers: Getting overall consecutive samples of: {0}.", base.Definition.SampleMask, null, "SetConsecutiveFailureNumbers", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallConsecutiveSampleValueBelowThresholdMonitor.cs", 113);
			Task consecutiveSampleValueBelowThresholdCounts = base.GetConsecutiveSampleValueBelowThresholdCounts(base.Definition.SampleMask, (double)((int)base.Definition.MonitoringThreshold), (int)base.Definition.SecondaryMonitoringThreshold, delegate(int newValue)
			{
				base.Result.NewValue = (double)newValue;
			}, delegate(int totalValue)
			{
				base.Result.TotalValue = (double)totalValue;
			}, cancellationToken);
			this.SetStateAttribute6ForScopeMonitoring(base.Result.NewValue);
			return consecutiveSampleValueBelowThresholdCounts;
		}

		// Token: 0x04000394 RID: 916
		private const string MinimumSampleCount = "MinimumSampleCount";

		// Token: 0x04000395 RID: 917
		private static readonly int DefaultInsufficientSamplesIntervalSeconds = Math.Max(Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]), (int)TimeSpan.FromHours(8.0).TotalSeconds);

		// Token: 0x04000396 RID: 918
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000397 RID: 919
		private static readonly string TypeName = typeof(OverallConsecutiveSampleValueBelowThresholdMonitor).FullName;
	}
}
