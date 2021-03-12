using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000010 RID: 16
	public class BufferPoolCollection
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002EC9 File Offset: 0x000010C9
		public BufferPoolCollection() : this(true)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002ED4 File Offset: 0x000010D4
		public BufferPoolCollection(bool cleanBuffers)
		{
			this.pools = new BufferPool[]
			{
				new BufferPool(1024, cleanBuffers),
				new BufferPool(2048, cleanBuffers),
				new BufferPool(4096, cleanBuffers),
				new BufferPool(8192, cleanBuffers),
				new BufferPool(10240, cleanBuffers),
				new BufferPool(16384, cleanBuffers),
				new BufferPool(20480, cleanBuffers),
				new BufferPool(24576, cleanBuffers),
				new BufferPool(30720, cleanBuffers),
				new BufferPool(32768, cleanBuffers),
				new BufferPool(40960, cleanBuffers),
				new BufferPool(49152, cleanBuffers),
				new BufferPool(51200, cleanBuffers),
				new BufferPool(61440, cleanBuffers),
				new BufferPool(65536, cleanBuffers),
				new BufferPool(71680, cleanBuffers),
				new BufferPool(81920, cleanBuffers),
				new BufferPool(92160, cleanBuffers),
				new BufferPool(98304, cleanBuffers),
				new BufferPool(102400, cleanBuffers),
				new BufferPool(112640, cleanBuffers),
				new BufferPool(122880, cleanBuffers),
				new BufferPool(131072, cleanBuffers),
				new BufferPool(262144, cleanBuffers),
				new BufferPool(524288, cleanBuffers),
				new BufferPool(1048576, cleanBuffers)
			};
			this.cleanBuffers = cleanBuffers;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000307A File Offset: 0x0000127A
		public static BufferPoolCollection AutoCleanupCollection
		{
			get
			{
				return BufferPoolCollection.collection;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003081 File Offset: 0x00001281
		public bool CleanBuffersOnRelease
		{
			get
			{
				return this.cleanBuffers;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000308C File Offset: 0x0000128C
		public BufferPool Acquire(BufferPoolCollection.BufferSize bufferSize)
		{
			if (bufferSize >= BufferPoolCollection.BufferSize.Size1K && bufferSize < (BufferPoolCollection.BufferSize)this.pools.Length)
			{
				return this.pools[(int)bufferSize];
			}
			throw new ArgumentOutOfRangeException("bufferSize");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000030C0 File Offset: 0x000012C0
		public bool TryMatchBufferSize(int size, out BufferPoolCollection.BufferSize result)
		{
			for (int i = 0; i < this.pools.Length; i++)
			{
				if (this.pools[i].BufferSize >= size)
				{
					result = (BufferPoolCollection.BufferSize)i;
					return true;
				}
			}
			result = BufferPoolCollection.BufferSize.Size1M;
			return false;
		}

		// Token: 0x04000027 RID: 39
		private static BufferPoolCollection collection = new BufferPoolCollection();

		// Token: 0x04000028 RID: 40
		private readonly bool cleanBuffers;

		// Token: 0x04000029 RID: 41
		private BufferPool[] pools;

		// Token: 0x02000011 RID: 17
		public enum BufferSize
		{
			// Token: 0x0400002B RID: 43
			Size1K,
			// Token: 0x0400002C RID: 44
			Size2K,
			// Token: 0x0400002D RID: 45
			Size4K,
			// Token: 0x0400002E RID: 46
			Size8K,
			// Token: 0x0400002F RID: 47
			Size10K,
			// Token: 0x04000030 RID: 48
			Size16K,
			// Token: 0x04000031 RID: 49
			Size20K,
			// Token: 0x04000032 RID: 50
			Size24K,
			// Token: 0x04000033 RID: 51
			Size30K,
			// Token: 0x04000034 RID: 52
			Size32K,
			// Token: 0x04000035 RID: 53
			Size40K,
			// Token: 0x04000036 RID: 54
			Size48K,
			// Token: 0x04000037 RID: 55
			Size50K,
			// Token: 0x04000038 RID: 56
			Size60K,
			// Token: 0x04000039 RID: 57
			Size64K,
			// Token: 0x0400003A RID: 58
			Size70K,
			// Token: 0x0400003B RID: 59
			Size80K,
			// Token: 0x0400003C RID: 60
			Size90K,
			// Token: 0x0400003D RID: 61
			Size96K,
			// Token: 0x0400003E RID: 62
			Size100K,
			// Token: 0x0400003F RID: 63
			Size110K,
			// Token: 0x04000040 RID: 64
			Size120K,
			// Token: 0x04000041 RID: 65
			Size128K,
			// Token: 0x04000042 RID: 66
			Size256K,
			// Token: 0x04000043 RID: 67
			Size512K,
			// Token: 0x04000044 RID: 68
			Size1M
		}
	}
}
