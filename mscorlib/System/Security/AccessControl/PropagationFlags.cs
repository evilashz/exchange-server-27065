using System;

namespace System.Security.AccessControl
{
	// Token: 0x020001F8 RID: 504
	[Flags]
	public enum PropagationFlags
	{
		// Token: 0x04000AA1 RID: 2721
		None = 0,
		// Token: 0x04000AA2 RID: 2722
		NoPropagateInherit = 1,
		// Token: 0x04000AA3 RID: 2723
		InheritOnly = 2
	}
}
