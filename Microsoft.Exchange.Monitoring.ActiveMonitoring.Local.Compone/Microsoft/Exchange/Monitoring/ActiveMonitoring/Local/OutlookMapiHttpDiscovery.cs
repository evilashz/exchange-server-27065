using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000206 RID: 518
	public sealed class OutlookMapiHttpDiscovery : OutlookDiscovery
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x00060E99 File Offset: 0x0005F099
		internal override MonitorStateTransition[] GetProtocolMonitorStateTransitions()
		{
			return OutlookMapiHttpDiscovery.ProtocolMonitorStateTransitions;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		protected override bool DidDiscoveryExecuteSuccessfully()
		{
			return OutlookMapiHttpDiscovery.didDiscoveryExecuteSuccessfully;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00060EA7 File Offset: 0x0005F0A7
		protected override void SetSuccessfulDiscoveryExecutionStatus()
		{
			OutlookMapiHttpDiscovery.didDiscoveryExecuteSuccessfully = true;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00060EB0 File Offset: 0x0005F0B0
		private static ResponderDefinition CreateCafeMapiHttpProxyRestartResponder(MonitorIdentity monitorIdentity)
		{
			string appPool = CafeProtocols.Get(HttpProtocol.Mapi).AppPool;
			return OutlookDiscovery.CreateAppPoolRestartResponder(monitorIdentity, appPool, ServiceHealthStatus.Degraded);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00060ED1 File Offset: 0x0005F0D1
		protected override ProbeIdentity GetDeepTest()
		{
			return OutlookConnectivity.MapiHttpDeepTest;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00060FB8 File Offset: 0x0005F1B8
		protected override void AddDeepTest(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier)
		{
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, baseProbeIdentity, Strings.NoBackendMonitoringAccountsAvailable))
			{
				int numberOfResources = databases.Count<MailboxDatabaseInfo>();
				base.AddForEachDatabaseForScheduledAndOnDemandExecution(databases, baseProbeIdentity, (ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo) => OutlookDiscovery.CreateProbe<LocalProbe.MapiHttpDeepTest>(probeIdentity).ConfigureDeepTest(numberOfResources).SetSecondaryEndpointAsPersonalizedServerName(dbInfo).SuppressOnFreshBootUntilServiceIsStarted("W3SVC").ApplyModifier(probeModifier, dbInfo).ConfigureAuthenticationForBackendProbe(dbInfo, this.UseServerAuthforBackEndProbes));
				base.CreateRelatedWorkItems<ProbeIdentity>(baseProbeIdentity, delegate(ProbeIdentity probeIdentity)
				{
					base.AddMonitorAndResponders(base.CreatePercentSuccessMonitor(probeIdentity, Extensions.ProtocolDeepTestAvailabilityThreshold, (int)(Extensions.ProtocolDeepTestFailureDetectionTime.TotalSeconds / (double)Extensions.ProtocolDeepTestProbeIntervalInSeconds * (100.0 - Extensions.ProtocolDeepTestAvailabilityThreshold) / 100.0 / 2.0), Extensions.ProtocolDeepTestFailureDetectionTime, Extensions.ProtocolDeepTestMonitorRecurrenceInterval).LimitRespondersTo(new ServiceHealthStatus[]
					{
						ServiceHealthStatus.Degraded1,
						ServiceHealthStatus.Unrecoverable
					}));
				});
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000610A0 File Offset: 0x0005F2A0
		protected override void AddSelfTest(IEnumerable<MailboxDatabaseInfo> databases)
		{
			ProbeIdentity mapiHttpSelfTest = OutlookConnectivity.MapiHttpSelfTest;
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, mapiHttpSelfTest, Strings.NoBackendMonitoringAccountsAvailable))
			{
				MailboxDatabaseInfo resource = (from db in databases
				orderby db.MonitoringAccount
				select db).First<MailboxDatabaseInfo>();
				base.CreateRelatedWorkItems<ProbeIdentity, MailboxDatabaseInfo>(mapiHttpSelfTest, resource, delegate(ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo)
				{
					ProbeDefinition definition = base.AddWorkDefinition<ProbeDefinition>(OutlookDiscovery.CreateProbe<LocalProbe.MapiHttpSelfTest>(probeIdentity).ConfigureSelfTest().TargetPrimaryMailbox(dbInfo).ConfigureAuthenticationForBackendProbe(dbInfo, this.UseServerAuthforBackEndProbes).SetSecondaryEndpointAsPersonalizedServerName(dbInfo).SuppressOnFreshBootUntilServiceIsStarted("W3SVC"));
					base.AddMonitorAndResponders(base.CreateSnappyMonitor(definition).LimitRespondersTo(new ServiceHealthStatus[]
					{
						ServiceHealthStatus.Degraded1,
						ServiceHealthStatus.Unrecoverable
					}));
				});
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00061107 File Offset: 0x0005F307
		protected override ProbeIdentity GetMailboxCTPTest()
		{
			return OutlookConnectivity.MapiHttpCtp;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0006110E File Offset: 0x0005F30E
		protected override ProbeIdentity GetArchiveCTPTest()
		{
			return OutlookConnectivity.MapiHttpArchiveCtp;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000611B4 File Offset: 0x0005F3B4
		protected override void AddCtp(IEnumerable<MailboxDatabaseInfo> databases, ProbeIdentity baseProbeIdentity, Func<ProbeDefinition, MailboxDatabaseInfo, ProbeDefinition> probeModifier, Func<MonitorDefinition, MonitorDefinition> monitorModifier)
		{
			if (base.CheckMonitoringAccountsAvailable<MailboxDatabaseInfo>(databases, baseProbeIdentity, Strings.NoCafeMonitoringAccountsAvailable))
			{
				int numberOfResources = databases.Count<MailboxDatabaseInfo>();
				base.AddForEachDatabaseForScheduledAndOnDemandExecution(databases, baseProbeIdentity, (ProbeIdentity probeIdentity, MailboxDatabaseInfo dbInfo) => OutlookDiscovery.CreateProbe<LocalProbe.MapiHttpCtp>(probeIdentity).ConfigureCtp(numberOfResources).ForceSslCtpAuthenticationMethod().ApplyModifier(probeModifier, dbInfo).AuthenticateAsUser(dbInfo));
				base.CreateRelatedWorkItems<ProbeIdentity>(baseProbeIdentity, delegate(ProbeIdentity probeIdentity)
				{
					this.AddMonitorAndResponders(this.CreateChunkingMonitor(probeIdentity, numberOfResources, Extensions.CtpFailureDetectionTime).LimitRespondersTo(new ServiceHealthStatus[]
					{
						ServiceHealthStatus.Degraded,
						ServiceHealthStatus.Unrecoverable
					}).ApplyModifier(monitorModifier));
				});
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00061230 File Offset: 0x0005F430
		protected override ResponderDefinition[] CreateProtocolSpecificResponders(MonitorIdentity monitor)
		{
			return new ResponderDefinition[]
			{
				OutlookDiscovery.CreateEscalateResponder(monitor),
				OutlookDiscovery.CreateAppPoolRestartResponder(monitor, "MSExchangeMapiMailboxAppPool", ServiceHealthStatus.Degraded1),
				OutlookMapiHttpDiscovery.CreateCafeMapiHttpProxyRestartResponder(monitor)
			};
		}

		// Token: 0x04000AE5 RID: 2789
		private const string MapiHttpBackendAppPoolName = "MSExchangeMapiMailboxAppPool";

		// Token: 0x04000AE6 RID: 2790
		private const string IisServiceName = "W3SVC";

		// Token: 0x04000AE7 RID: 2791
		private static bool didDiscoveryExecuteSuccessfully = false;

		// Token: 0x04000AE8 RID: 2792
		private static MonitorStateTransition[] ProtocolMonitorStateTransitions = new MonitorStateTransition[]
		{
			new MonitorStateTransition(ServiceHealthStatus.Degraded, TimeSpan.Zero),
			new MonitorStateTransition(ServiceHealthStatus.Degraded1, TimeSpan.Zero),
			new MonitorStateTransition(ServiceHealthStatus.Degraded2, OutlookDiscovery.FullDumpTimeout.Add(TimeSpan.FromMinutes(1.0))),
			new MonitorStateTransition(ServiceHealthStatus.Unhealthy1, TimeSpan.FromMinutes(12.0)),
			new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, TimeSpan.FromMinutes(15.0))
		};
	}
}
