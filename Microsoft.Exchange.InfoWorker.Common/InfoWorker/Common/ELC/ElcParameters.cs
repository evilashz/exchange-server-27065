using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001A5 RID: 421
	[Flags]
	internal enum ElcParameters
	{
		// Token: 0x04000844 RID: 2116
		None = 0,
		// Token: 0x04000845 RID: 2117
		HoldCleanup = 1,
		// Token: 0x04000846 RID: 2118
		EHAHiddenFolderCleanup = 2
	}
}
