using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200030B RID: 779
	internal enum LegacyAmDbActionCode
	{
		// Token: 0x0400147B RID: 5243
		Unknown = 1,
		// Token: 0x0400147C RID: 5244
		AdminMount,
		// Token: 0x0400147D RID: 5245
		AdminDismount,
		// Token: 0x0400147E RID: 5246
		AdminMove,
		// Token: 0x0400147F RID: 5247
		StartupAutoMount,
		// Token: 0x04001480 RID: 5248
		StoreRestartMount,
		// Token: 0x04001481 RID: 5249
		AutomaticFailover,
		// Token: 0x04001482 RID: 5250
		SwitchOver,
		// Token: 0x04001483 RID: 5251
		Remount
	}
}
