using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000201 RID: 513
	public enum RuleScope : byte
	{
		// Token: 0x04000ACE RID: 2766
		None,
		// Token: 0x04000ACF RID: 2767
		ContentFilter,
		// Token: 0x04000AD0 RID: 2768
		ProtocolFilterFrontdoor,
		// Token: 0x04000AD1 RID: 2769
		ProtocolFilterHub,
		// Token: 0x04000AD2 RID: 2770
		MailboxDeliveryFilter
	}
}
