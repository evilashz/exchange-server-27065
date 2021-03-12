using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000CA RID: 202
	internal class InternDataPool<DataType> where DataType : class
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003F46A File Offset: 0x0003D66A
		public InternDataPool(int initSize)
		{
			this.cache = new Dictionary<DataType, InternDataPool<DataType>.DataRecord>(initSize);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003F48C File Offset: 0x0003D68C
		public DataType Intern(DataType data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			DataType result;
			lock (this.thisLock)
			{
				InternDataPool<DataType>.DataRecord dataRecord;
				if (this.cache.TryGetValue(data, out dataRecord))
				{
					result = dataRecord.Data;
					dataRecord.Count += 1L;
				}
				else
				{
					result = data;
					this.cache.Add(data, new InternDataPool<DataType>.DataRecord(data));
				}
			}
			return result;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003F518 File Offset: 0x0003D718
		public void Release(DataType data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			lock (this.thisLock)
			{
				InternDataPool<DataType>.DataRecord dataRecord;
				if (this.cache.TryGetValue(data, out dataRecord))
				{
					long count = dataRecord.Count;
					if (count <= 1L)
					{
						this.cache.Remove(data);
					}
					else
					{
						dataRecord.Count -= 1L;
					}
				}
			}
		}

		// Token: 0x04000744 RID: 1860
		private object thisLock = new object();

		// Token: 0x04000745 RID: 1861
		private Dictionary<DataType, InternDataPool<DataType>.DataRecord> cache;

		// Token: 0x020000CB RID: 203
		private class DataRecord
		{
			// Token: 0x06000BBB RID: 3003 RVA: 0x0003F5A0 File Offset: 0x0003D7A0
			public DataRecord(DataType data)
			{
				this.Data = data;
				this.Count = 1L;
			}

			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0003F5B7 File Offset: 0x0003D7B7
			// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0003F5BF File Offset: 0x0003D7BF
			public DataType Data { get; set; }

			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0003F5C8 File Offset: 0x0003D7C8
			// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
			public long Count { get; set; }
		}
	}
}
