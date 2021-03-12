using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200000F RID: 15
	internal interface ISchedulerMetering
	{
		// Token: 0x06000039 RID: 57
		UsageData GetTotalUsage();

		// Token: 0x0600003A RID: 58
		void RecordStart(IEnumerable<IMessageScope> scopes, long memorySize);

		// Token: 0x0600003B RID: 59
		void RecordEnd(IEnumerable<IMessageScope> scopes, TimeSpan duration);

		// Token: 0x0600003C RID: 60
		bool TryGetUsage(IMessageScope scope, out UsageData data);
	}
}
