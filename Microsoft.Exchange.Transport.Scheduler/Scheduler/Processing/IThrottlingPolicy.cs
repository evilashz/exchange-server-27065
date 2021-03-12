using System;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000017 RID: 23
	internal interface IThrottlingPolicy
	{
		// Token: 0x0600005A RID: 90
		PolicyDecision Evaluate(IMessageScope scope, UsageData usage, UsageData totalUsage);
	}
}
