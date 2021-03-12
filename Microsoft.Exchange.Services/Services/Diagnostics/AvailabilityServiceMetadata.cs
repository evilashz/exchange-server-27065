using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002B RID: 43
	public enum AvailabilityServiceMetadata
	{
		// Token: 0x040001D5 RID: 469
		[DisplayName("AS", "ExtC")]
		ExtC,
		// Token: 0x040001D6 RID: 470
		[DisplayName("AS", "IntC")]
		IntC,
		// Token: 0x040001D7 RID: 471
		[DisplayName("AS", "PASQ1")]
		PASQ1,
		// Token: 0x040001D8 RID: 472
		[DisplayName("AS", "PASQ2")]
		PASQ2,
		// Token: 0x040001D9 RID: 473
		[DisplayName("AS", "PASQT")]
		PASQT,
		// Token: 0x040001DA RID: 474
		[DisplayName("AS", "TimeInAS")]
		TimeInAs,
		// Token: 0x040001DB RID: 475
		[DisplayName("AS", "PEL")]
		PreexecutionLatency,
		// Token: 0x040001DC RID: 476
		[DisplayName("AS", "ReqStats")]
		RequestStatistics
	}
}
