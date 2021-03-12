using System;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000372 RID: 882
	internal enum DiscardReason
	{
		// Token: 0x0400139E RID: 5022
		ExplicitlyDiscarded,
		// Token: 0x0400139F RID: 5023
		DeletedByAdmin,
		// Token: 0x040013A0 RID: 5024
		Resubmitted,
		// Token: 0x040013A1 RID: 5025
		DiscardAll,
		// Token: 0x040013A2 RID: 5026
		Expired
	}
}
