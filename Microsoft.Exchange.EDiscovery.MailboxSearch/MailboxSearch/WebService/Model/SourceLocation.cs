using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000046 RID: 70
	[Flags]
	public enum SourceLocation
	{
		// Token: 0x0400016D RID: 365
		PrimaryOnly = 1,
		// Token: 0x0400016E RID: 366
		ArchiveOnly = 2,
		// Token: 0x0400016F RID: 367
		All = 3
	}
}
