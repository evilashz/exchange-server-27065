using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002A7 RID: 679
	public enum AttachmentAddResultCode
	{
		// Token: 0x040012DC RID: 4828
		NoError,
		// Token: 0x040012DD RID: 4829
		IrresolvableConflictWhenSaving,
		// Token: 0x040012DE RID: 4830
		AttachmentExcceedsSizeLimit,
		// Token: 0x040012DF RID: 4831
		ItemExcceedsSizeLimit,
		// Token: 0x040012E0 RID: 4832
		InsertingNonImageAttachment,
		// Token: 0x040012E1 RID: 4833
		GeneralErrorWhenSaving
	}
}
