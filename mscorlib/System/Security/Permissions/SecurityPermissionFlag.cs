using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002D9 RID: 729
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		// Token: 0x04000E69 RID: 3689
		NoFlags = 0,
		// Token: 0x04000E6A RID: 3690
		Assertion = 1,
		// Token: 0x04000E6B RID: 3691
		UnmanagedCode = 2,
		// Token: 0x04000E6C RID: 3692
		SkipVerification = 4,
		// Token: 0x04000E6D RID: 3693
		Execution = 8,
		// Token: 0x04000E6E RID: 3694
		ControlThread = 16,
		// Token: 0x04000E6F RID: 3695
		ControlEvidence = 32,
		// Token: 0x04000E70 RID: 3696
		ControlPolicy = 64,
		// Token: 0x04000E71 RID: 3697
		SerializationFormatter = 128,
		// Token: 0x04000E72 RID: 3698
		ControlDomainPolicy = 256,
		// Token: 0x04000E73 RID: 3699
		ControlPrincipal = 512,
		// Token: 0x04000E74 RID: 3700
		ControlAppDomain = 1024,
		// Token: 0x04000E75 RID: 3701
		RemotingConfiguration = 2048,
		// Token: 0x04000E76 RID: 3702
		Infrastructure = 4096,
		// Token: 0x04000E77 RID: 3703
		BindingRedirects = 8192,
		// Token: 0x04000E78 RID: 3704
		AllFlags = 16383
	}
}
