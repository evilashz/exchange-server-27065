using System;
using System.Threading;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002DD RID: 733
	internal sealed class MemoryCache
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x0001AF61 File Offset: 0x00019161
		public MemoryCache(int bufferSize, int maxCachedBuffers)
		{
			this.bufferSize = bufferSize;
			this.cachedBuffers = new byte[maxCachedBuffers][];
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0001AF7C File Offset: 0x0001917C
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001AF84 File Offset: 0x00019184
		public static byte[] Duplicate(byte[] data, int length)
		{
			if (length == 0)
			{
				return MemoryCache.ZeroLengthArray;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(data, 0, array, 0, length);
			return array;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0001AFAC File Offset: 0x000191AC
		public byte[] Allocate()
		{
			int startingOffset = this.GetStartingOffset();
			for (int i = 0; i < this.cachedBuffers.Length; i++)
			{
				int num = (i + startingOffset) % this.cachedBuffers.Length;
				byte[] array = Interlocked.Exchange<byte[]>(ref this.cachedBuffers[num], null);
				if (array != null)
				{
					return array;
				}
			}
			return new byte[this.bufferSize];
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0001B004 File Offset: 0x00019204
		public void Free(ref byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length != this.bufferSize)
			{
				throw new ArgumentOutOfRangeException("data", data.Length, "buffer is not correct size for this MemoryCache");
			}
			int startingOffset = this.GetStartingOffset();
			for (int i = 0; i < this.cachedBuffers.Length; i++)
			{
				int num = (i + startingOffset) % this.cachedBuffers.Length;
				if (this.cachedBuffers[num] == null)
				{
					this.cachedBuffers[num] = data;
					break;
				}
			}
			data = null;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001B085 File Offset: 0x00019285
		private int GetStartingOffset()
		{
			return LibraryHelpers.GetCurrentManagedThreadId() % this.cachedBuffers.Length;
		}

		// Token: 0x0400090E RID: 2318
		private static readonly byte[] ZeroLengthArray = new byte[0];

		// Token: 0x0400090F RID: 2319
		private readonly int bufferSize;

		// Token: 0x04000910 RID: 2320
		private readonly byte[][] cachedBuffers;
	}
}
