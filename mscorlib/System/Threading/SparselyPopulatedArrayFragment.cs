using System;

namespace System.Threading
{
	// Token: 0x0200051D RID: 1309
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x06003E53 RID: 15955 RVA: 0x000E7916 File Offset: 0x000E5B16
		internal SparselyPopulatedArrayFragment(int size) : this(size, null)
		{
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000E7920 File Offset: 0x000E5B20
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this.m_elements = new T[size];
			this.m_freeCount = size;
			this.m_prev = prev;
		}

		// Token: 0x1700094D RID: 2381
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this.m_elements[index]);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x000E7959 File Offset: 0x000E5B59
		internal int Length
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000E7963 File Offset: 0x000E5B63
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this.m_prev;
			}
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000E7970 File Offset: 0x000E5B70
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this.m_elements[index], default(T), expectedElement);
			if (t != null)
			{
				this.m_freeCount++;
			}
			return t;
		}

		// Token: 0x040019FD RID: 6653
		internal readonly T[] m_elements;

		// Token: 0x040019FE RID: 6654
		internal volatile int m_freeCount;

		// Token: 0x040019FF RID: 6655
		internal volatile SparselyPopulatedArrayFragment<T> m_next;

		// Token: 0x04001A00 RID: 6656
		internal volatile SparselyPopulatedArrayFragment<T> m_prev;
	}
}
