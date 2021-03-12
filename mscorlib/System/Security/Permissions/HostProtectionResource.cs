using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002B7 RID: 695
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostProtectionResource
	{
		// Token: 0x04000DD7 RID: 3543
		None = 0,
		// Token: 0x04000DD8 RID: 3544
		Synchronization = 1,
		// Token: 0x04000DD9 RID: 3545
		SharedState = 2,
		// Token: 0x04000DDA RID: 3546
		ExternalProcessMgmt = 4,
		// Token: 0x04000DDB RID: 3547
		SelfAffectingProcessMgmt = 8,
		// Token: 0x04000DDC RID: 3548
		ExternalThreading = 16,
		// Token: 0x04000DDD RID: 3549
		SelfAffectingThreading = 32,
		// Token: 0x04000DDE RID: 3550
		SecurityInfrastructure = 64,
		// Token: 0x04000DDF RID: 3551
		UI = 128,
		// Token: 0x04000DE0 RID: 3552
		MayLeakOnAbort = 256,
		// Token: 0x04000DE1 RID: 3553
		All = 511
	}
}
