using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002C1 RID: 705
	[ComVisible(true)]
	[Serializable]
	public enum SecurityAction
	{
		// Token: 0x04000E22 RID: 3618
		Demand = 2,
		// Token: 0x04000E23 RID: 3619
		Assert,
		// Token: 0x04000E24 RID: 3620
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		Deny,
		// Token: 0x04000E25 RID: 3621
		PermitOnly,
		// Token: 0x04000E26 RID: 3622
		LinkDemand,
		// Token: 0x04000E27 RID: 3623
		InheritanceDemand,
		// Token: 0x04000E28 RID: 3624
		[Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		RequestMinimum,
		// Token: 0x04000E29 RID: 3625
		[Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		RequestOptional,
		// Token: 0x04000E2A RID: 3626
		[Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		RequestRefuse
	}
}
