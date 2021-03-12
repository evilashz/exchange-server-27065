using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001C RID: 28
	internal abstract class DropEntriesBatchSizeReducer : IBatchSizeReducer
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005CAD File Offset: 0x00003EAD
		protected DropEntriesBatchSizeReducer(ByteQuantifiedSize targetSize, ILogger logger)
		{
			this.targetSize = targetSize;
			this.logger = logger;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005F98 File Offset: 0x00004198
		public IEnumerable<BandMailboxRebalanceData> ReduceBatchSize(IEnumerable<BandMailboxRebalanceData> results)
		{
			this.logger.LogInformation("Reducing batch size by dropping entries until size is at or below {0} ({1})", new object[]
			{
				this.targetSize,
				base.GetType().Name
			});
			IEnumerable<BandMailboxRebalanceData> sortedResults = this.SortResults(results);
			ByteQuantifiedSize currentSize = ByteQuantifiedSize.Zero;
			foreach (BandMailboxRebalanceData datum in sortedResults)
			{
				ByteQuantifiedSize datumSize = this.GetRebalanceDatumSize(datum);
				if (currentSize + datumSize > this.targetSize)
				{
					this.logger.LogVerbose("Current selected size is {0}, the next data entry would need {1} which would exceed the maximum. Skipping.", new object[]
					{
						currentSize,
						datumSize
					});
				}
				else
				{
					currentSize += datumSize;
					this.logger.LogVerbose("Selected {0}, total size is now {1}", new object[]
					{
						datumSize,
						currentSize
					});
					yield return datum;
				}
			}
			yield break;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005FE8 File Offset: 0x000041E8
		protected ByteQuantifiedSize GetRebalanceDatumSize(BandMailboxRebalanceData datum)
		{
			return (from metric in datum.RebalanceInformation.Metrics
			where metric.IsSize
			select metric).Aggregate(ByteQuantifiedSize.Zero, (ByteQuantifiedSize current, LoadMetric metric) => current + datum.RebalanceInformation.GetSizeMetric(metric));
		}

		// Token: 0x060000FD RID: 253
		protected abstract IEnumerable<BandMailboxRebalanceData> SortResults(IEnumerable<BandMailboxRebalanceData> results);

		// Token: 0x04000062 RID: 98
		private readonly ILogger logger;

		// Token: 0x04000063 RID: 99
		private readonly ByteQuantifiedSize targetSize;
	}
}
