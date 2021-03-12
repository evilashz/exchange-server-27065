using System;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000092 RID: 146
	[Flags]
	internal enum DiagnosticsLoggingTag
	{
		// Token: 0x040001BC RID: 444
		None = 0,
		// Token: 0x040001BD RID: 445
		Informational = 1,
		// Token: 0x040001BE RID: 446
		Warnings = 2,
		// Token: 0x040001BF RID: 447
		Failures = 4
	}
}
