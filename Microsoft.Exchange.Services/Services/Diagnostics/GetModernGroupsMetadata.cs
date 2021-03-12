using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000042 RID: 66
	internal enum GetModernGroupsMetadata
	{
		// Token: 0x040002FB RID: 763
		[DisplayName("GMGS", "AADT")]
		AADLatency,
		// Token: 0x040002FC RID: 764
		[DisplayName("GMGS", "AADGC")]
		AADOnlyGroupCount
	}
}
