using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004FA RID: 1274
	[ComVisible(true)]
	[Serializable]
	public enum ThreadPriority
	{
		// Token: 0x04001963 RID: 6499
		Lowest,
		// Token: 0x04001964 RID: 6500
		BelowNormal,
		// Token: 0x04001965 RID: 6501
		Normal,
		// Token: 0x04001966 RID: 6502
		AboveNormal,
		// Token: 0x04001967 RID: 6503
		Highest
	}
}
