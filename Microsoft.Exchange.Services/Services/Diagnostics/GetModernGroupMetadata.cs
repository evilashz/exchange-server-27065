using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal enum GetModernGroupMetadata
	{
		// Token: 0x040002F3 RID: 755
		[DisplayName("GMG", "MC")]
		MemberCount,
		// Token: 0x040002F4 RID: 756
		[DisplayName("GMG", "MADTime")]
		MemberADLatency,
		// Token: 0x040002F5 RID: 757
		[DisplayName("GMG", "MMBTime")]
		MemberMBLatency,
		// Token: 0x040002F6 RID: 758
		[DisplayName("GMG", "MMBCount")]
		MemberMBCount,
		// Token: 0x040002F7 RID: 759
		[DisplayName("GMG", "SO")]
		SortOrder,
		// Token: 0x040002F8 RID: 760
		[DisplayName("GMG", "GI")]
		GeneralInfo,
		// Token: 0x040002F9 RID: 761
		[DisplayName("GMG", "OL")]
		OwnerList
	}
}
