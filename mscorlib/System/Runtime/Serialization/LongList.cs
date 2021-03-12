using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000722 RID: 1826
	[Serializable]
	internal class LongList
	{
		// Token: 0x06005198 RID: 20888 RVA: 0x0011E1FE File Offset: 0x0011C3FE
		internal LongList() : this(2)
		{
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0011E207 File Offset: 0x0011C407
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0011E22C File Offset: 0x0011C42C
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x0011E276 File Offset: 0x0011C476
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0011E27E File Offset: 0x0011C47E
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x0011E288 File Offset: 0x0011C488
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600519E RID: 20894 RVA: 0x0011E2D0 File Offset: 0x0011C4D0
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0011E2E0 File Offset: 0x0011C4E0
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0011E320 File Offset: 0x0011C520
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040023ED RID: 9197
		private const int InitialSize = 2;

		// Token: 0x040023EE RID: 9198
		private long[] m_values;

		// Token: 0x040023EF RID: 9199
		private int m_count;

		// Token: 0x040023F0 RID: 9200
		private int m_totalItems;

		// Token: 0x040023F1 RID: 9201
		private int m_currentItem;
	}
}
