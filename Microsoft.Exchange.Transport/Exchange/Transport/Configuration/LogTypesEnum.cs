using System;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002F0 RID: 752
	[Flags]
	internal enum LogTypesEnum : long
	{
		// Token: 0x04001185 RID: 4485
		None = 0L,
		// Token: 0x04001186 RID: 4486
		InboxRules = 1L,
		// Token: 0x04001187 RID: 4487
		TransportRules = 2L,
		// Token: 0x04001188 RID: 4488
		Arbitration = 4L
	}
}
