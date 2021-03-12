using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020B RID: 523
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoteServerHealthChecker : IRemoteServerHealthChecker
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00039E40 File Offset: 0x00038040
		private RemoteServerHealthChecker()
		{
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00039E48 File Offset: 0x00038048
		public static RemoteServerHealthChecker Instance
		{
			get
			{
				return RemoteServerHealthChecker.instance;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00039E50 File Offset: 0x00038050
		public RemoteServerHealthState GetRemoteServerHealthState(ISyncWorkerData subscription)
		{
			if (subscription.SyncPhase == SyncPhase.Initial || subscription.SyncPhase == SyncPhase.Finalization)
			{
				return RemoteServerHealthState.Clean;
			}
			string incomingServerName = subscription.IncomingServerName;
			bool isPartnerProtocol = subscription.IsPartnerProtocol;
			return AggregationComponent.CalculateRemoteServerHealth(incomingServerName, isPartnerProtocol);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00039E88 File Offset: 0x00038088
		public bool IsRemoteServerSlow(SyncEngineState syncEngineState, ISyncWorkerData subscription, out RemoteServerTooSlowException exception)
		{
			if (!subscription.IsPartnerProtocol)
			{
				TimeSpan? timeSpan = null;
				if (syncEngineState.CloudProviderState.AverageSuccessfulRoundtripTime >= AggregationConfiguration.Instance.RemoteRoundtripTimeThreshold)
				{
					timeSpan = new TimeSpan?(syncEngineState.CloudProviderState.AverageSuccessfulRoundtripTime);
					syncEngineState.SyncLogSession.LogDebugging((TSLID)1360UL, SyncEngine.Tracer, "Avg Successful Roundtrip time {0} exceeds threshold {1} and marking the sync with transient failure.", new object[]
					{
						timeSpan,
						AggregationConfiguration.Instance.RemoteRoundtripTimeThreshold
					});
				}
				else if (syncEngineState.CloudProviderState.AverageUnsuccessfulRoundtripTime >= AggregationConfiguration.Instance.RemoteRoundtripTimeThreshold)
				{
					timeSpan = new TimeSpan?(syncEngineState.CloudProviderState.AverageUnsuccessfulRoundtripTime);
					syncEngineState.SyncLogSession.LogDebugging((TSLID)1361UL, SyncEngine.Tracer, "Avg Unsuccessful Roundtrip time {0} exceeds threshold {1} and marking the sync with transient failure.", new object[]
					{
						timeSpan,
						AggregationConfiguration.Instance.RemoteRoundtripTimeThreshold
					});
				}
				if (timeSpan != null)
				{
					exception = new RemoteServerTooSlowException(subscription.IncomingServerName, subscription.IncomingServerPort, timeSpan.Value, AggregationConfiguration.Instance.RemoteRoundtripTimeThreshold);
					return true;
				}
			}
			exception = null;
			return false;
		}

		// Token: 0x040009A8 RID: 2472
		private static readonly RemoteServerHealthChecker instance = new RemoteServerHealthChecker();
	}
}
