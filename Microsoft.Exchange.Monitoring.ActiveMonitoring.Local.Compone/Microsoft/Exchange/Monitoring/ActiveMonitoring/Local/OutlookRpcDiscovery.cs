using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000207 RID: 519
	public sealed class OutlookRpcDiscovery : OutlookDiscovery
	{
		// Token: 0x06000EA5 RID: 3749 RVA: 0x000612FE File Offset: 0x0005F4FE
		protected override void AddBackendActiveMonitoring()
		{
			base.AddBackendActiveMonitoring();
			this.AddServiceTest();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0006130C File Offset: 0x0005F50C
		internal override MonitorStateTransition[] GetProtocolMonitorStateTransitions()
		{
			return OutlookRpcDiscovery.ProtocolMonitorStateTransitions;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00061313 File Offset: 0x0005F513
		protected override bool DidDiscoveryExecuteSuccessfully()
		{
			return OutlookRpcDiscovery.didDiscoveryExecuteSuccessfully;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0006131A File Offset: 0x0005F51A
		protected override void SetSuccessfulDiscoveryExecutionStatus()
		{
			OutlookRpcDiscovery.didDiscoveryExecuteSuccessfully = true;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00061324 File Offset: 0x0005F524
		private MonitorDefinition CreateCheckMemoryRestartSnappyMonitor(ProbeIdentity probeIdentity)
		{
			MonitorDefinition monitorDefinition = base.CreateSnappyMonitor(probeIdentity).ConfigureMonitorStateTransitions(new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, TimeSpan.FromMinutes(0.0)),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, TimeSpan.FromMinutes(15.0))
			});
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = string.Format("Validate {0} health is not impacted by recoverable issues", Extensions.MomtComponentName);
			return monitorDefinition;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00061394 File Offset: 0x0005F594
		private static ResponderDefinition CreateMapiMTCheckMemoryRestartResponder(MonitorIdentity monitorIdentity, ServiceHealthStatus targetState)
		{
			string targetResource = string.Format("{0}_{1}_{2}", "MSExchangeRPC", "MSExchangeRpcProxyAppPool", targetState);
			ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity("Restart", targetResource);
			string name = responderIdentity.Name;
			string alertMask = monitorIdentity.GetAlertMask();
			string serviceName = responderIdentity.ServiceName;
			return MapiMTCheckMemoryRestartResponder.CreateDefinition(name, alertMask, targetState, 15, 120, 0, false, DumpMode.FullDump, null, 15.0, 0, serviceName, null, true, true, "Dag").Apply(responderIdentity);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00061403 File Offset: 0x0005F603
		protected override ProbeIdentity GetDeepTest()
		{
			return OutlookConnectivity.DeepTest;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000614F4 File Offset: 0x0005F6F4
		protected override void AddDeepTest(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier)
		{
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, baseProbeIdentity, Strings.NoBackendMonitoringAccountsAvailable))
			{
				int numberOfResources = databases.Count<MailboxDatabaseInfo>();
				base.AddForEachDatabaseForScheduledAndOnDemandExecution(databases, baseProbeIdentity, (ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo) => OutlookDiscovery.CreateProbe<LocalProbe.DeepTest>(probeIdentity).ConfigureDeepTest(numberOfResources).SuppressOnFreshBootUntilServiceIsStarted("MSExchangeRPC").ApplyModifier(probeModifier, dbInfo).ConfigureAuthenticationForBackendProbe(dbInfo, this.UseServerAuthforBackEndProbes));
				base.CreateRelatedWorkItems<ProbeIdentity>(baseProbeIdentity, delegate(ProbeIdentity probeIdentity)
				{
					base.AddMonitorAndResponders(base.CreatePercentSuccessMonitor(probeIdentity, Extensions.ProtocolDeepTestAvailabilityThreshold, (int)(Extensions.ProtocolDeepTestFailureDetectionTime.TotalSeconds / (double)Extensions.ProtocolDeepTestProbeIntervalInSeconds * (100.0 - Extensions.ProtocolDeepTestAvailabilityThreshold) / 100.0 / 2.0), Extensions.ProtocolDeepTestFailureDetectionTime, Extensions.ProtocolDeepTestMonitorRecurrenceInterval).ConfigureMonitorStateTransitions(new MonitorStateTransition[]
					{
						new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, TimeSpan.FromMinutes(0.0))
					}));
				});
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00061580 File Offset: 0x0005F780
		private void AddServiceTest()
		{
			base.CreateRelatedWorkItems<ProbeIdentity>(ProbeIdentity.Create(ExchangeComponent.OutlookProtocol, ProbeType.Service, null, "MSExchangeRPC"), delegate(ProbeIdentity probeIdentity)
			{
				base.AddWorkDefinition<ProbeDefinition>(OutlookDiscovery.CreateProbe<GenericServiceProbe>(probeIdentity).ConfigureSelfTest());
			});
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00061610 File Offset: 0x0005F810
		protected override void AddSelfTest(IEnumerable<MailboxDatabaseInfo> databases)
		{
			ProbeIdentity rpcSelfTest = OutlookConnectivity.RpcSelfTest;
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, rpcSelfTest, Strings.NoBackendMonitoringAccountsAvailable))
			{
				MailboxDatabaseInfo resource = (from db in databases
				orderby db.MonitoringAccount
				select db).First<MailboxDatabaseInfo>();
				base.CreateRelatedWorkItems<ProbeIdentity, MailboxDatabaseInfo>(rpcSelfTest, resource, delegate(ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo)
				{
					base.AddWorkDefinition<ProbeDefinition>(OutlookDiscovery.CreateProbe<LocalProbe.SelfTest>(probeIdentity).ConfigureSelfTest().TargetPrimaryMailbox(dbInfo).ConfigureAuthenticationForBackendProbe(dbInfo, this.UseServerAuthforBackEndProbes).SuppressOnFreshBootUntilServiceIsStarted("MSExchangeRPC"));
					base.AddMonitorAndResponders(this.CreateCheckMemoryRestartSnappyMonitor(probeIdentity).LimitRespondersTo(new ServiceHealthStatus[]
					{
						ServiceHealthStatus.Degraded,
						ServiceHealthStatus.Unrecoverable
					}));
				});
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00061677 File Offset: 0x0005F877
		protected override ProbeIdentity GetMailboxCTPTest()
		{
			return OutlookConnectivity.Ctp;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0006167E File Offset: 0x0005F87E
		protected override ProbeIdentity GetArchiveCTPTest()
		{
			return OutlookConnectivity.ArchiveCtp;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00061728 File Offset: 0x0005F928
		protected override void AddCtp(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier, Func<MonitorDefinition, MonitorDefinition> monitorModifier)
		{
			List<AutodiscoverRpcHttpSettings> rpcHttpServiceSettings = DirectoryAccessor.Instance.GetRpcHttpServiceSettings();
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, baseProbeIdentity, Strings.NoCafeMonitoringAccountsAvailable) && base.CheckRpcProxyAuthenticationSettingsAvailable(rpcHttpServiceSettings, baseProbeIdentity))
			{
				int numberOfResources = databases.Count<MailboxDatabaseInfo>();
				AutodiscoverRpcHttpSettings settings = rpcHttpServiceSettings.First<AutodiscoverRpcHttpSettings>();
				base.AddForEachDatabaseForScheduledAndOnDemandExecution(databases, baseProbeIdentity, (ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo) => OutlookDiscovery.CreateProbe<LocalProbe.Ctp>(probeIdentity).ConfigureCtp(numberOfResources).ConfigureCtpAuthenticationMethod(settings).ApplyModifier(probeModifier, dbInfo).AuthenticateAsUser(dbInfo));
				base.CreateRelatedWorkItems<ProbeIdentity>(baseProbeIdentity, delegate(ProbeIdentity probeIdentity)
				{
					this.AddMonitorAndResponders(this.CreateChunkingMonitor(probeIdentity, numberOfResources, Extensions.CtpFailureDetectionTime).LimitRespondersTo(new ServiceHealthStatus[]
					{
						ServiceHealthStatus.Unrecoverable
					}).ApplyModifier(monitorModifier));
				});
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000617C4 File Offset: 0x0005F9C4
		protected override ResponderDefinition[] CreateProtocolSpecificResponders(MonitorIdentity monitor)
		{
			return new ResponderDefinition[]
			{
				OutlookDiscovery.CreateEscalateResponder(monitor),
				OutlookRpcDiscovery.CreateMapiMTCheckMemoryRestartResponder(monitor, ServiceHealthStatus.Degraded)
			};
		}

		// Token: 0x04000AEA RID: 2794
		private const string RpcClientAccessServiceName = "MSExchangeRPC";

		// Token: 0x04000AEB RID: 2795
		private static bool didDiscoveryExecuteSuccessfully = false;

		// Token: 0x04000AEC RID: 2796
		private static MonitorStateTransition[] ProtocolMonitorStateTransitions = new MonitorStateTransition[]
		{
			new MonitorStateTransition(ServiceHealthStatus.Degraded, TimeSpan.Zero),
			new MonitorStateTransition(ServiceHealthStatus.Degraded1, TimeSpan.FromSeconds(10.0)),
			new MonitorStateTransition(ServiceHealthStatus.Degraded2, OutlookDiscovery.FullDumpTimeout.Add(TimeSpan.FromMinutes(1.0))),
			new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.FromMinutes(5.0)),
			new MonitorStateTransition(ServiceHealthStatus.Unhealthy1, TimeSpan.FromMinutes(10.0)),
			new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, TimeSpan.FromMinutes(20.0))
		};
	}
}
