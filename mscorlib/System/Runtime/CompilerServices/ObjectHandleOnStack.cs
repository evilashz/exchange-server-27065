using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008A7 RID: 2215
	internal struct ObjectHandleOnStack
	{
		// Token: 0x06005CA8 RID: 23720 RVA: 0x00144DDA File Offset: 0x00142FDA
		internal ObjectHandleOnStack(IntPtr pObject)
		{
			this.m_ptr = pObject;
		}

		// Token: 0x0400297A RID: 10618
		private IntPtr m_ptr;
	}
}
