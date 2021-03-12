using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ABA RID: 2746
	public enum CertificateStatus
	{
		// Token: 0x04003566 RID: 13670
		[LocDescription(Strings.IDs.CertificateStatusUnknown)]
		Unknown,
		// Token: 0x04003567 RID: 13671
		[LocDescription(Strings.IDs.CertificateStatusValid)]
		Valid,
		// Token: 0x04003568 RID: 13672
		[LocDescription(Strings.IDs.CertificateStatusRevoked)]
		Revoked,
		// Token: 0x04003569 RID: 13673
		[LocDescription(Strings.IDs.CertificateStatusDateInvalid)]
		DateInvalid,
		// Token: 0x0400356A RID: 13674
		[LocDescription(Strings.IDs.CertificateStatusUntrusted)]
		Untrusted,
		// Token: 0x0400356B RID: 13675
		[LocDescription(Strings.IDs.CertificateStatusInvalid)]
		Invalid,
		// Token: 0x0400356C RID: 13676
		[LocDescription(Strings.IDs.CertificateStatusRevocationCheckFailure)]
		RevocationCheckFailure,
		// Token: 0x0400356D RID: 13677
		[LocDescription(Strings.IDs.CertificateStatusPendingRequest)]
		PendingRequest
	}
}
