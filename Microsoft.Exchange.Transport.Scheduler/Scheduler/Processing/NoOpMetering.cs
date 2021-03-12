using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200001E RID: 30
	internal class NoOpMetering : ISchedulerMetering
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000038AB File Offset: 0x00001AAB
		public UsageData GetTotalUsage()
		{
			return this.totalUsage;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000038B3 File Offset: 0x00001AB3
		public void RecordStart(IEnumerable<IMessageScope> scopes, long memorySize)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000038B5 File Offset: 0x00001AB5
		public void RecordEnd(IEnumerable<IMessageScope> scopes, TimeSpan duration)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038B7 File Offset: 0x00001AB7
		public bool TryGetUsage(IMessageScope scope, out UsageData data)
		{
			data = null;
			return false;
		}

		// Token: 0x04000050 RID: 80
		private readonly UsageData totalUsage = new UsageData(0, 0L, 0L);
	}
}
