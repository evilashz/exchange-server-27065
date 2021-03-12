using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000723 RID: 1827
	internal class ObjectHolderList
	{
		// Token: 0x060051A1 RID: 20897 RVA: 0x0011E37A File Offset: 0x0011C57A
		internal ObjectHolderList() : this(8)
		{
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0011E383 File Offset: 0x0011C583
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0011E3A0 File Offset: 0x0011C5A0
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0011E3DC File Offset: 0x0011C5DC
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0011E3E8 File Offset: 0x0011C5E8
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0011E442 File Offset: 0x0011C642
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0011E44A File Offset: 0x0011C64A
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x040023F2 RID: 9202
		internal const int DefaultInitialSize = 8;

		// Token: 0x040023F3 RID: 9203
		internal ObjectHolder[] m_values;

		// Token: 0x040023F4 RID: 9204
		internal int m_count;
	}
}
