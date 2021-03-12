using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A02 RID: 2562
	internal enum ResourceLoadState : uint
	{
		// Token: 0x04004BE9 RID: 19433
		Unknown,
		// Token: 0x04004BEA RID: 19434
		Underloaded,
		// Token: 0x04004BEB RID: 19435
		Full,
		// Token: 0x04004BEC RID: 19436
		Overloaded,
		// Token: 0x04004BED RID: 19437
		Critical
	}
}
