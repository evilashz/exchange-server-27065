using System;

namespace System.Threading
{
	// Token: 0x0200051C RID: 1308
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x000E78F6 File Offset: 0x000E5AF6
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this.m_source = source;
			this.m_index = index;
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000E7906 File Offset: 0x000E5B06
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000E790E File Offset: 0x000E5B0E
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x040019FB RID: 6651
		private SparselyPopulatedArrayFragment<T> m_source;

		// Token: 0x040019FC RID: 6652
		private int m_index;
	}
}
