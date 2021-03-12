using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001F RID: 31
	internal class FactorBasedBatchSizeReducer : IBatchSizeReducer
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00006088 File Offset: 0x00004288
		public FactorBasedBatchSizeReducer(double factor, ILogger logger)
		{
			AnchorUtil.ThrowOnNullArgument(logger, "logger");
			if (factor > 1.0 || factor < 0.0)
			{
				throw new ArgumentOutOfRangeException("factor", factor, "Factor must be between 0 and 1.");
			}
			this.logger = logger;
			this.factor = factor;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006394 File Offset: 0x00004594
		public IEnumerable<BandMailboxRebalanceData> ReduceBatchSize(IEnumerable<BandMailboxRebalanceData> results)
		{
			this.logger.Log(MigrationEventType.Information, "Reducing rebalance weights by a factor of {0}.", new object[]
			{
				this.factor
			});
			foreach (BandMailboxRebalanceData result in results)
			{
				BandMailboxRebalanceData reducedResult = new BandMailboxRebalanceData(result.SourceDatabase, result.TargetDatabase, new LoadMetricStorage())
				{
					RebalanceBatchName = result.RebalanceBatchName,
					ConstraintSetIdentity = result.ConstraintSetIdentity
				};
				foreach (LoadMetric metric in result.RebalanceInformation.Metrics)
				{
					reducedResult.RebalanceInformation[metric] = (long)((double)result.RebalanceInformation[metric] * this.factor);
				}
				yield return reducedResult;
			}
			yield break;
		}

		// Token: 0x04000065 RID: 101
		private readonly double factor;

		// Token: 0x04000066 RID: 102
		private readonly ILogger logger;
	}
}
