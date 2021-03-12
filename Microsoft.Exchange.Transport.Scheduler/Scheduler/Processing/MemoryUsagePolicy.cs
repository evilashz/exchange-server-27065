using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200001C RID: 28
	internal class MemoryUsagePolicy : IThrottlingPolicy
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000371E File Offset: 0x0000191E
		public MemoryUsagePolicy(long totalMemory, double allowedFactor)
		{
			this.totalMemory = totalMemory;
			this.allowedFactor = allowedFactor;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003734 File Offset: 0x00001934
		public PolicyDecision Evaluate(IMessageScope scope, UsageData usage, UsageData totalUsage)
		{
			if (totalUsage != null && usage != null)
			{
				long num = (long)(this.allowedFactor * (double)Math.Min(0L, this.totalMemory - totalUsage.MemoryUsed));
				if (usage.MemoryUsed >= num)
				{
					return PolicyDecision.Deny;
				}
			}
			return PolicyDecision.None;
		}

		// Token: 0x04000049 RID: 73
		private readonly long totalMemory;

		// Token: 0x0400004A RID: 74
		private readonly double allowedFactor;
	}
}
