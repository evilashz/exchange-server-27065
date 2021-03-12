using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000E3 RID: 227
	public class WatsonResponder : ResponderWorkItem
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string exceptionType, string watsonEventType, ReportOptions reportOptions)
		{
			if (targetHealthState == ServiceHealthStatus.None)
			{
				throw new ArgumentException("The responder does not support ServiceHealthStatus.None as target health state.", "targetHealthState");
			}
			if (string.IsNullOrEmpty(exceptionType))
			{
				throw new ArgumentNullException("Parameter cannot be null or empty.", "exceptionType");
			}
			if (string.IsNullOrEmpty(watsonEventType))
			{
				throw new ArgumentNullException("Parameter cannot be null or empty.", "watsonEventType");
			}
			if (!WatsonResponder.SupportedWatsonEventSet.Contains(watsonEventType))
			{
				throw new ArgumentException(string.Format("The responder does not support the event type '{0}'", watsonEventType));
			}
			if (string.IsNullOrWhiteSpace(targetResource))
			{
				throw new ArgumentException("The responder needs to have a process that needs a watson.", "targetResource");
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = WatsonResponder.AssemblyPath;
			responderDefinition.TypeName = WatsonResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.WaitIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.WatsonResponder.Enabled;
			responderDefinition.Attributes["EdsProcessName"] = targetResource;
			responderDefinition.Attributes["ExceptionType"] = exceptionType;
			responderDefinition.Attributes["WatsonEventType"] = watsonEventType;
			responderDefinition.Attributes["ReportOptions"] = reportOptions.ToString();
			responderDefinition.Attributes["FilesToUpload"] = string.Empty;
			responderDefinition.Attributes["AllowDefaultCallStack"] = true.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.Watson, targetResource, null);
			return responderDefinition;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0002BA56 File Offset: 0x00029C56
		protected static void TraceDebug(TracingContext traceContext, string formatString)
		{
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, traceContext, formatString, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\WatsonResponder.cs", 148);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002BDA4 File Offset: 0x00029FA4
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.Watson, base.Definition.TargetResource, this, true, cancellationToken, null);
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			recoveryActionRunner.Execute(delegate()
			{
				IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = this.Broker.GetLastSuccessfulResponderResult(this.Definition);
				Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, this.TraceContext);
				WatsonResponder.TraceDebug(this.TraceContext, "WatsonResponder.DoResponderWork: analyzing last result");
				task.Continue(delegate(ResponderResult lastResponderResult)
				{
					DateTime startTime = SqlDateTime.MinValue.Value;
					if (lastResponderResult != null)
					{
						startTime = lastResponderResult.ExecutionStartTime;
					}
					IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition.AlertMask, startTime, this.Result.ExecutionStartTime);
					Task<MonitorResult> task2 = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, this.TraceContext);
					task2.Continue(delegate(MonitorResult lastMonitorResult)
					{
						string text = this.Definition.Attributes["EdsProcessName"];
						string text2 = this.Definition.Attributes["WatsonEventType"];
						string text3 = this.Definition.Attributes["ExceptionType"];
						ReportOptions reportOptions;
						if (!Enum.TryParse<ReportOptions>(this.Definition.Attributes["ReportOptions"], out reportOptions))
						{
							reportOptions = ReportOptions.None;
						}
						bool flag;
						if (!bool.TryParse(this.Definition.Attributes["AllowDefaultCallStack"], out flag))
						{
							flag = true;
						}
						string formatString = string.Format("WatsonResponder.DoResponderWork: Setting up {0} Watson on {1} with exception: {2}. ReportOptions: {3}.", new object[]
						{
							text2,
							text,
							text3,
							reportOptions
						});
						WatsonResponder.TraceDebug(this.TraceContext, formatString);
						Process[] processesFromEdsName = WatsonResponder.GetProcessesFromEdsName(text, this.Definition.TargetExtension);
						if (processesFromEdsName.Length > 0)
						{
							foreach (Process process in processesFromEdsName)
							{
								using (process)
								{
									string text4 = this.CollectCallstack(process);
									if (string.IsNullOrEmpty(text4) || (!flag && text4.Equals(WatsonResponder.DefaultCallStack, StringComparison.OrdinalIgnoreCase)))
									{
										this.Result.RecoveryResult = ServiceRecoveryResult.Skipped;
										this.Result.IsRecoveryAttempted = false;
									}
									else
									{
										string extraData = this.GetExtraData(cancellationToken);
										string[] filesToUpload = this.GetFilesToUpload();
										Exception exceptionObject = this.GetExceptionObject(text3, text4);
										exceptionObject.Source = WatsonResponder.GetWatsonBugRoutingProcessName(text);
										ExWatson.SendExternalProcessWatsonReportWithFiles(process, text2, exceptionObject, extraData, filesToUpload, reportOptions);
										WatsonResponder.TraceDebug(this.TraceContext, string.Format("WatsonResponder.DoResponderWork: {0} Watson sent on {1} with Exception {2}", text2, text, text3));
									}
								}
							}
							return;
						}
						WatsonResponder.TraceDebug(this.TraceContext, string.Format("WatsonResponder.DoResponderWork: Failed to retrieve valid process id for {0}.", text));
					}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
				}, cancellationToken, TaskContinuationOptions.AttachedToParent);
			});
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002BE1F File Offset: 0x0002A01F
		internal virtual string CollectCallstack(Process process)
		{
			return WatsonResponder.DefaultCallStack;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0002BE28 File Offset: 0x0002A028
		protected virtual string[] GetFilesToUpload()
		{
			string text = base.Definition.Attributes["FilesToUpload"];
			if (string.IsNullOrEmpty(text))
			{
				return new string[0];
			}
			return text.Split(new char[]
			{
				';'
			});
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002BE70 File Offset: 0x0002A070
		protected virtual string GetExtraData(CancellationToken cancellationToken)
		{
			string text = string.Empty;
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult != null)
			{
				text = lastFailedProbeResult.Error;
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Replace("\\r\\n", Environment.NewLine);
				}
			}
			return text;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		internal virtual Exception GetExceptionObject(string exceptionType, string callstack)
		{
			if (string.IsNullOrEmpty(exceptionType))
			{
				throw new ArgumentException("Parameter may not be null or empty", "exceptionType");
			}
			switch (exceptionType)
			{
			case "ProcessProcessorTimeWarningException":
				return new WatsonResponder.ProcessProcessorTimeException(callstack);
			case "ProcessProcessorTimeErrorException":
				return new WatsonResponder.ProcessProcessorTimeException(callstack);
			case "PrivateWorkingSetWarningException":
				return new WatsonResponder.PrivateWorkingSetException(callstack);
			case "PrivateWorkingSetErrorException":
				return new WatsonResponder.PrivateWorkingSetException(callstack);
			case "VersionBucketsAllocatedWatsonResponderException":
				return new WatsonResponder.VersionBucketsAllocatedException(callstack);
			case "DatabasePercentRPCRequestsWatsonResponderException":
				return new WatsonResponder.DatabasePercentRPCRequestsException(callstack);
			case "MailboxAssistantsWatermarksWatsonResponderException":
				return new WatsonResponder.MailboxAssistantsWatermarksBehindException(callstack);
			}
			return new WatsonResponder.ResponderException(string.Empty, callstack);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
		private static string GetWatsonBugRoutingProcessName(string processName)
		{
			if (processName.EndsWith("apppool", StringComparison.OrdinalIgnoreCase) && !processName.StartsWith("w3wp#", StringComparison.OrdinalIgnoreCase))
			{
				return "w3wp#" + processName;
			}
			return processName;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002BFEC File Offset: 0x0002A1EC
		private static Process[] GetProcessesFromEdsName(string processName, string targetExtension)
		{
			if (processName.StartsWith("noderunner#", StringComparison.OrdinalIgnoreCase))
			{
				return WatsonResponder.GetProcessesForNodeRunner(processName);
			}
			if (processName.EndsWith("apppool", StringComparison.OrdinalIgnoreCase))
			{
				using (ServerManager serverManager = new ServerManager())
				{
					return ApplicationPoolHelper.GetRunningProcessesForAppPool(serverManager, processName);
				}
			}
			if (processName.Equals("Microsoft.Exchange.Store.Worker.exe", StringComparison.InvariantCultureIgnoreCase))
			{
				return StoreMonitoringHelpers.GetStoreWorkerProcess(targetExtension);
			}
			return Process.GetProcessesByName(processName) ?? new Process[0];
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002C070 File Offset: 0x0002A270
		private static Process[] GetProcessesForNodeRunner(string nodeRunnerInstanceName)
		{
			Dictionary<string, int> nodeProcessIds = SearchMonitoringHelper.GetNodeProcessIds();
			foreach (string text in nodeProcessIds.Keys)
			{
				if (nodeRunnerInstanceName.EndsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					return new Process[]
					{
						Process.GetProcessById(nodeProcessIds[text])
					};
				}
			}
			return new Process[0];
		}

		// Token: 0x040004CF RID: 1231
		public static readonly string DefaultCallStack = "Unknown";

		// Token: 0x040004D0 RID: 1232
		private static readonly string TypeName = typeof(WatsonResponder).FullName;

		// Token: 0x040004D1 RID: 1233
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004D2 RID: 1234
		private static readonly HashSet<string> SupportedWatsonEventSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"E12",
			"E12IIS"
		};

		// Token: 0x020000E4 RID: 228
		public class ResponderException : Exception
		{
			// Token: 0x06000752 RID: 1874 RVA: 0x0002C15F File Offset: 0x0002A35F
			public ResponderException(string message, string callstack) : base(message)
			{
				this.stackTrace = callstack;
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000753 RID: 1875 RVA: 0x0002C16F File Offset: 0x0002A36F
			public override string StackTrace
			{
				get
				{
					return this.stackTrace.ToString();
				}
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0002C17C File Offset: 0x0002A37C
			public override string ToString()
			{
				return string.Format("{0}: {1}{2}{3}", new object[]
				{
					base.GetType(),
					this.Message,
					Environment.NewLine,
					this.stackTrace
				});
			}

			// Token: 0x040004D3 RID: 1235
			private readonly string stackTrace;
		}

		// Token: 0x020000E5 RID: 229
		public class ProcessProcessorTimeException : WatsonResponder.ResponderException
		{
			// Token: 0x06000755 RID: 1877 RVA: 0x0002C1BE File Offset: 0x0002A3BE
			public ProcessProcessorTimeException(string callstack) : base("Process has exceeded its processor time threshold. The following hot path was parsed from the F1:", callstack)
			{
			}

			// Token: 0x040004D4 RID: 1236
			private const string ExceptionMessage = "Process has exceeded its processor time threshold. The following hot path was parsed from the F1:";
		}

		// Token: 0x020000E6 RID: 230
		public class VersionBucketsAllocatedException : WatsonResponder.ResponderException
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x0002C1CC File Offset: 0x0002A3CC
			public VersionBucketsAllocatedException(string callstack) : base("Version Buckets Allocated perf counter has exceeded its threshold", callstack)
			{
			}

			// Token: 0x040004D5 RID: 1237
			private const string ExceptionMessage = "Version Buckets Allocated perf counter has exceeded its threshold";
		}

		// Token: 0x020000E7 RID: 231
		public class DatabasePercentRPCRequestsException : WatsonResponder.ResponderException
		{
			// Token: 0x06000757 RID: 1879 RVA: 0x0002C1DA File Offset: 0x0002A3DA
			public DatabasePercentRPCRequestsException(string callstack) : base("% RPC Requests perf counter has exceeded its threshold", callstack)
			{
			}

			// Token: 0x040004D6 RID: 1238
			private const string ExceptionMessage = "% RPC Requests perf counter has exceeded its threshold";
		}

		// Token: 0x020000E8 RID: 232
		public class MailboxAssistantsWatermarksBehindException : WatsonResponder.ResponderException
		{
			// Token: 0x06000758 RID: 1880 RVA: 0x0002C1E8 File Offset: 0x0002A3E8
			public MailboxAssistantsWatermarksBehindException(string callstack) : base("Mailbox assistants watermarks have been behind past current threshold", callstack)
			{
			}

			// Token: 0x040004D7 RID: 1239
			private const string ExceptionMessage = "Mailbox assistants watermarks have been behind past current threshold";
		}

		// Token: 0x020000E9 RID: 233
		public class PrivateWorkingSetException : WatsonResponder.ResponderException
		{
			// Token: 0x06000759 RID: 1881 RVA: 0x0002C1F6 File Offset: 0x0002A3F6
			public PrivateWorkingSetException(string callstack) : base("Process has exceeded its private working set threshold", callstack)
			{
			}

			// Token: 0x040004D8 RID: 1240
			private const string ExceptionMessage = "Process has exceeded its private working set threshold";
		}

		// Token: 0x020000EA RID: 234
		internal static class AttributeNames
		{
			// Token: 0x040004D9 RID: 1241
			internal const string EdsProcessName = "EdsProcessName";

			// Token: 0x040004DA RID: 1242
			internal const string ExceptionType = "ExceptionType";

			// Token: 0x040004DB RID: 1243
			internal const string WatsonEventType = "WatsonEventType";

			// Token: 0x040004DC RID: 1244
			internal const string ReportOptions = "ReportOptions";

			// Token: 0x040004DD RID: 1245
			internal const string FilesToUpload = "FilesToUpload";

			// Token: 0x040004DE RID: 1246
			internal const string AllowDefaultCallStack = "AllowDefaultCallStack";
		}
	}
}
