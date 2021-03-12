using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000037 RID: 55
	internal enum GetFolderMetadata
	{
		// Token: 0x0400026D RID: 621
		[DisplayName("GF", "FT")]
		FolderType,
		// Token: 0x0400026E RID: 622
		[DisplayName("GF", "MBXT")]
		MailboxTarget,
		// Token: 0x0400026F RID: 623
		[DisplayName("GF", "PRIP")]
		Principal
	}
}
