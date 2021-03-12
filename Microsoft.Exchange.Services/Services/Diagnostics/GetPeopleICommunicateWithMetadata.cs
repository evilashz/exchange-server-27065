using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000044 RID: 68
	internal enum GetPeopleICommunicateWithMetadata
	{
		// Token: 0x04000305 RID: 773
		[DisplayName("GPICW", "TgEm")]
		TargetEmailAddress,
		// Token: 0x04000306 RID: 774
		[DisplayName("GPICW", "GPICWFail")]
		GetPeopleICommunicateWithFailed,
		// Token: 0x04000307 RID: 775
		[DisplayName("GPICW", "RCT")]
		ResponseContentType
	}
}
