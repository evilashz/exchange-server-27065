using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F5 RID: 1781
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002360 RID: 9056
		None = 0,
		// Token: 0x04002361 RID: 9057
		AppDomainID = 1,
		// Token: 0x04002362 RID: 9058
		ProcessID = 2,
		// Token: 0x04002363 RID: 9059
		CLRInstanceID = 4,
		// Token: 0x04002364 RID: 9060
		AssemblyName = 8,
		// Token: 0x04002365 RID: 9061
		TypeName = 16
	}
}
