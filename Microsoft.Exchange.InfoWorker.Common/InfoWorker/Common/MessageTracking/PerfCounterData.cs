using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000299 RID: 665
	internal class PerfCounterData
	{
		// Token: 0x04000C76 RID: 3190
		public static SlidingWindowResultCounter ResultCounter = new SlidingWindowResultCounter(10);
	}
}
