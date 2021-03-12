using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000990 RID: 2448
	[Flags]
	internal enum ActivationFlags : uint
	{
		// Token: 0x04002D24 RID: 11556
		Machine = 1U,
		// Token: 0x04002D25 RID: 11557
		GroupIdentity = 2U,
		// Token: 0x04002D26 RID: 11558
		Temporary = 4U,
		// Token: 0x04002D27 RID: 11559
		Cancel = 8U,
		// Token: 0x04002D28 RID: 11560
		Silent = 16U,
		// Token: 0x04002D29 RID: 11561
		SharedGroupIdentity = 32U,
		// Token: 0x04002D2A RID: 11562
		Delayed = 64U
	}
}
