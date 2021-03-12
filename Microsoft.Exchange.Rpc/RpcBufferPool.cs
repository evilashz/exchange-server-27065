using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000256 RID: 598
	internal class RpcBufferPool
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0002712C File Offset: 0x0002652C
		private RpcBufferPool()
		{
			this.BufferPool256k = new BufferPool(RpcBufferPool.Buffer256k, 20, true, true);
			this.BufferPool96k = new BufferPool(RpcBufferPool.Buffer96k, 20, true, true);
			this.BufferPool32k = new BufferPool(RpcBufferPool.Buffer32k, 20, true, true);
			this.BufferPool4k = new BufferPool(RpcBufferPool.Buffer4k, 20, true, true);
			this.BufferPool1k = new BufferPool(RpcBufferPool.Buffer1k, 20, true, true);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00027EB0 File Offset: 0x000272B0
		public static byte[] GetBuffer(int requestedBufferSize)
		{
			if (requestedBufferSize < 0)
			{
				throw new FailRpcException("Buffer size cannot be negative", -2147024809);
			}
			if (requestedBufferSize == 0)
			{
				return RpcBufferPool.EmptyBuffer;
			}
			if (requestedBufferSize <= RpcBufferPool.Buffer1k)
			{
				return RpcBufferPool.Pool.BufferPool1k.Acquire();
			}
			if (requestedBufferSize <= RpcBufferPool.Buffer4k)
			{
				return RpcBufferPool.Pool.BufferPool4k.Acquire();
			}
			if (requestedBufferSize <= RpcBufferPool.Buffer32k)
			{
				return RpcBufferPool.Pool.BufferPool32k.Acquire();
			}
			if (requestedBufferSize <= RpcBufferPool.Buffer96k)
			{
				return RpcBufferPool.Pool.BufferPool96k.Acquire();
			}
			if (requestedBufferSize <= RpcBufferPool.Buffer256k)
			{
				return RpcBufferPool.Pool.BufferPool256k.Acquire();
			}
			throw new FailRpcException("Buffer size too large", -2147024809);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000271A4 File Offset: 0x000265A4
		public static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer != null)
			{
				if (buffer.Length == RpcBufferPool.Buffer256k)
				{
					RpcBufferPool.Pool.BufferPool256k.Release(buffer);
				}
				else if (buffer.Length == RpcBufferPool.Buffer96k)
				{
					RpcBufferPool.Pool.BufferPool96k.Release(buffer);
				}
				else if (buffer.Length == RpcBufferPool.Buffer32k)
				{
					RpcBufferPool.Pool.BufferPool32k.Release(buffer);
				}
				else if (buffer.Length == RpcBufferPool.Buffer4k)
				{
					RpcBufferPool.Pool.BufferPool4k.Release(buffer);
				}
				else
				{
					int num = buffer.Length;
					if (num == RpcBufferPool.Buffer1k)
					{
						RpcBufferPool.Pool.BufferPool1k.Release(buffer);
					}
					else if (num != 0)
					{
						throw new ArgumentException("buffer being released doesn't match any buffer pool length");
					}
				}
			}
		}

		// Token: 0x04000CBF RID: 3263
		private static readonly int ExtendedHeaderSize = 8;

		// Token: 0x04000CC0 RID: 3264
		private static readonly int Buffer256k = 262144;

		// Token: 0x04000CC1 RID: 3265
		private static readonly int Buffer96k = RpcBufferPool.ExtendedHeaderSize + 98304;

		// Token: 0x04000CC2 RID: 3266
		private static readonly int Buffer32k = RpcBufferPool.ExtendedHeaderSize + 32768;

		// Token: 0x04000CC3 RID: 3267
		private static readonly int Buffer4k = RpcBufferPool.ExtendedHeaderSize + 4096;

		// Token: 0x04000CC4 RID: 3268
		private static readonly int Buffer1k = RpcBufferPool.ExtendedHeaderSize + 1024;

		// Token: 0x04000CC5 RID: 3269
		private static readonly byte[] EmptyBuffer = new byte[0];

		// Token: 0x04000CC6 RID: 3270
		private readonly BufferPool BufferPool256k;

		// Token: 0x04000CC7 RID: 3271
		private readonly BufferPool BufferPool96k;

		// Token: 0x04000CC8 RID: 3272
		private readonly BufferPool BufferPool32k;

		// Token: 0x04000CC9 RID: 3273
		private readonly BufferPool BufferPool4k;

		// Token: 0x04000CCA RID: 3274
		private readonly BufferPool BufferPool1k;

		// Token: 0x04000CCB RID: 3275
		private static readonly RpcBufferPool Pool = new RpcBufferPool();
	}
}
