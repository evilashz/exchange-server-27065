using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Psws.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Psws
{
	// Token: 0x02000516 RID: 1302
	public sealed class PswsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600200B RID: 8203 RVA: 0x000C3CFD File Offset: 0x000C1EFD
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.SetupBackEndProbeContexts();
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000C3D08 File Offset: 0x000C1F08
		private void SetupBackEndProbeContexts()
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsClientAccessRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PswsTracer, base.TraceContext, "PswsDiscovery.DoWork: no client access role installed, skip PswsBackEndProbe", null, "SetupBackEndProbeContexts", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 100);
				return;
			}
			if (!LocalEndpointManager.IsDataCenter)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PswsTracer, base.TraceContext, "PswsDiscovery.DoWork: PswsBackEndProbe can only be run in datacenter", null, "SetupBackEndProbeContexts", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 104);
				return;
			}
			if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PswsTracer, base.TraceContext, "PswsDiscovery.DoWork: no mailbox database found on this server", null, "SetupBackEndProbeContexts", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 110);
				return;
			}
			this.SetupPswsBackEndProbeContext(instance);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000C3DC4 File Offset: 0x000C1FC4
		private void SetupPswsBackEndProbeContext(LocalEndpointManager endpointManager)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.PswsTracer, base.TraceContext, "PswsDiscovery.SetupPswsBackEndProbeContext: Begin to setup context for PswsBackEndProbe", null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 125);
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccount))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PswsTracer, base.TraceContext, "PswsDiscovery.SetupPswsBackEndProbeContext: Ignore mailbox database {0} because it does not have monitoring mailbox", mailboxDatabaseInfo.MailboxDatabaseName, null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 134);
				}
				else if (LocalEndpointManager.IsDataCenter)
				{
					if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountSid) || string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPartitionId))
					{
						WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.PswsTracer, base.TraceContext, "Skip adding certificate logon probe due to missing mandatory fields Sid={0} Partition={1}", mailboxDatabaseInfo.MonitoringAccountSid, mailboxDatabaseInfo.MonitoringAccountPartitionId, null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 147);
					}
					else
					{
						ProbeDefinition definition = this.CreatePswsBackEndProbeDefinition(AccessTokenType.CertificateSid, mailboxDatabaseInfo);
						base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PswsTracer, base.TraceContext, "Add Certificate PswsBackEndProbe for account :{0}", mailboxDatabaseInfo.MonitoringAccount, null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 153);
					}
					if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPuid) || string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPartitionId) || string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountSid))
					{
						WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.PswsTracer, base.TraceContext, "Skip adding LiveId logon probe due to missing mandatory fields Sid={0} Partition={1} Puid={2}", mailboxDatabaseInfo.MonitoringAccountSid, mailboxDatabaseInfo.MonitoringAccountPartitionId, mailboxDatabaseInfo.MonitoringAccountPuid, null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 160);
					}
					else
					{
						ProbeDefinition definition2 = this.CreatePswsBackEndProbeDefinition(AccessTokenType.LiveIdBasic, mailboxDatabaseInfo);
						base.Broker.AddWorkDefinition<ProbeDefinition>(definition2, base.TraceContext);
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PswsTracer, base.TraceContext, "Add LiveId PswsBackEndProbe for account :{0}", mailboxDatabaseInfo.MonitoringAccount, null, "SetupPswsBackEndProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 166);
					}
				}
			}
			MonitorDefinition monitorDefinition = this.CreatePswsBackEndMonitorDefinition();
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate PSWS is not impacted by BE logon issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(PswsDiscovery.BuildWorkItemName(PswsDiscovery.PswsDeeptTestString, PswsDiscovery.ResetIISAppPoolString), monitorDefinition.Name, "MSExchangePswsAppPool", ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, "Exchange", true, null);
			responderDefinition.ServiceName = ExchangeComponent.Psws.Name;
			string escalationSubjectUnhealthy = Strings.PswsEscalationSubject(Environment.MachineName);
			string escalationMessageUnhealthy = Strings.PswsEscalationBody(Environment.MachineName) + Environment.NewLine + "{Probe.Exception}";
			ResponderDefinition definition3 = EscalateResponder.CreateDefinition(PswsDiscovery.BuildWorkItemName(PswsDiscovery.PswsDeeptTestString, PswsDiscovery.EscalateString), ExchangeComponent.Psws.Name, monitorDefinition.Name, monitorDefinition.Name, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Psws.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition3, base.TraceContext);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000C40E4 File Offset: 0x000C22E4
		internal ProbeDefinition CreatePswsBackEndProbeDefinition(AccessTokenType tokenType, MailboxDatabaseInfo dbInfo)
		{
			return this.CreatePswsBackEndProbeDefinition(tokenType, dbInfo.MonitoringAccount, dbInfo.MonitoringAccountPassword, dbInfo.MonitoringAccount + "@" + dbInfo.MonitoringAccountDomain);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000C4110 File Offset: 0x000C2310
		internal ProbeDefinition CreatePswsBackEndProbeDefinition(AccessTokenType tokenType, string monitoringAccountName, string monitoringAccountPassword, string monitoringAccountLiveID)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.TypeName = PswsDiscovery.PswsBackEndProbeTypeName;
			probeDefinition.Name = PswsDiscovery.BuildWorkItemName(PswsDiscovery.PswsDeeptTestString, PswsDiscovery.ProbeString);
			probeDefinition.ServiceName = ExchangeComponent.Psws.Name;
			probeDefinition.TargetResource = string.Format("{0}.{1}", tokenType, monitoringAccountName);
			probeDefinition.RecurrenceIntervalSeconds = 30 * instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count;
			probeDefinition.TimeoutSeconds = Math.Min(180, probeDefinition.RecurrenceIntervalSeconds);
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.AccountDisplayName = monitoringAccountName;
			probeDefinition.Account = monitoringAccountLiveID;
			probeDefinition.AccountPassword = monitoringAccountPassword;
			probeDefinition.Endpoint = PswsDiscovery.PswsBackEndUrl;
			probeDefinition.Attributes["PswsMailboxUrlSuffix"] = PswsDiscovery.PswsUrlSuffix;
			probeDefinition.Attributes["AccessTokenType"] = tokenType.ToString();
			return probeDefinition;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000C4208 File Offset: 0x000C2408
		internal MonitorDefinition CreatePswsBackEndMonitorDefinition()
		{
			MonitorStateTransition[] monitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 1800)
			};
			TimeSpan monitoringInterval = TimeSpan.FromMinutes(1.0);
			TimeSpan recurrenceInterval = TimeSpan.FromMinutes(1.0);
			TimeSpan secondaryMonitoringInterval = TimeSpan.FromMinutes(5.0);
			string name = PswsDiscovery.BuildWorkItemName(PswsDiscovery.PswsDeeptTestString, PswsDiscovery.MonitorString);
			string sampleMask = PswsDiscovery.BuildWorkItemName(PswsDiscovery.PswsDeeptTestString, PswsDiscovery.ProbeString);
			MonitorDefinition monitorDefinition = OverallPercentSuccessByStateAttribute1Monitor.CreateDefinition(name, sampleMask, ExchangeComponent.Psws.Name, ExchangeComponent.Psws, 90.0, monitoringInterval, recurrenceInterval, secondaryMonitoringInterval, "", true);
			monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
			monitorDefinition.TimeoutSeconds = Math.Min(monitorDefinition.TimeoutSeconds, (int)recurrenceInterval.TotalSeconds);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.PswsTracer, base.TraceContext, "Create {0}", monitorDefinition.Name, null, "CreatePswsBackEndMonitorDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PSWS\\PswsDiscovery.cs", 291);
			return monitorDefinition;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000C4306 File Offset: 0x000C2506
		private static string BuildWorkItemName(string probeType, string workItemType)
		{
			return string.Format("{0}{1}", probeType, workItemType);
		}

		// Token: 0x04001783 RID: 6019
		private const string PswsAppPoolName = "MSExchangePswsAppPool";

		// Token: 0x04001784 RID: 6020
		internal static readonly string PswsBackEndUrl = "https://localhost:444/psws/service.svc/";

		// Token: 0x04001785 RID: 6021
		internal static readonly string PswsUrlSuffix = "Mailbox?ResultSize=1";

		// Token: 0x04001786 RID: 6022
		internal static readonly string PswsDeeptTestString = "PswsDeepTest";

		// Token: 0x04001787 RID: 6023
		internal static readonly string ProbeString = "Probe";

		// Token: 0x04001788 RID: 6024
		internal static readonly string MonitorString = "Monitor";

		// Token: 0x04001789 RID: 6025
		internal static readonly string ResetIISAppPoolString = "ResetIISAppPool";

		// Token: 0x0400178A RID: 6026
		internal static readonly string EscalateString = "Escalate";

		// Token: 0x0400178B RID: 6027
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400178C RID: 6028
		private static readonly string PswsBackEndProbeTypeName = typeof(PswsBackEndProbe).FullName;
	}
}
