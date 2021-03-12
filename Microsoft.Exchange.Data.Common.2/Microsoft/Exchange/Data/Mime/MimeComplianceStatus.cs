using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000044 RID: 68
	[Flags]
	public enum MimeComplianceStatus
	{
		// Token: 0x04000224 RID: 548
		Compliant = 0,
		// Token: 0x04000225 RID: 549
		MissingBoundary = 1,
		// Token: 0x04000226 RID: 550
		InvalidBoundary = 2,
		// Token: 0x04000227 RID: 551
		InvalidWrapping = 4,
		// Token: 0x04000228 RID: 552
		BareLinefeedInBody = 8,
		// Token: 0x04000229 RID: 553
		InvalidHeader = 16,
		// Token: 0x0400022A RID: 554
		MissingBodySeparator = 32,
		// Token: 0x0400022B RID: 555
		MissingBoundaryParameter = 64,
		// Token: 0x0400022C RID: 556
		InvalidTransferEncoding = 128,
		// Token: 0x0400022D RID: 557
		InvalidExternalBody = 256,
		// Token: 0x0400022E RID: 558
		BareLinefeedInHeader = 512,
		// Token: 0x0400022F RID: 559
		UnexpectedBinaryContent = 1024
	}
}
