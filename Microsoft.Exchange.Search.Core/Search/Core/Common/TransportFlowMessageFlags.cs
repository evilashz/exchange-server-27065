using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200008E RID: 142
	[Flags]
	public enum TransportFlowMessageFlags
	{
		// Token: 0x040001A3 RID: 419
		None = 0,
		// Token: 0x040001A4 RID: 420
		ShouldBypassNlg = 1,
		// Token: 0x040001A5 RID: 421
		SkipTokenInfoGeneration = 2,
		// Token: 0x040001A6 RID: 422
		SkipMdmGeneration = 4,
		// Token: 0x040001A7 RID: 423
		ShouldDiscardToken = 6
	}
}
