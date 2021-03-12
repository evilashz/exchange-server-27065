using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000052 RID: 82
	public class OverallXFailuresMonitor : OverallPercentSuccessMonitor
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00015FD0 File Offset: 0x000141D0
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, string serviceName, Component component, int monitoringInterval, int recurrenceInterval, int numberOfFailures, bool enabled = true)
		{
			return new MonitorDefinition
			{
				AssemblyPath = OverallXFailuresMonitor.AssemblyPath,
				Component = component,
				Enabled = enabled,
				TypeName = OverallXFailuresMonitor.TypeName,
				MaxRetryAttempts = 0,
				MonitoringIntervalSeconds = monitoringInterval,
				MonitoringThreshold = (double)numberOfFailures,
				Name = name,
				RecurrenceIntervalSeconds = recurrenceInterval,
				SampleMask = sampleMask,
				ServiceName = serviceName,
				TargetResource = serviceName,
				TimeoutSeconds = Math.Max(recurrenceInterval / 2, 30),
				InsufficientSamplesIntervalSeconds = Math.Max(5 * recurrenceInterval, OverallXFailuresMonitor.DefaultInsufficientSamplesIntervalSeconds)
			};
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00016128 File Offset: 0x00014328
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallXFailuresMonitor: Calling into OverallPercentSuccessMonitor to get probe result counts.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallXFailuresMonitor.cs", 96);
			double minimumSampleCount = 0.0;
			if (base.Definition.Attributes.ContainsKey("MinimumSampleCount"))
			{
				minimumSampleCount = double.Parse(base.Definition.Attributes["MinimumSampleCount"]);
			}
			this.SetPercentSuccessNumbers(cancellationToken).ContinueWith(delegate(Task t)
			{
				this.HandleInsufficientSamples(() => (double)this.Result.TotalSampleCount < minimumSampleCount, cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Current).ContinueWith(delegate(Task t)
			{
				if (!base.Result.IsAlert)
				{
					if ((double)base.Result.TotalFailedCount >= base.Definition.MonitoringThreshold)
					{
						base.Result.IsAlert = true;
						this.OnAlert();
					}
					else
					{
						base.Result.IsAlert = false;
					}
				}
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallXFailuresMonitor: Finished analyzing probe results.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallXFailuresMonitor.cs", 134);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Current);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00016200 File Offset: 0x00014400
		protected virtual void OnAlert()
		{
		}

		// Token: 0x040003AE RID: 942
		private const string MinimumSampleCount = "MinimumSampleCount";

		// Token: 0x040003AF RID: 943
		private const int DefaultTimeoutSeconds = 30;

		// Token: 0x040003B0 RID: 944
		private static readonly int DefaultInsufficientSamplesIntervalSeconds = Math.Max(Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]), (int)TimeSpan.FromHours(8.0).TotalSeconds);

		// Token: 0x040003B1 RID: 945
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040003B2 RID: 946
		private static readonly string TypeName = typeof(OverallXFailuresMonitor).FullName;
	}
}
