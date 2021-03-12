using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components
{
	// Token: 0x02000278 RID: 632
	public sealed class PeopleConnectMaintenance : MaintenanceWorkItem
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x00077E4C File Offset: 0x0007604C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Skipping creating work items since we are in a non datacenter machine", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 62);
				base.Result.StateAttribute1 = "Not running on a Datacenter machine";
				return;
			}
			ExchangeServerRoleEndpoint exchangeServerRoleEndpoint = LocalEndpointManager.Instance.ExchangeServerRoleEndpoint;
			if (!exchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Skipping creating work items since we are in a non mailbox server", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 71);
				base.Result.StateAttribute1 = "Not running on a mailbox server";
				return;
			}
			if (!this.IsServerInFirstE15DAG(base.TraceContext))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Skipping creating work items since we are not running in the first E15 DAG", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 79);
				base.Result.StateAttribute1 = "Not running on first E15 DAG";
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Read the configuration values from XML file", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 85);
			this.Configure(base.TraceContext);
			if (ExEnvironment.IsTest && this.isDisabledForTest)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Skipping creating work items since we are running in a test environment and marked not to run", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 92);
				base.Result.StateAttribute1 = "Not running in test environment since it's marked as disabled";
				return;
			}
			string arg;
			if (ExEnvironment.IsTest)
			{
				arg = "https://exchangelabs.live-int.com/ecp/Connect/";
			}
			else
			{
				arg = "https://outlook.com/ecp/Connect/";
			}
			List<PeopleConnectMaintenance.AlertDefinition> list = new List<PeopleConnectMaintenance.AlertDefinition>
			{
				new PeopleConnectMaintenance.AlertDefinition
				{
					Name = "FacebookApplicationConfig",
					Type = typeof(FacebookProbe).FullName,
					IsEnabled = this.isFacebookProbeEnabled,
					RedirectUrl = string.Format("{0}FacebookSetup.aspx", arg)
				},
				new PeopleConnectMaintenance.AlertDefinition
				{
					Name = "LinkedInApplicationConfig",
					Type = typeof(LinkedInProbe).FullName,
					IsEnabled = this.isLinkedInProbeEnabled,
					RedirectUrl = string.Format("{0}LinkedInSetup.aspx", arg)
				}
			};
			foreach (PeopleConnectMaintenance.AlertDefinition alertDefinition in list)
			{
				ProbeDefinition probeDefinition = new ProbeDefinition();
				probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
				probeDefinition.TypeName = alertDefinition.Type;
				probeDefinition.Name = alertDefinition.Probe;
				probeDefinition.ServiceName = ExchangeComponent.PeopleConnect.Name;
				probeDefinition.Endpoint = alertDefinition.RedirectUrl;
				probeDefinition.TargetResource = Environment.MachineName;
				probeDefinition.RecurrenceIntervalSeconds = (int)this.probeRecurrenceInterval.TotalSeconds;
				probeDefinition.Enabled = alertDefinition.IsEnabled;
				probeDefinition.TimeoutSeconds = this.probeTimeoutSeconds;
				MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition(alertDefinition.Monitor, probeDefinition.ConstructWorkItemResultName(), ExchangeComponent.PeopleConnect.Name, ExchangeComponent.PeopleConnect, 90.0, this.monitoringInterval, 5, alertDefinition.IsEnabled);
				monitorDefinition.TargetResource = Environment.MachineName;
				monitorDefinition.RecurrenceIntervalSeconds = (int)this.monitorRecurrenceInterval.TotalSeconds;
				monitorDefinition.ServicePriority = 2;
				monitorDefinition.ScenarioDescription = "Validate PeopleConnect health is not impacted by any issues";
				ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(alertDefinition.Responder, ExchangeComponent.PeopleConnect.Name, alertDefinition.Monitor, monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.PeopleConnect.EscalationTeam, alertDefinition.MessageSubject, alertDefinition.MessageBody, alertDefinition.IsEnabled & this.alertResponderEnabled, NotificationServiceClass.UrgentInTraining, (int)TimeSpan.FromDays(1.0).TotalSeconds, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				responderDefinition.RecurrenceIntervalSeconds = (int)this.responderRecurrenceInterval.TotalSeconds;
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Finished added work definitions to the broker", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 184);
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00078270 File Offset: 0x00076470
		private void Configure(TracingContext traceContext)
		{
			this.probeRecurrenceInterval = this.ReadAttribute("ProbeRecurrenceInterval", TimeSpan.FromDays(1.0));
			this.monitoringInterval = this.ReadAttribute("MonitoringInterval", TimeSpan.FromDays(1.0));
			this.monitorRecurrenceInterval = this.ReadAttribute("MonitorRecurrenceInterval", TimeSpan.FromHours(1.0));
			this.responderRecurrenceInterval = this.ReadAttribute("ResponderRecurrenceInterval", TimeSpan.FromHours(1.0));
			this.alertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", true);
			this.isFacebookProbeEnabled = this.ReadAttribute("IsFacebookProbeEnabled", true);
			this.isLinkedInProbeEnabled = this.ReadAttribute("IsLinkedInProbeEnabled", true);
			this.isDisabledForTest = this.ReadAttribute("IsDisabledForTest", true);
			this.probeTimeoutSeconds = this.ReadAttribute("TimeoutSeconds", 5);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "Configuration value are initialized successfully", null, "Configure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 204);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00078378 File Offset: 0x00076578
		internal static bool ShouldRun(TracingContext traceContext)
		{
			bool result = false;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "ShouldRun(): starting", null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 215);
			if (ExEnvironment.IsTest)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "ShouldRun(): returns true because this is test environment", null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 219);
				return true;
			}
			try
			{
				IADServer localServer = CachedAdReader.Instance.LocalServer;
				if (localServer == null || localServer.DatabaseAvailabilityGroup == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "ShouldRun(): returns false because Local Server DAG is null.", null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 228);
					return false;
				}
				IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
				if (localDAG != null)
				{
					AmServerName primaryActiveManagerNode = DagTaskHelper.GetPrimaryActiveManagerNode(localDAG);
					if (primaryActiveManagerNode != null)
					{
						result = primaryActiveManagerNode.IsLocalComputerName;
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PeopleConnectTracer, traceContext, "ShouldRun(): got exception {0}", ex.ToString(), null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 245);
			}
			return result;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0007846C File Offset: 0x0007666C
		internal static void LogApplicationConfig(ProbeResult probeResult, IPeopleConnectApplicationConfig appConfig)
		{
			probeResult.StateAttribute11 = "AppId = " + appConfig.AppId;
			probeResult.StateAttribute12 = "AppSecretClearTextHashCode = " + appConfig.AppSecretClearText.GetHashCode().ToString("X8");
			probeResult.StateAttribute13 = "AuthorizationEndpoint = " + appConfig.AuthorizationEndpoint;
			probeResult.StateAttribute14 = "GraphTokenEndpoint = " + appConfig.GraphTokenEndpoint;
			probeResult.StateAttribute15 = "GraphApiEndpoint = " + appConfig.GraphApiEndpoint;
			probeResult.StateAttribute21 = "RequestTokenEndpoint = " + appConfig.RequestTokenEndpoint;
			probeResult.StateAttribute22 = "AccessTokenEndpoint = " + appConfig.AccessTokenEndpoint;
			probeResult.StateAttribute23 = "ProfileEndpoint = " + appConfig.ProfileEndpoint;
			probeResult.StateAttribute24 = "ConnectionsEndpoint = " + appConfig.ConnectionsEndpoint;
			probeResult.StateAttribute25 = "RemoveAppEndpoint = " + appConfig.RemoveAppEndpoint;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00078568 File Offset: 0x00076768
		private bool IsServerInFirstE15DAG(TracingContext traceContext)
		{
			bool result = false;
			IADDatabaseAvailabilityGroup iaddatabaseAvailabilityGroup = null;
			if (ExEnvironment.IsTest)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): returns true because this is test environment", null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 282);
				return true;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): Get Local Server DAG.", null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 286);
			IADServer localServer = CachedAdReader.Instance.LocalServer;
			if (localServer == null || localServer.DatabaseAvailabilityGroup == null)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG() return false because Local Server DAG is null.", null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 290);
				return false;
			}
			IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
			if (localDAG != null)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): Local Server DAG is {0}. Now get all DAGs sort by Name.", localDAG.Name, null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 297);
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 298, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs");
				DatabaseAvailabilityGroup[] array = topologyConfigurationSession.Find<DatabaseAvailabilityGroup>(null, QueryScope.SubTree, null, new SortBy(DatabaseAvailabilityGroupSchema.Name, SortOrder.Ascending), 0);
				if (array != null)
				{
					foreach (DatabaseAvailabilityGroup databaseAvailabilityGroup in array)
					{
						if (databaseAvailabilityGroup.Servers != null && databaseAvailabilityGroup.Servers.Count > 0)
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): Check if DAG {0} contains E15 server.", databaseAvailabilityGroup.Name, null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 306);
							Server server = topologyConfigurationSession.Read<Server>(databaseAvailabilityGroup.Servers[0]);
							if (server.AdminDisplayVersion.Major >= 15)
							{
								WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): Found first E15 DAG: {0}.", databaseAvailabilityGroup.Name, null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 310);
								iaddatabaseAvailabilityGroup = ADObjectWrapperFactory.CreateWrapper(databaseAvailabilityGroup);
								break;
							}
						}
					}
				}
				if (iaddatabaseAvailabilityGroup != null && iaddatabaseAvailabilityGroup.Name.CompareTo(localDAG.Name) == 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, traceContext, "IsServerInFirstE15DAG(): Server is in first E15 DAG.", null, "IsServerInFirstE15DAG", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\peopleconnectmaintenance.cs", 320);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000D68 RID: 3432
		private const int E15MajorVersion = 15;

		// Token: 0x04000D69 RID: 3433
		private TimeSpan probeRecurrenceInterval;

		// Token: 0x04000D6A RID: 3434
		private TimeSpan monitoringInterval;

		// Token: 0x04000D6B RID: 3435
		private TimeSpan monitorRecurrenceInterval;

		// Token: 0x04000D6C RID: 3436
		private TimeSpan responderRecurrenceInterval;

		// Token: 0x04000D6D RID: 3437
		private bool alertResponderEnabled;

		// Token: 0x04000D6E RID: 3438
		private bool isFacebookProbeEnabled;

		// Token: 0x04000D6F RID: 3439
		private bool isLinkedInProbeEnabled;

		// Token: 0x04000D70 RID: 3440
		private bool isDisabledForTest;

		// Token: 0x04000D71 RID: 3441
		private int probeTimeoutSeconds;

		// Token: 0x02000279 RID: 633
		private class AlertDefinition
		{
			// Token: 0x1700035E RID: 862
			// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0007876F File Offset: 0x0007696F
			// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00078777 File Offset: 0x00076977
			public string Name { get; set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00078780 File Offset: 0x00076980
			// (set) Token: 0x060011DA RID: 4570 RVA: 0x00078788 File Offset: 0x00076988
			public string Type { get; set; }

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x060011DB RID: 4571 RVA: 0x00078791 File Offset: 0x00076991
			// (set) Token: 0x060011DC RID: 4572 RVA: 0x00078799 File Offset: 0x00076999
			public bool IsEnabled { get; set; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x060011DD RID: 4573 RVA: 0x000787A2 File Offset: 0x000769A2
			// (set) Token: 0x060011DE RID: 4574 RVA: 0x000787AA File Offset: 0x000769AA
			public string RedirectUrl { get; set; }

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x000787B3 File Offset: 0x000769B3
			public string Probe
			{
				get
				{
					return string.Format("{0} Probe", this.Name);
				}
			}

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000787C5 File Offset: 0x000769C5
			public string Monitor
			{
				get
				{
					return string.Format("{0} Monitor", this.Name);
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000787D7 File Offset: 0x000769D7
			public string Responder
			{
				get
				{
					return string.Format("{0} Responder", this.Name);
				}
			}

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000787E9 File Offset: 0x000769E9
			public string MessageSubject
			{
				get
				{
					return string.Format("{0} Application Configuration Probe failed. Got the following error {{Probe.StateAttribute1}}", this.Name);
				}
			}

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000787FB File Offset: 0x000769FB
			public string MessageBody
			{
				get
				{
					return "Error: {Probe.Error} \n Exception: {Probe.Exception} \n Execution Context: {Probe.ExecutionContext}\n Failure Context: {Probe.FailureContext} \n Status Code : {Probe.StateAttribute4}";
				}
			}
		}
	}
}
