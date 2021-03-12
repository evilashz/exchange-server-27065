using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200037F RID: 895
	internal sealed class ShadowRedundancyHeartbeatHelper
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x000965A0 File Offset: 0x000947A0
		public ShadowRedundancyHeartbeatHelper(NextHopSolutionKey key, IShadowRedundancyConfigurationSource configuration, ShadowRedundancyEventLogger eventLogger)
		{
			if (key.NextHopType != NextHopType.ShadowRedundancy)
			{
				throw new ArgumentException("key is not for Shadow queue");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (eventLogger == null)
			{
				throw new ArgumentNullException("eventLogger");
			}
			this.key = key;
			this.configuration = configuration;
			this.eventLogger = eventLogger;
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00096618 File Offset: 0x00094818
		public bool HasHeartbeatFailure
		{
			get
			{
				return this.heartbeatRetryCount != 0;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x00096626 File Offset: 0x00094826
		public DateTime LastHeartbeatTime
		{
			get
			{
				return this.lastHeartbeatTime;
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00096630 File Offset: 0x00094830
		public void ScheduleImmediateHeartbeat()
		{
			lock (this.syncHeartbeat)
			{
				this.heartbeatImmediately = true;
				this.CreateHeartbeatIfNecessary();
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x00096678 File Offset: 0x00094878
		public void CreateHeartbeatIfNecessary()
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Entering ShadowRedundancyHeartbeatHelper.CreateHeartbeatIfNecessary() for queue {0}", this.key);
			if (this.heartbeatTmi != null && DateTime.UtcNow - this.lastHeartbeatTime >= this.configuration.MaxPendingHeartbeatInterval)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, TimeSpan>((long)this.GetHashCode(), "Forced reset of heartbeat state for queue {0} after {1} has elapsed.", this.key, this.configuration.MaxPendingHeartbeatInterval);
				this.eventLogger.LogHeartbeatForcedReset(this.key.NextHopDomain, this.configuration.MaxPendingHeartbeatInterval);
				this.ResetHeartbeatState(this.lastHeartbeatTime);
			}
			else if (this.heartbeatTmi != null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Bailing from ShadowRedundancyHeartbeatHelper.CreateHeartbeatIfNecessary() for queue {0} because we have a TMI already", this.key);
				return;
			}
			TransportMailItem transportMailItem = null;
			lock (this.syncHeartbeat)
			{
				if (this.heartbeatTmi == null)
				{
					bool flag2;
					bool flag3;
					this.CanSendHeartbeat(out flag2, out flag3);
					if (!flag2)
					{
						return;
					}
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Creating heartbeat mailitem for queue {0}", this.key);
				this.heartbeatRetryCount = 0;
				transportMailItem = TransportMailItem.NewMailItem(LatencyComponent.Heartbeat);
				this.heartbeatTmi = transportMailItem;
				this.heartbeatImmediately = false;
			}
			transportMailItem.From = RoutingAddress.NullReversePath;
			transportMailItem.Recipients.Add(RoutingAddress.NullReversePath.ToString());
			NextHopSolutionKey nextHop = new NextHopSolutionKey(NextHopType.Heartbeat, this.key.NextHopDomain, this.key.NextHopConnector);
			transportMailItem.Recipients[0].NextHop = nextHop;
			transportMailItem.UpdateNextHopSolutionTable(nextHop, transportMailItem.Recipients[0]);
			transportMailItem.CommitLazy();
			Components.RemoteDeliveryComponent.QueueMessageForNextHop(transportMailItem);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat initiated for shadow queue '{0}'", this.key);
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x00096878 File Offset: 0x00094A78
		public void UpdateHeartbeat(DateTime heartbeatTime, NextHopSolutionKey key, bool successfulHeartbeat)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "UpdateHeartbeat called for queue {0} heartbeatTime={1} key={2}, success={3}", new object[]
			{
				this.key,
				heartbeatTime,
				key,
				successfulHeartbeat
			});
			if (successfulHeartbeat || Components.RemoteDeliveryComponent.IsPaused)
			{
				if (successfulHeartbeat)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat state cleared for shadow queue '{0}' due to successful heartbeat", this.key);
				}
				else
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat state cleared for shadow queue '{0}' due to Remote Delivery pause", this.key);
				}
				lock (this.syncHeartbeat)
				{
					this.ResetHeartbeatState(heartbeatTime);
					return;
				}
			}
			if (key.NextHopType == NextHopType.Heartbeat && key.NextHopConnector == this.key.NextHopConnector)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceError<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat could not be detected for shadow queue '{0}'", this.key);
				lock (this.syncHeartbeat)
				{
					this.heartbeatRetryCount++;
					this.lastHeartbeatTime = heartbeatTime;
					this.heartbeatInProgress = false;
					this.StopHeartbeatTimer();
				}
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000969E8 File Offset: 0x00094BE8
		public void EvaluateHeartbeatAttempt(out bool sendHeartbeat, out bool abortHeartbeat)
		{
			lock (this.syncHeartbeat)
			{
				this.CanSendHeartbeat(out sendHeartbeat, out abortHeartbeat);
				if (sendHeartbeat)
				{
					this.lastHeartbeatTime = DateTime.UtcNow;
					this.heartbeatInProgress = true;
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "EvaluateHeartbeatAttempt: setting heartbeat in progress for queue {0}", this.key);
					this.StartHeartbeatTimer();
				}
				else if (abortHeartbeat)
				{
					this.ResetHeartbeatState(DateTime.UtcNow);
				}
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00096A74 File Offset: 0x00094C74
		public bool CanResubmit()
		{
			return this.heartbeatRetryCount >= this.configuration.HeartbeatRetryCount;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00096A8C File Offset: 0x00094C8C
		public void ResetHeartbeat()
		{
			lock (this.syncHeartbeat)
			{
				this.ResetHeartbeatState(DateTime.UtcNow);
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00096AD4 File Offset: 0x00094CD4
		public void NotifyConfigUpdated(IShadowRedundancyConfigurationSource oldConfiguration)
		{
			if (oldConfiguration == null)
			{
				throw new ArgumentNullException("oldConfiguration");
			}
			lock (this.syncHeartbeat)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "NotifyConfigUpdated() called for queue {0}", this.key);
				this.heartbeatRetryCount = 0;
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x00096B40 File Offset: 0x00094D40
		private void CanSendHeartbeat(out bool sendHeartbeat, out bool abortHeartbeat)
		{
			abortHeartbeat = Components.RemoteDeliveryComponent.IsPaused;
			sendHeartbeat = (!abortHeartbeat && !this.heartbeatInProgress && (DateTime.UtcNow - this.lastHeartbeatTime >= this.configuration.HeartbeatFrequency || this.heartbeatImmediately));
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "CanSendHeartbeat() called for queue {0}: Send={1} Abort={2} RemoteDelivery.IsPaused={3} heartbeatInProgress={4} lastHeartbeatTime={5} interval={6} heartbeatImmediately={7}", new object[]
			{
				this.key,
				sendHeartbeat,
				abortHeartbeat,
				Components.RemoteDeliveryComponent.IsPaused,
				this.heartbeatInProgress,
				this.lastHeartbeatTime,
				this.configuration.HeartbeatFrequency,
				this.heartbeatImmediately
			});
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x00096C24 File Offset: 0x00094E24
		private void ResetHeartbeatState(DateTime heartbeatTime)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, DateTime>((long)this.GetHashCode(), "ResetHeartbeatState: resetting heartbeat state for queue {0} at {1}", this.key, heartbeatTime);
			this.heartbeatRetryCount = 0;
			this.heartbeatTmi = null;
			this.lastHeartbeatTime = heartbeatTime;
			this.heartbeatInProgress = false;
			this.StopHeartbeatTimer();
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00096C70 File Offset: 0x00094E70
		private void StartHeartbeatTimer()
		{
			this.heartbeatLatencyTimer = ShadowRedundancyManager.PerfCounters.ShadowHeartbeatLatencyCounter(this.key.NextHopDomain);
			this.heartbeatLatencyTimer.Start();
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00096CA6 File Offset: 0x00094EA6
		private void StopHeartbeatTimer()
		{
			if (this.heartbeatLatencyTimer != null)
			{
				this.heartbeatLatencyTimer.Stop();
				this.heartbeatLatencyTimer = null;
				return;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "StopHeartbeatTimer called with null latency timer");
		}

		// Token: 0x040013D6 RID: 5078
		private readonly IShadowRedundancyConfigurationSource configuration;

		// Token: 0x040013D7 RID: 5079
		private readonly NextHopSolutionKey key;

		// Token: 0x040013D8 RID: 5080
		private readonly ShadowRedundancyEventLogger eventLogger;

		// Token: 0x040013D9 RID: 5081
		private DateTime lastHeartbeatTime = DateTime.UtcNow;

		// Token: 0x040013DA RID: 5082
		private int heartbeatRetryCount;

		// Token: 0x040013DB RID: 5083
		private TransportMailItem heartbeatTmi;

		// Token: 0x040013DC RID: 5084
		private bool heartbeatInProgress;

		// Token: 0x040013DD RID: 5085
		private bool heartbeatImmediately;

		// Token: 0x040013DE RID: 5086
		private ITimerCounter heartbeatLatencyTimer;

		// Token: 0x040013DF RID: 5087
		private object syncHeartbeat = new object();
	}
}
