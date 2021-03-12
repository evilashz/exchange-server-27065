using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa
{
	// Token: 0x02000272 RID: 626
	internal sealed class InstantMessagingLocalDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x00076C88 File Offset: 0x00074E88
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (!LocalEndpointManager.IsDataCenter || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				return;
			}
			IEnumerable<MailboxDatabaseInfo> enumerable = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			enumerable = from x in enumerable
			where !string.IsNullOrEmpty(x.MonitoringAccount) && !string.IsNullOrEmpty(x.MonitoringAccountPassword)
			select x;
			if (enumerable == null || enumerable.Count<MailboxDatabaseInfo>() == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "No mailbox databases with a monitoring mailbox found on this server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\InstantMessaging\\InstantMessagingLocalDiscovery.cs", 75);
				return;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in enumerable)
			{
				if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid))
				{
					ProbeDefinition probeDefinition = new ProbeDefinition();
					probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
					probeDefinition.TypeName = typeof(InstantMessagingLocalProbe).FullName;
					probeDefinition.Name = "OwaIMLocalProbe";
					probeDefinition.Account = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
					probeDefinition.AccountPassword = mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.AccountDisplayName = mailboxDatabaseInfo.MonitoringAccount + " - Pwd: " + mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.SecondaryAccount = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
					probeDefinition.SecondaryAccountPassword = mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.SecondaryAccountDisplayName = mailboxDatabaseInfo.MonitoringAccount + " - Pwd: " + mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.ServiceName = ExchangeComponent.InstantMessaging.Name;
					probeDefinition.TargetResource = mailboxDatabaseInfo.MailboxDatabaseName;
					probeDefinition.MaxRetryAttempts = 2;
					probeDefinition.RecurrenceIntervalSeconds = 600;
					probeDefinition.TimeoutSeconds = 120;
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "Configured probe OwaIMLocalProbe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\InstantMessaging\\InstantMessagingLocalDiscovery.cs", 102);
					base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
					MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("OwaIMLocalMonitor", "OwaIMLocalProbe", ExchangeComponent.InstantMessaging.Name, ExchangeComponent.InstantMessaging, 6, true, 3600);
					monitorDefinition.RecurrenceIntervalSeconds = 3600;
					MonitorStateTransition[] monitorStateTransitions = new MonitorStateTransition[]
					{
						new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 0)
					};
					monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
					monitorDefinition.ServicePriority = 0;
					monitorDefinition.ScenarioDescription = "Validate OWA health is not impacted by instanct messaging issues";
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
					ResponderDefinition definition = EscalateResponder.CreateDefinition("OwaIMLocalResponder", ExchangeComponent.InstantMessaging.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.InstantMessaging.EscalationTeam, Strings.OwaIMSigninFailedSubject(Environment.MachineName), Strings.OwaIMSigninFailedMessage(Environment.MachineName), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
					base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
					break;
				}
			}
		}

		// Token: 0x04000D55 RID: 3413
		public const string IMLocalProbeName = "OwaIMLocalProbe";

		// Token: 0x04000D56 RID: 3414
		public const string IMLocalMonitorName = "OwaIMLocalMonitor";

		// Token: 0x04000D57 RID: 3415
		public const string IMLocalResponderName = "OwaIMLocalResponder";

		// Token: 0x04000D58 RID: 3416
		public const int ProbeTimeoutSeconds = 120;

		// Token: 0x04000D59 RID: 3417
		public const int ProbeRecurrenceIntervalSeconds = 600;

		// Token: 0x04000D5A RID: 3418
		public const int ProbeFailureCountThreshold = 6;

		// Token: 0x04000D5B RID: 3419
		public const int MonitoringInterval = 3600;
	}
}
