using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002B RID: 43
	[Flags]
	internal enum JobSortFlags
	{
		// Token: 0x040000C9 RID: 201
		None = 0,
		// Token: 0x040000CA RID: 202
		IsInteractive = 1,
		// Token: 0x040000CB RID: 203
		HasReservations = 2
	}
}
