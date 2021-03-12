using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000191 RID: 401
	[Flags]
	internal enum MailboxType
	{
		// Token: 0x04000A0F RID: 2575
		Unknown = 0,
		// Token: 0x04000A10 RID: 2576
		Default = 1,
		// Token: 0x04000A11 RID: 2577
		Archive = 2,
		// Token: 0x04000A12 RID: 2578
		PublicFolder = 4,
		// Token: 0x04000A13 RID: 2579
		TeamSite = 8,
		// Token: 0x04000A14 RID: 2580
		ModernGroup = 16,
		// Token: 0x04000A15 RID: 2581
		Shared = 32
	}
}
