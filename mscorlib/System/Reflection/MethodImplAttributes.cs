using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DA RID: 1498
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplAttributes
	{
		// Token: 0x04001CD4 RID: 7380
		[__DynamicallyInvokable]
		CodeTypeMask = 3,
		// Token: 0x04001CD5 RID: 7381
		[__DynamicallyInvokable]
		IL = 0,
		// Token: 0x04001CD6 RID: 7382
		[__DynamicallyInvokable]
		Native,
		// Token: 0x04001CD7 RID: 7383
		[__DynamicallyInvokable]
		OPTIL,
		// Token: 0x04001CD8 RID: 7384
		[__DynamicallyInvokable]
		Runtime,
		// Token: 0x04001CD9 RID: 7385
		[__DynamicallyInvokable]
		ManagedMask,
		// Token: 0x04001CDA RID: 7386
		[__DynamicallyInvokable]
		Unmanaged = 4,
		// Token: 0x04001CDB RID: 7387
		[__DynamicallyInvokable]
		Managed = 0,
		// Token: 0x04001CDC RID: 7388
		[__DynamicallyInvokable]
		ForwardRef = 16,
		// Token: 0x04001CDD RID: 7389
		[__DynamicallyInvokable]
		PreserveSig = 128,
		// Token: 0x04001CDE RID: 7390
		[__DynamicallyInvokable]
		InternalCall = 4096,
		// Token: 0x04001CDF RID: 7391
		[__DynamicallyInvokable]
		Synchronized = 32,
		// Token: 0x04001CE0 RID: 7392
		[__DynamicallyInvokable]
		NoInlining = 8,
		// Token: 0x04001CE1 RID: 7393
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		// Token: 0x04001CE2 RID: 7394
		[__DynamicallyInvokable]
		NoOptimization = 64,
		// Token: 0x04001CE3 RID: 7395
		SecurityMitigations = 1024,
		// Token: 0x04001CE4 RID: 7396
		MaxMethodImplVal = 65535
	}
}
