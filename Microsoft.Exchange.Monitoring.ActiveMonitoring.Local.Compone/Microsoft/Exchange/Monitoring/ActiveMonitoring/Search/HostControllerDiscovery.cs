using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search
{
	// Token: 0x0200048F RID: 1167
	public sealed class HostControllerDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001D79 RID: 7545 RVA: 0x000B0A2C File Offset: 0x000AEC2C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.ExchangeServerRoleEndpoint == null || !instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.SearchTracer, base.TraceContext, "HostControllerDiscovery.DoWork: Mailbox role is not installed on this server, no need to create FAST related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Search\\HostControllerDiscovery.cs", 100);
				}
				else
				{
					this.CreateMonitoringContexts();
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				SearchMonitoringHelper.LogInfo(this, "EndpointManagerEndpointUninitializedException is caught. Endpoint is not available to do monitoring.", new object[0]);
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000B0AA4 File Offset: 0x000AECA4
		private void CreateMonitoringContexts()
		{
			if (SearchMonitoringHelper.IsInMaintenance())
			{
				SearchMonitoringHelper.LogInfo(this, "Server is in maintenance. Skipping monitoring discovery.", new object[0]);
				return;
			}
			this.attributeHelper = new AttributeHelper(base.Definition);
			string @string = this.attributeHelper.GetString("EscalateDailySchedulePattern", false, SearchEscalateResponder.EscalateDailySchedulePattern);
			SearchEscalateResponder.EscalateDailySchedulePattern = @string;
			SearchMonitoringHelper.SetDiagnosticDefaults();
			this.CreateHostControllerServiceContext();
			this.CreateFastNodeAvailabilityContext();
			this.CreateProcessCrashingContext();
			this.CreateQueryStxContext();
			this.CreateInstantSearchStxContext();
			this.CreateWordBreakerLoadingContext();
			this.CreateFastNodeRestartContext();
			this.CreateIndexNumDiskPartsContext();
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000B0B30 File Offset: 0x000AED30
		private void CreateProcessCrashingContext()
		{
			string text = "NodeRunner";
			bool @bool = this.attributeHelper.GetBool("FastNodeCrashEnabled", true, true);
			int @int = this.attributeHelper.GetInt("FastNodeCrashProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("FastNodeCrashMonitoringIntervalSeconds", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("FastNodeCrashMonitoringThreshold", true, 0, null, null);
			ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("FastNodeCrashProbe", typeof(GenericProcessCrashDetectionProbe), text, @int, @bool);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateMonitorDefinition("FastNodeCrashMonitor", HostControllerDiscovery.OverallXFailuresMonitorType, probeDefinition.ConstructWorkItemResultName(), text, @int, int2, int3, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by FastNode issues";
			string settingPrefix = "FastNodeCrash";
			MonitorDefinition monitorDefinition2 = monitorDefinition;
			bool restartHostControllerServiceNeeded = true;
			SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, Strings.SearchProcessCrashingTooManyTimesEscalationMessage(text, int3, int2), @bool, false, false, false, restartHostControllerServiceNeeded, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000B0C68 File Offset: 0x000AEE68
		private void CreateFastNodeAvailabilityContext()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			bool @bool = this.attributeHelper.GetBool("FastNodeAvailabilityEnabled", true, true);
			int @int = this.attributeHelper.GetInt("FastNodeAvailabilityProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("FastNodeAvailabilityMonitorMonitoringIntervalSeconds", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("FastNodeAvailabilityMonitorMonitoringThreshold", true, 0, null, null);
			bool bool2 = this.attributeHelper.GetBool("FastNodeAvailabilityProbeCheckNodeMemory", true, false);
			double num = double.Parse(base.Definition.Attributes["FastNodeAvailabilityProbeIndexNodePrivateByteLimitGB"], CultureInfo.InvariantCulture.NumberFormat);
			double num2 = double.Parse(base.Definition.Attributes["FastNodeAvailabilityProbeCtsNodePrivateByteLimitGB"], CultureInfo.InvariantCulture.NumberFormat);
			double num3 = double.Parse(base.Definition.Attributes["FastNodeAvailabilityProbeImsNodePrivateByteLimitGB"], CultureInfo.InvariantCulture.NumberFormat);
			double num4 = double.Parse(base.Definition.Attributes["FastNodeAvailabilityProbeAdminNodePrivateByteLimitGB"], CultureInfo.InvariantCulture.NumberFormat);
			ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("FastNodeAvailabilityProbe", HostControllerDiscovery.HostControllerNodeAvailabilityProbeType, string.Empty, @int, @bool);
			probeDefinition.Attributes["CheckNodeMemory"] = bool2.ToString();
			probeDefinition.Attributes["IndexNodePrivateByteLimitGB"] = num.ToString();
			probeDefinition.Attributes["CtsNodePrivateByteLimitGB"] = num2.ToString();
			probeDefinition.Attributes["ImsNodePrivateByteLimitGB"] = num3.ToString();
			probeDefinition.Attributes["AdminNodePrivateByteLimitGB"] = num4.ToString();
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateMonitorDefinition("FastNodeAvailabilityMonitor", HostControllerDiscovery.OverallXFailuresMonitorType, probeDefinition.ConstructWorkItemResultName(), string.Empty, @int, int2, int3, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by FastNode issues";
			string settingPrefix = "FastNodeAvailability";
			MonitorDefinition monitorDefinition2 = monitorDefinition;
			bool restartSearchServiceNeeded = false;
			bool restartNodesNeeded = true;
			bool restartHostControllerServiceNeeded = true;
			SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, Strings.FastNodeNotHealthyEscalationMessage, @bool, false, restartSearchServiceNeeded, restartNodesNeeded, restartHostControllerServiceNeeded, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000B0EC8 File Offset: 0x000AF0C8
		private void CreateFastNodeRestartContext()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			bool @bool = this.attributeHelper.GetBool("FastNodeRestartEnabled", true, true);
			int @int = this.attributeHelper.GetInt("FastNodeRestartProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("FastNodeRestartProbeRestartThreshold", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("FastNodeRestartProbeRestartHistoryCheckWindowMinutes", true, 0, null, null);
			string[] array = new string[]
			{
				"AdminNode1",
				"ContentEngineNode1",
				"IndexNode1",
				"InteractionEngineNode1"
			};
			foreach (string targetResource in array)
			{
				ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("FastNodeRestartProbe", typeof(HostContollerNodeRestartProbe), targetResource, @int, @bool);
				probeDefinition.Attributes["RestartThreshold"] = int2.ToString();
				probeDefinition.Attributes["RestartHistoryCheckWindowMinutes"] = int3.ToString();
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000B1014 File Offset: 0x000AF214
		private void CreateQueryStxContext()
		{
			bool @bool = this.attributeHelper.GetBool("SearchQueryStxEnabled", true, true);
			int @int = this.attributeHelper.GetInt("SearchQueryStxProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("SearchQueryStxMonitoringThreshold", true, 0, null, null);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				string mailboxDatabaseName = mailboxDatabaseInfo.MailboxDatabaseName;
				ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("SearchQueryStxProbe", typeof(SearchQueryStxProbe), mailboxDatabaseName, @int, @bool);
				string monitoringMailboxFromDatabaseInfo = this.GetMonitoringMailboxFromDatabaseInfo(mailboxDatabaseInfo);
				probeDefinition.Attributes["MonitoringMailboxSmtpAddress"] = monitoringMailboxFromDatabaseInfo;
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateOverallConsecutiveProbeFailuresMonitorDefinition("SearchQueryStxMonitor", probeDefinition.ConstructWorkItemResultName(), mailboxDatabaseName, @int, int2, int2 * @int, @bool);
				monitorDefinition.ServicePriority = 1;
				monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by BE issues";
				string settingPrefix = "SearchQueryStx";
				MonitorDefinition monitorDefinition2 = monitorDefinition;
				bool databaseFailoverNeeded = true;
				bool restartSearchServiceNeeded = true;
				bool restartNodesNeeded = true;
				bool restartHostControllerServiceNeeded = true;
				string escalationMessage = Strings.SearchQueryStxEscalationMessage(mailboxDatabaseName, int2, @int);
				SearchEscalateResponder.EscalateModes escalateMode = SearchEscalateResponder.EscalateModes.Urgent;
				SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, escalationMessage, @bool, databaseFailoverNeeded, restartSearchServiceNeeded, restartNodesNeeded, restartHostControllerServiceNeeded, escalateMode, true);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000B11A0 File Offset: 0x000AF3A0
		private void CreateInstantSearchStxContext()
		{
			bool @bool = this.attributeHelper.GetBool("SearchInstantSearchStxEnabled", true, true);
			int @int = this.attributeHelper.GetInt("SearchInstantSearchStxProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("SearchInstantSearchStxMonitoringThreshold", true, 0, null, null);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				string mailboxDatabaseName = mailboxDatabaseInfo.MailboxDatabaseName;
				ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("SearchInstantSearchStxProbe", typeof(SearchInstantSearchStxProbe), mailboxDatabaseName, @int, @bool);
				string monitoringMailboxFromDatabaseInfo = this.GetMonitoringMailboxFromDatabaseInfo(mailboxDatabaseInfo);
				probeDefinition.Attributes["MonitoringMailboxSmtpAddress"] = monitoringMailboxFromDatabaseInfo;
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateOverallConsecutiveProbeFailuresMonitorDefinition("SearchInstantSearchStxMonitor", probeDefinition.ConstructWorkItemResultName(), mailboxDatabaseName, @int, int2, int2 * @int, @bool);
				monitorDefinition.ServicePriority = 1;
				monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by Instant Search issues";
				string settingPrefix = "SearchInstantSearchStx";
				MonitorDefinition monitorDefinition2 = monitorDefinition;
				bool databaseFailoverNeeded = true;
				bool restartSearchServiceNeeded = true;
				bool restartNodesNeeded = true;
				bool restartHostControllerServiceNeeded = true;
				SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, Strings.SearchInstantSearchStxEscalationMessage(mailboxDatabaseName, int2, @int), @bool, databaseFailoverNeeded, restartSearchServiceNeeded, restartNodesNeeded, restartHostControllerServiceNeeded, SearchEscalateResponder.EscalateModes.Scheduled, true);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			}
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000B1328 File Offset: 0x000AF528
		private void CreateHostControllerServiceContext()
		{
			bool enabled = bool.Parse(base.Definition.Attributes["HostControllerServiceRunningEnabled"]);
			int num = int.Parse(base.Definition.Attributes["HostControllerServiceRunningProbeRecurrenceIntervalSeconds"]);
			ProbeDefinition probeDefinition = SearchMonitoringHelper.CreateProbeDefinition("HostControllerServiceRunningProbe", typeof(GenericServiceProbe), "HostControllerService", num, enabled);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			int num2 = int.Parse(base.Definition.Attributes["HostControllerServiceRunningMonitorMonitoringThreshold"]);
			MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateOverallConsecutiveProbeFailuresMonitorDefinition("HostControllerServiceRunningMonitor", probeDefinition.ConstructWorkItemResultName(), "HostControllerService", num, num2, num2 * num, enabled);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by host controller issues";
			string settingPrefix = "HostControllerServiceRunning";
			MonitorDefinition monitorDefinition2 = monitorDefinition;
			bool restartSearchServiceNeeded = false;
			bool restartNodesNeeded = false;
			bool restartHostControllerServiceNeeded = true;
			SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, Strings.HostControllerServiceRunningMessage, enabled, false, restartSearchServiceNeeded, restartNodesNeeded, restartHostControllerServiceNeeded, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000B1424 File Offset: 0x000AF624
		private void CreateWordBreakerLoadingContext()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			bool @bool = this.attributeHelper.GetBool("WordBreakerLoadingFailureEnabled", true, true);
			int @int = this.attributeHelper.GetInt("WordBreakerLoadingFailureProbeRecurrenceIntervalSeconds", true, 0, null, null);
			ProbeDefinition probeDefinition = SearchWordBreakerLoadingProbe.CreateDefinition("SearchWordBreakerLoadingProbe", @int, @bool);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			int int2 = this.attributeHelper.GetInt("WordBreakerLoadingFailureMonitorMonitoringThreshold", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("WordBreakerLoadingFailureMonitorMonitorMonitoringIntervalSeconds", true, 0, null, null);
			string monitorName = "SearchWordBreakerLoadingMonitor";
			Type overallXFailuresMonitorType = HostControllerDiscovery.OverallXFailuresMonitorType;
			string sampleMask = probeDefinition.ConstructWorkItemResultName();
			string targetResource = "HostControllerService";
			int recurrenceIntervalSeconds = @int;
			int monitoringThreshold = int2;
			MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateMonitorDefinition(monitorName, overallXFailuresMonitorType, sampleMask, targetResource, recurrenceIntervalSeconds, int3, monitoringThreshold, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by WordBreaker issues";
			string settingPrefix = "WordBreakerLoadingFailure";
			MonitorDefinition monitorDefinition2 = monitorDefinition;
			bool restartSearchServiceNeeded = false;
			bool restartNodesNeeded = true;
			bool restartHostControllerServiceNeeded = false;
			SearchMonitoringHelper.CreateResponderChainForMonitor(this, settingPrefix, monitorDefinition2, Strings.SearchWordBreakerLoadingFailureEscalationMessage, @bool, false, restartSearchServiceNeeded, restartNodesNeeded, restartHostControllerServiceNeeded, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000B155C File Offset: 0x000AF75C
		private void CreateIndexNumDiskPartsContext()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (instance.MailboxDatabaseEndpoint == null || instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				return;
			}
			bool @bool = this.attributeHelper.GetBool("FastIndexNumDiskPartsEnabled", true, true);
			int @int = this.attributeHelper.GetInt("FastIndexNumDiskPartsMonitorMonitoringThreshold", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("FastIndexNumDiskPartsMonitorMonitoringIntervalSeconds", true, 0, null, null);
			bool bool2 = this.attributeHelper.GetBool("FastIndexNumDiskPartsEscalateResponderUrgentInTraining", true, true);
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, "FastNumDiskPartsTrigger_Warning", string.Format("FSPlugin_IndexNode1-00000000-0000-0000-0000-000000000000-{0}.Single", FastIndexVersion.GetIndexSystemName(mailboxDatabaseInfo.MailboxDatabaseGuid)));
				MonitorDefinition monitorDefinition = SearchMonitoringHelper.CreateOverallConsecutiveProbeFailuresMonitorDefinition("FastIndexNumDiskPartsMonitor", sampleMask, mailboxDatabaseInfo.MailboxDatabaseName, 0, @int, int2, @bool);
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				monitorDefinition.ServicePriority = 1;
				monitorDefinition.ScenarioDescription = "Validate Search health is not impacted by disk issues";
				ResponderDefinition definition = SearchMonitoringHelper.CreateEscalateResponderDefinition(monitorDefinition, Strings.SearchNumDiskPartsEscalationMessage(mailboxDatabaseInfo.MailboxDatabaseName), @bool, ServiceHealthStatus.None, SearchEscalateResponder.EscalateModes.Urgent, bool2);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000B16F4 File Offset: 0x000AF8F4
		private string GetMonitoringMailboxFromDatabaseInfo(MailboxDatabaseInfo dbInfo)
		{
			bool @bool = this.attributeHelper.GetBool("SearchQueryStxUseLegacyMonitoringMailbox", false, false);
			if (@bool)
			{
				MailboxDatabase mailboxDatabaseFromGuid = DirectoryAccessor.Instance.GetMailboxDatabaseFromGuid(dbInfo.MailboxDatabaseGuid);
				ADUser aduser = DirectoryAccessor.Instance.SearchOrCreateMonitoringMailbox(true, ref mailboxDatabaseFromGuid, mailboxDatabaseFromGuid.Guid, null);
				if (aduser != null)
				{
					return aduser.Name + '@' + DirectoryAccessor.Instance.DefaultMonitoringDomain.DomainName.Domain.ToString();
				}
			}
			return dbInfo.MonitoringAccount + '@' + dbInfo.MonitoringAccountDomain;
		}

		// Token: 0x04001473 RID: 5235
		internal const string ServiceName = "HostControllerService";

		// Token: 0x04001474 RID: 5236
		internal const string NodeRunnerProcessName = "NodeRunner";

		// Token: 0x04001475 RID: 5237
		private static readonly Type HostControllerNodeAvailabilityProbeType = typeof(HostControllerNodeAvailabilityProbe);

		// Token: 0x04001476 RID: 5238
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001477 RID: 5239
		private static readonly Type OverallXFailuresMonitorType = typeof(OverallXFailuresMonitor);

		// Token: 0x04001478 RID: 5240
		private static TimeSpan nodeStartTimeOut = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001479 RID: 5241
		private static TimeSpan nodeStopTimeOut = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400147A RID: 5242
		private AttributeHelper attributeHelper;

		// Token: 0x02000490 RID: 1168
		private enum ResponderChainType
		{
			// Token: 0x0400147C RID: 5244
			FailoverSingleDatabaseRestartServiceAndEscalate,
			// Token: 0x0400147D RID: 5245
			FailOverAllDatabasesRestartNodeRestartServiceAndEscalate,
			// Token: 0x0400147E RID: 5246
			RestartServiceAndEscalate
		}
	}
}
