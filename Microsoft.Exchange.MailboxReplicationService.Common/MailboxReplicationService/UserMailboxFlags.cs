using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000156 RID: 342
	[Flags]
	public enum UserMailboxFlags
	{
		// Token: 0x040006C7 RID: 1735
		None = 0,
		// Token: 0x040006C8 RID: 1736
		RecoveryMDB = 1,
		// Token: 0x040006C9 RID: 1737
		Disconnected = 2,
		// Token: 0x040006CA RID: 1738
		SoftDeleted = 4,
		// Token: 0x040006CB RID: 1739
		MoveDestination = 8
	}
}
