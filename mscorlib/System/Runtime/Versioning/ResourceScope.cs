using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F4 RID: 1780
	[Flags]
	public enum ResourceScope
	{
		// Token: 0x04002358 RID: 9048
		None = 0,
		// Token: 0x04002359 RID: 9049
		Machine = 1,
		// Token: 0x0400235A RID: 9050
		Process = 2,
		// Token: 0x0400235B RID: 9051
		AppDomain = 4,
		// Token: 0x0400235C RID: 9052
		Library = 8,
		// Token: 0x0400235D RID: 9053
		Private = 16,
		// Token: 0x0400235E RID: 9054
		Assembly = 32
	}
}
