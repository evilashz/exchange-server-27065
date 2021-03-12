using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000183 RID: 387
	internal class TextCache
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0007A0A8 File Offset: 0x000782A8
		public int Length
		{
			get
			{
				return this.cachedLength;
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0007A0B0 File Offset: 0x000782B0
		public void Reset()
		{
			while (this.headEntry != null)
			{
				this.headEntry.Reset();
				TextCache.CacheEntry cacheEntry = this.headEntry;
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

		// Token: 0x060010BA RID: 4282 RVA: 0x0007A113 File Offset: 0x00078313
		public void GetBuffer(int size, out char[] buffer, out int offset, out int realSize)
		{
			if (this.tailEntry != null && this.tailEntry.GetBuffer(size, out buffer, out offset, out realSize))
			{
				return;
			}
			this.AllocateTail(size);
			this.tailEntry.GetBuffer(size, out buffer, out offset, out realSize);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0007A148 File Offset: 0x00078348
		public void Commit(int count)
		{
			this.tailEntry.Commit(count);
			this.cachedLength += count;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0007A164 File Offset: 0x00078364
		public void GetData(out char[] outputBuffer, out int outputOffset, out int outputCount)
		{
			this.headEntry.GetData(out outputBuffer, out outputOffset, out outputCount);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0007A174 File Offset: 0x00078374
		public void ReportRead(int count)
		{
			this.headEntry.ReportRead(count);
			this.cachedLength -= count;
			if (this.headEntry.Length == 0)
			{
				TextCache.CacheEntry cacheEntry = this.headEntry;
				this.headEntry = this.headEntry.Next;
				if (this.headEntry == null)
				{
					this.tailEntry = null;
				}
				cacheEntry.Next = this.freeList;
				this.freeList = cacheEntry;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0007A1E4 File Offset: 0x000783E4
		public int Read(char[] buffer, int offset, int count)
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
					TextCache.CacheEntry cacheEntry = this.headEntry;
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

		// Token: 0x060010BF RID: 4287 RVA: 0x0007A278 File Offset: 0x00078478
		private void AllocateTail(int size)
		{
			TextCache.CacheEntry cacheEntry = this.freeList;
			if (cacheEntry != null)
			{
				this.freeList = cacheEntry.Next;
				cacheEntry.Next = null;
			}
			else
			{
				cacheEntry = new TextCache.CacheEntry(size);
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

		// Token: 0x0400115D RID: 4445
		private int cachedLength;

		// Token: 0x0400115E RID: 4446
		private TextCache.CacheEntry headEntry;

		// Token: 0x0400115F RID: 4447
		private TextCache.CacheEntry tailEntry;

		// Token: 0x04001160 RID: 4448
		private TextCache.CacheEntry freeList;

		// Token: 0x02000184 RID: 388
		internal class CacheEntry
		{
			// Token: 0x060010C0 RID: 4288 RVA: 0x0007A2CF File Offset: 0x000784CF
			public CacheEntry(int size)
			{
				this.AllocateBuffer(size);
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0007A2DE File Offset: 0x000784DE
			public int Length
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0007A2E6 File Offset: 0x000784E6
			// (set) Token: 0x060010C3 RID: 4291 RVA: 0x0007A2EE File Offset: 0x000784EE
			public TextCache.CacheEntry Next
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

			// Token: 0x060010C4 RID: 4292 RVA: 0x0007A2F7 File Offset: 0x000784F7
			public void Reset()
			{
				this.count = 0;
			}

			// Token: 0x060010C5 RID: 4293 RVA: 0x0007A300 File Offset: 0x00078500
			public bool GetBuffer(int size, out char[] buffer, out int offset, out int realSize)
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
					realSize = this.buffer.Length - offset;
					return true;
				}
				if (this.count < 64 && this.buffer.Length - this.count >= size)
				{
					Buffer.BlockCopy(this.buffer, this.offset * 2, this.buffer, 0, this.count * 2);
					this.offset = 0;
					buffer = this.buffer;
					offset = this.offset + this.count;
					realSize = this.buffer.Length - offset;
					return true;
				}
				buffer = null;
				offset = 0;
				realSize = 0;
				return false;
			}

			// Token: 0x060010C6 RID: 4294 RVA: 0x0007A3E5 File Offset: 0x000785E5
			public void Commit(int count)
			{
				this.count += count;
			}

			// Token: 0x060010C7 RID: 4295 RVA: 0x0007A3F5 File Offset: 0x000785F5
			public void GetData(out char[] outputBuffer, out int outputOffset, out int outputCount)
			{
				outputBuffer = this.buffer;
				outputOffset = this.offset;
				outputCount = this.count;
			}

			// Token: 0x060010C8 RID: 4296 RVA: 0x0007A40F File Offset: 0x0007860F
			public void ReportRead(int count)
			{
				this.offset += count;
				this.count -= count;
			}

			// Token: 0x060010C9 RID: 4297 RVA: 0x0007A430 File Offset: 0x00078630
			public int Read(char[] buffer, int offset, int count)
			{
				int num = Math.Min(count, this.count);
				Buffer.BlockCopy(this.buffer, this.offset * 2, buffer, offset * 2, num * 2);
				this.count -= num;
				this.offset += num;
				count -= num;
				offset += num;
				return num;
			}

			// Token: 0x060010CA RID: 4298 RVA: 0x0007A48B File Offset: 0x0007868B
			private void AllocateBuffer(int size)
			{
				if (size < 2048)
				{
					size = 2048;
				}
				size = (size * 2 + 1023) / 1024 * 1024;
				this.buffer = new char[size];
			}

			// Token: 0x04001161 RID: 4449
			private const int DefaultMaxLength = 4096;

			// Token: 0x04001162 RID: 4450
			private char[] buffer;

			// Token: 0x04001163 RID: 4451
			private int count;

			// Token: 0x04001164 RID: 4452
			private int offset;

			// Token: 0x04001165 RID: 4453
			private TextCache.CacheEntry next;
		}
	}
}
