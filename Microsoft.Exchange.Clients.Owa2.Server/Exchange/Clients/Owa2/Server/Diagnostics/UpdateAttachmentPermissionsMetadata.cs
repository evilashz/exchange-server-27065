using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000466 RID: 1126
	public enum UpdateAttachmentPermissionsMetadata
	{
		// Token: 0x040015F4 RID: 5620
		[DisplayName("UAP", "UID")]
		NumberOfUserIDs,
		// Token: 0x040015F5 RID: 5621
		[DisplayName("UAP", "DL")]
		NumberOfDLs,
		// Token: 0x040015F6 RID: 5622
		[DisplayName("UAP", "LDL")]
		NumberOfLargeDLs,
		// Token: 0x040015F7 RID: 5623
		[DisplayName("UAP", "RDL")]
		NumberOfRecipientsInDLs,
		// Token: 0x040015F8 RID: 5624
		[DisplayName("UAP", "RSDL")]
		NumberOfRecipientsInSmallestDL,
		// Token: 0x040015F9 RID: 5625
		[DisplayName("UAP", "RLDL")]
		NumberOfRecipientsInLargestDL,
		// Token: 0x040015FA RID: 5626
		[DisplayName("UAP", "ADP")]
		NumberOfAttachmentDataProviders,
		// Token: 0x040015FB RID: 5627
		[DisplayName("UAP", "UTSP")]
		NumberOfUsersToSetPermissions,
		// Token: 0x040015FC RID: 5628
		[DisplayName("UAP", "GMTT")]
		GetMailTipsTime,
		// Token: 0x040015FD RID: 5629
		[DisplayName("UAP", "DLET")]
		DLExpandTime
	}
}
