using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E3B RID: 3643
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class DataPoolBase<DataType> where DataType : class
	{
		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06007E7D RID: 32381 RVA: 0x0022C248 File Offset: 0x0022A448
		protected virtual int MaxNumberOfEntries
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x0022C250 File Offset: 0x0022A450
		public DataType Intern(DataType data)
		{
			if (data == null)
			{
				return default(DataType);
			}
			DataType data2 = this.GetData(data);
			if (data2 == null && this.dataDataTable.Count < this.MaxNumberOfEntries)
			{
				uint hashCode;
				byte[] bytes;
				this.ProcessData(data, out hashCode, out bytes);
				this.SetData(hashCode, data, bytes);
				return data;
			}
			return data2;
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x0022C2AC File Offset: 0x0022A4AC
		public DataType GetData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			uint hashCode;
			int startIndex;
			int length;
			this.ProcessStream(reader, componentDataPool, out hashCode, out startIndex, out length);
			MemoryStream memoryStream = (MemoryStream)componentDataPool.ConstStringDataReader.BaseStream;
			return this.GetData(hashCode, memoryStream.GetBuffer(), startIndex, length);
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x0022C2F8 File Offset: 0x0022A4F8
		protected void SetData(uint hashCode, DataType data, byte[] bytes)
		{
			try
			{
				this.poolLock.EnterWriteLock();
				DataPoolBase<DataType>.PoolDataList poolDataList;
				if (!this.hashDataTable.TryGetValue(hashCode, out poolDataList))
				{
					poolDataList = new DataPoolBase<DataType>.PoolDataList();
					poolDataList.AddData(data, bytes);
					this.hashDataTable.Add(hashCode, poolDataList);
					this.dataDataTable.Add(data, data);
				}
				else if (!poolDataList.Contains(data))
				{
					poolDataList.AddData(data, bytes);
					this.dataDataTable.Add(data, data);
				}
			}
			finally
			{
				try
				{
					this.poolLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x0022C398 File Offset: 0x0022A598
		protected DataType GetData(uint hashCode, byte[] bytes, int startIndex, int length)
		{
			try
			{
				this.poolLock.EnterReadLock();
				DataPoolBase<DataType>.PoolDataList poolDataList;
				if (this.hashDataTable.TryGetValue(hashCode, out poolDataList))
				{
					return poolDataList.GetData(bytes, startIndex, length);
				}
			}
			finally
			{
				try
				{
					this.poolLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return default(DataType);
		}

		// Token: 0x06007E82 RID: 32386 RVA: 0x0022C408 File Offset: 0x0022A608
		protected DataType GetData(DataType data)
		{
			try
			{
				this.poolLock.EnterReadLock();
				DataType result;
				if (this.dataDataTable.TryGetValue(data, out result))
				{
					return result;
				}
			}
			finally
			{
				try
				{
					this.poolLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return default(DataType);
		}

		// Token: 0x06007E83 RID: 32387
		protected abstract void ProcessStream(BinaryReader reader, ComponentDataPool componentDataPool, out uint hashCode, out int startIndex, out int length);

		// Token: 0x06007E84 RID: 32388
		protected abstract void ProcessData(DataType data, out uint hashCode, out byte[] bytes);

		// Token: 0x040055FF RID: 22015
		private ReaderWriterLockSlim poolLock = new ReaderWriterLockSlim();

		// Token: 0x04005600 RID: 22016
		private Dictionary<uint, DataPoolBase<DataType>.PoolDataList> hashDataTable = new Dictionary<uint, DataPoolBase<DataType>.PoolDataList>(100);

		// Token: 0x04005601 RID: 22017
		private Dictionary<DataType, DataType> dataDataTable = new Dictionary<DataType, DataType>(100);

		// Token: 0x02000E3C RID: 3644
		private class PoolData
		{
			// Token: 0x06007E85 RID: 32389 RVA: 0x0022C470 File Offset: 0x0022A670
			public PoolData(DataType data, byte[] bytes)
			{
				this.data = data;
				this.bytes = bytes;
			}

			// Token: 0x170021D1 RID: 8657
			// (get) Token: 0x06007E86 RID: 32390 RVA: 0x0022C486 File Offset: 0x0022A686
			public DataType Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x170021D2 RID: 8658
			// (get) Token: 0x06007E87 RID: 32391 RVA: 0x0022C48E File Offset: 0x0022A68E
			public byte[] Bytes
			{
				get
				{
					return this.bytes;
				}
			}

			// Token: 0x06007E88 RID: 32392 RVA: 0x0022C496 File Offset: 0x0022A696
			public bool Equals(DataType data)
			{
				return data != null && data.Equals(this.data);
			}

			// Token: 0x06007E89 RID: 32393 RVA: 0x0022C4BC File Offset: 0x0022A6BC
			public bool Equals(byte[] bytes, int startIndex, int length)
			{
				if (bytes == null || this.bytes.Length != length)
				{
					return false;
				}
				for (int i = 0; i < this.bytes.Length; i++)
				{
					if (bytes[i + startIndex] != this.bytes[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04005602 RID: 22018
			private DataType data;

			// Token: 0x04005603 RID: 22019
			private byte[] bytes;
		}

		// Token: 0x02000E3D RID: 3645
		private class PoolDataList
		{
			// Token: 0x06007E8A RID: 32394 RVA: 0x0022C500 File Offset: 0x0022A700
			public DataType GetData(byte[] bytes, int startIndex, int length)
			{
				foreach (DataPoolBase<DataType>.PoolData poolData in this.dataList)
				{
					if (poolData.Equals(bytes, startIndex, length))
					{
						return poolData.Data;
					}
				}
				return default(DataType);
			}

			// Token: 0x06007E8B RID: 32395 RVA: 0x0022C56C File Offset: 0x0022A76C
			public void AddData(DataType data, byte[] bytes)
			{
				this.dataList.Add(new DataPoolBase<DataType>.PoolData(data, bytes));
			}

			// Token: 0x06007E8C RID: 32396 RVA: 0x0022C580 File Offset: 0x0022A780
			public bool Contains(DataType data)
			{
				foreach (DataPoolBase<DataType>.PoolData poolData in this.dataList)
				{
					if (poolData.Equals(data))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04005604 RID: 22020
			private List<DataPoolBase<DataType>.PoolData> dataList = new List<DataPoolBase<DataType>.PoolData>(5);
		}
	}
}
