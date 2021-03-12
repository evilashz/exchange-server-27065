using System;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x020000A0 RID: 160
	internal enum OperatorLocation
	{
		// Token: 0x04000226 RID: 550
		None,
		// Token: 0x04000227 RID: 551
		DiagnosticsStarted,
		// Token: 0x04000228 RID: 552
		EndOfFlow,
		// Token: 0x04000229 RID: 553
		BeginProcessRecord = 10,
		// Token: 0x0400022A RID: 554
		EndProcessRecord,
		// Token: 0x0400022B RID: 555
		EndProcessRecordException,
		// Token: 0x0400022C RID: 556
		BeginWrite = 20,
		// Token: 0x0400022D RID: 557
		EndWrite,
		// Token: 0x0400022E RID: 558
		EndWriteException
	}
}
