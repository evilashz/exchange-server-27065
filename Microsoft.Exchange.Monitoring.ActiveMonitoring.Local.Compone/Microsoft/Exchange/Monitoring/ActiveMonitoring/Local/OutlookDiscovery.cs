using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess;
using Microsoft.Exchange.Net;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000204 RID: 516
	public abstract class OutlookDiscovery : DiscoveryWorkItem
	{
		// Token: 0x06000E67 RID: 3687
		internal abstract MonitorStateTransition[] GetProtocolMonitorStateTransitions();

		// Token: 0x06000E68 RID: 3688
		protected abstract bool DidDiscoveryExecuteSuccessfully();

		// Token: 0x06000E69 RID: 3689
		protected abstract void SetSuccessfulDiscoveryExecutionStatus();

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00060376 File Offset: 0x0005E576
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.RPCHTTPTracer;
			}
		}

		// Token: 0x170002DE RID: 734
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0006037D File Offset: 0x0005E57D
		internal bool IsTestRun
		{
			set
			{
				this.isTestRun = value;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00060388 File Offset: 0x0005E588
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			this.ReadExtensionAttributes();
			if (this.DidDiscoveryExecuteSuccessfully())
			{
				if (!this.isTestRun)
				{
					goto IL_94;
				}
			}
			try
			{
				this.ValidateEndpoints();
				if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsClientAccessRoleInstalled)
				{
					this.AddBackendActiveMonitoring();
				}
				if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					this.AddCafeActiveMonitoring();
				}
				this.SetSuccessfulDiscoveryExecutionStatus();
				return;
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				string text = string.Format("OutlookDiscovery:: DoWork(): Endpoint initialization threw an exception. Exception:{0}", ex.ToString());
				base.Result.StateAttribute3 = text;
				WTFDiagnostics.TraceInformation(this.Trace, base.TraceContext, text, null, "CreateWorkTasks", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MapiMT\\OutlookDiscovery.cs", 165);
				return;
			}
			IL_94:
			string text2 = "OutlookDiscovery:: DoWork(): Work definitions have already been created";
			base.Result.StateAttribute3 = text2;
			WTFDiagnostics.TraceInformation(this.Trace, base.TraceContext, text2, null, "CreateWorkTasks", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MapiMT\\OutlookDiscovery.cs", 176);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000604FC File Offset: 0x0005E6FC
		protected virtual void AddBackendActiveMonitoring()
		{
			DiscoveryWorkItem.AddTestsForEachResource<MailboxDatabaseInfo>(delegate
			{
				return from dbInfo in LocalEndpointManager.Instance.MailboxDatabaseEndpoint.EnsureNotNull((MailboxDatabaseEndpoint x) => x.MailboxDatabaseInfoCollectionForBackend)
				where !string.IsNullOrEmpty(dbInfo.MonitoringAccount)
				select dbInfo;
			}, new Action<IEnumerable<MailboxDatabaseInfo>>[]
			{
				new Action<IEnumerable<MailboxDatabaseInfo>>(this.AddMailboxDeepTest),
				new Action<IEnumerable<MailboxDatabaseInfo>>(this.AddSelfTest)
			});
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000605E0 File Offset: 0x0005E7E0
		protected virtual void AddCafeActiveMonitoring()
		{
			DiscoveryWorkItem.AddTestsForEachResource<MailboxDatabaseInfo>(delegate
			{
				return from dbInfo in LocalEndpointManager.Instance.MailboxDatabaseEndpoint.EnsureNotNull((MailboxDatabaseEndpoint x) => x.MailboxDatabaseInfoCollectionForCafe)
				where !string.IsNullOrEmpty(dbInfo.MonitoringAccount)
				select dbInfo;
			}, new Action<IEnumerable<MailboxDatabaseInfo>>[]
			{
				new Action<IEnumerable<MailboxDatabaseInfo>>(this.AddMailboxCtp),
				new Action<IEnumerable<MailboxDatabaseInfo>>(this.AddArchiveCtp)
			});
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00060638 File Offset: 0x0005E838
		private void ValidateEndpoints()
		{
			LocalEndpointManager.Instance.EnsureNotNull((LocalEndpointManager x) => x.ExchangeServerRoleEndpoint);
			LocalEndpointManager.Instance.EnsureNotNull((LocalEndpointManager x) => x.MailboxDatabaseEndpoint);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000606D1 File Offset: 0x0005E8D1
		private void ReadExtensionAttributes()
		{
			this.UseServerAuthforBackEndProbes = this.ReadAttribute("UseServerAuthforBackEndProbes", false);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000606E8 File Offset: 0x0005E8E8
		protected static ProbeDefinition CreateProbe<TProbe>(ProbeIdentity probeIdentity) where TProbe : ProbeWorkItem, new()
		{
			ProbeDefinition probeDefinition = new ProbeDefinition
			{
				Endpoint = ComputerInformation.DnsPhysicalFullyQualifiedDomainName,
				MaxRetryAttempts = 0,
				Enabled = true
			}.Apply(probeIdentity);
			probeDefinition.SetType(typeof(TProbe));
			return probeDefinition;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00060730 File Offset: 0x0005E930
		internal static ResponderDefinition CreateEscalateResponder(MonitorIdentity monitorIdentity)
		{
			string escalationTeam = monitorIdentity.Component.EscalationTeam;
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Escalate", null);
			return EscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.ServiceName, monitorIdentity.Name, monitorIdentity.GetAlertMask(), monitorIdentity.TargetResource, ServiceHealthStatus.Unrecoverable, escalationTeam, Strings.RcaEscalationSubject(monitorIdentity.GetAlertMask(), Environment.MachineName), Datacenter.IsMicrosoftHostedOnly(true) ? "EscalationMessage.LocalProbe.html" : Strings.RcaEscalationBodyEnt, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", Datacenter.IsMicrosoftHostedOnly(true)).Apply(responderIdentity).ApplyModifier(new Func<ResponderDefinition, ResponderDefinition>(OutlookEscalateResponder<InterpretedResult>.Configure));
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000607D4 File Offset: 0x0005E9D4
		protected static ResponderDefinition CreateServiceRestartResponder(MonitorIdentity monitorIdentity, ServiceHealthStatus targetState, string windowsServiceName)
		{
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Restart", windowsServiceName);
			string name = responderIdentity.Name;
			string alertMask = monitorIdentity.GetAlertMask();
			string serviceName = responderIdentity.ServiceName;
			return RestartServiceResponder.CreateDefinition(name, alertMask, windowsServiceName, targetState, 15, 120, 0, false, DumpMode.FullDump, null, 15.0, 0, serviceName, null, true, true, null, false).Apply(responderIdentity);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00060828 File Offset: 0x0005EA28
		protected static ResponderDefinition CreateAppPoolRestartResponder(MonitorIdentity monitorIdentity, string appPoolName, ServiceHealthStatus responderTargetState)
		{
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Restart", appPoolName);
			string name = responderIdentity.Name;
			string alertMask = monitorIdentity.GetAlertMask();
			string serviceName = responderIdentity.ServiceName;
			return ResetIISAppPoolResponder.CreateDefinition(name, alertMask, appPoolName, responderTargetState, DumpMode.FullDump, null, 15.0, 0, serviceName, true, null).Apply(responderIdentity);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00060874 File Offset: 0x0005EA74
		protected static ResponderDefinition CreateFailoverResponder(MonitorIdentity monitorIdentity)
		{
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Failover", null);
			return SystemFailoverResponder.CreateDefinition(responderIdentity.Name, monitorIdentity.GetAlertMask(), ServiceHealthStatus.Unhealthy, responderIdentity.Component.ShortName, "Exchange", true).Apply(responderIdentity);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000608B8 File Offset: 0x0005EAB8
		protected MonitorDefinition CreateSnappyMonitor(ProbeIdentity probeIdentity)
		{
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.ServiceName, monitorIdentity.Component, 5, true, 300).Apply(monitorIdentity);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitorStateTransitions = this.GetProtocolMonitorStateTransitions();
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = string.Format("Validate {0} health is not impacted by instantaneous recoverable issues", Extensions.MomtComponentName);
			return monitorDefinition;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00060928 File Offset: 0x0005EB28
		protected MonitorDefinition CreatePercentSuccessMonitor(ProbeIdentity probeIdentity, double availabilityThreshold, int minimumErrorCount, TimeSpan detectionTime, TimeSpan recurrenceInterval)
		{
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.ServiceName, monitorIdentity.Component, availabilityThreshold, detectionTime, minimumErrorCount, true).Apply(monitorIdentity);
			monitorDefinition.RecurrenceIntervalSeconds = (int)recurrenceInterval.TotalSeconds;
			monitorDefinition.MonitorStateTransitions = this.GetProtocolMonitorStateTransitions();
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = string.Format("Validate {0} health is not impacted by alertable issues", Extensions.MomtComponentName);
			return monitorDefinition;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000609A0 File Offset: 0x0005EBA0
		protected MonitorDefinition CreateChunkingMonitor(ProbeIdentity probeIdentity, int numberOfResources, TimeSpan detectionTime)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("numberOfResources", numberOfResources);
			TimeSpan timeSpan = TimeSpan.FromSeconds(detectionTime.TotalSeconds / 5.0);
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			MonitorDefinition monitorDefinition = OverallPercentSuccessByStateAttribute1Monitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.ServiceName, monitorIdentity.Component, 90.0, timeSpan, timeSpan, detectionTime, "", true).Apply(monitorIdentity);
			monitorDefinition.MonitorStateTransitions = this.GetProtocolMonitorStateTransitions();
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = string.Format("Validate {0} health is not impacted by alertable issues", Extensions.MomtComponentName);
			return monitorDefinition;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00060A38 File Offset: 0x0005EC38
		private void AddForEachDatabase(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Action<ProbeIdentity, MailboxDatabaseInfo> workDefinitionTaskCreator)
		{
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in databases)
			{
				base.CreateRelatedWorkItems<ProbeIdentity, MailboxDatabaseInfo>(baseProbeIdentity.ForTargetResource(mailboxDatabaseInfo.MailboxDatabaseName), mailboxDatabaseInfo, workDefinitionTaskCreator);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00060AD4 File Offset: 0x0005ECD4
		protected void AddForEachDatabaseForScheduledAndOnDemandExecution(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeIdentity, MailboxDatabaseInfo, ProbeDefinition> probeDefinitionCreator)
		{
			this.AddForEachDatabase(databases, baseProbeIdentity, delegate(ProbeIdentity probeIdentity, MailboxDatabaseInfo database)
			{
				this.AddWorkDefinition<ProbeDefinition>(probeDefinitionCreator(probeIdentity, database));
			});
			base.CreateRelatedWorkItems<ProbeIdentity, MailboxDatabaseInfo>(baseProbeIdentity, databases.First<MailboxDatabaseInfo>(), delegate(ProbeIdentity probeIdentity, MailboxDatabaseInfo database)
			{
				this.AddWorkDefinition<ProbeDefinition>(probeDefinitionCreator(probeIdentity, database).MakeTemplateForOnDemandExecution());
			});
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00060B9C File Offset: 0x0005ED9C
		protected void AddMonitorAndResponders(MonitorDefinition monitor)
		{
			monitor.IsHaImpacting = monitor.MonitorStateTransitions.Any((MonitorStateTransition transition) => transition.ToState == ServiceHealthStatus.Unhealthy);
			base.AddWorkDefinition<MonitorDefinition>(monitor);
			ResponderDefinition[] source = this.CreateProtocolSpecificResponders(monitor);
			foreach (ResponderDefinition responderDefinition in from responder in source
			where responder != null && monitor.MonitorStateTransitions.Any((MonitorStateTransition transition) => transition.ToState == responder.TargetHealthState)
			select responder)
			{
				responderDefinition.RecurrenceIntervalSeconds = 0;
				base.AddWorkDefinition<ResponderDefinition>(responderDefinition);
			}
		}

		// Token: 0x06000E7C RID: 3708
		protected abstract ResponderDefinition[] CreateProtocolSpecificResponders(MonitorIdentity monitor);

		// Token: 0x06000E7D RID: 3709 RVA: 0x00060C6D File Offset: 0x0005EE6D
		private void AddMailboxDeepTest(IEnumerable<MailboxDatabaseInfo> databases)
		{
			this.AddDeepTest(databases, this.GetDeepTest(), (ProbeDefinition probe, MailboxDatabaseInfo dbInfo) => probe.TargetPrimaryMailbox(dbInfo));
		}

		// Token: 0x06000E7E RID: 3710
		protected abstract ProbeIdentity GetDeepTest();

		// Token: 0x06000E7F RID: 3711
		protected abstract void AddDeepTest(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier);

		// Token: 0x06000E80 RID: 3712
		protected abstract void AddSelfTest(IEnumerable<MailboxDatabaseInfo> databases);

		// Token: 0x06000E81 RID: 3713 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		private void AddMailboxCtp(IEnumerable<MailboxDatabaseInfo> databases)
		{
			if (!Extensions.IsDatacenter)
			{
				this.AddCtp(databases, this.GetMailboxCTPTest(), (ProbeDefinition probe, MailboxDatabaseInfo dbInfo) => probe.TargetPrimaryMailbox(dbInfo), (MonitorDefinition monitor) => monitor);
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00060D1C File Offset: 0x0005EF1C
		private void AddArchiveCtp(IEnumerable<MailboxDatabaseInfo> databases)
		{
			if (!Extensions.IsDatacenter)
			{
				this.AddCtp(databases, this.GetArchiveCTPTest(), (ProbeDefinition probe, MailboxDatabaseInfo dbInfo) => probe.TargetArchiveMailbox(dbInfo), (MonitorDefinition monitor) => monitor.DelayStateTransitions(OutlookDiscovery.ArchiveMonitorStateTransitionDelay));
			}
		}

		// Token: 0x06000E83 RID: 3715
		protected abstract void AddCtp(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier, Func<MonitorDefinition, MonitorDefinition> monitorModifier);

		// Token: 0x06000E84 RID: 3716
		protected abstract ProbeIdentity GetMailboxCTPTest();

		// Token: 0x06000E85 RID: 3717
		protected abstract ProbeIdentity GetArchiveCTPTest();

		// Token: 0x06000E86 RID: 3718 RVA: 0x00060D80 File Offset: 0x0005EF80
		internal bool CheckRpcProxyAuthenticationSettingsAvailable(IEnumerable<AutodiscoverRpcHttpSettings> settings, WorkItemIdentity baseIdentity)
		{
			if (!settings.Any<AutodiscoverRpcHttpSettings>())
			{
				WTFDiagnostics.TraceInformation<WorkItemIdentity>(this.Trace, base.TraceContext, "Unable to get any OutlookAnywhere settings for {0}", baseIdentity, null, "CheckRpcProxyAuthenticationSettingsAvailable", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MapiMT\\OutlookDiscovery.cs", 642);
				base.CreateRelatedWorkItems<WorkItemIdentity>(baseIdentity, delegate(WorkItemIdentity unused)
				{
					throw new OutlookAnywhereNotFoundException();
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00060DFC File Offset: 0x0005EFFC
		protected bool CheckMonitoringAccountsAvailable<T>(IEnumerable<T> accounts, WorkItemIdentity baseIdentity, LocalizedString message)
		{
			if (!accounts.Any<T>())
			{
				WTFDiagnostics.TraceInformation<WorkItemIdentity, LocalizedString>(this.Trace, base.TraceContext, "OutlookDiscovery.CheckMonitoringAccountsAvailable while creating work items for {0}: {1}", baseIdentity, message, null, "CheckMonitoringAccountsAvailable", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MapiMT\\OutlookDiscovery.cs", 674);
				base.CreateRelatedWorkItems<WorkItemIdentity>(baseIdentity, delegate(WorkItemIdentity unused)
				{
					throw new NoMonitoringAccountsAvailableException(message);
				});
				return false;
			}
			return true;
		}

		// Token: 0x04000ACB RID: 2763
		public const int FailureCountToTriggerAlert = 5;

		// Token: 0x04000ACC RID: 2764
		internal const string FederatedAuthServiceName = "MSExchangeProtectedServiceHost";

		// Token: 0x04000ACD RID: 2765
		internal const string DatacenterEscalationMessageResourceName = "EscalationMessage.LocalProbe.html";

		// Token: 0x04000ACE RID: 2766
		private bool isTestRun;

		// Token: 0x04000ACF RID: 2767
		protected static readonly TimeSpan ArchiveMonitorStateTransitionDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000AD0 RID: 2768
		protected static readonly TimeSpan FullDumpTimeout = TimeSpan.FromMinutes(3.0);

		// Token: 0x04000AD1 RID: 2769
		protected bool UseServerAuthforBackEndProbes;

		// Token: 0x02000205 RID: 517
		protected static class RecoveryAction
		{
			// Token: 0x04000ADD RID: 2781
			public const ServiceHealthStatus RestartProtocolProxy = ServiceHealthStatus.Degraded;

			// Token: 0x04000ADE RID: 2782
			public const ServiceHealthStatus RestartMAPIHTTPCafeAppPool = ServiceHealthStatus.Degraded;

			// Token: 0x04000ADF RID: 2783
			public const ServiceHealthStatus RestartProtocolImplementation = ServiceHealthStatus.Degraded1;

			// Token: 0x04000AE0 RID: 2784
			public const ServiceHealthStatus RestartMAPIHTTPBackEndAppPool = ServiceHealthStatus.Degraded1;

			// Token: 0x04000AE1 RID: 2785
			public const ServiceHealthStatus RestartAuthenticationServices = ServiceHealthStatus.Degraded2;

			// Token: 0x04000AE2 RID: 2786
			public const ServiceHealthStatus Failover = ServiceHealthStatus.Unhealthy;

			// Token: 0x04000AE3 RID: 2787
			public const ServiceHealthStatus Reboot = ServiceHealthStatus.Unhealthy1;

			// Token: 0x04000AE4 RID: 2788
			public const ServiceHealthStatus Escalate = ServiceHealthStatus.Unrecoverable;
		}
	}
}
