using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200004C RID: 76
	public class OverallConsecutiveProbeFailuresMonitor : OverallConsecutiveFailuresMonitor
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00013B6C File Offset: 0x00011D6C
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, int failureCount, bool enabled = true, int monitoringInterval = 300)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = OverallConsecutiveProbeFailuresMonitor.AssemblyPath;
			monitorDefinition.TypeName = OverallConsecutiveProbeFailuresMonitor.TypeName;
			monitorDefinition.Name = name;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.ServiceName = serviceName;
			monitorDefinition.Component = component;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TimeoutSeconds = 30;
			monitorDefinition.MonitoringThreshold = (double)failureCount;
			monitorDefinition.MonitoringIntervalSeconds = monitoringInterval;
			monitorDefinition.RecurrenceIntervalSeconds = monitorDefinition.MonitoringIntervalSeconds / 2;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, OverallConsecutiveProbeFailuresMonitor.DefaultInsufficientSamplesIntervalSeconds);
			return monitorDefinition;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013C00 File Offset: 0x00011E00
		protected override bool ShouldAlert()
		{
			return base.Result.TotalValue >= base.Definition.MonitoringThreshold;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00013C20 File Offset: 0x00011E20
		protected override bool HaveInsufficientSamples()
		{
			double num = 0.0;
			if (base.Definition.Attributes.ContainsKey("MinimumSampleCount"))
			{
				num = double.Parse(base.Definition.Attributes["MinimumSampleCount"]);
			}
			return (double)base.Result.TotalSampleCount < num;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013C98 File Offset: 0x00011E98
		protected override Task SetConsecutiveFailureNumbers(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveProbeFailuresMonitor.SetConsecutiveProbeFailureNumbers: Getting overall consecutive failures of: {0}.", base.Definition.SampleMask, null, "SetConsecutiveFailureNumbers", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallConsecutiveProbeFailuresMonitor.cs", 114);
			base.GetLastFailedProbeResultId(base.Definition.SampleMask, cancellationToken);
			return base.GetConsecutiveProbeFailureInformation(base.Definition.SampleMask, (int)base.Definition.MonitoringThreshold, delegate(int newValue)
			{
				base.Result.NewValue = (double)newValue;
			}, delegate(int totalValue)
			{
				base.Result.TotalValue = (double)totalValue;
			}, cancellationToken);
		}

		// Token: 0x0400038C RID: 908
		private const string MinimumSampleCount = "MinimumSampleCount";

		// Token: 0x0400038D RID: 909
		private static readonly int DefaultInsufficientSamplesIntervalSeconds = Math.Max(Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]), (int)TimeSpan.FromHours(8.0).TotalSeconds);

		// Token: 0x0400038E RID: 910
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400038F RID: 911
		private static readonly string TypeName = typeof(OverallConsecutiveProbeFailuresMonitor).FullName;
	}
}
