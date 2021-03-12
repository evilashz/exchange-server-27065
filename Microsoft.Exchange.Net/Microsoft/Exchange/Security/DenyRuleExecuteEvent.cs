using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A30 RID: 2608
	[Flags]
	internal enum DenyRuleExecuteEvent : short
	{
		// Token: 0x040030C0 RID: 12480
		PreAuthentication = 1,
		// Token: 0x040030C1 RID: 12481
		PostAuthentication = 2,
		// Token: 0x040030C2 RID: 12482
		Always = 3
	}
}
