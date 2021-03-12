using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E3 RID: 483
	internal class AsyncBufferPools
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x00026E10 File Offset: 0x00026210
		private AsyncBufferPools(int bufferSize)
		{
			this.privateBufferPool = new AsyncBufferPool(bufferSize);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00027014 File Offset: 0x00026414
		private byte[] Aquire()
		{
			return this.privateBufferPool.Acquire();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002702C File Offset: 0x0002642C
		private void Release(byte[] buffer)
		{
			this.privateBufferPool.Release(buffer);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0001B874 File Offset: 0x0001AC74
		private int BufferSize
		{
			get
			{
				return this.privateBufferPool.BufferSize;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00027048 File Offset: 0x00026448
		public static byte[] GetBuffer(int requestedBufferSize)
		{
			if (requestedBufferSize < 0)
			{
				throw new FailRpcException("Buffer size cannot be negative", -2147024809);
			}
			if (requestedBufferSize == 0)
			{
				return AsyncBufferPools.EmptyBuffer;
			}
			int num = 0;
			if (0 < AsyncBufferPools.BufferPools.Length)
			{
				do
				{
					AsyncBufferPools asyncBufferPools = AsyncBufferPools.BufferPools[num];
					if (requestedBufferSize <= asyncBufferPools.privateBufferPool.BufferSize)
					{
						goto IL_4D;
					}
					num++;
				}
				while (num < AsyncBufferPools.BufferPools.Length);
				goto IL_5F;
				IL_4D:
				return AsyncBufferPools.BufferPools[num].privateBufferPool.Acquire();
			}
			IL_5F:
			throw new FailRpcException("Buffer size too large", -2147024809);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000270C8 File Offset: 0x000264C8
		public static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer != null && buffer.Length != 0)
			{
				int num = 0;
				if (0 < AsyncBufferPools.BufferPools.Length)
				{
					do
					{
						int bufferSize = AsyncBufferPools.BufferPools[num].privateBufferPool.BufferSize;
						if (buffer.Length == bufferSize)
						{
							goto IL_38;
						}
						num++;
					}
					while (num < AsyncBufferPools.BufferPools.Length);
					goto IL_4C;
					IL_38:
					AsyncBufferPools.BufferPools[num].privateBufferPool.Release(buffer);
					return;
				}
				IL_4C:
				throw new ArgumentException("buffer being released doesn't match any buffer pool length");
			}
		}

		// Token: 0x04000B9F RID: 2975
		private static readonly byte[] EmptyBuffer = new byte[0];

		// Token: 0x04000BA0 RID: 2976
		private static readonly AsyncBufferPools[] BufferPools = new AsyncBufferPools[]
		{
			new AsyncBufferPools(EmsmdbConstants.ExtendedBufferHeaderSize + 1024),
			new AsyncBufferPools(EmsmdbConstants.MaxExtendedAuxBufferSize),
			new AsyncBufferPools(EmsmdbConstants.MaxExtendedRopBufferSize),
			new AsyncBufferPools(EmsmdbConstants.MaxOutlookChainedExtendedRopBufferSize),
			new AsyncBufferPools(EmsmdbConstants.MaxMapiHttpChainedOutlookPayloadSize),
			new AsyncBufferPools(EmsmdbConstants.MaxChainedExtendedRopBufferSize),
			new AsyncBufferPools(EmsmdbConstants.MaxMapiHttpChainedPayloadSize)
		};

		// Token: 0x04000BA1 RID: 2977
		private readonly AsyncBufferPool privateBufferPool;

		// Token: 0x04000BA2 RID: 2978
		public static readonly int MaxBufferSize = EmsmdbConstants.MaxMapiHttpChainedPayloadSize;
	}
}
