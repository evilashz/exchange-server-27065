using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips.Responders
{
	// Token: 0x0200050B RID: 1291
	public class CollectFIPSLogsResponder : ResponderWorkItem
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x000C25EC File Offset: 0x000C07EC
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x000C25F4 File Offset: 0x000C07F4
		private string LogDestination { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000C25FD File Offset: 0x000C07FD
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x000C2605 File Offset: 0x000C0805
		private int LifeTimeSeconds { get; set; }

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000C2610 File Offset: 0x000C0810
		internal static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, int recurrenceIntervalSeconds = 900, int waitIntervalSeconds = 28800, int timeoutSeconds = 600, int maxRetryAttempts = 1, string logDestination = null, int logLifeTimeSeconds = 432000, bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = CollectFIPSLogsResponder.AssemblyPath;
			responderDefinition.TypeName = CollectFIPSLogsResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.WaitIntervalSeconds = waitIntervalSeconds;
			responderDefinition.TimeoutSeconds = timeoutSeconds;
			responderDefinition.MaxRetryAttempts = maxRetryAttempts;
			responderDefinition.Attributes["LifeTimeSeconds"] = logLifeTimeSeconds.ToString();
			if (!string.IsNullOrEmpty(logDestination))
			{
				CollectFIPSLogsResponder.TestDirectoryPath(logDestination);
				responderDefinition.Attributes["LogDestination"] = logDestination;
			}
			else
			{
				responderDefinition.Attributes["LogDestination"] = CollectFIPSLogsResponder.DefaultValues.LogDestination;
			}
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000C2838 File Offset: 0x000C0A38
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			IDataAccessQuery<ResponderResult> lastSuccessfulRecoveryAttemptedResponderResult = base.Broker.GetLastSuccessfulRecoveryAttemptedResponderResult(base.Definition, TimeSpan.FromSeconds((double)base.Definition.WaitIntervalSeconds));
			Task<ResponderResult> task = lastSuccessfulRecoveryAttemptedResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				if (lastResponderResult != null)
				{
					base.Result.IsThrottled = (DateTime.Parse(lastResponderResult.StateAttribute1) > base.Result.ExecutionStartTime);
				}
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.FIPSTracer, base.TraceContext, "CollectFIPSLogsResponder.DoResponderWork: Throttled:{1}", base.Result.IsThrottled.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CollectFIPSLogsResponder.cs", 157);
				if (!base.Result.IsThrottled)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					if (this.LogDestination != null)
					{
						dictionary.Add("Path", this.LogDestination);
					}
					FipsUtils.RunFipsCmdlet<string>("Start-Diagnostics", dictionary);
					WTFDiagnostics.TraceDebug(ExTraceGlobals.FIPSTracer, base.TraceContext, "Log collection succeeded.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CollectFIPSLogsResponder.cs", 173);
					OldFileDeletionPolicy policy = new OldFileDeletionPolicy(TimeSpan.FromSeconds((double)this.LifeTimeSeconds));
					FileDeleter.Delete(this.LogDestination, policy);
					WTFDiagnostics.TraceDebug(ExTraceGlobals.FIPSTracer, base.TraceContext, "Logs have been successfully deleted as per the specified file deletion policy.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CollectFIPSLogsResponder.cs", 182);
					base.Result.StateAttribute1 = DateTime.UtcNow.AddSeconds((double)base.Definition.WaitIntervalSeconds).ToString();
					return;
				}
				base.Result.StateAttribute1 = lastResponderResult.StateAttribute1;
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000C2894 File Offset: 0x000C0A94
		private static void TestDirectoryPath(string directoryPath)
		{
			try
			{
				Directory.CreateDirectory(directoryPath);
			}
			catch (Exception ex)
			{
				string message = string.Format("Invalid Log Destination. Exception: {0}", ex.Message);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.FIPSTracer, TracingContext.Default, message, null, "TestDirectoryPath", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CollectFIPSLogsResponder.cs", 215);
				throw;
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000C28F0 File Offset: 0x000C0AF0
		private void InitializeAttributes()
		{
			this.LogDestination = base.Definition.Attributes["LogDestination"];
			this.LifeTimeSeconds = int.Parse(base.Definition.Attributes["LifeTimeSeconds"]);
		}

		// Token: 0x04001746 RID: 5958
		private const string CmdletStartDiagnostics = "Start-Diagnostics";

		// Token: 0x04001747 RID: 5959
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001748 RID: 5960
		private static readonly string TypeName = typeof(CollectFIPSLogsResponder).FullName;

		// Token: 0x0200050C RID: 1292
		internal static class AttributeNames
		{
			// Token: 0x0400174B RID: 5963
			internal const string LifeTimeSeconds = "LifeTimeSeconds";

			// Token: 0x0400174C RID: 5964
			internal const string LogDestination = "LogDestination";

			// Token: 0x0400174D RID: 5965
			internal const string RecurrenceIntervalSeconds = "RecurrenceIntervalSeconds";

			// Token: 0x0400174E RID: 5966
			internal const string WaitIntervalSeconds = "WaitIntervalSeconds";

			// Token: 0x0400174F RID: 5967
			internal const string TimeoutSeconds = "TimeoutSeconds";

			// Token: 0x04001750 RID: 5968
			internal const string MaxRetryAttempts = "MaxRetryAttempts";

			// Token: 0x04001751 RID: 5969
			internal const string Enabled = "Enabled";
		}

		// Token: 0x0200050D RID: 1293
		internal static class DefaultValues
		{
			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000C2954 File Offset: 0x000C0B54
			internal static string LogDestination
			{
				get
				{
					if (CollectFIPSLogsResponder.DefaultValues.logDestination == null)
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs\\"))
						{
							if (registryKey != null)
							{
								string value = (string)registryKey.GetValue("FipsDiagnosticsLogPath");
								if (!string.IsNullOrEmpty(value))
								{
									CollectFIPSLogsResponder.DefaultValues.logDestination = value;
								}
							}
						}
					}
					return CollectFIPSLogsResponder.DefaultValues.logDestination;
				}
			}

			// Token: 0x04001752 RID: 5970
			internal const int LifeTimeSeconds = 432000;

			// Token: 0x04001753 RID: 5971
			internal const int RecurrenceIntervalSeconds = 900;

			// Token: 0x04001754 RID: 5972
			internal const int WaitIntervalSeconds = 28800;

			// Token: 0x04001755 RID: 5973
			internal const int TimeoutSeconds = 600;

			// Token: 0x04001756 RID: 5974
			internal const int MaxRetryAttempts = 1;

			// Token: 0x04001757 RID: 5975
			internal const bool Enabled = true;

			// Token: 0x04001758 RID: 5976
			private const string ExchangeLabsRegKey = "SOFTWARE\\Microsoft\\ExchangeLabs\\";

			// Token: 0x04001759 RID: 5977
			private static string logDestination;
		}
	}
}
