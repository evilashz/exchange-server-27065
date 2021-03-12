using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200068F RID: 1679
	internal class FixedTimeQuota : FixedTimeSumBase
	{
		// Token: 0x06001E8D RID: 7821 RVA: 0x00038F89 File Offset: 0x00037189
		public FixedTimeQuota(ushort windowBucketLength, ushort numberOfBuckets, uint limit) : base((uint)windowBucketLength, numberOfBuckets, new uint?(limit))
		{
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00038F99 File Offset: 0x00037199
		internal new bool TryAdd(uint addend)
		{
			return base.TryAdd(addend);
		}
	}
}
