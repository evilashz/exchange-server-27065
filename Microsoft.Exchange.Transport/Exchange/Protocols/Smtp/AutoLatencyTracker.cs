using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B1 RID: 1201
	internal class AutoLatencyTracker : DisposeTrackableBase
	{
		// Token: 0x06003648 RID: 13896 RVA: 0x000DE774 File Offset: 0x000DC974
		public AutoLatencyTracker(AgentLatencyTracker agentLatencyTracker, LatencyComponent eventComponent, LatencyTracker tmiLatencyTracker)
		{
			ArgumentValidator.ThrowIfNull("agentLatencyTracker", agentLatencyTracker);
			if (eventComponent != LatencyComponent.None && tmiLatencyTracker != null)
			{
				this.agentLatencyTracker = agentLatencyTracker;
				this.agentLatencyTracker.BeginTrackLatency(eventComponent, tmiLatencyTracker);
			}
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000DE7A1 File Offset: 0x000DC9A1
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AutoLatencyTracker>(this);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000DE7A9 File Offset: 0x000DC9A9
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.agentLatencyTracker != null)
			{
				this.agentLatencyTracker.EndTrackLatency();
			}
		}

		// Token: 0x04001BC6 RID: 7110
		private readonly AgentLatencyTracker agentLatencyTracker;
	}
}
