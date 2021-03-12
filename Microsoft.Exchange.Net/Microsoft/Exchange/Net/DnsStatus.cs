using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C0C RID: 3084
	internal enum DnsStatus
	{
		// Token: 0x0400397C RID: 14716
		Success,
		// Token: 0x0400397D RID: 14717
		InfoNoRecords,
		// Token: 0x0400397E RID: 14718
		InfoDomainNonexistent,
		// Token: 0x0400397F RID: 14719
		InfoMxLoopback,
		// Token: 0x04003980 RID: 14720
		ErrorInvalidData,
		// Token: 0x04003981 RID: 14721
		ErrorExcessiveData,
		// Token: 0x04003982 RID: 14722
		InfoTruncated,
		// Token: 0x04003983 RID: 14723
		ErrorRetry,
		// Token: 0x04003984 RID: 14724
		[EditorBrowsable(EditorBrowsableState.Never)]
		ErrorTimeout,
		// Token: 0x04003985 RID: 14725
		[EditorBrowsable(EditorBrowsableState.Never)]
		ErrorDisconnectException,
		// Token: 0x04003986 RID: 14726
		[EditorBrowsable(EditorBrowsableState.Never)]
		Pending,
		// Token: 0x04003987 RID: 14727
		[EditorBrowsable(EditorBrowsableState.Never)]
		ServerFailure,
		// Token: 0x04003988 RID: 14728
		ConfigChanged,
		// Token: 0x04003989 RID: 14729
		ErrorSubQueryTimeout,
		// Token: 0x0400398A RID: 14730
		ErrorNoDns,
		// Token: 0x0400398B RID: 14731
		NoOutboundFrontendServers
	}
}
