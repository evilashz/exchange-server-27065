using System;

namespace System.Threading
{
	// Token: 0x020004E8 RID: 1256
	internal struct ThreadHandle
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x000E1810 File Offset: 0x000DFA10
		internal ThreadHandle(IntPtr pThread)
		{
			this.m_ptr = pThread;
		}

		// Token: 0x0400192C RID: 6444
		private IntPtr m_ptr;
	}
}
