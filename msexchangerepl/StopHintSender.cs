using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000009 RID: 9
	internal sealed class StopHintSender : ChangePoller
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004C24 File Offset: 0x00002E24
		internal StopHintSender(ReplayService replayService) : base(true)
		{
			this.m_replayService = replayService;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C34 File Offset: 0x00002E34
		protected override void PollerThread()
		{
			try
			{
				while (!this.m_fShutdown)
				{
					this.m_replayService.RequestAdditionalTime(30000);
					this.m_shutdownEvent.WaitOne(15000, false);
				}
			}
			catch (InvalidOperationException arg)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<InvalidOperationException>((long)this.GetHashCode(), "Caught {0} in StopHintSender PollerThread", arg);
			}
		}

		// Token: 0x0400002B RID: 43
		private const int Timeout = 15000;

		// Token: 0x0400002C RID: 44
		private ReplayService m_replayService;
	}
}
