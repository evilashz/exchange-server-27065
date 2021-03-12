using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200059C RID: 1436
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum AssemblyNameFlags
	{
		// Token: 0x04001B65 RID: 7013
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001B66 RID: 7014
		[__DynamicallyInvokable]
		PublicKey = 1,
		// Token: 0x04001B67 RID: 7015
		EnableJITcompileOptimizer = 16384,
		// Token: 0x04001B68 RID: 7016
		EnableJITcompileTracking = 32768,
		// Token: 0x04001B69 RID: 7017
		[__DynamicallyInvokable]
		Retargetable = 256
	}
}
