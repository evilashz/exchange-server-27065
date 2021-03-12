using System;

namespace System.Threading
{
	// Token: 0x0200051B RID: 1307
	internal class SparselyPopulatedArray<T> where T : class
	{
		// Token: 0x06003E4D RID: 15949 RVA: 0x000E7754 File Offset: 0x000E5954
		internal SparselyPopulatedArray(int initialSize)
		{
			this.m_head = (this.m_tail = new SparselyPopulatedArrayFragment<T>(initialSize));
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000E777E File Offset: 0x000E597E
		internal SparselyPopulatedArrayFragment<T> Tail
		{
			get
			{
				return this.m_tail;
			}
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000E7788 File Offset: 0x000E5988
		internal SparselyPopulatedArrayAddInfo<T> Add(T element)
		{
			SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment2;
			int num2;
			for (;;)
			{
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment = this.m_tail;
				while (sparselyPopulatedArrayFragment.m_next != null)
				{
					sparselyPopulatedArrayFragment = (this.m_tail = sparselyPopulatedArrayFragment.m_next);
				}
				for (sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment; sparselyPopulatedArrayFragment2 != null; sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment2.m_prev)
				{
					if (sparselyPopulatedArrayFragment2.m_freeCount < 1)
					{
						sparselyPopulatedArrayFragment2.m_freeCount--;
					}
					if (sparselyPopulatedArrayFragment2.m_freeCount > 0 || sparselyPopulatedArrayFragment2.m_freeCount < -10)
					{
						int length = sparselyPopulatedArrayFragment2.Length;
						int num = (length - sparselyPopulatedArrayFragment2.m_freeCount) % length;
						if (num < 0)
						{
							num = 0;
							sparselyPopulatedArrayFragment2.m_freeCount--;
						}
						for (int i = 0; i < length; i++)
						{
							num2 = (num + i) % length;
							if (sparselyPopulatedArrayFragment2.m_elements[num2] == null && Interlocked.CompareExchange<T>(ref sparselyPopulatedArrayFragment2.m_elements[num2], element, default(T)) == null)
							{
								goto Block_5;
							}
						}
					}
				}
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment3 = new SparselyPopulatedArrayFragment<T>((sparselyPopulatedArrayFragment.m_elements.Length == 4096) ? 4096 : (sparselyPopulatedArrayFragment.m_elements.Length * 2), sparselyPopulatedArrayFragment);
				if (Interlocked.CompareExchange<SparselyPopulatedArrayFragment<T>>(ref sparselyPopulatedArrayFragment.m_next, sparselyPopulatedArrayFragment3, null) == null)
				{
					this.m_tail = sparselyPopulatedArrayFragment3;
				}
			}
			Block_5:
			int num3 = sparselyPopulatedArrayFragment2.m_freeCount - 1;
			sparselyPopulatedArrayFragment2.m_freeCount = ((num3 > 0) ? num3 : 0);
			return new SparselyPopulatedArrayAddInfo<T>(sparselyPopulatedArrayFragment2, num2);
		}

		// Token: 0x040019F9 RID: 6649
		private readonly SparselyPopulatedArrayFragment<T> m_head;

		// Token: 0x040019FA RID: 6650
		private volatile SparselyPopulatedArrayFragment<T> m_tail;
	}
}
