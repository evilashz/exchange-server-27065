using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A5 RID: 677
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct TABLE_NOTIFICATION
	{
		// Token: 0x0400116B RID: 4459
		internal int ulTableEvent;

		// Token: 0x0400116C RID: 4460
		internal int hResult;

		// Token: 0x0400116D RID: 4461
		internal SPropValue propIndex;

		// Token: 0x0400116E RID: 4462
		internal SPropValue propPrior;

		// Token: 0x0400116F RID: 4463
		internal SRow row;

		// Token: 0x04001170 RID: 4464
		internal int ulPad;
	}
}
