using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws
{
	// Token: 0x0200045C RID: 1116
	public sealed class RwsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x000A4528 File Offset: 0x000A2728
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.DoWork: enter", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 103);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			this.SetupRwsCallProbeContext(instance, "https://localhost:444/ecp/reportingwebservice/reporting.svc/");
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000A456C File Offset: 0x000A276C
		private ProbeDefinition CreateProbe(string probeName, string probeTypeName, string endPoint, int recurrenceIntervalSeconds, int timeoutSeconds, int maxRetryAttempts, MailboxDatabaseInfo dbInfo)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.CreateProbe: Creating probe {0}", probeName, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 158);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = RwsDiscovery.AssemblyPath;
			probeDefinition.TypeName = RwsDiscovery.RwsProbeTypeName;
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = ExchangeComponent.Rws.Service;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = timeoutSeconds;
			probeDefinition.MaxRetryAttempts = maxRetryAttempts;
			probeDefinition.Account = dbInfo.MonitoringAccount + "@" + dbInfo.MonitoringAccountDomain;
			probeDefinition.AccountPassword = dbInfo.MonitoringAccountPassword;
			probeDefinition.AccountDisplayName = dbInfo.MonitoringAccount;
			probeDefinition.Endpoint = endPoint;
			probeDefinition.TargetResource = ((dbInfo == null) ? string.Empty : dbInfo.MailboxDatabaseName);
			probeDefinition.Attributes["SslValidationOptions"] = SslValidationOptions.NoSslValidation.ToString();
			WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "puid:{0}/sid:{1}/partitionId:{2}", dbInfo.MonitoringAccountPuid, dbInfo.MonitoringAccountSid, dbInfo.MonitoringAccountPartitionId, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 183);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.CreateProbe: Created probe {0}", probeName, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 191);
			return probeDefinition;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000A46C0 File Offset: 0x000A28C0
		private MonitorDefinition CreateMonitor(string monitorName, string sampleMask, double availabilityPercentage, TimeSpan recurrenceInterval, TimeSpan monitoringInterval, TimeSpan secondaryMonitoringInterval, MonitorStateTransition[] monitorStateTransitions)
		{
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.CreateMonitor: Creating monitor {0} of type {1}", monitorName, RwsDiscovery.OverallPercentSuccessByStateAttribute1MonitorTypeName, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 220);
			MonitorDefinition monitorDefinition = OverallPercentSuccessByStateAttribute1Monitor.CreateDefinition(monitorName, sampleMask, ExchangeComponent.Rws.Service, ExchangeComponent.Rws, availabilityPercentage, monitoringInterval, recurrenceInterval, secondaryMonitoringInterval, "", true);
			monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.CreateMonitor: Creating monitor {0} of type {1}", monitorName, RwsDiscovery.OverallPercentSuccessByStateAttribute1MonitorTypeName, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 239);
			return monitorDefinition;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000A4750 File Offset: 0x000A2950
		private ResponderDefinition CreateResponder(string responderName, string assemblyPath, string responderTypeName, ServiceHealthStatus targetHealthState, string alertTypeId, string alertMask, int recurrenceIntervalSeconds, int timeoutSeconds, int waitIntervalSeconds, string serviceName)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("RwsDiscovery.CreateResponder: Creating responder {0} of type {1}", responderName, responderTypeName), null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 275);
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.Name = responderName;
			responderDefinition.AssemblyPath = assemblyPath;
			responderDefinition.TypeName = responderTypeName;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.TimeoutSeconds = timeoutSeconds;
			responderDefinition.WaitIntervalSeconds = waitIntervalSeconds;
			responderDefinition.ServiceName = serviceName;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("RwsDiscovery.CreateResponder: Created responder {0} of type {1}", responderName, responderTypeName), null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 292);
			return responderDefinition;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000A480C File Offset: 0x000A2A0C
		private void SetupRwsCallProbeContext(LocalEndpointManager endpointManager, string endpointUrl)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.SetupRwsCallContext: Begin to setup context for RwsCallProbe. Endpoint url: {0}", endpointUrl, null, "SetupRwsCallProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 307);
			int timeoutSeconds = 180;
			int maxRetryAttempts = 0;
			string text = this.BuildWorkItemName(RwsDiscovery.RwsSelfTestString, RwsDiscovery.ProbeString);
			int recurrenceIntervalSeconds = 600;
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccount))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.DoWork: Ignore mailbox database {0} because it does not have monitoring mailbox", mailboxDatabaseInfo.MailboxDatabaseName, null, "SetupRwsCallProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 325);
				}
				else if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPuid) || string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountPartitionId) || string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccountSid))
				{
					WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.DoWork: Ignore mailbox database {0} due to missing mandatory fields on the monitoring mailbox Sid={0} Partition={1} Puid={2}", mailboxDatabaseInfo.MonitoringAccountSid, mailboxDatabaseInfo.MonitoringAccountPartitionId, mailboxDatabaseInfo.MonitoringAccountPuid, null, "SetupRwsCallProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 337);
				}
				else
				{
					ProbeDefinition definition = this.CreateProbe(text, RwsDiscovery.RwsProbeTypeName, endpointUrl, recurrenceIntervalSeconds, timeoutSeconds, maxRetryAttempts, mailboxDatabaseInfo);
					base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
				}
			}
			MonitorDefinition monitorDefinition = this.CreateMonitor(this.BuildWorkItemName(RwsDiscovery.RwsSelfTestString, RwsDiscovery.MonitorString), text, 60.0, TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(90.0), new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)TimeSpan.FromMinutes(30.0).TotalSeconds)
			});
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Valdiate RWS health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string responderName = this.BuildWorkItemName(RwsDiscovery.RwsSelfTestString, RwsDiscovery.ResetIISAppPoolString);
			string name = this.BuildWorkItemName(RwsDiscovery.RwsSelfTestString, RwsDiscovery.EscalateString);
			ResponderDefinition responderDefinition = this.CreateResponder(responderName, RwsDiscovery.AssemblyPath, RwsDiscovery.ResetIISAppPoolResponderTypeName, ServiceHealthStatus.Degraded, monitorDefinition.Name, monitorDefinition.Name, 300, 60, 300, ExchangeComponent.Rws.Service);
			responderDefinition.Attributes["AppPoolName"] = "MSExchangeReportingWebServiceAppPool";
			ResponderDefinition definition2 = OBDEscalateResponder.CreateDefinition(name, ExchangeComponent.Rws.Service, monitorDefinition.Name, monitorDefinition.Name, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Rws.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.EscalationMessagePercentUnhealthy(Strings.GenericOverallXFailureEscalationMessage(ExchangeComponent.Rws.Name)), true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition2, base.TraceContext);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, "RwsDiscovery.SetupRwsCallProbeContext: Finish to setup context for RwsCallProbe", null, "SetupRwsCallProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDiscovery.cs", 413);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000A4B48 File Offset: 0x000A2D48
		private string BuildWorkItemName(string probeType, string workItemType)
		{
			return string.Format("{0}{1}", probeType, workItemType);
		}

		// Token: 0x04001375 RID: 4981
		private const string RwsAppPoolName = "MSExchangeReportingWebServiceAppPool";

		// Token: 0x04001376 RID: 4982
		private const string RwsEndpointUrl = "https://localhost:444/ecp/reportingwebservice/reporting.svc/";

		// Token: 0x04001377 RID: 4983
		internal static readonly string RwsSelfTestString = "RwsSelfTest";

		// Token: 0x04001378 RID: 4984
		internal static readonly string ProbeString = "Probe";

		// Token: 0x04001379 RID: 4985
		internal static readonly string MonitorString = "Monitor";

		// Token: 0x0400137A RID: 4986
		internal static readonly string ResetIISAppPoolString = "ResetIISAppPool";

		// Token: 0x0400137B RID: 4987
		internal static readonly string EscalateString = "Escalate";

		// Token: 0x0400137C RID: 4988
		private static readonly string RwsMailboxActivityReportUrl = string.Format("{0}{1}", "https://localhost:444/ecp/reportingwebservice/reporting.svc/", "MailboxActivityDaily");

		// Token: 0x0400137D RID: 4989
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400137E RID: 4990
		private static readonly string RwsProbeTypeName = typeof(RwsProbe).FullName;

		// Token: 0x0400137F RID: 4991
		private static readonly string OverallPercentSuccessByStateAttribute1MonitorTypeName = typeof(OverallPercentSuccessByStateAttribute1Monitor).FullName;

		// Token: 0x04001380 RID: 4992
		private static readonly string ResetIISAppPoolResponderTypeName = typeof(ResetIISAppPoolResponder).FullName;

		// Token: 0x04001381 RID: 4993
		private static readonly string EscalateResponderTypeName = typeof(OBDEscalateResponder).FullName;
	}
}
