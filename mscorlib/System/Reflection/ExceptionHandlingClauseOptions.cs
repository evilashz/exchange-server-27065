using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E4 RID: 1508
	[Flags]
	[ComVisible(true)]
	public enum ExceptionHandlingClauseOptions
	{
		// Token: 0x04001D0F RID: 7439
		Clause = 0,
		// Token: 0x04001D10 RID: 7440
		Filter = 1,
		// Token: 0x04001D11 RID: 7441
		Finally = 2,
		// Token: 0x04001D12 RID: 7442
		Fault = 4
	}
}
