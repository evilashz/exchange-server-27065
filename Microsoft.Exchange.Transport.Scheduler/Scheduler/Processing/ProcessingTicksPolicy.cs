using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000024 RID: 36
	internal class ProcessingTicksPolicy : IThrottlingPolicy
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004163 File Offset: 0x00002363
		public ProcessingTicksPolicy(long maximumTicksAllowed)
		{
			this.maximumTicksAllowed = maximumTicksAllowed;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004172 File Offset: 0x00002372
		public PolicyDecision Evaluate(IMessageScope scope, UsageData usage, UsageData totalUsage)
		{
			if (usage != null && usage.ProcessingTicks >= this.maximumTicksAllowed)
			{
				return PolicyDecision.Deny;
			}
			return PolicyDecision.None;
		}

		// Token: 0x04000067 RID: 103
		private readonly long maximumTicksAllowed;
	}
}
