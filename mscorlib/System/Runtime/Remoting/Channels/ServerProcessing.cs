using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000819 RID: 2073
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		// Token: 0x0400283D RID: 10301
		Complete,
		// Token: 0x0400283E RID: 10302
		OneWay,
		// Token: 0x0400283F RID: 10303
		Async
	}
}
