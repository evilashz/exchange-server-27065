using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BD RID: 189
	internal enum MigrationRowSelectorResult
	{
		// Token: 0x040003FE RID: 1022
		AcceptRow = 1,
		// Token: 0x040003FF RID: 1023
		RejectRowContinueProcessing,
		// Token: 0x04000400 RID: 1024
		RejectRowStopProcessing
	}
}
