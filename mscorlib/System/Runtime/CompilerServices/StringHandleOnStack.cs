using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008A6 RID: 2214
	internal struct StringHandleOnStack
	{
		// Token: 0x06005CA7 RID: 23719 RVA: 0x00144DD1 File Offset: 0x00142FD1
		internal StringHandleOnStack(IntPtr pString)
		{
			this.m_ptr = pString;
		}

		// Token: 0x04002979 RID: 10617
		private IntPtr m_ptr;
	}
}
