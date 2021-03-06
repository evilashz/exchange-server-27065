using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration
{
	// Token: 0x02000210 RID: 528
	public sealed class MigrationDiscovery : MaintenanceWorkItem
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00062E05 File Offset: 0x00061005
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00062E0C File Offset: 0x0006100C
		internal static int MonitoringThreshold { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00062E14 File Offset: 0x00061014
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00062E1B File Offset: 0x0006101B
		internal static int RecurrenceInterval { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00062E23 File Offset: 0x00061023
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00062E2A File Offset: 0x0006102A
		internal static int MonitoringInterval { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00062E32 File Offset: 0x00061032
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00062E39 File Offset: 0x00061039
		internal static int EscalationWaitInterval { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00062E41 File Offset: 0x00061041
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00062E48 File Offset: 0x00061048
		internal static int UnrecoverableStateTimeout { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00062E50 File Offset: 0x00061050
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00062E57 File Offset: 0x00061057
		internal static string DumpFileDirectory { get; set; }

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00062E60 File Offset: 0x00061060
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint == null || !LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.MigrationTracer, base.TraceContext, "MigrationDiscovery.DoWork: Mailbox role is not installed on this server, no need to create migration related probes", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Migration\\MigrationDiscovery.cs", 152);
				}
				else
				{
					this.DoWorkHelper(cancellationToken);
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MigrationTracer, base.TraceContext, "MigrationDiscovery.DoWork: Enpoint couldn't be initialized. We'll try again next time.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Migration\\MigrationDiscovery.cs", 161);
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00062EF4 File Offset: 0x000610F4
		private static string GetProbeName(string baseName)
		{
			return string.Format("MRS{0}Probe", baseName);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00062F01 File Offset: 0x00061101
		private static string GetMonitorName(string baseName)
		{
			return string.Format("MRS{0}Monitor", baseName);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00062F0E File Offset: 0x0006110E
		private static string GetResponderName(string baseName, string action)
		{
			return string.Format("MRS{0}{1}", baseName, action);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00062F1C File Offset: 0x0006111C
		private void DoWorkHelper(CancellationToken cancellationToken)
		{
			MigrationDiscovery.Server = LocalServer.GetServer();
			MigrationDiscovery.MonitoringThreshold = 4;
			MigrationDiscovery.RecurrenceInterval = (int)TimeSpan.FromMinutes(5.0).TotalSeconds;
			MigrationDiscovery.MonitoringInterval = (int)TimeSpan.FromMinutes(25.0).TotalSeconds;
			MigrationDiscovery.EscalationWaitInterval = (int)TimeSpan.FromHours(8.0).TotalSeconds;
			MigrationDiscovery.UnrecoverableStateTimeout = (int)TimeSpan.FromMinutes(15.0).TotalSeconds;
			if (ExEnvironment.IsTest)
			{
				MigrationDiscovery.MonitoringThreshold = 1;
				MigrationDiscovery.RecurrenceInterval = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
				MigrationDiscovery.MonitoringInterval = (int)TimeSpan.FromMinutes(1.0).TotalSeconds;
				MigrationDiscovery.EscalationWaitInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
				MigrationDiscovery.UnrecoverableStateTimeout = (int)TimeSpan.FromMinutes(2.0).TotalSeconds;
			}
			MigrationDiscovery.DumpFileDirectory = Path.Combine(CommonUtils.SafeInstallPath, "Diagnostics\\MigrationResponderDumps");
			if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(MigrationDiscovery.Server.Name))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MigrationTracer, base.TraceContext, "MigrationDiscovery.DoWork: Machine is in maintenance mode but MRS doesn't run in MM so no need to create MRS monitoring pipeline.", null, "DoWorkHelper", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Migration\\MigrationDiscovery.cs", 226);
			}
			else
			{
				this.CreateMRSServiceRunningContext();
				this.CreateQueueScanContext();
				this.CreateRPCPingContext();
				this.CreateDiagnosticsContext();
			}
			this.CreateNotificationEventContext();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0006309C File Offset: 0x0006129C
		private void CreateNotificationEventContext()
		{
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, "MailboxLocked", new Action<Component, MonitorDefinition, string>(this.CreateMailboxUnlockResponderContext));
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, "MailboxCannotBeUnlocked", new Action<Component, MonitorDefinition, string>(this.CreateNotificationEscalateResponderContext));
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, "RequestIsPoisoned", new Action<Component, MonitorDefinition, string>(this.CreateNotificationEscalateResponderContext));
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, "SourceMailboxNotMorphedToMeu", new Action<Component, MonitorDefinition, string>(this.CreateNotificationEscalateResponderContext));
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, MigrationNotifications.CorruptJobItemNotification, new Action<Component, MonitorDefinition, string>(this.CreateNotificationEscalateResponderContext));
			this.CreateNotificationWorkflowContext(ExchangeComponent.MailboxMigration, "MRSConfigSettingsCorrupted", new Action<Component, MonitorDefinition, string>(this.CreateMinorNotificationEscalateResponderContext));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00063154 File Offset: 0x00061354
		private void CreateNotificationWorkflowContext(Component component, string notification, Action<Component, MonitorDefinition, string> createResponder)
		{
			int recurrenceInterval = MigrationDiscovery.RecurrenceInterval;
			int monitoringInterval = recurrenceInterval;
			string sampleMask = NotificationItem.GenerateResultName(component.Name, component.Name, notification);
			string name = string.Format("{0}{1}Monitor", component.Name, notification);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(name, sampleMask, component.Name, component, monitoringInterval, recurrenceInterval, 1, true);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MRS health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			createResponder(component, monitorDefinition, notification);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000631F0 File Offset: 0x000613F0
		private void CreateNotificationEscalateResponderContext(Component component, MonitorDefinition monitor, string notification)
		{
			string name = string.Format("{0}{1}Escalate", component.Name, notification);
			ResponderDefinition definition = EscalateResponder.CreateDefinition(name, component.Name, monitor.Name, monitor.ConstructWorkItemResultName(), component.Name, ServiceHealthStatus.None, component.EscalationTeam, Strings.MigrationNotificationSubject(component.Name, notification), Strings.MigrationNotificationMessage(notification), true, NotificationServiceClass.UrgentInTraining, 1, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00063270 File Offset: 0x00061470
		private void CreateMailboxUnlockResponderContext(Component component, MonitorDefinition monitor, string notification)
		{
			string responderName = string.Format("{0}{1}Unlocker", component.Name, notification);
			ResponderDefinition definition = MailboxLockedResponder.CreateDefinition(responderName, monitor.SampleMask, monitor.Name, monitor.ConstructWorkItemResultName(), 0, component, ServiceHealthStatus.None);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000632C0 File Offset: 0x000614C0
		private void CreateMRSServiceRunningContext()
		{
			int recurrenceInterval = MigrationDiscovery.RecurrenceInterval;
			int monitoringThreshold = MigrationDiscovery.MonitoringThreshold;
			int monitoringInterval = MigrationDiscovery.MonitoringInterval;
			int escalationWaitInterval = MigrationDiscovery.EscalationWaitInterval;
			int unrecoverableStateTimeout = MigrationDiscovery.UnrecoverableStateTimeout;
			ProbeDefinition probeDefinition = this.CreateProbe(ExchangeComponent.MailboxMigration, MigrationDiscovery.MRSServiceName, null, MigrationDiscovery.MigrationServiceProbeType, MigrationDiscovery.MRSServiceProbeName, recurrenceInterval);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(MigrationDiscovery.GetMonitorName(MigrationDiscovery.MRSServiceWorkItemName), probeDefinition.Name, ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, monitoringThreshold, true, monitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, unrecoverableStateTimeout)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MRS health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = RestartServiceResponder.CreateDefinition(MigrationDiscovery.GetResponderName(MigrationDiscovery.MRSServiceWorkItemName, "Restart"), monitorDefinition.Name, MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.MailboxMigration.Name, null, true, true, null, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			string escalationMessageUnhealthy;
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageDc(ExchangeComponent.MailboxMigration.Name);
			}
			else
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageEnt(ExchangeComponent.MailboxMigration.Name);
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(MigrationDiscovery.GetResponderName(MigrationDiscovery.MRSServiceWorkItemName, "Escalate"), ExchangeComponent.MailboxMigration.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.MRSServiceNotRunningSubject(MigrationDiscovery.MRSServiceName), escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = escalationWaitInterval;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000634A0 File Offset: 0x000616A0
		private void CreateQueueScanContext()
		{
			int recurrenceInterval = MigrationDiscovery.RecurrenceInterval;
			int monitoringThreshold = MigrationDiscovery.MonitoringThreshold;
			int monitoringInterval = MigrationDiscovery.MonitoringInterval;
			int escalationWaitInterval = MigrationDiscovery.EscalationWaitInterval;
			string baseName = "QueueScan";
			ProbeDefinition probeDefinition = this.CreateProbe(ExchangeComponent.MailboxMigration, MigrationDiscovery.Server.Name, MigrationDiscovery.Server.Fqdn, MigrationDiscovery.MRSQueueScanProbeType, MigrationDiscovery.GetProbeName(baseName), recurrenceInterval);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(MigrationDiscovery.GetMonitorName(baseName), probeDefinition.Name, ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, monitoringThreshold, true, monitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MRS health is not impacted by MRS scan queues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string responderName = MigrationDiscovery.GetResponderName(baseName, "Dump");
			string name = monitorDefinition.Name;
			string mrsserviceName = MigrationDiscovery.MRSServiceName;
			ServiceHealthStatus responderTargetState = ServiceHealthStatus.Unhealthy;
			int serviceStopTimeoutInSeconds = 15;
			int serviceStartTimeoutInSeconds = 120;
			bool dumpIgnoreRegistryLimit = true;
			bool restartEnabled = true;
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition(responderName, name, mrsserviceName, responderTargetState, serviceStopTimeoutInSeconds, serviceStartTimeoutInSeconds, 0, false, DumpMode.FullDump, MigrationDiscovery.DumpFileDirectory, 15.0, 0, ExchangeComponent.MailboxMigration.Name, null, restartEnabled, true, null, dumpIgnoreRegistryLimit);
			responderDefinition.WaitIntervalSeconds = escalationWaitInterval;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition(MigrationDiscovery.GetResponderName(baseName, "Escalate"), ExchangeComponent.MailboxMigration.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Unhealthy, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.MRSLongQueueScanSubject(MigrationDiscovery.MRSServiceName), Strings.MRSLongQueueScanMessage(MigrationDiscovery.DumpFileDirectory), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition2.WaitIntervalSeconds = escalationWaitInterval;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00063664 File Offset: 0x00061864
		private void CreateRPCPingContext()
		{
			int recurrenceInterval = MigrationDiscovery.RecurrenceInterval;
			int monitoringThreshold = MigrationDiscovery.MonitoringThreshold;
			int monitoringInterval = MigrationDiscovery.MonitoringInterval;
			int escalationWaitInterval = MigrationDiscovery.EscalationWaitInterval;
			int unrecoverableStateTimeout = MigrationDiscovery.UnrecoverableStateTimeout;
			string baseName = "RPCPing";
			ProbeDefinition probeDefinition = this.CreateProbe(ExchangeComponent.MailboxMigration, MigrationDiscovery.Server.Name, MigrationDiscovery.Server.Fqdn, MigrationDiscovery.MRSRPCPingProbeType, MigrationDiscovery.GetProbeName(baseName), recurrenceInterval);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(MigrationDiscovery.GetMonitorName(baseName), probeDefinition.Name, ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, monitoringThreshold, true, monitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, unrecoverableStateTimeout)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MRS health is not impacted by RPC issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = RestartServiceResponder.CreateDefinition(MigrationDiscovery.GetResponderName(baseName, "Restart"), monitorDefinition.Name, MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.MailboxMigration.Name, null, true, true, null, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(MigrationDiscovery.GetResponderName(baseName, "Escalate"), ExchangeComponent.MailboxMigration.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.MRSRPCPingSubject(MigrationDiscovery.MRSServiceName), Strings.MRSUnhealthyMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = escalationWaitInterval;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00063824 File Offset: 0x00061A24
		private void CreateDiagnosticsContext()
		{
			int recurrenceInterval = MigrationDiscovery.RecurrenceInterval;
			int failureCount = 2;
			int monitoringInterval = MigrationDiscovery.MonitoringInterval;
			int escalationWaitInterval = MigrationDiscovery.EscalationWaitInterval;
			int unrecoverableStateTimeout = MigrationDiscovery.UnrecoverableStateTimeout;
			string baseName = "Diagnostics";
			ProbeDefinition probeDefinition = this.CreateProbe(ExchangeComponent.MailboxMigration, MigrationDiscovery.Server.Name, MigrationDiscovery.Server.Fqdn, typeof(MRSDiagnosticsProbe), MigrationDiscovery.GetProbeName(baseName), recurrenceInterval);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(MigrationDiscovery.GetMonitorName(baseName), probeDefinition.Name, ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, failureCount, true, monitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MRS health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = RestartServiceResponder.CreateDefinition(MigrationDiscovery.GetResponderName(baseName, "Restart"), monitorDefinition.Name, MigrationDiscovery.MRSServiceName, ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.MailboxMigration.Name, null, true, true, null, false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00063958 File Offset: 0x00061B58
		private void CreateMinorNotificationEscalateResponderContext(Component component, MonitorDefinition monitor, string notification)
		{
			string name = string.Format("{0}{1}Escalate", component.Name, notification);
			ResponderDefinition definition = EscalateResponder.CreateDefinition(name, component.Name, monitor.Name, monitor.ConstructWorkItemResultName(), component.Name, ServiceHealthStatus.None, component.EscalationTeam, Strings.MigrationNotificationSubject(component.Name, notification), Strings.MigrationNotificationMessage(notification), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000639DC File Offset: 0x00061BDC
		private ProbeDefinition CreateProbe(Component component, string targetResource, string targetExtension, Type probeType, string probeName, int recurrenceInterval)
		{
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.MigrationTracer, base.TraceContext, "MigrationDiscovery.DoWork: Creating {0} for {1}", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Migration\\MigrationDiscovery.cs", 646);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = probeType.Assembly.Location;
			probeDefinition.TypeName = probeType.FullName;
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = component.Name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			probeDefinition.TimeoutSeconds = recurrenceInterval / 2;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = targetResource;
			probeDefinition.TargetExtension = targetExtension;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.MigrationTracer, base.TraceContext, "MigrationDiscovery.DoWork: Created {0} for {1}", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Migration\\MigrationDiscovery.cs", 665);
			return probeDefinition;
		}

		// Token: 0x04000B23 RID: 2851
		private const int MaxRetryAttempt = 3;

		// Token: 0x04000B24 RID: 2852
		internal static readonly string MRSServiceWorkItemName = "Started";

		// Token: 0x04000B25 RID: 2853
		internal static readonly string MRSServiceProbeName = MigrationDiscovery.GetProbeName(MigrationDiscovery.MRSServiceWorkItemName);

		// Token: 0x04000B26 RID: 2854
		private static readonly string MRSServiceName = "MSExchangeMailboxReplication";

		// Token: 0x04000B27 RID: 2855
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000B28 RID: 2856
		private static readonly Type MigrationServiceProbeType = typeof(GenericServiceProbe);

		// Token: 0x04000B29 RID: 2857
		private static readonly Type MRSQueueScanProbeType = typeof(MRSQueueScanProbe);

		// Token: 0x04000B2A RID: 2858
		private static readonly Type MRSRPCPingProbeType = typeof(MRSRPCPingProbe);

		// Token: 0x04000B2B RID: 2859
		private static readonly Type OverallConsecutiveProbeFailuresMonitorType = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x04000B2C RID: 2860
		private static Server Server;
	}
}
