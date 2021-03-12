using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008A8 RID: 2216
	internal struct StackCrawlMarkHandle
	{
		// Token: 0x06005CA9 RID: 23721 RVA: 0x00144DE3 File Offset: 0x00142FE3
		internal StackCrawlMarkHandle(IntPtr stackMark)
		{
			this.m_ptr = stackMark;
		}

		// Token: 0x0400297B RID: 10619
		private IntPtr m_ptr;
	}
}
