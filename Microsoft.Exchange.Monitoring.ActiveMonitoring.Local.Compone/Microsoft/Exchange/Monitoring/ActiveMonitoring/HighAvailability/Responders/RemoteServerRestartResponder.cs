using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders
{
	// Token: 0x020001BA RID: 442
	public class RemoteServerRestartResponder : RemotePowerShellResponder
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000517EC File Offset: 0x0004F9EC
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x000517F4 File Offset: 0x0004F9F4
		internal string ComponentName { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000517FD File Offset: 0x0004F9FD
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00051805 File Offset: 0x0004FA05
		internal string[] ServersInGroup { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0005180E File Offset: 0x0004FA0E
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00051816 File Offset: 0x0004FA16
		internal int MinimumRequiredServers { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0005181F File Offset: 0x0004FA1F
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00051827 File Offset: 0x0004FA27
		internal string[] RemoteTargetServers { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00051830 File Offset: 0x0004FA30
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00051838 File Offset: 0x0004FA38
		internal int MaxNodesToReboot { get; set; }

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00051844 File Offset: 0x0004FA44
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServiceHealthStatus responderTargetState, string componentName, string targetServerName, string serviceName = "Exchange", bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = RemoteServerRestartResponder.AssemblyPath;
			responderDefinition.TypeName = RemoteServerRestartResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.Attributes["RemoteTargetServers"] = targetServerName;
			responderDefinition.Attributes["MinimumRequiredServers"] = -1.ToString();
			responderDefinition.Attributes["MaxNodeToReboot"] = "1";
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.RemoteForceReboot, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00051928 File Offset: 0x0004FB28
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			this.ComponentName = attributeHelper.GetString("ComponentName", true, null);
			this.ServersInGroup = Dependencies.ThrottleHelper.Settings.GetServersInGroup("Dag");
			this.MaxNodesToReboot = attributeHelper.GetInt("MaxNodeToReboot", true, 1, null, null);
			this.MinimumRequiredServers = attributeHelper.GetInt("MinimumRequiredServers", false, -1, null, null);
			if (this.MinimumRequiredServers == -1)
			{
				this.MinimumRequiredServers = this.ServersInGroup.Length / 2 + 1;
			}
			string @string = attributeHelper.GetString("RemoteTargetServers", false, null);
			if (string.IsNullOrEmpty(@string))
			{
				this.RemoteTargetServers = null;
				return;
			}
			this.RemoteTargetServers = @string.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00051A38 File Offset: 0x0004FC38
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			if (this.RemoteTargetServers == null || this.RemoteTargetServers.Length < 1)
			{
				base.Result.StateAttribute1 = "RemoteTargetServer is NULL. Responder work is skipped.";
				return;
			}
			if (LocalEndpointManager.IsDataCenter)
			{
				try
				{
					Component component = Component.FindWellKnownComponent(this.ComponentName);
					if (component != null)
					{
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "RemoteServerRestartResponder.DoWork: Attempting to perform remote server force reboot (componentName={0})", component.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Responders\\RemoteServerRestartResponder.cs", 227);
						string arg = string.Join(",", this.RemoteTargetServers);
						this.RemoteTargetServers = this.RemoteTargetServers.Take(this.MaxNodesToReboot).ToArray<string>();
						base.Result.StateAttribute1 = string.Format("Responder is going to reboot on the following nodes: {0}, list received={1}, max nodes to reboot={2}", string.Join(",", this.RemoteTargetServers), arg, this.MaxNodesToReboot);
						new RecoveryActionRunner(RecoveryActionId.RemoteForceReboot, Environment.MachineName, this, true, cancellationToken, null)
						{
							IsIgnoreResourceName = true
						}.Execute(delegate()
						{
							this.PerformRemoteForceReboot(component.ToString());
						});
						if (this.errorMessage.Count > 0)
						{
							throw new HighAvailabilityMAResponderException(string.Format("Error occurred inside DoWork: {0}", string.Join(Environment.NewLine, this.errorMessage)));
						}
					}
					return;
				}
				catch (Exception ex)
				{
					RemoteServerRestartResponder.LogFailure((this.RemoteTargetServers != null && this.RemoteTargetServers.Length > 0) ? string.Join(",", this.RemoteTargetServers) : "NULL", ex.Message);
					throw ex;
				}
			}
			base.Result.StateAttribute1 = "Responder not running in Datacenter environment, ignored.";
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00051D88 File Offset: 0x0004FF88
		private void PerformRemoteForceReboot(string componentName)
		{
			RecoveryActionHelper.RunAndMeasure(string.Format("RemoteForceReboot(WorkitemId={0}, ResultId={1}, Component={2}, TargetServer={3})", new object[]
			{
				base.Id,
				base.Result.ResultId,
				componentName,
				(this.RemoteTargetServers != null) ? string.Join(",", this.RemoteTargetServers) : "NULL"
			}), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
			{
				base.CreateRunspace();
				int num = Math.Max(this.RemoteTargetServers.Length, this.MaxNodesToReboot);
				for (int i = 0; i < num; i++)
				{
					string text = this.RemoteTargetServers[i];
					if (!string.IsNullOrWhiteSpace(text))
					{
						PSCommand pscommand = new PSCommand();
						pscommand.AddScript(string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring RemoteServerRestartResponder from host {1}'", text, Environment.MachineName), false);
						base.Result.StateAttribute5 = string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring RemoteServerRestartResponder from host {1}'", text, Environment.MachineName);
						try
						{
							Collection<PSObject> collection = base.RemotePowerShell.InvokePSCommand(pscommand);
							StringBuilder stringBuilder = new StringBuilder();
							if (collection != null)
							{
								foreach (PSObject psobject in collection)
								{
									stringBuilder.AppendLine(psobject.ToString());
								}
							}
							base.Result.StateAttribute5 = "Execution Result=" + stringBuilder.ToString();
							RemoteServerRestartResponder.LogSuccess(text, stringBuilder.ToString());
						}
						catch (Exception ex)
						{
							this.errorMessage.Add(string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, ex));
							base.Result.StateAttribute5 = string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, ex);
							RemoteServerRestartResponder.LogFailure(text, ex.Message);
						}
					}
				}
				return string.Empty;
			});
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00051E08 File Offset: 0x00050008
		private static void LogSuccess(string target, string additionalMessage)
		{
			int num = 1320;
			object[] eventData = new object[]
			{
				target,
				additionalMessage
			};
			HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", (long)num, 7, EventLogEntryType.Information, eventData);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00051E40 File Offset: 0x00050040
		private static void LogFailure(string target, string additionalMessage)
		{
			int num = 1321;
			object[] eventData = new object[]
			{
				target,
				additionalMessage
			};
			HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", (long)num, 7, EventLogEntryType.Warning, eventData);
		}

		// Token: 0x04000942 RID: 2370
		private const string LogName = "Microsoft-Exchange-HighAvailability/Operational";

		// Token: 0x04000943 RID: 2371
		private const string LogSource = "Microsoft-Exchange-HighAvailability";

		// Token: 0x04000944 RID: 2372
		private const int TaskId = 7;

		// Token: 0x04000945 RID: 2373
		protected const string SetMachinePowerstateScript = "Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring RemoteServerRestartResponder from host {1}'";

		// Token: 0x04000946 RID: 2374
		internal readonly TimeSpan CoordinatedQueryDuration = TimeSpan.FromDays(1.0);

		// Token: 0x04000947 RID: 2375
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000948 RID: 2376
		private static readonly string TypeName = typeof(RemoteServerRestartResponder).FullName;

		// Token: 0x04000949 RID: 2377
		private readonly List<string> errorMessage = new List<string>();

		// Token: 0x020001BB RID: 443
		internal static class AttributeNames
		{
			// Token: 0x0400094F RID: 2383
			internal const string IsAutomaticallyDetectServers = "IsAutomaticallyDetectServers";

			// Token: 0x04000950 RID: 2384
			internal const string ComponentName = "ComponentName";

			// Token: 0x04000951 RID: 2385
			internal const string MinimumRequiredServers = "MinimumRequiredServers";

			// Token: 0x04000952 RID: 2386
			internal const string RemoteTargetServers = "RemoteTargetServers";

			// Token: 0x04000953 RID: 2387
			internal const string MaxNodeToReboot = "MaxNodeToReboot";
		}

		// Token: 0x020001BC RID: 444
		internal static class DefaultValues
		{
			// Token: 0x04000954 RID: 2388
			internal const string[] ServersInGroup = null;

			// Token: 0x04000955 RID: 2389
			internal const bool IsAutomaticallyDetectServers = true;

			// Token: 0x04000956 RID: 2390
			internal const int MinimumRequiredServers = -1;

			// Token: 0x04000957 RID: 2391
			internal const string ServiceName = "Exchange";

			// Token: 0x04000958 RID: 2392
			internal const bool Enabled = true;
		}
	}
}
