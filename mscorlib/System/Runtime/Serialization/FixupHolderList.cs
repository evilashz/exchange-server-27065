using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000721 RID: 1825
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x06005193 RID: 20883 RVA: 0x0011E0EF File Offset: 0x0011C2EF
		internal FixupHolderList() : this(2)
		{
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0011E0F8 File Offset: 0x0011C2F8
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x0011E114 File Offset: 0x0011C314
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0011E168 File Offset: 0x0011C368
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x0011E1A4 File Offset: 0x0011C3A4
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
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040023EA RID: 9194
		internal const int InitialSize = 2;

		// Token: 0x040023EB RID: 9195
		internal FixupHolder[] m_values;

		// Token: 0x040023EC RID: 9196
		internal int m_count;
	}
}
