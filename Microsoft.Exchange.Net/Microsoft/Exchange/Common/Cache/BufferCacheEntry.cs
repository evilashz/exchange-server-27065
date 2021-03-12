using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000670 RID: 1648
	internal class BufferCacheEntry
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x000367CD File Offset: 0x000349CD
		public BufferCacheEntry(byte[] array, bool ownedByBufferCache)
		{
			this.Buffer = array;
			this.OwnedByBufferCache = ownedByBufferCache;
		}

		// Token: 0x04001E0B RID: 7691
		public readonly byte[] Buffer;

		// Token: 0x04001E0C RID: 7692
		public readonly bool OwnedByBufferCache;
	}
}
