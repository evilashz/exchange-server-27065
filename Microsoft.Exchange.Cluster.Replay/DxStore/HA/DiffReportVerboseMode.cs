using System;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x02000165 RID: 357
	[Flags]
	public enum DiffReportVerboseMode
	{
		// Token: 0x040005E0 RID: 1504
		Disabled = 1,
		// Token: 0x040005E1 RID: 1505
		ShowKeyNames = 2,
		// Token: 0x040005E2 RID: 1506
		ShowPropertyNames = 4,
		// Token: 0x040005E3 RID: 1507
		ShowPropertyValues = 8,
		// Token: 0x040005E4 RID: 1508
		ShowMatchingKeys = 16,
		// Token: 0x040005E5 RID: 1509
		ShowMatchingProperties = 32,
		// Token: 0x040005E6 RID: 1510
		All = 60
	}
}
