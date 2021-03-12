using System;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000005 RID: 5
	[Flags]
	public enum StatusFlags : uint
	{
		// Token: 0x04000002 RID: 2
		Unknown = 0U,
		// Token: 0x04000003 RID: 3
		Receive = 1U,
		// Token: 0x04000004 RID: 4
		Defer = 2U,
		// Token: 0x04000005 RID: 5
		Expand = 4U,
		// Token: 0x04000006 RID: 6
		Send = 8U,
		// Token: 0x04000007 RID: 7
		Deliver = 16U,
		// Token: 0x04000008 RID: 8
		Fail = 32U,
		// Token: 0x04000009 RID: 9
		Resolve = 64U
	}
}
