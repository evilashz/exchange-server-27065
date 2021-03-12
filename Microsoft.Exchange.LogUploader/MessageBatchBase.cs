using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001F RID: 31
	internal abstract class MessageBatchBase : LogDataBatch
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00008F4C File Offset: 0x0000714C
		public MessageBatchBase(int batchSizeInBytes, long beginOffSet, string fullLogName, string logPrefix) : base(batchSizeInBytes, beginOffSet, fullLogName, logPrefix)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001AF RID: 431
		// (set) Token: 0x060001B0 RID: 432
		internal abstract int MessageBatchFlushInterval { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B2 RID: 434
		internal abstract bool Flushed { get; set; }

		// Token: 0x060001B3 RID: 435
		internal abstract bool ReadyToFlush(DateTime newestLogLineTS);

		// Token: 0x060001B4 RID: 436
		internal abstract bool ContainsMessage(ParsedReadOnlyRow parsedRow);

		// Token: 0x060001B5 RID: 437
		internal abstract bool ReachedDalOptimizationLimit();
	}
}
