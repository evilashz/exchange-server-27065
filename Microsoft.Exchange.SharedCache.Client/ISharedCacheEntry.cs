using System;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000003 RID: 3
	public interface ISharedCacheEntry
	{
		// Token: 0x06000003 RID: 3
		void FromByteArray(byte[] bytes);

		// Token: 0x06000004 RID: 4
		byte[] ToByteArray();
	}
}
