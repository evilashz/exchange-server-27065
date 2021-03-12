using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Mapi
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BufferPools
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public static byte[] GetBuffer(int exactBufferSize, out BufferPool bufferPool)
		{
			bufferPool = null;
			if (exactBufferSize <= 0)
			{
				return null;
			}
			bufferPool = BufferPools.GetBufferPoolOfExactLength(exactBufferSize);
			if (bufferPool == null)
			{
				return new byte[exactBufferSize];
			}
			return bufferPool.Acquire();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AD0 File Offset: 0x00000CD0
		private static BufferPool GetBufferPoolOfExactLength(int exactBufferSize)
		{
			BufferPoolCollection.BufferSize bufferSize;
			if (BufferPools.bufferPoolCollection.TryMatchBufferSize(exactBufferSize, out bufferSize))
			{
				BufferPool bufferPool = BufferPools.bufferPoolCollection.Acquire(bufferSize);
				if (bufferPool.BufferSize == exactBufferSize)
				{
					return bufferPool;
				}
			}
			return null;
		}

		// Token: 0x04000023 RID: 35
		private static readonly BufferPoolCollection bufferPoolCollection = new BufferPoolCollection(false);
	}
}
