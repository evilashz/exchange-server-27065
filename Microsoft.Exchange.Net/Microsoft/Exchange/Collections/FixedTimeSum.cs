using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000690 RID: 1680
	internal class FixedTimeSum : FixedTimeSumBase
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x00038FA4 File Offset: 0x000371A4
		public FixedTimeSum(ushort windowBucketLength, ushort numberOfBuckets) : base((uint)windowBucketLength, numberOfBuckets, null)
		{
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00038FC2 File Offset: 0x000371C2
		internal void Add(uint value)
		{
			base.TryAdd(value);
		}
	}
}
