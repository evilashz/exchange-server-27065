using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000099 RID: 153
	public enum HeaderPromotionMode
	{
		// Token: 0x0400022A RID: 554
		[LocDescription(DataStrings.IDs.HeaderPromotionModeNoCreate)]
		NoCreate,
		// Token: 0x0400022B RID: 555
		[LocDescription(DataStrings.IDs.HeaderPromotionModeMayCreate)]
		MayCreate,
		// Token: 0x0400022C RID: 556
		[LocDescription(DataStrings.IDs.HeaderPromotionModeMustCreate)]
		MustCreate
	}
}
