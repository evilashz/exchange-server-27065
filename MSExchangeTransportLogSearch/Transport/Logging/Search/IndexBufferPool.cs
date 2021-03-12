using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000036 RID: 54
	internal class IndexBufferPool
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00008E64 File Offset: 0x00007064
		private IndexBufferPool()
		{
			for (int i = 0; i < 6; i++)
			{
				this.indexBuffers.Enqueue(new List<LogIndex.IndexRecord>(51200));
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00008EA4 File Offset: 0x000070A4
		internal static IndexBufferPool Instance
		{
			get
			{
				return IndexBufferPool.instance;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008EAC File Offset: 0x000070AC
		internal List<LogIndex.IndexRecord> CheckOut()
		{
			List<LogIndex.IndexRecord> result;
			lock (this)
			{
				if (this.indexBuffers.Count == 0)
				{
					this.indexBuffers.Enqueue(new List<LogIndex.IndexRecord>(51200));
				}
				result = this.indexBuffers.Dequeue();
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008F10 File Offset: 0x00007110
		internal void CheckIn(List<LogIndex.IndexRecord> indexBuffer)
		{
			lock (this)
			{
				indexBuffer.Clear();
				this.indexBuffers.Enqueue(indexBuffer);
			}
		}

		// Token: 0x040000D2 RID: 210
		private const int InitialCapacity = 6;

		// Token: 0x040000D3 RID: 211
		private const int BufferSize = 51200;

		// Token: 0x040000D4 RID: 212
		private static IndexBufferPool instance = new IndexBufferPool();

		// Token: 0x040000D5 RID: 213
		private Queue<List<LogIndex.IndexRecord>> indexBuffers = new Queue<List<LogIndex.IndexRecord>>(6);
	}
}
