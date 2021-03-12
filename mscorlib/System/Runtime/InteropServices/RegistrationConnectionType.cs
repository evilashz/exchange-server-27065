using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000948 RID: 2376
	[Flags]
	public enum RegistrationConnectionType
	{
		// Token: 0x04002B13 RID: 11027
		SingleUse = 0,
		// Token: 0x04002B14 RID: 11028
		MultipleUse = 1,
		// Token: 0x04002B15 RID: 11029
		MultiSeparate = 2,
		// Token: 0x04002B16 RID: 11030
		Suspended = 4,
		// Token: 0x04002B17 RID: 11031
		Surrogate = 8
	}
}
