using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000915 RID: 2325
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CallingConvention
	{
		// Token: 0x04002A79 RID: 10873
		[__DynamicallyInvokable]
		Winapi = 1,
		// Token: 0x04002A7A RID: 10874
		[__DynamicallyInvokable]
		Cdecl,
		// Token: 0x04002A7B RID: 10875
		[__DynamicallyInvokable]
		StdCall,
		// Token: 0x04002A7C RID: 10876
		[__DynamicallyInvokable]
		ThisCall,
		// Token: 0x04002A7D RID: 10877
		FastCall
	}
}
