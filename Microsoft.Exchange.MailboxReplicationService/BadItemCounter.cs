using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000077 RID: 119
	internal class BadItemCounter
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001EA23 File Offset: 0x0001CC23
		internal BadItemCounter(bool categorize)
		{
			this.categorize = categorize;
			this.classifier = new BadItemClassifier();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001EA48 File Offset: 0x0001CC48
		internal bool Count(string categoryName)
		{
			if (!this.categorize)
			{
				return true;
			}
			if (categoryName == null)
			{
				categoryName = string.Empty;
			}
			BadItemCounter.BadItemCount badItemCount;
			if (!this.counters.TryGetValue(categoryName, out badItemCount))
			{
				int limit = this.classifier.GetLimit(categoryName);
				badItemCount = new BadItemCounter.BadItemCount(limit);
				this.counters.Add(categoryName, badItemCount);
			}
			return badItemCount.Count();
		}

		// Token: 0x0400021D RID: 541
		private readonly bool categorize;

		// Token: 0x0400021E RID: 542
		private readonly BadItemClassifier classifier;

		// Token: 0x0400021F RID: 543
		private Dictionary<string, BadItemCounter.BadItemCount> counters = new Dictionary<string, BadItemCounter.BadItemCount>();

		// Token: 0x02000078 RID: 120
		private class BadItemCount
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
			public BadItemCount(Unlimited<int> limit)
			{
				this.limit = limit;
				this.count = 0;
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x0001EABB File Offset: 0x0001CCBB
			public bool Count()
			{
				this.count++;
				return this.count > this.limit;
			}

			// Token: 0x04000220 RID: 544
			private Unlimited<int> limit;

			// Token: 0x04000221 RID: 545
			private int count;
		}
	}
}
