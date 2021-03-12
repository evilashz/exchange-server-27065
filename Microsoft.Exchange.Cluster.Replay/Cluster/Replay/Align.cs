using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000FE RID: 254
	internal static class Align
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x0002F540 File Offset: 0x0002D740
		internal static long RoundUp(long val, long quantum)
		{
			long num = val % quantum;
			if (num > 0L)
			{
				return val + (quantum - num);
			}
			return val;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002F560 File Offset: 0x0002D760
		internal static uint RoundUp(uint val, uint quantum)
		{
			uint num = val % quantum;
			if (num > 0U)
			{
				return val + (quantum - num);
			}
			return val;
		}
	}
}
