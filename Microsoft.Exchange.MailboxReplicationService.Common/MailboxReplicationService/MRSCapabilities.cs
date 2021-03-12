using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200014D RID: 333
	internal enum MRSCapabilities
	{
		// Token: 0x040006A6 RID: 1702
		E14_RTM,
		// Token: 0x040006A7 RID: 1703
		Merges,
		// Token: 0x040006A8 RID: 1704
		ArchiveSeparation,
		// Token: 0x040006A9 RID: 1705
		TickleWithSide,
		// Token: 0x040006AA RID: 1706
		TenantHint,
		// Token: 0x040006AB RID: 1707
		MrsProxyVerification,
		// Token: 0x040006AC RID: 1708
		AutoResume,
		// Token: 0x040006AD RID: 1709
		MrsProxyPing,
		// Token: 0x040006AE RID: 1710
		SubType,
		// Token: 0x040006AF RID: 1711
		AutoResumeMerges,
		// Token: 0x040006B0 RID: 1712
		SyncNow,
		// Token: 0x040006B1 RID: 1713
		GetMailboxInformationWithRequestJob,
		// Token: 0x040006B2 RID: 1714
		CreatePublicFoldersUnderParentInSecondary,
		// Token: 0x040006B3 RID: 1715
		MaxElement
	}
}
