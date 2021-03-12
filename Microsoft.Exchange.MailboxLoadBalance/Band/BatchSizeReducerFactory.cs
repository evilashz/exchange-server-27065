using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000018 RID: 24
	internal class BatchSizeReducerFactory
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00005BA5 File Offset: 0x00003DA5
		protected BatchSizeReducerFactory()
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005BAD File Offset: 0x00003DAD
		public static BatchSizeReducerFactory Instance
		{
			get
			{
				return BatchSizeReducerFactory.HookableInstance.Value;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005BBC File Offset: 0x00003DBC
		public virtual IBatchSizeReducer GetBatchSizeReducer(ByteQuantifiedSize maximumSize, ByteQuantifiedSize totalSize, ILogger logger)
		{
			switch (LoadBalanceADSettings.Instance.Value.BatchBatchSizeReducer)
			{
			case BatchSizeReducerType.DropLargest:
				return this.CreateDropLargestBatchSizeReducer(maximumSize, logger);
			case BatchSizeReducerType.DropSmallest:
				return this.CreateDropSmallestBatchSizeReducer(maximumSize, logger);
			default:
				return this.GetFactorBasedBatchSizeReducer(maximumSize, totalSize, logger);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005C06 File Offset: 0x00003E06
		protected virtual IBatchSizeReducer CreateDropLargestBatchSizeReducer(ByteQuantifiedSize targetSize, ILogger logger)
		{
			return new DropLargestEntriesBatchSizeReducer(targetSize, logger);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005C0F File Offset: 0x00003E0F
		protected virtual IBatchSizeReducer CreateDropSmallestBatchSizeReducer(ByteQuantifiedSize targetSize, ILogger logger)
		{
			return new DropSmallestEntriesBatchSizeReducer(targetSize, logger);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005C18 File Offset: 0x00003E18
		protected virtual IBatchSizeReducer CreateFactorBasedReducer(ILogger logger, double weightReductionFactor)
		{
			return new FactorBasedBatchSizeReducer(weightReductionFactor, logger);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005C24 File Offset: 0x00003E24
		protected virtual IBatchSizeReducer GetFactorBasedBatchSizeReducer(ByteQuantifiedSize maximumSize, ByteQuantifiedSize totalSize, ILogger logger)
		{
			double weightReductionFactor = maximumSize.ToMB() / totalSize.ToMB();
			return this.CreateFactorBasedReducer(logger, weightReductionFactor);
		}

		// Token: 0x0400005C RID: 92
		protected static readonly Hookable<BatchSizeReducerFactory> HookableInstance = Hookable<BatchSizeReducerFactory>.Create(true, new BatchSizeReducerFactory());
	}
}
