using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Ediscovery;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Ediscovery.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000167 RID: 359
	public class EdiscoveryDiscovery : DiscoveryWorkItem
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0004130B File Offset: 0x0003F50B
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.EdiscoveryTracer;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00041358 File Offset: 0x0003F558
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				DiscoveryWorkItem.AddTestsForEachResource<MailboxDatabaseInfo>(() => from dbinfo in LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend
				where !string.IsNullOrEmpty(dbinfo.MonitoringAccount)
				select dbinfo, new Action<IEnumerable<MailboxDatabaseInfo>>[]
				{
					new Action<IEnumerable<MailboxDatabaseInfo>>(this.AddEdiscoveryDeepTest)
				});
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000413B0 File Offset: 0x0003F5B0
		private static ProbeDefinition CreateProbe<TProbe>(ProbeIdentity probeIdentity)
		{
			return new ProbeDefinition
			{
				ServiceName = probeIdentity.ServiceName,
				Name = probeIdentity.Name,
				TargetResource = probeIdentity.TargetResource,
				AssemblyPath = typeof(TProbe).Assembly.Location,
				TypeName = typeof(TProbe).FullName,
				MaxRetryAttempts = 0,
				Enabled = true
			};
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0004143C File Offset: 0x0003F63C
		private void AddEdiscoveryDeepTest(IEnumerable<MailboxDatabaseInfo> databases)
		{
			ProbeIdentity probeIdentity2 = ProbeIdentity.Create(ExchangeComponent.EdiscoveryProtocol, ProbeType.DeepTest, null, null);
			if (this.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, probeIdentity2, Strings.NoBackendMonitoringAccountsAvailable))
			{
				this.AddForEachDatabase(databases, probeIdentity2, delegate(ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo)
				{
					base.AddWorkDefinition<ProbeDefinition>(EdiscoveryDiscovery.CreateProbe<EdiscoveryProbe>(probeIdentity).ConfigureEdiscoveryProbe(dbInfo));
				});
				MonitorDefinition definition = this.CreateConsecutiveFailureMonitor(probeIdentity2);
				base.AddWorkDefinition<MonitorDefinition>(definition);
				base.AddWorkDefinition<ResponderDefinition>(this.CreateEscalateResponder(definition));
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000414A4 File Offset: 0x0003F6A4
		private void AddForEachDatabase(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Action<ProbeIdentity, MailboxDatabaseInfo> workDefinitionTaskCreator)
		{
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in databases)
			{
				base.CreateRelatedWorkItems<ProbeIdentity, MailboxDatabaseInfo>(baseProbeIdentity.ForTargetResource(mailboxDatabaseInfo.MailboxDatabaseName), mailboxDatabaseInfo, workDefinitionTaskCreator);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00041514 File Offset: 0x0003F714
		private bool CheckMonitoringAccountsAvailable<T>(IEnumerable<T> accounts, WorkItemIdentity baseIdentity, LocalizedString message)
		{
			if (!accounts.Any<T>())
			{
				WTFDiagnostics.TraceInformation<WorkItemIdentity, LocalizedString>(this.Trace, this.traceContext, "EDiscoveryDiscovery.CheckMonitoringAccountsAvailable while creating work items for {0}: {1}", baseIdentity, message, null, "CheckMonitoringAccountsAvailable", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EDiscovery\\EdiscoveryDiscovery.cs", 140);
				base.CreateRelatedWorkItems<WorkItemIdentity, object>(baseIdentity, null, delegate(WorkItemIdentity unused1, object unused2)
				{
					throw new NoMonitoringAccountsAvailableException(message);
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00041584 File Offset: 0x0003F784
		private MonitorDefinition CreateConsecutiveFailureMonitor(ProbeIdentity probeIdentity)
		{
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.ServiceName, monitorIdentity.Component, 4, true, (int)EdiscoveryProbeHelper.MonitoringInterval.TotalSeconds).Apply(monitorIdentity);
			monitorDefinition.MinimumErrorCount = 4;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.Zero)
			};
			monitorDefinition.IsHaImpacting = false;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate EDiscovery health is not impacted by any issues";
			monitorDefinition.RecurrenceIntervalSeconds = (int)EdiscoveryProbeHelper.MonitoringRecurrenceInterval.TotalSeconds;
			return monitorDefinition;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00041620 File Offset: 0x0003F820
		private ResponderDefinition CreateEscalateResponder(MonitorIdentity monitorIdentity)
		{
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Escalate", null);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.ServiceName, monitorIdentity.Name, monitorIdentity.GetAlertMask(), monitorIdentity.TargetResource, ServiceHealthStatus.Unhealthy, monitorIdentity.Component.EscalationTeam, Strings.EDiscoveryscalationSubject(monitorIdentity.GetAlertMask(), Environment.MachineName), string.Format(Datacenter.IsMicrosoftHostedOnly(true) ? Strings.EDiscoveryEscalationBodyDCHTML : Strings.EDiscoveryEscalationBodyEntText, new object[0]), true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false).Apply(responderIdentity);
			responderDefinition.NotificationServiceClass = NotificationServiceClass.UrgentInTraining;
			responderDefinition.RecurrenceIntervalSeconds = (int)EdiscoveryProbeHelper.ResponderRecurrenceInterval.TotalSeconds;
			return responderDefinition;
		}

		// Token: 0x04000761 RID: 1889
		private TracingContext traceContext = TracingContext.Default;
	}
}
