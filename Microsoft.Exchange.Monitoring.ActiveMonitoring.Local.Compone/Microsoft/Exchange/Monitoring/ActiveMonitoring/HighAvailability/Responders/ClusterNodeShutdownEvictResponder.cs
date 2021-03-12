using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders
{
	// Token: 0x020001C1 RID: 449
	public class ClusterNodeShutdownEvictResponder : ResponderWorkItem
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000526F7 File Offset: 0x000508F7
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x000526FF File Offset: 0x000508FF
		internal string ComponentName { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00052708 File Offset: 0x00050908
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00052710 File Offset: 0x00050910
		internal string[] ServersInGroup { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00052719 File Offset: 0x00050919
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00052721 File Offset: 0x00050921
		internal int MinimumRequiredServers { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0005272A File Offset: 0x0005092A
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00052732 File Offset: 0x00050932
		internal string RemoteTargetServer { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0005273B File Offset: 0x0005093B
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00052743 File Offset: 0x00050943
		internal int MaximumNumberOfNodesDown { get; set; }

		// Token: 0x06000CCE RID: 3278 RVA: 0x0005274C File Offset: 0x0005094C
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServiceHealthStatus responderTargetState, string componentName, string serviceName = "Exchange", bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ClusterNodeShutdownEvictResponder.AssemblyPath;
			responderDefinition.TypeName = ClusterNodeShutdownEvictResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 900;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.Attributes["MinimumRequiredServers"] = -1.ToString();
			responderDefinition.Attributes["MaxNodeDownAllowed"] = 3.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.ClusterNodeHammerDown, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00052824 File Offset: 0x00050A24
		internal static void SetActiveMonitoringCertificateSettings(ResponderDefinition definition)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(ClusterNodeShutdownEvictResponder.ActiveMonitoringRegistryPath, false))
			{
				if (registryKey != null)
				{
					string text;
					if (definition.Account == null && (text = (string)registryKey.GetValue("RPSCertificateSubject", null)) != null)
					{
						definition.Account = text;
					}
					if (definition.Endpoint == null && (text = (string)registryKey.GetValue("RPSEndpoint", null)) != null)
					{
						definition.Endpoint = text;
					}
				}
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000528AC File Offset: 0x00050AAC
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			this.ComponentName = attributeHelper.GetString("ComponentName", true, null);
			this.ServersInGroup = Dependencies.ThrottleHelper.Settings.GetServersInGroup("Dag");
			this.MinimumRequiredServers = attributeHelper.GetInt("MinimumRequiredServers", false, -1, null, null);
			if (this.MinimumRequiredServers == -1)
			{
				this.MinimumRequiredServers = this.ServersInGroup.Length - 2;
			}
			this.MaximumNumberOfNodesDown = attributeHelper.GetInt("MaxNodeDownAllowed", true, 3, null, null);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00052AF8 File Offset: 0x00050CF8
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			this.RemoteTargetServer = null;
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult == null)
			{
				base.Result.StateAttribute1 = "Unable to get Target server from last failed probe result.";
				return;
			}
			this.RemoteTargetServer = lastFailedProbeResult.StateAttribute1;
			if (this.RemoteTargetServer.Contains(','))
			{
				this.RemoteTargetServer = this.RemoteTargetServer.Split(new char[]
				{
					','
				}).FirstOrDefault<string>();
			}
			if (string.IsNullOrWhiteSpace(this.RemoteTargetServer))
			{
				base.Result.StateAttribute1 = "RemoteTargetServer is NULL. Responder work is skipped.";
				return;
			}
			if (base.Broker.IsLocal() && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.EscalateResponder.Enabled)
			{
				try
				{
					Component component = Component.FindWellKnownComponent(this.ComponentName);
					if (component != null)
					{
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "ClusterNodeShutdownEvictResponder.DoWork: Attempting to perform remote server shutdown (componentName={0})", component.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Responders\\ClusterNodeShutdownEvictResponder.cs", 272);
						base.Result.StateAttribute1 = string.Format("Responder is going to restart the following nodes: {0}", string.Join(",", new string[]
						{
							this.RemoteTargetServer
						}));
						new RecoveryActionRunner(RecoveryActionId.ClusterNodeHammerDown, Environment.MachineName, this, true, cancellationToken, null)
						{
							IsIgnoreResourceName = true
						}.Execute(delegate()
						{
							this.AppendStrikeHistory(this.RemoteTargetServer, ClusterHungNodesForceRestartResponder.StrikeAction.HammerDown);
							this.Result.StateAttribute5 = string.Format("Trying to restart node '{0}'...", this.RemoteTargetServer);
							this.PerformRemoteForceReboot(component.ToString());
							this.Result.StateAttribute5 = string.Format("Waiting till node '{0}' unpingable", this.RemoteTargetServer);
							this.WaitUntilPingFailed(this.RemoteTargetServer, TimeSpan.FromMinutes(5.0));
							this.Result.StateAttribute5 = "Checking if there are enough nodes up in the DAG...";
							this.CheckClusterNodeCount();
							this.Result.StateAttribute5 = string.Format("Trying to evict node '{0}' from cluster", this.RemoteTargetServer);
							this.EvictNode(this.RemoteTargetServer);
							this.Result.StateAttribute5 = string.Format("HammerDown completed for node '{0}'", this.RemoteTargetServer);
							EventNotificationItem eventNotificationItem = new EventNotificationItem("ExCapacity", "NodeEvicted", "RepeatedlyOffendingNode", string.Format("Node '{0}' is a repeated offender causing cluster hang. Node is evicted from cluster and require assistance from Capacity team to bring the server offline before root cause identified.", this.RemoteTargetServer), this.RemoteTargetServer, ResultSeverityLevel.Critical);
							eventNotificationItem.Publish(false);
							HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", 1322L, 7, EventLogEntryType.Information, new object[]
							{
								this.RemoteTargetServer
							});
						});
						if (!string.IsNullOrWhiteSpace(this.errorMessage))
						{
							throw new HighAvailabilityMAResponderException(string.Format("Error occurred inside DoWork: {0}", string.Join(Environment.NewLine, new string[]
							{
								this.errorMessage
							})));
						}
					}
					return;
				}
				catch (Exception ex)
				{
					HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", 1325L, 7, EventLogEntryType.Error, new object[]
					{
						this.RemoteTargetServer,
						ex.ToString()
					});
					throw ex;
				}
			}
			base.Result.StateAttribute1 = "Responder not running in Datacenter environment, ignored.";
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00052D58 File Offset: 0x00050F58
		private void CheckClusterNodeCount()
		{
			IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
			int count = localDAG.Servers.Count;
			Dictionary<string, AmNodeState> source = LatencyChecker.QueryClusterNodeStatus(TimeSpan.FromMinutes(2.0), false);
			IEnumerable<bool> enumerable = from node in source
			select node.Value != AmNodeState.Up && !node.Key.Equals(this.RemoteTargetServer, StringComparison.OrdinalIgnoreCase);
			int num = (enumerable == null) ? 0 : enumerable.Count<bool>();
			if (num < 1 || count - num > this.MaximumNumberOfNodesDown || num <= (int)Math.Round((double)count / 2.0))
			{
				string message = string.Format("Unable to evict node due to exceeding Maximum allowed downed nodes (Threshold={0}). Current Up Nodes = {1}, Total = {2}", this.MaximumNumberOfNodesDown, num, count);
				HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", 1323L, 7, EventLogEntryType.Error, new object[]
				{
					this.RemoteTargetServer,
					this.MaximumNumberOfNodesDown.ToString(),
					num.ToString(),
					count.ToString()
				});
				throw new HighAvailabilityMAResponderException(message);
			}
			string.Format("Cluster up node check passed. Threshold = {0}, Current Up Nodes = {1}, Total = {2}", this.MaximumNumberOfNodesDown, num, count);
			HighAvailabilityUtility.LogEvent("Microsoft-Exchange-HighAvailability/Operational", "Microsoft-Exchange-HighAvailability", 1324L, 7, EventLogEntryType.Information, new object[]
			{
				this.RemoteTargetServer,
				this.MaximumNumberOfNodesDown.ToString(),
				num.ToString(),
				count.ToString()
			});
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00052ED4 File Offset: 0x000510D4
		private void EvictNode(string targetNetbiosName)
		{
			try
			{
				using (PowerShell powerShell = PowerShell.Create())
				{
					PSCommand pscommand = new PSCommand();
					pscommand.Clear();
					pscommand.AddCommand("Import-Module").AddParameter("Name", "FailoverClusters");
					this.RunPSCommand(powerShell, pscommand, false);
					pscommand.Clear();
					pscommand.AddCommand("Remove-ClusterNode").AddParameter("Name", targetNetbiosName).AddParameter("Force", true).AddParameter("Wait", 30).AddParameter("Confirm", false);
					this.RunPSCommand(powerShell, pscommand, false);
				}
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute4 = string.Format("Ex caught in Evict - {0}", ex.ToString());
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00052FB8 File Offset: 0x000511B8
		private Collection<PSObject> RunPSCommand(PowerShell ps, PSCommand cmd, bool asString)
		{
			ps.Streams.ClearStreams();
			ps.Commands = cmd;
			if (asString)
			{
				cmd.AddCommand("Out-String");
				ps.Commands = cmd;
			}
			Collection<PSObject> result = null;
			try
			{
				result = ps.Invoke();
			}
			catch (CommandNotFoundException ex)
			{
				throw new HighAvailabilityMAResponderException(ex.ToString());
			}
			if (ps.Streams.Error.Count > 0)
			{
				string psStream = this.GetPsStream<ErrorRecord>(ps.Streams.Error);
				throw new HighAvailabilityMAResponderException(string.Format("Powershell error - {0}", psStream));
			}
			return result;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00053050 File Offset: 0x00051250
		private string GetPsStream<T>(PSDataCollection<T> collection)
		{
			string result = null;
			if (collection != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (T t in collection)
				{
					stringBuilder.AppendLine(t.ToString());
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000530B8 File Offset: 0x000512B8
		private void WaitUntilPingFailed(string targetNetbiosName, TimeSpan timeout)
		{
			DateTime utcNow = DateTime.UtcNow;
			PingReply pingReply = null;
			using (Ping ping = new Ping())
			{
				while (DateTime.UtcNow - utcNow < timeout)
				{
					try
					{
						pingReply = ping.Send(targetNetbiosName, 10000);
						if (pingReply.Status != IPStatus.Success)
						{
							break;
						}
						Thread.Sleep(5000);
					}
					catch
					{
					}
				}
			}
			if (pingReply == null || pingReply.Status == IPStatus.Success)
			{
				throw new HighAvailabilityMAResponderException(string.Format("Unable to wait till host '{0}' to be unpingable within {1} seconds.", targetNetbiosName, timeout.TotalSeconds));
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00053290 File Offset: 0x00051490
		private void PerformRemoteForceReboot(string componentName)
		{
			RecoveryActionHelper.RunAndMeasure(string.Format("RemoteForceReboot(WorkitemId={0}, ResultId={1}, Component={2}, TargetServer={3})", new object[]
			{
				base.Id,
				base.Result.ResultId,
				componentName,
				(this.RemoteTargetServer != null) ? this.RemoteTargetServer : string.Empty
			}), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
			{
				this.CreateRunspace();
				string remoteTargetServer = this.RemoteTargetServer;
				if (!string.IsNullOrWhiteSpace(remoteTargetServer))
				{
					PSCommand pscommand = new PSCommand();
					pscommand.AddScript(string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring Hammer Down for Cluster Db Hang'", remoteTargetServer), false);
					base.Result.StateAttribute5 = string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring Hammer Down for Cluster Db Hang'", remoteTargetServer);
					try
					{
						Collection<PSObject> collection = this.remotePowershell.InvokePSCommand(pscommand);
						StringBuilder stringBuilder = new StringBuilder();
						if (collection != null)
						{
							foreach (PSObject psobject in collection)
							{
								stringBuilder.AppendLine(psobject.ToString());
							}
						}
						base.Result.StateAttribute5 = "Execution Result=" + stringBuilder.ToString();
					}
					catch (Exception arg)
					{
						this.errorMessage = string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, arg);
						base.Result.StateAttribute5 = string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, arg);
					}
				}
				return string.Empty;
			});
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00053304 File Offset: 0x00051504
		private void CreateRunspace()
		{
			if (base.Definition.Account == null)
			{
				ClusterNodeShutdownEvictResponder.SetActiveMonitoringCertificateSettings(base.Definition);
				base.Result.StateAttribute5 = "No authentication values were defined in ClusterNodeShutdownEvictResponder. Certification settings have now been set.";
			}
			if (!string.IsNullOrWhiteSpace(base.Definition.AccountPassword))
			{
				this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCredential(new Uri(base.Definition.Endpoint), base.Definition.Account, base.Definition.AccountPassword, true);
				return;
			}
			if (base.Definition.Endpoint.Contains(";"))
			{
				this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCertificate(base.Definition.Endpoint.Split(new char[]
				{
					';'
				}), base.Definition.Account, true);
				return;
			}
			this.remotePowershell = RemotePowerShell.CreateRemotePowerShellByCertificate(new Uri(base.Definition.Endpoint), base.Definition.Account, true);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000533F4 File Offset: 0x000515F4
		private void AppendStrikeHistory(string serverName, ClusterHungNodesForceRestartResponder.StrikeAction action)
		{
			HighAvailabilityUtility.RegWriter.CreateSubKey(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\StrikeHistory");
			string value = HighAvailabilityUtility.NonCachedRegReader.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\StrikeHistory", serverName, string.Empty);
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(value))
			{
				stringBuilder.Append(value);
				stringBuilder.Append(';');
			}
			stringBuilder.Append(action.ToString());
			stringBuilder.Append('|');
			stringBuilder.Append(DateTime.UtcNow.ToString("o"));
			HighAvailabilityUtility.RegWriter.SetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\StrikeHistory", serverName, stringBuilder.ToString());
		}

		// Token: 0x04000967 RID: 2407
		private const string LogName = "Microsoft-Exchange-HighAvailability/Operational";

		// Token: 0x04000968 RID: 2408
		private const string LogSource = "Microsoft-Exchange-HighAvailability";

		// Token: 0x04000969 RID: 2409
		private const int TaskId = 7;

		// Token: 0x0400096A RID: 2410
		private const string SetMachinePowerstateScript = "Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring Hammer Down for Cluster Db Hang'";

		// Token: 0x0400096B RID: 2411
		internal readonly TimeSpan CoordinatedQueryDuration = TimeSpan.FromDays(1.0);

		// Token: 0x0400096C RID: 2412
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400096D RID: 2413
		private static readonly string TypeName = typeof(ClusterNodeShutdownEvictResponder).FullName;

		// Token: 0x0400096E RID: 2414
		private static readonly string ActiveMonitoringRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x0400096F RID: 2415
		private string errorMessage;

		// Token: 0x04000970 RID: 2416
		private RemotePowerShell remotePowershell;

		// Token: 0x020001C2 RID: 450
		internal static class AttributeNames
		{
			// Token: 0x04000976 RID: 2422
			internal const string IsAutomaticallyDetectServers = "IsAutomaticallyDetectServers";

			// Token: 0x04000977 RID: 2423
			internal const string ComponentName = "ComponentName";

			// Token: 0x04000978 RID: 2424
			internal const string MinimumRequiredServers = "MinimumRequiredServers";

			// Token: 0x04000979 RID: 2425
			internal const string MaxNodeDownAllowed = "MaxNodeDownAllowed";
		}

		// Token: 0x020001C3 RID: 451
		internal static class DefaultValues
		{
			// Token: 0x0400097A RID: 2426
			internal const string[] ServersInGroup = null;

			// Token: 0x0400097B RID: 2427
			internal const bool IsAutomaticallyDetectServers = true;

			// Token: 0x0400097C RID: 2428
			internal const int MinimumRequiredServers = -1;

			// Token: 0x0400097D RID: 2429
			internal const int MaxNodeDownAllowed = 3;

			// Token: 0x0400097E RID: 2430
			internal const string ServiceName = "Exchange";

			// Token: 0x0400097F RID: 2431
			internal const bool Enabled = true;
		}
	}
}
