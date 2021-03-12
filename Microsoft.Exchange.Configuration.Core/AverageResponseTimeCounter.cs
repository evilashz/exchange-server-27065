using System;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200001E RID: 30
	public class AverageResponseTimeCounter
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005310 File Offset: 0x00003510
		internal long GetAverageResponseTime(long requestTime, long lastValue)
		{
			try
			{
				this.accumulatedRequestSec += requestTime;
			}
			catch
			{
				this.accumulatedRequestSec = lastValue;
				this.accumulatedRequestCounts = 1;
				return 0L;
			}
			this.accumulatedRequestCounts++;
			return this.accumulatedRequestSec / (long)this.accumulatedRequestCounts;
		}

		// Token: 0x04000082 RID: 130
		private int accumulatedRequestCounts;

		// Token: 0x04000083 RID: 131
		private long accumulatedRequestSec;
	}
}
