using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200016B RID: 363
	internal class ByteCache
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000756D0 File Offset: 0x000738D0
		public int Length
		{
			get
			{
				return this.cachedLength;
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000756D8 File Offset: 0x000738D8
		public void Reset()
		{
			while (this.headEntry != null)
			{
				this.headEntry.Reset();
				ByteCache.CacheEntry cacheEntry = this.headEntry;
				this.headEntry = this.headEntry.Next;
				if (this.headEntry == null)
				{
					this.tailEntry = null;
				}
				cacheEntry.Next = this.freeList;
				this.freeList = cacheEntry;
			}
			this.cachedLength = 0;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0007573B File Offset: 0x0007393B
		public void GetBuffer(int size, out byte[] buffer, out int offset)
		{
			if (this.tailEntry != null && this.tailEntry.GetBuffer(size, out buffer, out offset))
			{
				return;
			}
			this.AllocateTail(size);
			this.tailEntry.GetBuffer(size, out buffer, out offset);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0007576C File Offset: 0x0007396C
		public void Commit(int count)
		{
			this.tailEntry.Commit(count);
			this.cachedLength += count;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00075788 File Offset: 0x00073988
		public void GetData(out byte[] outputBuffer, out int outputOffset, out int outputCount)
		{
			this.headEntry.GetData(out outputBuffer, out outputOffset, out outputCount);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00075798 File Offset: 0x00073998
		public void ReportRead(int count)
		{
			this.headEntry.ReportRead(count);
			this.cachedLength -= count;
			if (this.headEntry.Length == 0)
			{
				ByteCache.CacheEntry cacheEntry = this.headEntry;
				this.headEntry = this.headEntry.Next;
				if (this.headEntry == null)
				{
					this.tailEntry = null;
				}
				cacheEntry.Next = this.freeList;
				this.freeList = cacheEntry;
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00075808 File Offset: 0x00073A08
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (count != 0)
			{
				int num2 = this.headEntry.Read(buffer, offset, count);
				offset += num2;
				count -= num2;
				num += num2;
				this.cachedLength -= num2;
				if (this.headEntry.Length == 0)
				{
					ByteCache.CacheEntry cacheEntry = this.headEntry;
					this.headEntry = this.headEntry.Next;
					if (this.headEntry == null)
					{
						this.tailEntry = null;
					}
					cacheEntry.Next = this.freeList;
					this.freeList = cacheEntry;
				}
				if (count == 0 || this.headEntry == null)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0007589C File Offset: 0x00073A9C
		private void AllocateTail(int size)
		{
			ByteCache.CacheEntry cacheEntry = this.freeList;
			if (cacheEntry != null)
			{
				this.freeList = cacheEntry.Next;
				cacheEntry.Next = null;
			}
			else
			{
				cacheEntry = new ByteCache.CacheEntry(size);
			}
			if (this.tailEntry != null)
			{
				this.tailEntry.Next = cacheEntry;
			}
			else
			{
				this.headEntry = cacheEntry;
			}
			this.tailEntry = cacheEntry;
		}

		// Token: 0x040010B2 RID: 4274
		private int cachedLength;

		// Token: 0x040010B3 RID: 4275
		private ByteCache.CacheEntry headEntry;

		// Token: 0x040010B4 RID: 4276
		private ByteCache.CacheEntry tailEntry;

		// Token: 0x040010B5 RID: 4277
		private ByteCache.CacheEntry freeList;

		// Token: 0x0200016C RID: 364
		internal class CacheEntry
		{
			// Token: 0x06000FE0 RID: 4064 RVA: 0x000758F3 File Offset: 0x00073AF3
			public CacheEntry(int size)
			{
				this.AllocateBuffer(size);
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00075902 File Offset: 0x00073B02
			public int Length
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0007590A File Offset: 0x00073B0A
			// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00075912 File Offset: 0x00073B12
			public ByteCache.CacheEntry Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x0007591B File Offset: 0x00073B1B
			public void Reset()
			{
				this.count = 0;
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x00075924 File Offset: 0x00073B24
			public bool GetBuffer(int size, out byte[] buffer, out int offset)
			{
				if (this.count == 0)
				{
					this.offset = 0;
					if (this.buffer.Length < size)
					{
						this.AllocateBuffer(size);
					}
				}
				if (this.buffer.Length - (this.offset + this.count) >= size)
				{
					buffer = this.buffer;
					offset = this.offset + this.count;
					return true;
				}
				if (this.count < 64 && this.buffer.Length - this.count >= size)
				{
					Buffer.BlockCopy(this.buffer, this.offset, this.buffer, 0, this.count);
					this.offset = 0;
					buffer = this.buffer;
					offset = this.offset + this.count;
					return true;
				}
				buffer = null;
				offset = 0;
				return false;
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x000759E5 File Offset: 0x00073BE5
			public void Commit(int count)
			{
				this.count += count;
			}

			// Token: 0x06000FE7 RID: 4071 RVA: 0x000759F5 File Offset: 0x00073BF5
			public void GetData(out byte[] outputBuffer, out int outputOffset, out int outputCount)
			{
				outputBuffer = this.buffer;
				outputOffset = this.offset;
				outputCount = this.count;
			}

			// Token: 0x06000FE8 RID: 4072 RVA: 0x00075A0F File Offset: 0x00073C0F
			public void ReportRead(int count)
			{
				this.offset += count;
				this.count -= count;
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x00075A30 File Offset: 0x00073C30
			public int Read(byte[] buffer, int offset, int count)
			{
				int num = Math.Min(count, this.count);
				Buffer.BlockCopy(this.buffer, this.offset, buffer, offset, num);
				this.count -= num;
				this.offset += num;
				count -= num;
				offset += num;
				return num;
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00075A85 File Offset: 0x00073C85
			private void AllocateBuffer(int size)
			{
				if (size < 2048)
				{
					size = 2048;
				}
				size = (size * 2 + 1023) / 1024 * 1024;
				this.buffer = new byte[size];
			}

			// Token: 0x040010B6 RID: 4278
			private const int DefaultMaxLength = 4096;

			// Token: 0x040010B7 RID: 4279
			private byte[] buffer;

			// Token: 0x040010B8 RID: 4280
			private int count;

			// Token: 0x040010B9 RID: 4281
			private int offset;

			// Token: 0x040010BA RID: 4282
			private ByteCache.CacheEntry next;
		}
	}
}
