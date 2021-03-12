using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab
{
	// Token: 0x0200023C RID: 572
	public sealed class OabDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0006ABE1 File Offset: 0x00068DE1
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0006ABE9 File Offset: 0x00068DE9
		public bool IsOnPremisesEnabled { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0006ABF2 File Offset: 0x00068DF2
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0006ABFA File Offset: 0x00068DFA
		public int OabProtocolProbeTimeout { get; private set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0006AC03 File Offset: 0x00068E03
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0006AC0B File Offset: 0x00068E0B
		public int OabMailboxProbeTimeout { get; private set; }

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0006AC14 File Offset: 0x00068E14
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.Configure();
			if (!LocalEndpointManager.IsDataCenter && !this.IsOnPremisesEnabled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.DoWork: In case of on-premises, EnableOnPrem should be true in order to create probe/monitor/responder", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 141);
				base.Result.StateAttribute1 = "OabDiscovery.DoWork: In case of on-premises, EnableOnPrem should be true in order to create probe/monitor/responder";
				return;
			}
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.DoWork: Skip creating the probe for non-MBX server", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 156);
				}
				else
				{
					this.SetupOabProbes(base.TraceContext, instance);
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.DoWork: Endpoint initialization failed. Treating as transient error. Exception:{0}", ex.ToString(), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 170);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0006ACF4 File Offset: 0x00068EF4
		private void Configure()
		{
			this.IsOnPremisesEnabled = this.ReadAttribute("EnableOnPrem", false);
			this.OabProtocolProbeTimeout = this.ReadAttribute("OabProtocolProbeTimeoutInSeconds", 20);
			this.OabMailboxProbeTimeout = this.ReadAttribute("OabMailboxProbeTimeoutInSeconds", 20);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0006AD64 File Offset: 0x00068F64
		private void SetupOabProbes(TracingContext traceContext, LocalEndpointManager endpointManager)
		{
			if (endpointManager.MailboxDatabaseEndpoint == null)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OnlineMeetingTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: No mailbox endpoint found {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 199);
				return;
			}
			IEnumerable<MailboxDatabaseInfo> enumerable = endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			if (enumerable == null)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OnlineMeetingTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: No mailbox databases found {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 212);
				return;
			}
			enumerable = from x in enumerable
			where !string.IsNullOrEmpty(x.MonitoringAccount) && !string.IsNullOrEmpty(x.MonitoringAccountPassword)
			select x;
			if (enumerable.Count<MailboxDatabaseInfo>() == 0)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OnlineMeetingTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: No mailbox databases were found with a monitoring mailbox {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 227);
				return;
			}
			MailboxDatabaseInfo credentialHolder = enumerable.First<MailboxDatabaseInfo>();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: Creating Oab protocol probe for server {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 239);
			this.SetupOabProtocolProbe(base.TraceContext, credentialHolder, endpointManager);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: Created Oab protocol probe for server {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 248);
			if (LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: No oab, skip the work item", null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 299);
				return;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: Creating Oab mailbox probe for server {0}", Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 257);
			if (endpointManager.OfflineAddressBookEndpoint.OrganizationMailboxDatabases == null || endpointManager.OfflineAddressBookEndpoint.OrganizationMailboxDatabases.Length == 0)
			{
				throw new ApplicationException(Strings.OabMailboxNoOrgMailbox);
			}
			IEnumerable<Guid> first = from n in endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend
			select n.MailboxDatabaseGuid;
			IEnumerable<Guid> source = first.Intersect(endpointManager.OfflineAddressBookEndpoint.OrganizationMailboxDatabases);
			IEnumerable<string> values = from n in source
			select n.ToString("D");
			string text = string.Join(",", values);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: OrgDatabases is {0}", text, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 275);
			foreach (Guid guid in endpointManager.OfflineAddressBookEndpoint.OfflineAddressBooks)
			{
				string text2 = guid.ToString("D");
				string endPoint = string.Format("{0}{1}/oab.xml", "https://localhost:444/oab/", text2);
				this.CreateOabMailboxProbe(base.TraceContext, credentialHolder, endpointManager, endPoint, text2, text);
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProbes: Created Oab Mailbox probe for OAB {0} on Server {1}", text2, Environment.MachineName, null, "SetupOabProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 289);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0006B050 File Offset: 0x00069250
		private void SetupOabProtocolProbe(TracingContext traceContext, MailboxDatabaseInfo credentialHolder, LocalEndpointManager endpointManager)
		{
			string name = "OabProtocolProbe";
			string text = "OabProtocolMonitor";
			string text2 = "OabProtocolResponder";
			string escalationSubjectUnhealthy = Strings.OabProtocolEscalationSubject(Environment.MachineName);
			string escalationMessageUnhealthy = Strings.OabProtocolEscalationBody;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProtocolProbe: Creating Oab protocol probe for this server", null, "SetupOabProtocolProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 322);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = OabDiscovery.AssemblyPath;
			probeDefinition.TypeName = OabDiscovery.OabProtocolProbeTypeName;
			probeDefinition.Name = name;
			probeDefinition.TargetResource = Environment.MachineName;
			probeDefinition.RecurrenceIntervalSeconds = 60;
			probeDefinition.TimeoutSeconds = this.OabProtocolProbeTimeout;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.Endpoint = string.Format("{0}{1}/oab.xml", "https://localhost:444/oab/", "123");
			probeDefinition.Account = credentialHolder.MonitoringAccount + "@" + credentialHolder.MonitoringAccountDomain;
			probeDefinition.AccountPassword = credentialHolder.MonitoringAccountPassword;
			probeDefinition.ServiceName = ExchangeComponent.Oab.Name;
			probeDefinition.Attributes["TrustAnySslCertificate"] = "True";
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(text, probeDefinition.ConstructWorkItemResultName(), ExchangeComponent.Oab.Name, ExchangeComponent.Oab, 4, true, 300);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)TimeSpan.FromMinutes(15.0).TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate OAB health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string text3 = monitorDefinition.ConstructWorkItemResultName();
			ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(text2, text3, "MSExchangeOABAppPool", ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, "Exchange", true, null);
			responderDefinition.ServiceName = ExchangeComponent.Oab.Name;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition(text2, ExchangeComponent.Oab.Name, text, text3, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Oab.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition2.WaitIntervalSeconds = 28800;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabDiscovery.SetupOabProtocolProbe: Created Oab protocol probe for this server", null, "SetupOabProtocolProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabDiscovery.cs", 396);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0006B2D8 File Offset: 0x000694D8
		private void CreateOabMailboxProbe(TracingContext traceContext, MailboxDatabaseInfo credentialHolder, LocalEndpointManager endpointManager, string endPoint, string oabGuid, string orgMbxDatabaseIds)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = OabDiscovery.AssemblyPath;
			probeDefinition.TypeName = OabDiscovery.OabMailboxProbeTypeName;
			probeDefinition.Name = "OabMailboxProbe";
			probeDefinition.TargetResource = oabGuid;
			probeDefinition.RecurrenceIntervalSeconds = 1800;
			probeDefinition.TimeoutSeconds = this.OabMailboxProbeTimeout;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.Endpoint = endPoint;
			probeDefinition.Account = credentialHolder.MonitoringAccount + "@" + credentialHolder.MonitoringAccountDomain;
			probeDefinition.AccountPassword = credentialHolder.MonitoringAccountPassword;
			probeDefinition.ServiceName = ExchangeComponent.Oab.Name;
			probeDefinition.Attributes["OrgMbxDBId"] = orgMbxDatabaseIds;
			probeDefinition.Attributes["TrustAnySslCertificate"] = "True";
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			this.CreateOabMailboxMonitor(probeDefinition, base.TraceContext, credentialHolder);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0006B3BC File Offset: 0x000695BC
		private void CreateOabMailboxMonitor(ProbeDefinition probeDef, TracingContext traceContext, MailboxDatabaseInfo dbInfo)
		{
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("OabMailboxMonitor", probeDef.ConstructWorkItemResultName(), ExchangeComponent.Oab.Name, ExchangeComponent.Oab, 4000, 0, 4, true);
			monitorDefinition.TargetResource = probeDef.TargetResource;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate OAB health is not impacted by mailbox issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			this.CreateOabMailboxResponders(probeDef, monitorDefinition, base.TraceContext, dbInfo);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0006B44C File Offset: 0x0006964C
		private void CreateOabMailboxResponders(ProbeDefinition probeDef, MonitorDefinition monitorDef, TracingContext traceContext, MailboxDatabaseInfo dbInfo)
		{
			string alertMask = monitorDef.ConstructWorkItemResultName();
			string name = "OabMailboxResponder";
			string escalationSubjectUnhealthy = Strings.OabMailboxEscalationSubject(probeDef.TargetResource, Environment.MachineName);
			string escalationMessageUnhealthy = Strings.OabMailboxEscalationBody;
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, ExchangeComponent.Oab.Name, monitorDef.Name, alertMask, probeDef.TargetResource, ServiceHealthStatus.Unhealthy, ExchangeComponent.Oab.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = 28800;
			responderDefinition.RecurrenceIntervalSeconds = 1800;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x04000BF3 RID: 3059
		private const int OabMailboxProbeRecurrenceIntervalSeconds = 1800;

		// Token: 0x04000BF4 RID: 3060
		private const int OabMailboxMonitorRecurrenceIntervalSeconds = 4000;

		// Token: 0x04000BF5 RID: 3061
		private const int OabMailboxResponderRecurrenceIntervalSeconds = 1800;

		// Token: 0x04000BF6 RID: 3062
		private const int OabProtocolProbeRecurrenceIntervalSeconds = 60;

		// Token: 0x04000BF7 RID: 3063
		private const int OabResponderWaitIntervalSeconds = 28800;

		// Token: 0x04000BF8 RID: 3064
		private const int OabProtocolProbeDefaultTimeout = 20;

		// Token: 0x04000BF9 RID: 3065
		private const int OabMailboxProbeDefaultTimeout = 20;

		// Token: 0x04000BFA RID: 3066
		private const string OabBrickUrl = "https://localhost:444/oab/";

		// Token: 0x04000BFB RID: 3067
		private const string OabProtocolProbeName = "OabProtocolProbe";

		// Token: 0x04000BFC RID: 3068
		private const string OabProtocolMonitorName = "OabProtocolMonitor";

		// Token: 0x04000BFD RID: 3069
		private const string OabProtocolResponderName = "OabProtocolResponder";

		// Token: 0x04000BFE RID: 3070
		private const string OabMailboxProbeName = "OabMailboxProbe";

		// Token: 0x04000BFF RID: 3071
		private const string OabMailboxMonitorName = "OabMailboxMonitor";

		// Token: 0x04000C00 RID: 3072
		private const string OabMailboxResponderName = "OabMailboxResponder";

		// Token: 0x04000C01 RID: 3073
		private const string OabAppPoolName = "MSExchangeOABAppPool";

		// Token: 0x04000C02 RID: 3074
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000C03 RID: 3075
		private static readonly string OabProtocolProbeTypeName = typeof(OabProtocolProbe).FullName;

		// Token: 0x04000C04 RID: 3076
		private static readonly string OabMailboxProbeTypeName = typeof(OabMailboxProbe).FullName;
	}
}
