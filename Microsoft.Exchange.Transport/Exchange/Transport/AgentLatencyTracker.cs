using System;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000142 RID: 322
	internal class AgentLatencyTracker : IDisposable
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x00036E90 File Offset: 0x00035090
		public AgentLatencyTracker(IMExSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.session = session;
			this.eventComponent = LatencyComponent.None;
			if (LatencyTracker.ComponentLatencyTrackingEnabled)
			{
				this.session.Dispatcher.OnAgentInvokeStart += new AgentInvokeStartHandler(this.AgentInvokeStartHandler);
				this.session.Dispatcher.OnAgentInvokeEnd += new AgentInvokeEndHandler(this.AgentInvokeEndHandler);
				this.session.Dispatcher.OnAgentInvokeScheduled += new AgentInvokeScheduledHandler(this.AgentInvokeScheduledHandler);
				this.session.Dispatcher.OnAgentInvokeResumed += new AgentInvokeResumedHandler(this.AgentInvokeResumedHandler);
				this.subscribed = true;
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00036F40 File Offset: 0x00035140
		public static void RegisterMExRuntime(LatencyAgentGroup agentGroup, MExRuntime runtime)
		{
			string[] array = new string[runtime.AgentCount];
			for (int i = 0; i < runtime.AgentCount; i++)
			{
				array[i] = LoggingFormatter.EncodeAgentName(runtime.GetAgentName(i));
			}
			LatencyTracker.SetAgentNames(agentGroup, array);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00036F80 File Offset: 0x00035180
		public void Dispose()
		{
			if (this.subscribed)
			{
				this.session.Dispatcher.OnAgentInvokeStart -= new AgentInvokeStartHandler(this.AgentInvokeStartHandler);
				this.session.Dispatcher.OnAgentInvokeEnd -= new AgentInvokeEndHandler(this.AgentInvokeEndHandler);
				this.subscribed = false;
			}
			this.latencyTracker = null;
			this.session = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00036FE8 File Offset: 0x000351E8
		public virtual void BeginTrackLatency(LatencyComponent eventComponent, LatencyTracker latencyTracker)
		{
			if (!this.subscribed)
			{
				return;
			}
			this.eventComponent = eventComponent;
			this.latencyTracker = latencyTracker;
			LatencyTracker.BeginTrackLatency(eventComponent, latencyTracker);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00037008 File Offset: 0x00035208
		public virtual void EndTrackLatency()
		{
			this.EndTrackLatency(true);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00037011 File Offset: 0x00035211
		public void EndTrackLatency(bool mailItemAvailable)
		{
			if (!this.subscribed)
			{
				return;
			}
			if (mailItemAvailable)
			{
				LatencyTracker.EndTrackLatency(this.eventComponent, this.latencyTracker);
			}
			this.eventComponent = LatencyComponent.None;
			this.latencyTracker = null;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003703F File Offset: 0x0003523F
		public void EndTrackingCurrentEvent()
		{
			this.EndTrackingCurrentEvent(this.latencyTracker);
			this.eventComponent = LatencyComponent.None;
			this.latencyTracker = null;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003705C File Offset: 0x0003525C
		public void EndTrackingCurrentEvent(LatencyTracker tracker)
		{
			if (!this.subscribed)
			{
				return;
			}
			if (this.eventComponent != LatencyComponent.None)
			{
				if (this.session.CurrentAgent != null)
				{
					LatencyTracker.EndTrackLatency(this.eventComponent, this.session.CurrentAgent.SequenceNumber, tracker);
				}
				LatencyTracker.EndTrackLatency(this.eventComponent, tracker);
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000370B1 File Offset: 0x000352B1
		private void AgentInvokeStartHandler(object source, IMExSession session)
		{
			if (this.eventComponent != LatencyComponent.None)
			{
				LatencyTracker.BeginTrackLatency(this.eventComponent, this.session.CurrentAgent.SequenceNumber, this.latencyTracker);
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000370DC File Offset: 0x000352DC
		private void AgentInvokeEndHandler(object dispatcher, IMExSession session)
		{
			if (this.eventComponent != LatencyComponent.None)
			{
				LatencyTracker.EndTrackLatency(this.eventComponent, this.session.CurrentAgent.SequenceNumber, this.latencyTracker);
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00037108 File Offset: 0x00035308
		private void AgentInvokeScheduledHandler(object dispatcher, IMExSession session)
		{
			if (this.eventComponent != LatencyComponent.None)
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MexRuntimeThreadpoolQueue, this.latencyTracker);
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003711F File Offset: 0x0003531F
		private void AgentInvokeResumedHandler(object dispatcher, IMExSession session)
		{
			if (this.eventComponent != LatencyComponent.None)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.MexRuntimeThreadpoolQueue, this.latencyTracker);
			}
		}

		// Token: 0x040006EC RID: 1772
		private IMExSession session;

		// Token: 0x040006ED RID: 1773
		private bool subscribed;

		// Token: 0x040006EE RID: 1774
		private LatencyTracker latencyTracker;

		// Token: 0x040006EF RID: 1775
		private LatencyComponent eventComponent;
	}
}
