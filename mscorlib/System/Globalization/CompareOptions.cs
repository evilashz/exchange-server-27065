using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x0200037A RID: 890
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CompareOptions
	{
		// Token: 0x04001253 RID: 4691
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001254 RID: 4692
		[__DynamicallyInvokable]
		IgnoreCase = 1,
		// Token: 0x04001255 RID: 4693
		[__DynamicallyInvokable]
		IgnoreNonSpace = 2,
		// Token: 0x04001256 RID: 4694
		[__DynamicallyInvokable]
		IgnoreSymbols = 4,
		// Token: 0x04001257 RID: 4695
		[__DynamicallyInvokable]
		IgnoreKanaType = 8,
		// Token: 0x04001258 RID: 4696
		[__DynamicallyInvokable]
		IgnoreWidth = 16,
		// Token: 0x04001259 RID: 4697
		[__DynamicallyInvokable]
		OrdinalIgnoreCase = 268435456,
		// Token: 0x0400125A RID: 4698
		[__DynamicallyInvokable]
		StringSort = 536870912,
		// Token: 0x0400125B RID: 4699
		[__DynamicallyInvokable]
		Ordinal = 1073741824
	}
}
