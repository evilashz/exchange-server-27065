using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000724 RID: 1828
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x060051A8 RID: 20904 RVA: 0x0011E452 File Offset: 0x0011C652
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0011E480 File Offset: 0x0011C680
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x0011E507 File Offset: 0x0011C707
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x040023F5 RID: 9205
		private bool m_isFixupEnumerator;

		// Token: 0x040023F6 RID: 9206
		private ObjectHolderList m_list;

		// Token: 0x040023F7 RID: 9207
		private int m_startingVersion;

		// Token: 0x040023F8 RID: 9208
		private int m_currPos;
	}
}
