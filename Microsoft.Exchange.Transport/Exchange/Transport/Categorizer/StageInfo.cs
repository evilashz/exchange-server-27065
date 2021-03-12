using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C3 RID: 451
	internal sealed class StageInfo
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0005308D File Offset: 0x0005128D
		public StageInfo(StageHandler handler, LatencyComponent latencyComponent)
		{
			this.Handler = handler;
			this.LatencyComponent = latencyComponent;
		}

		// Token: 0x04000A6E RID: 2670
		public readonly StageHandler Handler;

		// Token: 0x04000A6F RID: 2671
		public readonly LatencyComponent LatencyComponent;
	}
}
