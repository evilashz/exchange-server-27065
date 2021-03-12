using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000046 RID: 70
	internal enum GetUserUnifiedGroupsMetadata
	{
		// Token: 0x0400030C RID: 780
		[DisplayName("GUUGS", "AADT")]
		AADLatency,
		// Token: 0x0400030D RID: 781
		[DisplayName("GUUGS", "AADGC")]
		AADOnlyGroupCount
	}
}
