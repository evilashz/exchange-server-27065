using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000890 RID: 2192
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplOptions
	{
		// Token: 0x04002960 RID: 10592
		Unmanaged = 4,
		// Token: 0x04002961 RID: 10593
		ForwardRef = 16,
		// Token: 0x04002962 RID: 10594
		[__DynamicallyInvokable]
		PreserveSig = 128,
		// Token: 0x04002963 RID: 10595
		InternalCall = 4096,
		// Token: 0x04002964 RID: 10596
		Synchronized = 32,
		// Token: 0x04002965 RID: 10597
		[__DynamicallyInvokable]
		NoInlining = 8,
		// Token: 0x04002966 RID: 10598
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		// Token: 0x04002967 RID: 10599
		[__DynamicallyInvokable]
		NoOptimization = 64,
		// Token: 0x04002968 RID: 10600
		SecurityMitigations = 1024
	}
}
