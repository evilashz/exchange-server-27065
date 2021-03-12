using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace
{
	// Token: 0x020001E9 RID: 489
	public sealed class MailboxSpaceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0005BEC4 File Offset: 0x0005A0C4
		internal static void PopulateProbeDefinition(ProbeDefinition probeDefinition, string targetResource, string probeTypeName, string probeName, TimeSpan recurrenceInterval, TimeSpan timeoutInterval)
		{
			probeDefinition.AssemblyPath = MailboxSpaceDiscovery.AssemblyPath;
			probeDefinition.TypeName = probeTypeName;
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = MailboxSpaceDiscovery.mailboxSpaceHealthsetName;
			probeDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			probeDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = targetResource;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0005BF20 File Offset: 0x0005A120
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.DoWork: Mailbox role is not installed on this server, no need to create database space related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 227);
				}
				else if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "MailboxSpaceDiscovery.DoWork: No mailbox database found on this server", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 238);
				}
				else
				{
					MailboxSpaceDiscovery.isTestTopology = ExEnvironment.IsTest;
					MailboxSpaceDiscovery.isDatacenter = LocalEndpointManager.IsDataCenter;
					MailboxSpaceDiscovery.isDatacenterDedicated = LocalEndpointManager.IsDataCenterDedicated;
					foreach (MailboxDatabaseInfo dbInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
					{
						this.CreateDatabaseSizeContext(dbInfo);
						this.CreateStorageSpaceNotificationMonitor(dbInfo);
					}
					this.CreateDatabaseSizeEscalateWorkitems();
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.DoWork: EndpointManagerEndpointUninitializedException is caught.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 273);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0005C05C File Offset: 0x0005A25C
		private void CreateDatabaseSizeContext(MailboxDatabaseInfo dbInfo)
		{
			string mailboxDatabaseName = dbInfo.MailboxDatabaseName;
			string targetExtension = dbInfo.MailboxDatabaseGuid.ToString();
			TimeSpan monitoringInterval = new TimeSpan(2L * MailboxSpaceDiscovery.databaseSizeRecurrence.Ticks);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateDatabaseSizeContext: Starting creation of database size context", null, "CreateDatabaseSizeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 290);
			ProbeDefinition probeDefinition = this.CreateProbe(mailboxDatabaseName, MailboxSpaceDiscovery.DBSpaceProbeTypeName, "DatabaseSpaceProbe", MailboxSpaceDiscovery.databaseSizeRecurrence, MailboxSpaceDiscovery.databaseSizeTimeout);
			probeDefinition.TargetExtension = targetExtension;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitor(mailboxDatabaseName, MailboxSpaceDiscovery.DBSizeMonitorTypeName, "DatabaseSizeMonitor", MailboxSpaceDiscovery.databaseSizeRecurrence, monitoringInterval, null, MailboxSpaceDiscovery.databaseSizeTimeout, probeDefinition.ConstructWorkItemResultName());
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, new TimeSpan(6L * MailboxSpaceDiscovery.databaseSizeRecurrence.Ticks))
			};
			if ((MailboxSpaceDiscovery.isDatacenter || MailboxSpaceDiscovery.isDatacenterDedicated) && !MailboxSpaceDiscovery.isTestTopology)
			{
				monitorDefinition.Attributes["DatabaseBufferThreshold"] = MailboxSpaceDiscovery.DatabaseBufferThreshold.ToString();
				monitorDefinition.Attributes["DatabaseLogsThreshold"] = MailboxSpaceDiscovery.DatabaseLogsThreshold.ToString();
				monitorDefinition.Attributes["SearchSizeFactorThreshold"] = 0.2.ToString();
				monitorDefinition.Attributes["NumberOfDatabasesPerDisk"] = 4.ToString();
			}
			else
			{
				monitorDefinition.Attributes["DatabaseBufferThreshold"] = "1MB";
				monitorDefinition.Attributes["DatabaseLogsThreshold"] = "1MB";
				monitorDefinition.Attributes["SearchSizeFactorThreshold"] = 0.2.ToString();
				monitorDefinition.Attributes["NumberOfDatabasesPerDisk"] = "1";
			}
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MailboxSpace health is not impacted by database health issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition responderDefinition = this.CreateResponder(mailboxDatabaseName, MailboxSpaceDiscovery.DatabaseProvisioningResponderTypeName, "DatabaseSizeProvisioning", monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.Name, MailboxSpaceDiscovery.databaseSizeRecurrence, MailboxSpaceDiscovery.databaseSizeWaitInterval, MailboxSpaceDiscovery.databaseSizeTimeout, ServiceHealthStatus.Unhealthy);
			responderDefinition.TargetExtension = targetExtension;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			ResponderDefinition responderDefinition2 = this.CreateResponder(mailboxDatabaseName, MailboxSpaceDiscovery.DatabaseSizeEscalationNotificationResponderTypeName, "DatabaseSizeEscalationNotification", monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.Name, TimeSpan.Zero, MailboxSpaceDiscovery.databaseSizeWaitInterval, ServiceHealthStatus.Unrecoverable);
			responderDefinition2.ActionOnCorrelatedMonitors = CorrelatedMonitorAction.GenerateException;
			responderDefinition2.CorrelatedMonitors = new CorrelatedMonitorInfo[]
			{
				StoreMonitoringHelpers.GetStoreCorrelation(mailboxDatabaseName)
			};
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateDatabaseSizeContext: Finished creation of database size context", null, "CreateDatabaseSizeContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 391);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0005C35C File Offset: 0x0005A55C
		private void CreateStorageSpaceNotificationMonitor(MailboxDatabaseInfo dbInfo)
		{
			string text = dbInfo.MailboxDatabaseName.ToUpper();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("StorageLogicalDriveSpaceMonitor", NotificationItem.GenerateResultName("MSExchangeDagMgmt", "EdbAndLogVolSpace", text), ExchangeComponent.MailboxSpace.Name, ExchangeComponent.MailboxSpace, 1, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.TargetResource = text;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MailboxSpace log and edb volume spaces are adequate.";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("StorageLogicalDriveSpaceEscalate", ExchangeComponent.MailboxSpace.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), text, ServiceHealthStatus.Unhealthy, "High Availability", Strings.LogVolumeSpaceEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.LogVolumeSpaceEscalationMessage(dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.RecurrenceIntervalSeconds = 0;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateLogVolumeSpaceNotificationMonitor: Finished creation of log volume space context", null, "CreateStorageSpaceNotificationMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 448);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0005C490 File Offset: 0x0005A690
		private void CreateDatabaseLogicalPhysicalSizeRatioContext(MailboxDatabaseInfo dbInfo)
		{
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateDatabaseLogicalPhysicalSizeRatioContext: Starting creation of database logical physical size ratio context", null, "CreateDatabaseLogicalPhysicalSizeRatioContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 460);
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "DatabaseLogicalPhysicalSizeRatioTrigger_Error", dbInfo.MailboxDatabaseName);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("DatabaseLogicalPhysicalSizeRatioMonitor", sampleMask, ExchangeComponent.MailboxSpace.Name, ExchangeComponent.MailboxSpace, 1, true, (int)MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioMonitoringInterval.TotalSeconds);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MailboxSpace health is not impacted by database health issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = this.CreateResponder(dbInfo.MailboxDatabaseName, MailboxSpaceDiscovery.EscalationNotificationResponderTypeName, "DatabaseLogicalPhysicalSizeRatioEscalationNotification", monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.Name, TimeSpan.Zero, MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioWaitInterval, ServiceHealthStatus.Unhealthy);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			ResponderDefinition definition2 = SetMonitorStateRepairingResponder.CreateDefinition("SetDatabaseLogicalPhysicalSizeRatioMonitorStateRepairing", ExchangeComponent.MailboxSpace.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), dbInfo.MailboxDatabaseName, monitorDefinition.Name, ServiceHealthStatus.Unhealthy, true, 0);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition2, base.TraceContext);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateDatabaseLogicalPhysicalSizeRatioContext: Finished creation of database logical physical size ratio context", null, "CreateDatabaseLogicalPhysicalSizeRatioContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 517);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0005C604 File Offset: 0x0005A804
		private ProbeDefinition CreateProbe(string targetResource, string probeTypeName, string probeName, TimeSpan recurrenceInterval, TimeSpan timeoutInterval)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateProbe: Creating {0} for {1}", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 534);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			MailboxSpaceDiscovery.PopulateProbeDefinition(probeDefinition, targetResource, probeTypeName, probeName, recurrenceInterval, timeoutInterval);
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateProbe: Created {0} for {1}", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 543);
			return probeDefinition;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0005C674 File Offset: 0x0005A874
		private MonitorDefinition CreateMonitor(string targetResource, string monitorTypeName, string monitorName, TimeSpan recurrenceInterval, TimeSpan monitoringInterval, int? monitoringThreshold, TimeSpan timeoutInterval, string sampleMask)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateMonitor: Creating {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 575);
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = MailboxSpaceDiscovery.AssemblyPath;
			monitorDefinition.TypeName = monitorTypeName;
			monitorDefinition.Name = monitorName;
			monitorDefinition.ServiceName = MailboxSpaceDiscovery.mailboxSpaceHealthsetName;
			monitorDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			monitorDefinition.MaxRetryAttempts = 3;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.MonitoringIntervalSeconds = (int)monitoringInterval.TotalSeconds;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = ExchangeComponent.MailboxSpace;
			if (monitoringThreshold != null)
			{
				monitorDefinition.MonitoringThreshold = (double)monitoringThreshold.Value;
			}
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateMonitor: Created {0} for {1}", monitorName, targetResource, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 600);
			return monitorDefinition;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0005C784 File Offset: 0x0005A984
		private ResponderDefinition CreateResponder(string targetResource, string responderTypeName, string responderName, string alertMask, string alertTypeId, TimeSpan recurrenceInterval, TimeSpan waitInterval, ServiceHealthStatus targetHealthState)
		{
			return this.CreateResponder(targetResource, responderTypeName, responderName, alertMask, alertTypeId, recurrenceInterval, waitInterval, TimeSpan.FromMinutes(2.0), targetHealthState);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
		private ResponderDefinition CreateResponder(string targetResource, string responderTypeName, string responderName, string alertMask, string alertTypeId, TimeSpan recurrenceInterval, TimeSpan waitInterval, TimeSpan timeoutInterval, ServiceHealthStatus targetHealthState)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateResponder: Creating {0} for {1}", responderName, targetResource, null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 668);
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = MailboxSpaceDiscovery.AssemblyPath;
			responderDefinition.TypeName = responderTypeName;
			responderDefinition.Name = responderName;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			responderDefinition.TimeoutSeconds = (int)timeoutInterval.TotalSeconds;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.ServiceName = MailboxSpaceDiscovery.mailboxSpaceHealthsetName;
			responderDefinition.WaitIntervalSeconds = (int)waitInterval.TotalSeconds;
			responderDefinition.TargetHealthState = targetHealthState;
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "MailboxSpaceDiscovery.CreateResponder: Created {0} for {1}", responderName, targetResource, null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\MailboxSpaceDiscovery.cs", 690);
			return responderDefinition;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0005C88C File Offset: 0x0005AA8C
		private void CreateDatabaseSizeEscalateWorkitems()
		{
			TimeSpan timeSpan = new TimeSpan(MailboxSpaceDiscovery.databaseSizeRecurrence.Ticks + MailboxSpaceDiscovery.databaseSizeRecurrence.Ticks / 2L);
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.MailboxSpace.Name, "DatabaseSizeEscalationNotification", null);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("DatabaseSizeEscalationProcessingMonitor", sampleMask, ExchangeComponent.MailboxSpace.Name, ExchangeComponent.MailboxSpace, 1, true, (int)timeSpan.TotalSeconds);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, MailboxSpaceDiscovery.databaseSizeRecurrence)
			};
			monitorDefinition.RecurrenceIntervalSeconds = (int)MailboxSpaceDiscovery.databaseSizeRecurrence.TotalSeconds;
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MailboxSpace health is not impacted by database health issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string escalationMessageUnhealthy;
			if (MailboxSpaceDiscovery.isDatacenter)
			{
				escalationMessageUnhealthy = Strings.DatabaseSizeEscalationMessageDc(string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{2}' -Server {3}", new object[]
				{
					MailboxSpaceDiscovery.mailboxSpaceHealthsetName,
					"DatabaseSpaceProbe",
					"{Probe.StateAttribute1}",
					Environment.MachineName
				}), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.MailboxSpace.Name, "DatabaseSizeMonitor"));
			}
			else
			{
				escalationMessageUnhealthy = Strings.DatabaseSizeEscalationMessageEnt(string.Format("Invoke-MonitoringProbe -Identity '{0}\\{1}\\{2}' -Server {3}", new object[]
				{
					MailboxSpaceDiscovery.mailboxSpaceHealthsetName,
					"DatabaseSpaceProbe",
					"{Probe.StateAttribute1}",
					Environment.MachineName
				}), string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.MailboxSpace.Name, "DatabaseSizeMonitor"));
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("DatabaseSizeEscalate", MailboxSpaceDiscovery.mailboxSpaceHealthsetName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), null, ServiceHealthStatus.Unrecoverable, ExchangeComponent.MailboxSpace.EscalationTeam, Strings.DatabaseSizeEscalationSubject, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.RecurrenceIntervalSeconds = 0;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0005CA7C File Offset: 0x0005AC7C
		private void CreateDatabaseLogicalPhysicalSizeRatioConsolidatedWorkitems()
		{
			TimeSpan duration = new TimeSpan(2L * MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioRecurrence.Ticks);
			string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.MailboxSpace.Name, "DatabaseLogicalPhysicalSizeRatioEscalationNotification", null);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("DatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitor", sampleMask, ExchangeComponent.MailboxSpace.Name, ExchangeComponent.MailboxSpace, 1, true, (int)MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioMonitoringInterval.TotalSeconds);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioRecurrence)
			};
			monitorDefinition.RecurrenceIntervalSeconds = (int)MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioRecurrence.TotalSeconds;
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate MailboxSpace health is not impacted by database health issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string escalationMessageUnhealthy;
			if (MailboxSpaceDiscovery.isDatacenter)
			{
				escalationMessageUnhealthy = Strings.DatabaseLogicalPhysicalSizeRatioEscalationMessageDc(0.9, duration, string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.MailboxSpace.Name, "DatabaseLogicalPhysicalSizeRatioMonitor"));
			}
			else
			{
				escalationMessageUnhealthy = Strings.DatabaseLogicalPhysicalSizeRatioEscalationMessageEnt(0.9, duration, string.Format("Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}", Environment.MachineName, ExchangeComponent.MailboxSpace.Name, "DatabaseLogicalPhysicalSizeRatioMonitor"));
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("DatabaseLogicalPhysicalSizeRatioEscalate", ExchangeComponent.MailboxSpace.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), null, ServiceHealthStatus.Unrecoverable, ExchangeComponent.MailboxSpace.EscalationTeam, Strings.DatabaseLogicalPhysicalSizeRatioEscalationSubject(duration), escalationMessageUnhealthy, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.RecurrenceIntervalSeconds = 0;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			ResponderDefinition definition = SetMonitorStateRepairingResponder.CreateDefinition("SetDatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitorStateRepairing", ExchangeComponent.MailboxSpace.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), string.Empty, monitorDefinition.Name, ServiceHealthStatus.Unrecoverable, true, 0);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x04000A22 RID: 2594
		internal const string DatabaseBufferThresholdString = "DatabaseBufferThreshold";

		// Token: 0x04000A23 RID: 2595
		internal const string DatabaseLogsThresholdString = "DatabaseLogsThreshold";

		// Token: 0x04000A24 RID: 2596
		internal const string SearchSizeFactorThresholdString = "SearchSizeFactorThreshold";

		// Token: 0x04000A25 RID: 2597
		internal const string NumberOfDatabasesPerDiskString = "NumberOfDatabasesPerDisk";

		// Token: 0x04000A26 RID: 2598
		internal const string DatabaseDriveSpaceTriggerString = "DatabaseDriveSpaceTrigger_Error";

		// Token: 0x04000A27 RID: 2599
		internal const string DatabaseLogicalPhysicalSizeRatioTriggerString = "DatabaseLogicalPhysicalSizeRatioTrigger_Error";

		// Token: 0x04000A28 RID: 2600
		internal const double SearchSizeFactorThreshold = 0.2;

		// Token: 0x04000A29 RID: 2601
		internal const int NumberOfDatabasesPerDisk = 4;

		// Token: 0x04000A2A RID: 2602
		private const int DriveSpaceMonitoringThreshold = 1;

		// Token: 0x04000A2B RID: 2603
		private const int DatabaseLogicalPhysicalSizeRatioNumberOfSamplesAboveThreshold = 1;

		// Token: 0x04000A2C RID: 2604
		private const double DatabaseLogicalPhysicalRatioThreshold = 0.9;

		// Token: 0x04000A2D RID: 2605
		private const int MaxRetryAttempt = 3;

		// Token: 0x04000A2E RID: 2606
		internal static ByteQuantifiedSize DatabaseBufferThreshold = ByteQuantifiedSize.FromGB(100UL);

		// Token: 0x04000A2F RID: 2607
		internal static ByteQuantifiedSize DatabaseLogsThreshold = ByteQuantifiedSize.FromGB(100UL);

		// Token: 0x04000A30 RID: 2608
		private static TimeSpan databaseSizeRecurrence = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000A31 RID: 2609
		private static TimeSpan databaseSizeTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000A32 RID: 2610
		private static TimeSpan databaseSizeWaitInterval = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000A33 RID: 2611
		private static TimeSpan databaseLogicalPhysicalSizeRatioRecurrence = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000A34 RID: 2612
		private static TimeSpan databaseLogicalPhysicalSizeRatioMonitoringInterval = new TimeSpan(MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioRecurrence.Ticks + MailboxSpaceDiscovery.databaseLogicalPhysicalSizeRatioRecurrence.Ticks / 2L);

		// Token: 0x04000A35 RID: 2613
		private static TimeSpan databaseLogicalPhysicalSizeRatioWaitInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000A36 RID: 2614
		private static bool isTestTopology;

		// Token: 0x04000A37 RID: 2615
		private static bool isDatacenter;

		// Token: 0x04000A38 RID: 2616
		private static bool isDatacenterDedicated;

		// Token: 0x04000A39 RID: 2617
		private static string mailboxSpaceHealthsetName = ExchangeComponent.MailboxSpace.Name;

		// Token: 0x04000A3A RID: 2618
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000A3B RID: 2619
		private static readonly string DBSpaceProbeTypeName = typeof(DatabaseSpaceProbe).FullName;

		// Token: 0x04000A3C RID: 2620
		private static readonly string DBSizeMonitorTypeName = typeof(DatabaseSizeMonitor).FullName;

		// Token: 0x04000A3D RID: 2621
		private static readonly string DatabaseProvisioningResponderTypeName = typeof(DatabaseProvisioningResponder).FullName;

		// Token: 0x04000A3E RID: 2622
		private static readonly string DatabaseSizeEscalationNotificationResponderTypeName = typeof(DatabaseSizeEscalationNotificationResponder).FullName;

		// Token: 0x04000A3F RID: 2623
		private static readonly string EscalationNotificationResponderTypeName = typeof(EscalationNotificationResponder).FullName;
	}
}
