using System;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D5 RID: 469
	[Flags]
	internal enum IMAPMailFlags
	{
		// Token: 0x040007DF RID: 2015
		None = 0,
		// Token: 0x040007E0 RID: 2016
		Answered = 1,
		// Token: 0x040007E1 RID: 2017
		Flagged = 2,
		// Token: 0x040007E2 RID: 2018
		Deleted = 4,
		// Token: 0x040007E3 RID: 2019
		Seen = 8,
		// Token: 0x040007E4 RID: 2020
		Draft = 16,
		// Token: 0x040007E5 RID: 2021
		All = 31
	}
}
