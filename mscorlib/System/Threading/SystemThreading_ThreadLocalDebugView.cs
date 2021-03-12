using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000514 RID: 1300
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x06003DE0 RID: 15840 RVA: 0x000E5CFF File Offset: 0x000E3EFF
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x000E5D0E File Offset: 0x000E3F0E
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000E5D1B File Offset: 0x000E3F1B
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000E5D28 File Offset: 0x000E3F28
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x040019C4 RID: 6596
		private readonly ThreadLocal<T> m_tlocal;
	}
}
