using System;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007D3 RID: 2003
	internal struct MessageData
	{
		// Token: 0x0400277D RID: 10109
		internal IntPtr pFrame;

		// Token: 0x0400277E RID: 10110
		internal IntPtr pMethodDesc;

		// Token: 0x0400277F RID: 10111
		internal IntPtr pDelegateMD;

		// Token: 0x04002780 RID: 10112
		internal IntPtr pSig;

		// Token: 0x04002781 RID: 10113
		internal IntPtr thGoverningType;

		// Token: 0x04002782 RID: 10114
		internal int iFlags;
	}
}
