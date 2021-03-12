using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200030C RID: 780
	internal enum AmDbActionCategory
	{
		// Token: 0x04001485 RID: 5253
		None = 10,
		// Token: 0x04001486 RID: 5254
		Mount,
		// Token: 0x04001487 RID: 5255
		Dismount,
		// Token: 0x04001488 RID: 5256
		Move,
		// Token: 0x04001489 RID: 5257
		Remount,
		// Token: 0x0400148A RID: 5258
		ForceDismount,
		// Token: 0x0400148B RID: 5259
		SyncState,
		// Token: 0x0400148C RID: 5260
		SyncAd
	}
}
