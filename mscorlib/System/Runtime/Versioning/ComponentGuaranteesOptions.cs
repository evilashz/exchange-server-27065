using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F0 RID: 1776
	[Flags]
	[Serializable]
	public enum ComponentGuaranteesOptions
	{
		// Token: 0x0400234F RID: 9039
		None = 0,
		// Token: 0x04002350 RID: 9040
		Exchange = 1,
		// Token: 0x04002351 RID: 9041
		Stable = 2,
		// Token: 0x04002352 RID: 9042
		SideBySide = 4
	}
}
