using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A1D RID: 2589
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum INVOKEKIND
	{
		// Token: 0x04002D1B RID: 11547
		[__DynamicallyInvokable]
		INVOKE_FUNC = 1,
		// Token: 0x04002D1C RID: 11548
		[__DynamicallyInvokable]
		INVOKE_PROPERTYGET = 2,
		// Token: 0x04002D1D RID: 11549
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04002D1E RID: 11550
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUTREF = 8
	}
}
