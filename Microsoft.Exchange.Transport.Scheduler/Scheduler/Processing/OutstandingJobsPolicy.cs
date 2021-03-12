using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000021 RID: 33
	internal class OutstandingJobsPolicy : IThrottlingPolicy
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000038F3 File Offset: 0x00001AF3
		public OutstandingJobsPolicy(int maxJobsAllowed, double allowedFactor)
		{
			this.allowedFactor = allowedFactor;
			this.maxJobsAllowed = maxJobsAllowed;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000390C File Offset: 0x00001B0C
		public PolicyDecision Evaluate(IMessageScope scope, UsageData usage, UsageData totalUsage)
		{
			if (totalUsage != null && usage != null)
			{
				long num = (long)(this.allowedFactor * (double)Math.Min(0, this.maxJobsAllowed - totalUsage.OutstandingJobs));
				if ((long)usage.OutstandingJobs >= num)
				{
					return PolicyDecision.Deny;
				}
			}
			return PolicyDecision.None;
		}

		// Token: 0x04000051 RID: 81
		private readonly int maxJobsAllowed;

		// Token: 0x04000052 RID: 82
		private readonly double allowedFactor;
	}
}
