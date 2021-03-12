using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000257 RID: 599
	[Flags]
	public enum RecurrenceExpansionOption
	{
		// Token: 0x040011F1 RID: 4593
		None = 0,
		// Token: 0x040011F2 RID: 4594
		IncludeMaster = 1,
		// Token: 0x040011F3 RID: 4595
		IncludeRegularOccurrences = 2,
		// Token: 0x040011F4 RID: 4596
		IncludeAll = 3,
		// Token: 0x040011F5 RID: 4597
		TruncateMaster = 4
	}
}
