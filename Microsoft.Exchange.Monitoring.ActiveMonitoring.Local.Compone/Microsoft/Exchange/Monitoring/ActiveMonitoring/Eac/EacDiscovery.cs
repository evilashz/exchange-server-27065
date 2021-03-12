using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Eac.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Eac
{
	// Token: 0x02000166 RID: 358
	public sealed class EacDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00040834 File Offset: 0x0003EA34
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0004083C File Offset: 0x0003EA3C
		public bool LogSuccessProbeResult { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00040845 File Offset: 0x0003EA45
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0004084D File Offset: 0x0003EA4D
		public int ConsecutiveFailureCount { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00040856 File Offset: 0x0003EA56
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0004085E File Offset: 0x0003EA5E
		public int ProbeMaxRetryAttempts { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00040867 File Offset: 0x0003EA67
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0004086F File Offset: 0x0003EA6F
		public int EacBackEndPingProbeRecurrenceInterval { get; private set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00040878 File Offset: 0x0003EA78
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00040880 File Offset: 0x0003EA80
		public int EacBackEndPingProbeTimeout { get; private set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00040889 File Offset: 0x0003EA89
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00040891 File Offset: 0x0003EA91
		public int EacBackEndPingMonitorRecurrenceInterval { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0004089A File Offset: 0x0003EA9A
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x000408A2 File Offset: 0x0003EAA2
		public int EacBackEndLogonProbeRecurrenceInterval { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000408AB File Offset: 0x0003EAAB
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x000408B3 File Offset: 0x0003EAB3
		public int EacBackEndLogonProbeTimeout { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000408BC File Offset: 0x0003EABC
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000408C4 File Offset: 0x0003EAC4
		public TimeSpan EacBackEndLogonMonitorRecurrenceInterval { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000408CD File Offset: 0x0003EACD
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000408D5 File Offset: 0x0003EAD5
		public TimeSpan SecondaryMonitoringInterval { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000408DE File Offset: 0x0003EADE
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000408E6 File Offset: 0x0003EAE6
		public TimeSpan MonitorStateTransitions { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000408EF File Offset: 0x0003EAEF
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000408F7 File Offset: 0x0003EAF7
		public int AvailabilityPercentage { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00040900 File Offset: 0x0003EB00
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00040908 File Offset: 0x0003EB08
		public int DegradedTransitionSpan { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00040911 File Offset: 0x0003EB11
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00040919 File Offset: 0x0003EB19
		public int UnrecoverableTransitionSpan { get; private set; }

		// Token: 0x06000A56 RID: 2646 RVA: 0x00040924 File Offset: 0x0003EB24
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				this.LogSuccessProbeResult = this.ReadAttribute("LogSuccessProbeResult", true);
				this.ConsecutiveFailureCount = this.ReadAttribute("ConsecutiveFailureCount", 4);
				this.ProbeMaxRetryAttempts = this.ReadAttribute("ProbeMaxRetryAttempts", 0);
				this.EacBackEndPingProbeRecurrenceInterval = (int)this.ReadAttribute("EacBackEndPingProbeRecurrenceInterval", TimeSpan.FromSeconds(60.0)).TotalSeconds;
				this.EacBackEndPingProbeTimeout = (int)this.ReadAttribute("EacBackEndPingProbeTimeout", TimeSpan.FromSeconds(30.0)).TotalSeconds;
				this.EacBackEndPingMonitorRecurrenceInterval = (int)this.ReadAttribute("EacBackEndPingMonitorRecurrenceInterval", TimeSpan.FromSeconds(0.0)).TotalSeconds;
				this.EacBackEndLogonProbeRecurrenceInterval = (int)this.ReadAttribute("EacBackEndLogonProbeRecurrenceInterval", TimeSpan.FromSeconds(300.0)).TotalSeconds;
				this.EacBackEndLogonProbeTimeout = (int)this.ReadAttribute("EacBackEndLogonProbeTimeout", TimeSpan.FromSeconds(120.0)).TotalSeconds;
				this.EacBackEndLogonMonitorRecurrenceInterval = this.ReadAttribute("EacBackEndLogonMonitorRecurrenceInterval", TimeSpan.FromSeconds(3600.0));
				this.SecondaryMonitoringInterval = this.ReadAttribute("SecondaryMonitoringInterval", TimeSpan.FromSeconds(7200.0));
				this.MonitorStateTransitions = this.ReadAttribute("MonitorStateTransitions", TimeSpan.FromSeconds(7200.0));
				this.AvailabilityPercentage = this.ReadAttribute("AvailabilityPercentage", 60);
				this.DegradedTransitionSpan = (int)this.ReadAttribute("DegradedTransitionSpan", TimeSpan.FromSeconds(0.0)).TotalSeconds;
				this.UnrecoverableTransitionSpan = (int)this.ReadAttribute("UnrecoverableTransitionSpan", TimeSpan.FromSeconds(1200.0)).TotalSeconds;
				if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsClientAccessRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.DoWork: no client access role installed, skip EacBackEndPingProbe and EacBackEndLogonProbe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 168);
				}
				else
				{
					this.SetupEacBackEndPingProbeContext();
					if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.DoWork: no mailbox database found on this server, skip EacBackEndLogonProbe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 176);
					}
					else
					{
						this.SetupEacBackEndLogonProbeContext(instance);
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceWarning(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.DoWork: EndpointManagerEndpointUninitializedException is caught.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 186);
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00040BCC File Offset: 0x0003EDCC
		private void SetupEacBackEndPingProbeContext()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.SetupEacBackEndPingProbeContext: Begin to setup context for EacBackEndPingProbe", null, "SetupEacBackEndPingProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 198);
			string text = this.BuildWorkItemName("EacBackEndPing", "Probe");
			ProbeDefinition definition = this.CreateProbe(text, EacDiscovery.AssemblyPath, EacDiscovery.EacBackEndPingProbeTypeName, EacDiscovery.EacBackEndUrl, this.EacBackEndPingProbeRecurrenceInterval, this.EacBackEndPingProbeTimeout, this.ProbeMaxRetryAttempts, "EacBackEndPingProbe", null);
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
			string name = this.BuildWorkItemName("EacBackEndPing", "Monitor");
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, text, ExchangeComponent.Ecp.Name, ExchangeComponent.Ecp, this.ConsecutiveFailureCount, true, 300);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSpan),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSpan)
			};
			monitorDefinition.RecurrenceIntervalSeconds = this.EacBackEndPingMonitorRecurrenceInterval;
			monitorDefinition.IsHaImpacting = false;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate EAC health is not impacted by BE connectivity issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string escalationSubjectUnhealthy = Strings.EacSelfTestEscalationSubject(Environment.MachineName);
			string escalationMessageUnhealthy = Strings.EacSelfTestEscalationBody(Environment.MachineName, Path.Combine(EacDiscovery.EacMonitoringLogFolderPath.Value, "EacBackEndPingProbe"));
			ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(this.BuildWorkItemName("EacBackEndPing", "ResetIISAppPool"), monitorDefinition.Name, "MSExchangeECPAppPool", ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, "Exchange", true, null);
			responderDefinition.ServiceName = ExchangeComponent.Ecp.Name;
			ResponderDefinition definition2 = EscalateResponder.CreateDefinition(this.BuildWorkItemName("EacBackEndPing", "Escalate"), ExchangeComponent.Ecp.Name, monitorDefinition.Name, monitorDefinition.Name, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Ecp.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, EacDiscovery.EacDailyScheduledPattern, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition2, base.TraceContext);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.SetupEacBackEndPingProbeContext: Finish to setup context for EacBackEndPingProbe", null, "SetupEacBackEndPingProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 273);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00040E08 File Offset: 0x0003F008
		private void SetupEacBackEndLogonProbeContext(LocalEndpointManager endpointManager)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.SetupEacBackEndLogonProbeContext: Begin to setup context for EacBackEndLogonProbe", null, "SetupEacBackEndLogonProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 285);
			string text = this.BuildWorkItemName("EacBackEndLogon", "Probe");
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in endpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				if (string.IsNullOrWhiteSpace(mailboxDatabaseInfo.MonitoringAccount))
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.SetupEacBackEndLogonProbe: Ignore mailbox database {0} because it does not have monitoring mailbox", mailboxDatabaseInfo.MailboxDatabaseName, null, "SetupEacBackEndLogonProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 298);
				}
				else
				{
					ProbeDefinition probeDefinition = this.CreateProbe(text, EacDiscovery.AssemblyPath, EacDiscovery.EacBackEndLogonProbeTypeName, EacDiscovery.EacBackEndUrl, this.EacBackEndLogonProbeRecurrenceInterval, this.EacBackEndLogonProbeTimeout, this.ProbeMaxRetryAttempts, "EacBackEndLogonProbe", mailboxDatabaseInfo);
					probeDefinition.Attributes["DatabaseGuid"] = mailboxDatabaseInfo.MailboxDatabaseGuid.ToString();
					base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				}
			}
			string name = this.BuildWorkItemName("EacBackEndLogon", "Monitor");
			MonitorDefinition monitorDefinition = OverallPercentSuccessByStateAttribute1Monitor.CreateDefinition(name, text, ExchangeComponent.Ecp.Name, ExchangeComponent.Ecp, (double)this.AvailabilityPercentage, this.SecondaryMonitoringInterval, this.EacBackEndLogonMonitorRecurrenceInterval, this.MonitorStateTransitions, "", true);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSpan),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSpan)
			};
			monitorDefinition.IsHaImpacting = false;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate EAC health is not impacted by BE logon issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string escalationSubjectUnhealthy = Strings.EacDeepTestEscalationSubject(Environment.MachineName);
			string escalationMessageUnhealthy = Strings.EacDeepTestEscalationBody(Environment.MachineName, Path.Combine(EacDiscovery.EacMonitoringLogFolderPath.Value, "EacBackEndLogonProbe"));
			ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(this.BuildWorkItemName("EacBackEndLogon", "ResetIISAppPool"), monitorDefinition.Name, "MSExchangeECPAppPool", ServiceHealthStatus.Degraded, DumpMode.None, null, 15.0, 0, "Exchange", true, null);
			responderDefinition.ServiceName = ExchangeComponent.Ecp.Name;
			ResponderDefinition definition = EscalateResponder.CreateDefinition(this.BuildWorkItemName("EacBackEndLogon", "Escalate"), ExchangeComponent.Ecp.Name, monitorDefinition.Name, monitorDefinition.Name, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Ecp.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, EacDiscovery.EacDailyScheduledPattern, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, "EacDiscovery.SetupEacBackEndLogonProbeContext: Finish to setup context for EacBackEndLogonProbe", null, "SetupEacBackEndLogonProbeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 378);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000410FC File Offset: 0x0003F2FC
		private ProbeDefinition CreateProbe(string probeName, string assemblyPath, string probeTypeName, string endPoint, int recurrenceIntervalSeconds, int timeoutSeconds, int maxRetryAttempts, string logFileInstance = null, MailboxDatabaseInfo dbInfo = null)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, string.Format("EacDiscovery.CreateProbe: Creating probe {0} of type {1}", probeName, probeTypeName), null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 408);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = ExchangeComponent.Ecp.Name;
			probeDefinition.AssemblyPath = assemblyPath;
			probeDefinition.TypeName = probeTypeName;
			probeDefinition.Endpoint = endPoint;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = timeoutSeconds;
			probeDefinition.MaxRetryAttempts = maxRetryAttempts;
			probeDefinition.Attributes["SslValidationOptions"] = SslValidationOptions.NoSslValidation.ToString();
			if (!string.IsNullOrEmpty(logFileInstance))
			{
				probeDefinition.Attributes["LogFileInstanceName"] = logFileInstance;
				probeDefinition.Attributes["LogSuccessProbeResult"] = this.LogSuccessProbeResult.ToString();
			}
			if (dbInfo != null)
			{
				string monitoringDomain = dbInfo.MonitoringAccountUserPrincipalName.Substring(dbInfo.MonitoringAccountUserPrincipalName.IndexOf('@') + 1);
				probeDefinition.Account = dbInfo.MonitoringAccountUserPrincipalName;
				probeDefinition.AccountPassword = dbInfo.MonitoringAccountPassword;
				probeDefinition.AccountDisplayName = dbInfo.MonitoringAccountUserPrincipalName;
				probeDefinition.TargetResource = dbInfo.MailboxDatabaseName;
				OwaUtils.AddBackendAuthenticationParameters(probeDefinition, dbInfo.MonitoringAccountSid, monitoringDomain);
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, string.Format("EacDiscovery.CreateProbe: Created probe {0} of type {1}", probeName, probeTypeName), null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Eac\\EacDiscovery.cs", 445);
			return probeDefinition;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00041264 File Offset: 0x0003F464
		private string BuildWorkItemName(string probeType, string workItemType)
		{
			return string.Format("{0}{1}", probeType, workItemType);
		}

		// Token: 0x04000745 RID: 1861
		private const string EacBackEndPingString = "EacBackEndPing";

		// Token: 0x04000746 RID: 1862
		private const string EacBackEndLogonString = "EacBackEndLogon";

		// Token: 0x04000747 RID: 1863
		private const string ProbeString = "Probe";

		// Token: 0x04000748 RID: 1864
		private const string MonitorString = "Monitor";

		// Token: 0x04000749 RID: 1865
		private const string EscalateString = "Escalate";

		// Token: 0x0400074A RID: 1866
		private const string ResetIISAppPoolString = "ResetIISAppPool";

		// Token: 0x0400074B RID: 1867
		private const string EacAppPoolName = "MSExchangeECPAppPool";

		// Token: 0x0400074C RID: 1868
		private static readonly Lazy<string> EacMonitoringLogFolderPath = new Lazy<string>(() => Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Monitoring\\ECP\\"));

		// Token: 0x0400074D RID: 1869
		private static readonly string EacBackEndUrl = "https://localhost:444/ecp/";

		// Token: 0x0400074E RID: 1870
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400074F RID: 1871
		private static readonly string EacBackEndPingProbeTypeName = typeof(EacBackEndPingProbe).FullName;

		// Token: 0x04000750 RID: 1872
		private static readonly string EacBackEndLogonProbeTypeName = typeof(EacBackEndLogonProbe).FullName;

		// Token: 0x04000751 RID: 1873
		private static readonly string EacDailyScheduledPattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday/09:00/17:00";
	}
}
