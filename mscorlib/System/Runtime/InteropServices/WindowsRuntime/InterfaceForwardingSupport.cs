using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E0 RID: 2528
	[Flags]
	internal enum InterfaceForwardingSupport
	{
		// Token: 0x04002C80 RID: 11392
		None = 0,
		// Token: 0x04002C81 RID: 11393
		IBindableVector = 1,
		// Token: 0x04002C82 RID: 11394
		IVector = 2,
		// Token: 0x04002C83 RID: 11395
		IBindableVectorView = 4,
		// Token: 0x04002C84 RID: 11396
		IVectorView = 8,
		// Token: 0x04002C85 RID: 11397
		IBindableIterableOrIIterable = 16
	}
}
