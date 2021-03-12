using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants
{
	// Token: 0x02000179 RID: 377
	public sealed class EventAssistantsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x000454F0 File Offset: 0x000436F0
		internal static void PopulateProbeDefinition(ProbeDefinition probeDefinition, string targetResource, Type probeType, string probeName, TimeSpan recurrenceInterval, TimeSpan timeoutInterval)
		{
			probeDefinition.AssemblyPath = probeType.Assembly.Location;
			probeDefinition.TypeName = probeType.FullName;
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = ExchangeComponent.EventAssistants.Name;
			probeDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			probeDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = targetResource;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0004555C File Offset: 0x0004375C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Mailbox role is not installed on this server, no need to create event assistants related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 231);
				}
				else
				{
					EventAssistantsDiscovery.isDatacenter = LocalEndpointManager.IsDataCenter;
					this.CreateAssistantsServiceContext();
					this.CreateProcessCrashingContext();
					if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: No mailbox database found on this server", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 251);
					}
					else
					{
						foreach (MailboxDatabaseInfo databaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
						{
							this.CreateMailboxAssistantsWatermarksContext(databaseInfo);
							this.CreateMailSubmissionWatermarksContext(databaseInfo);
						}
						this.CreateMailboxAssistantsWatermarksEscalateWorkitems();
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: EndpointManagerEndpointUninitializedException is caught.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 273);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00045690 File Offset: 0x00043890
		private void CreateAssistantsServiceContext()
		{
			int value = 2;
			TimeSpan timeSpan = TimeSpan.FromMinutes(5.0);
			TimeSpan monitoringInterval = TimeSpan.FromMinutes(12.0);
			TimeSpan t = TimeSpan.FromMinutes(15.0);
			ProbeDefinition probeDefinition = this.CreateProbeDefinition("MSExchangeMailboxAssistants", EventAssistantsDiscovery.AssistantsServiceProbeType, "EventAssistantsServiceProbe", timeSpan);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition("MSExchangeMailboxAssistants", EventAssistantsDiscovery.OverallXFailuresMonitorType, "EventAssistantsServiceMonitor", timeSpan, monitoringInterval, new int?(value), string.Format("{0}/{1}", probeDefinition.Name, "MSExchangeMailboxAssistants"));
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)(t + timeSpan).TotalSeconds)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Event Assistants health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("EventAssistantsServiceRestart", monitorDefinition.Name, "MSExchangeMailboxAssistants", ServiceHealthStatus.Degraded, 15, (int)t.TotalSeconds, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.EventAssistants.Name, null, true, true, "Dag", false);
			responderDefinition.RecurrenceIntervalSeconds = (int)timeSpan.TotalSeconds;
			responderDefinition.AlertMask = monitorDefinition.ConstructWorkItemResultName();
			responderDefinition.AlertTypeId = monitorDefinition.Name;
			responderDefinition.TargetResource = "MSExchangeMailboxAssistants";
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			string escalationMessageUnhealthy;
			if (EventAssistantsDiscovery.isDatacenter)
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageDc("MSExchangeMailboxAssistants");
			}
			else
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageEnt("MSExchangeMailboxAssistants");
			}
			ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition("EventAssistantsServiceEscalate", ExchangeComponent.EventAssistants.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), "MSExchangeMailboxAssistants", ServiceHealthStatus.Unrecoverable, ExchangeComponent.TimeBasedAssistants.EscalationTeam, Strings.ServiceNotRunningEscalationSubject("MSExchangeMailboxAssistants"), escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition2.RecurrenceIntervalSeconds = (int)timeSpan.TotalSeconds;
			responderDefinition2.TimeoutSeconds = (int)timeSpan.TotalSeconds;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000458CA File Offset: 0x00043ACA
		private void CreateProcessCrashingContext()
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000458CC File Offset: 0x00043ACC
		private void CreateMailboxAssistantsWatermarksContext(MailboxDatabaseInfo databaseInfo)
		{
			int value = 3;
			TimeSpan timeoutInterval = TimeSpan.FromMinutes(5.0);
			TimeSpan.FromHours(4.0);
			ProbeDefinition probeDefinition = this.CreateProbeDefinition(databaseInfo.MailboxDatabaseName, EventAssistantsDiscovery.AssistantsWatermarksProbeType, "MailboxAssistantsWatermarksProbe", EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence, timeoutInterval);
			probeDefinition.Attributes["WatermarksVariationThreshold"] = EventAssistantsDiscovery.mailboxAssistantsWatermarksVariationThreshold.ToString();
			probeDefinition.Attributes["WatermarksBehindWarningThreshold"] = EventAssistantsDiscovery.mailboxAssistantsWatermarksBehindWarningThreshold.ToString();
			probeDefinition.Attributes["IncludedAssistantType"] = "MultipleAssistants";
			probeDefinition.Attributes["ExcludedAssistantType"] = "MailboxTransportSubmissionAssistant";
			probeDefinition.TargetExtension = databaseInfo.MailboxDatabaseGuid.ToString();
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition(databaseInfo.MailboxDatabaseName, EventAssistantsDiscovery.OverallConsecutiveProbeFailuresMonitorType, "MailboxAssistantsWatermarksMonitor", EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence, timeoutInterval, EventAssistantsDiscovery.mailboxAssistantsWatermarksMonitoringInterval, new int?(value), probeDefinition.ConstructWorkItemResultName());
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, EventAssistantsDiscovery.mailboxAssistantsWatermarksServiceRestartInterval),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, EventAssistantsDiscovery.mailboxAssistantsWatermarksEscalationNotificationInterval)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Event Assistants health is not impacted any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			if (EventAssistantsDiscovery.isDatacenter)
			{
				ResponderDefinition responderDefinition = WatsonResponder.CreateDefinition("MailboxAssistantsWatermarksWatsonResponder", ExchangeComponent.EventAssistants.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), "MSExchangeMailboxAssistants", ServiceHealthStatus.Degraded, string.Format("{0}Exception", "MailboxAssistantsWatermarksWatsonResponder"), "E12", ReportOptions.None);
				responderDefinition.RecurrenceIntervalSeconds = 0;
				responderDefinition.TargetExtension = databaseInfo.MailboxDatabaseName;
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
			ResponderDefinition responderDefinition2 = RestartServiceResponder.CreateDefinition("MailboxAssistantsWatermarksRestart", monitorDefinition.Name, "MSExchangeMailboxAssistants", ServiceHealthStatus.Unhealthy, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.EventAssistants.Name, null, true, true, "Dag", false);
			responderDefinition2.RecurrenceIntervalSeconds = (int)EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence.TotalSeconds;
			responderDefinition2.AlertMask = monitorDefinition.ConstructWorkItemResultName();
			responderDefinition2.AlertTypeId = monitorDefinition.Name;
			responderDefinition2.TargetResource = databaseInfo.MailboxDatabaseName;
			responderDefinition2.TypeName = EventAssistantsDiscovery.WatermarksRestartServiceResponderType.FullName;
			responderDefinition2.AssemblyPath = EventAssistantsDiscovery.WatermarksRestartServiceResponderType.Assembly.Location;
			responderDefinition2.Attributes["MultipleAssistants"] = "MSExchangeMailboxAssistants";
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
			ResponderDefinition responderDefinition3 = this.CreateResponderDefinition(databaseInfo.MailboxDatabaseName, EventAssistantsDiscovery.EscalationNotificationResponderType, "MailboxAssistantsWatermarksEscalationNotification", monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.Name, TimeSpan.Zero, TimeSpan.FromSeconds(15.0), timeoutInterval, ServiceHealthStatus.Unrecoverable);
			responderDefinition3.ActionOnCorrelatedMonitors = CorrelatedMonitorAction.GenerateException;
			responderDefinition3.CorrelatedMonitors = new CorrelatedMonitorInfo[]
			{
				StoreMonitoringHelpers.GetStoreCorrelation(databaseInfo.MailboxDatabaseName)
			};
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition3, base.TraceContext);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00045BE0 File Offset: 0x00043DE0
		private void CreateMailSubmissionWatermarksContext(MailboxDatabaseInfo databaseInfo)
		{
			ProbeDefinition probeDefinition = this.CreateProbeDefinition(databaseInfo.MailboxDatabaseName, EventAssistantsDiscovery.AssistantsWatermarksProbeType, "MailSubmissionWatermarksProbe", EventAssistantsDiscovery.mailSubmissionWatermarksRecurrence, EventAssistantsDiscovery.mailSubmissionWatermarksTimeoutInterval);
			probeDefinition.Attributes["WatermarksVariationThreshold"] = 50.ToString();
			probeDefinition.Attributes["WatermarksBehindWarningThreshold"] = EventAssistantsDiscovery.mailSubmissionWatermarksBehindWarningThreshold.ToString();
			probeDefinition.Attributes["IncludedAssistantType"] = "MailboxTransportSubmissionAssistant";
			probeDefinition.TargetExtension = databaseInfo.MailboxDatabaseGuid.ToString();
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition(databaseInfo.MailboxDatabaseName, EventAssistantsDiscovery.OverallConsecutiveProbeFailuresMonitorType, "MailSubmissionWatermarksMonitor", EventAssistantsDiscovery.mailSubmissionWatermarksRecurrence, EventAssistantsDiscovery.mailSubmissionWatermarksTimeoutInterval, EventAssistantsDiscovery.mailSubmissionWatermarksMonitoringInterval, new int?(3), probeDefinition.ConstructWorkItemResultName());
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, EventAssistantsDiscovery.mailSubmissionWatermarksEscalationInterval)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate mail submission assistant health is not impacted by any issues";
			monitorDefinition.Component = ExchangeComponent.EventAssistants;
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("MailSubmissionWatermarksRestart", monitorDefinition.Name, "MSExchangeSubmission", ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, ExchangeComponent.EventAssistants.Name, null, true, true, "Dag", false);
			responderDefinition.RecurrenceIntervalSeconds = (int)TimeSpan.Zero.TotalSeconds;
			responderDefinition.TargetResource = databaseInfo.MailboxDatabaseName;
			responderDefinition.AlertMask = monitorDefinition.ConstructWorkItemResultName();
			responderDefinition.AlertTypeId = monitorDefinition.Name;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			string escalationMessageUnhealthy;
			if (EventAssistantsDiscovery.isDatacenter)
			{
				escalationMessageUnhealthy = Strings.MailSubmissionBehindWatermarksEscalationMessageDc(EventAssistantsDiscovery.mailSubmissionWatermarksBehindWarningThreshold, EventAssistantsDiscovery.mailSubmissionWatermarksMonitoringInterval, databaseInfo.MailboxDatabaseName, string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{{Probe.StateAttribute1}}' -Server {2}", ExchangeComponent.EventAssistants.Name, "MailSubmissionWatermarksProbe", Environment.MachineName), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.EventAssistants.Name, "MailSubmissionWatermarksMonitor"));
			}
			else
			{
				escalationMessageUnhealthy = Strings.MailSubmissionBehindWatermarksEscalationMessageEnt(EventAssistantsDiscovery.mailSubmissionWatermarksBehindWarningThreshold, EventAssistantsDiscovery.mailSubmissionWatermarksMonitoringInterval, databaseInfo.MailboxDatabaseName, string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{{Probe.StateAttribute1}}' -Server {2}", ExchangeComponent.EventAssistants.Name, "MailSubmissionWatermarksProbe", Environment.MachineName), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.EventAssistants.Name, "MailSubmissionWatermarksMonitor"));
			}
			ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition("MailSubmissionWatermarksEscalate", ExchangeComponent.EventAssistants.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), databaseInfo.MailboxDatabaseName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Transport.EscalationTeam, Strings.MailSubmissionBehindWatermarksEscalationSubject(EventAssistantsDiscovery.mailSubmissionWatermarksBehindWarningThreshold, EventAssistantsDiscovery.mailSubmissionWatermarksMonitoringInterval, databaseInfo.MailboxDatabaseName), escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition2.RecurrenceIntervalSeconds = (int)TimeSpan.Zero.TotalSeconds;
			responderDefinition2.ActionOnCorrelatedMonitors = CorrelatedMonitorAction.GenerateException;
			responderDefinition2.CorrelatedMonitors = new CorrelatedMonitorInfo[]
			{
				StoreMonitoringHelpers.GetStoreCorrelation(databaseInfo.MailboxDatabaseName)
			};
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00045F04 File Offset: 0x00044104
		private ProbeDefinition CreateProbeDefinition(string targetResource, Type probeType, string probeName, TimeSpan recurrenceInterval)
		{
			return this.CreateProbeDefinition(targetResource, probeType, probeName, recurrenceInterval, new TimeSpan(recurrenceInterval.Ticks / 2L));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00045F20 File Offset: 0x00044120
		private ProbeDefinition CreateProbeDefinition(string targetResource, Type probeType, string probeName, TimeSpan recurrenceInterval, TimeSpan timeoutInterval)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Creating {0} for {1}", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 734);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			EventAssistantsDiscovery.PopulateProbeDefinition(probeDefinition, targetResource, probeType, probeName, recurrenceInterval, timeoutInterval);
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Created {0} for {1}", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 751);
			return probeDefinition;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00045F90 File Offset: 0x00044190
		private MonitorDefinition CreateMonitorDefinition(string targetResource, Type monitorType, string monitorName, TimeSpan recurrenceInterval, TimeSpan monitoringInterval, int? monitoringThreshold, string sampleMask)
		{
			return this.CreateMonitorDefinition(targetResource, monitorType, monitorName, recurrenceInterval, new TimeSpan(recurrenceInterval.Ticks / 2L), monitoringInterval, monitoringThreshold, sampleMask);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00045FC0 File Offset: 0x000441C0
		private MonitorDefinition CreateMonitorDefinition(string targetResource, Type monitorType, string monitorName, TimeSpan recurrenceInterval, TimeSpan timeoutInterval, TimeSpan monitoringInterval, int? monitoringThreshold, string sampleMask)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Creating {0} for {1}", monitorName, targetResource, null, "CreateMonitorDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 814);
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = monitorType.Assembly.Location;
			monitorDefinition.TypeName = monitorType.FullName;
			monitorDefinition.Name = monitorName;
			monitorDefinition.ServiceName = ExchangeComponent.EventAssistants.Name;
			monitorDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			monitorDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			monitorDefinition.MaxRetryAttempts = 3;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.MonitoringIntervalSeconds = (int)monitoringInterval.TotalSeconds;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = ExchangeComponent.EventAssistants;
			if (monitoringThreshold != null)
			{
				monitorDefinition.MonitoringThreshold = (double)monitoringThreshold.Value;
			}
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Created {0} for {1}", monitorName, targetResource, null, "CreateMonitorDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 838);
			return monitorDefinition;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000460B8 File Offset: 0x000442B8
		private ResponderDefinition CreateResponderDefinition(string targetResource, Type responderType, string responderName, string alertMask, string alertTypeId, TimeSpan recurrenceInterval, TimeSpan waitInterval, ServiceHealthStatus targetHealthState)
		{
			return this.CreateResponderDefinition(targetResource, responderType, responderName, alertMask, alertTypeId, recurrenceInterval, waitInterval, new TimeSpan(recurrenceInterval.Ticks / 2L), targetHealthState);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000460E8 File Offset: 0x000442E8
		private ResponderDefinition CreateResponderDefinition(string targetResource, Type responderType, string responderName, string alertMask, string alertTypeId, TimeSpan recurrenceInterval, TimeSpan waitInterval, TimeSpan timeoutInterval, ServiceHealthStatus targetHealthState)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Creating {0} for {1}", responderName, targetResource, null, "CreateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 906);
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = responderType.Assembly.Location;
			responderDefinition.TypeName = responderType.FullName;
			responderDefinition.Name = responderName;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			responderDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.ServiceName = ExchangeComponent.EventAssistants.Name;
			responderDefinition.WaitIntervalSeconds = (int)waitInterval.TotalSeconds;
			responderDefinition.TargetHealthState = targetHealthState;
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.EventAssistantsTracer, base.TraceContext, "EventAssistantsDiscovery.DoWork: Created {0} for {1}", responderName, targetResource, null, "CreateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\EventAssistants\\EventAssistantsDiscovery.cs", 928);
			return responderDefinition;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000461D0 File Offset: 0x000443D0
		private void CreateMailboxAssistantsWatermarksEscalateWorkitems()
		{
			TimeSpan timeSpan = new TimeSpan(EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence.Ticks + EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence.Ticks / 2L);
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.EventAssistants.Name, "MailboxAssistantsWatermarksEscalationNotification", null);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("MailboxAssistantsWatermarksEscalationProcessingMonitor", sampleMask, ExchangeComponent.EventAssistants.Name, ExchangeComponent.EventAssistants, 1, true, (int)timeSpan.TotalSeconds);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)EventAssistantsDiscovery.escalationInterval.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Event Assistances health is not impacted any issues";
			monitorDefinition.RecurrenceIntervalSeconds = (int)EventAssistantsDiscovery.mailboxAssistantsWatermarksRecurrence.TotalSeconds;
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			TimeSpan duration = EventAssistantsDiscovery.mailboxAssistantsWatermarksMonitoringInterval + EventAssistantsDiscovery.mailboxAssistantsWatermarksEscalationNotificationInterval + EventAssistantsDiscovery.escalationInterval;
			string escalationMessageUnhealthy;
			if (EventAssistantsDiscovery.isDatacenter)
			{
				escalationMessageUnhealthy = Strings.MailboxAssistantsBehindWatermarksEscalationMessageDc(EventAssistantsDiscovery.mailboxAssistantsWatermarksBehindWarningThreshold, duration, string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{{Probe.StateAttribute1}}' -Server {2}", ExchangeComponent.EventAssistants.Name, "MailboxAssistantsWatermarksProbe", Environment.MachineName), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.EventAssistants.Name, "MailboxAssistantsWatermarksMonitor"));
			}
			else
			{
				escalationMessageUnhealthy = Strings.MailboxAssistantsBehindWatermarksEscalationMessageEnt(EventAssistantsDiscovery.mailboxAssistantsWatermarksBehindWarningThreshold, duration, string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{{Probe.StateAttribute1}}' -Server {2}", ExchangeComponent.EventAssistants.Name, "MailboxAssistantsWatermarksProbe", Environment.MachineName), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.EventAssistants.Name, "MailboxAssistantsWatermarksMonitor"));
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("MailboxAssistantsWatermarksEscalate", ExchangeComponent.EventAssistants.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), null, ServiceHealthStatus.Unrecoverable, ExchangeComponent.EventAssistants.EscalationTeam, Strings.MailboxAssistantsBehindWatermarksEscalationSubject(EventAssistantsDiscovery.mailboxAssistantsWatermarksBehindWarningThreshold, duration), escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.TypeName = EventAssistantsDiscovery.WatermarksEscalateResponderType.FullName;
			responderDefinition.RecurrenceIntervalSeconds = (int)TimeSpan.Zero.TotalSeconds;
			responderDefinition.Attributes["ElcEventBasedAssistant"] = ExchangeComponent.Compliance.EscalationTeam;
			responderDefinition.Attributes["DiscoverySearchEventBasedAssistant"] = ExchangeComponent.Compliance.EscalationTeam;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x04000835 RID: 2101
		internal const string WatermarksVariationThresholdString = "WatermarksVariationThreshold";

		// Token: 0x04000836 RID: 2102
		internal const string WatermarksBehindWarningThresholdString = "WatermarksBehindWarningThreshold";

		// Token: 0x04000837 RID: 2103
		internal const string EscalationTypeString = "EscalationType";

		// Token: 0x04000838 RID: 2104
		internal const string IncludedAssistantTypeString = "IncludedAssistantType";

		// Token: 0x04000839 RID: 2105
		internal const string ExcludedAssistantTypeString = "ExcludedAssistantType";

		// Token: 0x0400083A RID: 2106
		private const int MaxRetryAttempt = 3;

		// Token: 0x0400083B RID: 2107
		private const string AssistantsServiceName = "MSExchangeMailboxAssistants";

		// Token: 0x0400083C RID: 2108
		private const string MailSubmissionServiceName = "MSExchangeSubmission";

		// Token: 0x0400083D RID: 2109
		private const int mailSubmissionWatermarksVariationThreshold = 50;

		// Token: 0x0400083E RID: 2110
		private const int mailSubmissionWatermarksMonitoringThreshold = 3;

		// Token: 0x0400083F RID: 2111
		private static bool isDatacenter;

		// Token: 0x04000840 RID: 2112
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000841 RID: 2113
		private static readonly Type AssistantsServiceProbeType = typeof(GenericServiceProbe);

		// Token: 0x04000842 RID: 2114
		private static readonly Type AssistantsWatermarksProbeType = typeof(EventAssistantsWatermarksProbe);

		// Token: 0x04000843 RID: 2115
		private static readonly Type OverallXFailuresMonitorType = typeof(OverallXFailuresMonitor);

		// Token: 0x04000844 RID: 2116
		private static readonly Type OverallConsecutiveProbeFailuresMonitorType = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x04000845 RID: 2117
		private static readonly Type WatermarksRestartServiceResponderType = typeof(EventAssistantsWatermarksRestartResponder);

		// Token: 0x04000846 RID: 2118
		private static readonly Type EscalationNotificationResponderType = typeof(EscalationNotificationResponder);

		// Token: 0x04000847 RID: 2119
		private static readonly Type WatermarksEscalateResponderType = typeof(EventAssistantsWatermarksEscalateResponder);

		// Token: 0x04000848 RID: 2120
		private static TimeSpan mailboxAssistantsWatermarksBehindWarningThreshold = TimeSpan.FromHours(1.0);

		// Token: 0x04000849 RID: 2121
		private static int mailboxAssistantsWatermarksVariationThreshold = 50;

		// Token: 0x0400084A RID: 2122
		private static TimeSpan mailboxAssistantsWatermarksRecurrence = TimeSpan.FromMinutes(30.0);

		// Token: 0x0400084B RID: 2123
		private static TimeSpan mailboxAssistantsWatermarksMonitoringInterval = TimeSpan.FromHours(2.0);

		// Token: 0x0400084C RID: 2124
		private static TimeSpan mailboxAssistantsWatermarksServiceRestartInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400084D RID: 2125
		private static TimeSpan mailboxAssistantsWatermarksEscalationNotificationInterval = TimeSpan.FromHours(1.5);

		// Token: 0x0400084E RID: 2126
		private static TimeSpan escalationInterval = TimeSpan.FromMinutes(30.0);

		// Token: 0x0400084F RID: 2127
		private static TimeSpan mailSubmissionWatermarksRecurrence = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000850 RID: 2128
		private static TimeSpan mailSubmissionWatermarksBehindWarningThreshold = TimeSpan.FromHours(1.0);

		// Token: 0x04000851 RID: 2129
		private static TimeSpan mailSubmissionWatermarksMonitoringInterval = TimeSpan.FromHours(2.0);

		// Token: 0x04000852 RID: 2130
		private static TimeSpan mailSubmissionWatermarksEscalationInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000853 RID: 2131
		private static TimeSpan mailSubmissionWatermarksTimeoutInterval = TimeSpan.FromMinutes(5.0);
	}
}
