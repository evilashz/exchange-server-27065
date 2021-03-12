using System;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000FF RID: 255
	[Flags]
	public enum SpamScanFlags
	{
		// Token: 0x04000533 RID: 1331
		IsOutbound = 1,
		// Token: 0x04000534 RID: 1332
		AllowUserOptOut = 2,
		// Token: 0x04000535 RID: 1333
		CsfmTestXHeader = 4,
		// Token: 0x04000536 RID: 1334
		CsfmTestSubjectMod = 8
	}
}
